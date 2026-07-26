namespace WpfApp1;

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

    public void RemoveTask(int index)
    {
        _todoList.RemoveAt(index);
    }
}
