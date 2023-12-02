namespace mywork.ishatechnohub.Models;

public partial class TaskModel: ObservableObject
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private double _completedPercentage;
    [ObservableProperty] private string _username;
    [ObservableProperty] private bool _isDone;

}