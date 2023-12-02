namespace mywork.ishatechnohub.Models;

public partial class ProjectModel : ObservableObject
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private double _completedPercentage;
    [ObservableProperty] private ObservableCollection<TaskModel> _tasks;
    [ObservableProperty] private int _taskCount;
    [ObservableProperty] private string _dateString;
    [ObservableProperty] private bool _isBeingDragged;
    [ObservableProperty] private bool _isBeingDraggedOver;
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
    partial void OnTasksChanged(ObservableCollection<TaskModel> oldValue, ObservableCollection<TaskModel> newValue)
    {
        TaskCount = newValue.Count();
    }
}