using System.Collections.ObjectModel;

namespace WpfApp1;

public class ToDoApp
{
    private ObservableCollection<string> _todoList = new ObservableCollection<string>();

    public void AddTask(string task)
    {
        _todoList.Add(task);
    }

    public ObservableCollection<string> Get()
    {
        return _todoList;
    }

    public void SetTasks(List<string> tasks)
    {
        _todoList = new ObservableCollection<string>(tasks);
    }

    public void RemoveTask(int index)
    {
        _todoList.RemoveAt(index);
    }
}
