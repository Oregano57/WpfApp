using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ToDoApp app;
    
    public MainWindow()
    {
        InitializeComponent();
        app = new ToDoApp();
        app.SetTasks(LoadTasks());
        RefreshTaskList();
    }

    public class ToDoApp
    {
        private List<string> _todoList = new List<string>();

        public void AddTask(string task)
        {
            _todoList.Add(task);
        }

        public List<string> Get()
        {
            return _todoList;
        }

        public void SetTasks(List<string> tasks)
        {
            _todoList = tasks;
        }
    }

    public void RefreshTaskList()
    {
        TaskListBox.Items.Clear();
        foreach (string task in app.Get())
            TaskListBox.Items.Add(task);
        
    }

    public List<string> LoadTasks()
    {
        List<string> tasks = new List<string>();

        if (File.Exists("tasks.txt"))
            tasks = new List<string>(File.ReadAllLines("tasks.txt"));
        return tasks;
    }

    public void SaveTasks()
    {
        File.WriteAllLines("tasks.txt", app.Get());
    }
    
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        string task = TaskInput.Text;
        app.AddTask(task);
        RefreshTaskList();
        TaskInput.Text = "";
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        SaveTasks();
        SaveSuccessfulBox.Text = "Save Successful";
        await Task.Delay(5000);
        SaveSuccessfulBox.Text = "";
    }
}