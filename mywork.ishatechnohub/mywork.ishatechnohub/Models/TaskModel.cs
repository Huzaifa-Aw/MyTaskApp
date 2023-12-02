namespace mywork.ishatechnohub.Models;

public partial class TaskModel: ObservableObject
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private int _completedPercentage;
    [ObservableProperty] private string _username;
    [ObservableProperty] private string _linkedProjectName;
    [ObservableProperty] private bool _isDone;

}