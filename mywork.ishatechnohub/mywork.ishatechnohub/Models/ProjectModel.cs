namespace mywork.ishatechnohub.Models;

public partial class ProjectModel : ObservableObject
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private double _completedPercentage;
    [ObservableProperty] private IEnumerable<TaskModel> _tasks;
    [ObservableProperty] private int _taskCount;
    [ObservableProperty] private string _dateString;

    public DateTime Date
    {
        get => _tempDate;
        set
        {
            _tempDate = value;
            DateString = value.ToShortDateString();
        }
    }

    private DateTime _tempDate;
    partial void OnTasksChanged(IEnumerable<TaskModel> oldValue, IEnumerable<TaskModel> newValue)
    {
        TaskCount = newValue.Count();
    }
}