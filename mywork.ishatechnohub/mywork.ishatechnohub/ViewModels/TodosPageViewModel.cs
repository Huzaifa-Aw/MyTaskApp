using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace mywork.ishatechnohub.ViewModels;

public partial class TodosPageViewModel : ObservableObject
{
    private TodoItem _itemBeingDragged;
    [RelayCommand]
    private void ItemDragged(TodoItem user)
    {
        user.IsBeingDragged = true;
        _itemBeingDragged = user;
    }
    [RelayCommand]
    private void ItemDragLeave(TodoItem user)
    {
        user.IsBeingDraggedOver = false;
    }
    [RelayCommand]
    private void ItemDraggedOver(TodoItem user)
    {
        if (user == _itemBeingDragged)
        {
            user.IsBeingDragged = false;
        }
        user.IsBeingDraggedOver = user != _itemBeingDragged;
    }
    [RelayCommand]
    private void ItemDropped(TodoItem user)
    {
        try
        {
            var itemToMove = _itemBeingDragged;
            var itemToInsertBefore = user;
            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;
            int insertAtIndex = TodoItemsList.IndexOf(itemToInsertBefore);
            if (insertAtIndex >= 0 && insertAtIndex < TodoItemsList.Count)
            {
                TodoItemsList.Remove(itemToMove);
                TodoItemsList.Insert(insertAtIndex, itemToMove);
                itemToMove.IsBeingDragged = false;
                itemToInsertBefore.IsBeingDraggedOver = false;
            }
        }
        catch (Exception ex)
        {
            // ignored
        }
    }
    
    
    
    
    [ObservableProperty] private ObservableCollection<TodoItem> _todoItemsList;
    [ObservableProperty] private string _newToDoTitle;
    [ObservableProperty] private DateTime _newToDoDate;

    public TodosPageViewModel()
    {
        NewToDoDate = DateTime.Today;
        TodoItemsList = new ObservableCollection<TodoItem>
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

    
    [RelayCommand]
    private void DateSelected(DateTime date)
    {
        NewToDoDate = date;
    }
    
    [RelayCommand]
    private void AddToDo()
    {
        TodoItemsList.Add(new TodoItem
        {
            Title = NewToDoTitle,
            IsDone = false,
            Date = NewToDoDate
        });
        NewToDoTitle = string.Empty;
        NewToDoDate = DateTime.Today;
        
    }
    [RelayCommand]
    private void ChangeDoneStatus(TodoItem todo)
    {
        todo.IsDone = !todo.IsDone;
    }
}