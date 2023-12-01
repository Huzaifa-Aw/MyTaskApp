using System.Collections.ObjectModel;

namespace mywork.ishatechnohub.ViewModels;

public partial class TodosPageViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<TodoItem> _toDoItemsList;

    public TodosPageViewModel()
    {
        ToDoItemsList = new ObservableCollection<TodoItem>
        {
            new ()
            {
                Title = "Buy milk",
                IsDone = false,
                Date = DateTime.Today.AddDays(-1)
            },
            new ()
            {
                Title = "Finish homework",
                IsDone = false,
                Date = DateTime.Today.AddDays(-6)
            },
            new ()
            {
                Title = "Go to the gym",
                IsDone = false,
                Date = DateTime.Today.AddDays(9)
            },
            new ()
            {
                Title = "Clean the house",
                IsDone = false,
                Date = DateTime.Today.AddDays(3)
            },
            new ()
            {
                Title = "Call Mom",
                IsDone = false,
                Date = DateTime.Today.AddDays(-2)
            }
        };
    }
}