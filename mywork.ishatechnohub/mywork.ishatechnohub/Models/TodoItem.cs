namespace mywork.ishatechnohub.Models;

public partial class TodoItem : ObservableObject
{
    [ObservableProperty] private bool _isDone;
    [ObservableProperty] private string _title;
    public string DateString { get; private set; }

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

}