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
using System.Collections.ObjectModel;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ToDoApp app;
    private CancellationTokenSource? _cts;
    public ObservableCollection<string> Tasks => app.Get();
    
    public MainWindow()
    {
        InitializeComponent();
        app = new ToDoApp();
        app.SetTasks(LoadTasks());
        DataContext = this;
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
        if (task == "")
            app.AddTask("Empty Task");
        else
            app.AddTask(task);
        TaskInput.Text = "";
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        SaveTasks();
        SaveSuccessfulBox.Text = "Save Successful";
        await Task.Delay(5000);
        SaveSuccessfulBox.Text = "";
    }

    private async void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            if (TaskListBox.SelectedIndex != -1)
            {
                app.RemoveTask(TaskListBox.SelectedIndex);
                RemoveSuccessfulBox.Text = "Task Removed";
                await Task.Delay(5000, token);
                RemoveSuccessfulBox.Text = "";
                return;
            }
            RemoveSuccessfulBox.Text = "Please Select A Task";
            await Task.Delay(3000, token);
            RemoveSuccessfulBox.Text = "";
        }
        catch (TaskCanceledException)
        {
            return;
        }

    }
}