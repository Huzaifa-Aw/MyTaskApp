using CommunityToolkit.Maui.Core.Extensions;

namespace mywork.ishatechnohub.ViewModels;

public partial class TasksPageViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ProjectModel> _projectTaskList;
    [ObservableProperty] private string _newTaskTitle;
    [ObservableProperty] private string _selectedProjectTitle;
    [ObservableProperty] private string _newUserName;
    [ObservableProperty] private DateTime _newTaskDate;
    [ObservableProperty] private ObservableCollection<string> _projectsList;
    [ObservableProperty] private ObservableCollection<string> _usersList;

    public TasksPageViewModel()
    {
        ProjectTaskList = new ObservableCollection<ProjectModel>()
        {
            new ProjectModel()
            {
                Title = "Project 1",
                CompletedPercentage = 0,
                Tasks = new ObservableCollection<TaskModel>()
                {
                    new TaskModel()
                    {
                        Title = "Task 1.1",
                        CompletedPercentage = 50,
                        Username = "user1",
                        LinkedProjectName = "Project 1"
                    },
                    new TaskModel()
                    {
                        Title = "Task 1.2",
                        CompletedPercentage = 100,
                        Username = "user2",
                        LinkedProjectName = "Project 1"
                    }
                },
                Date = DateTime.Now
            },
            new ProjectModel()
            {
                Title = "Project 2",
                CompletedPercentage = 0,
                Tasks = new ObservableCollection<TaskModel>()
                {
                    new TaskModel()
                    {
                        Title = "Task 2.1",
                        CompletedPercentage = 25,
                        Username = "user3",
                        LinkedProjectName = "Project 2"
                    },
                    new TaskModel()
                    {
                        Title = "Task 2.2",
                        CompletedPercentage = 75,
                        Username = "user4",
                        LinkedProjectName = "Project 2"
                    }
                },
                Date = DateTime.Now
            },
            new ProjectModel()
            {
                Title = "Project 3",
                CompletedPercentage = 0,
                Tasks = new ObservableCollection<TaskModel>()
                {
                    new TaskModel()
                    {
                        Title = "Task 3.1",
                        CompletedPercentage = 0,
                        Username = "user5",
                        LinkedProjectName = "Project 3"
                    },
                    new TaskModel()
                    {
                        Title = "Task 3.2",
                        CompletedPercentage = 90,
                        Username = "user6",
                        LinkedProjectName = "Project 3"
                    }
                },
                Date = DateTime.Now
            }
        };
        ProjectsList = ProjectTaskList.Select(x => x.Title).ToObservableCollection();
        SelectedProjectTitle = ProjectsList.FirstOrDefault()!;
        UsersList = ProjectTaskList
            .SelectMany(project => project.Tasks.Select(task => task.Username))
            .Distinct().ToObservableCollection();
    }
    [RelayCommand]
    private void ChangeDoneStatus(TaskModel task)
    {
        if (task.IsDone) return;
        var project = ProjectTaskList.First(x =>
            x.Title.Equals(task.LinkedProjectName, StringComparison.InvariantCultureIgnoreCase));
        var tobeRemoved=project.Tasks.First(t=>t.Title.Equals(task.Title));
        var remainingTasks=project.Tasks;
        remainingTasks.Remove(tobeRemoved);
        project.TaskCount = remainingTasks.Count;
        //project.Tasks= new ObservableCollection<TaskModel>(remainingTasks);
    }

[RelayCommand]
    private void DateSelected(DateTime date)
    {
        NewTaskDate = date;
    }
    [RelayCommand]
    private void TaskProgressUpdated(TaskModel date)
    {
        var projects = ProjectTaskList;
        var project = projects.First(x => x.Title.Equals(date.LinkedProjectName));
         project.CompletedPercentage= project.Tasks.Average(t => t.CompletedPercentage);
         // ProjectTaskList = new ObservableCollection<ProjectModel>(projects);

    }
   
    [RelayCommand]
    private void AddTask()
    {
        var project = ProjectTaskList.FirstOrDefault(x =>
            x.Title.Equals(SelectedProjectTitle, StringComparison.InvariantCultureIgnoreCase));

        if (project != null)
        {
            project.Date = NewTaskDate;
            var tasksList = project.Tasks.ToList();
            tasksList.Add(new TaskModel
            {
                Title = NewTaskTitle,
                IsDone = false,
                LinkedProjectName=SelectedProjectTitle,
                Username = NewUserName,
                CompletedPercentage = 0
            });

            project.Tasks = tasksList.ToObservableCollection(); 
        }

        NewUserName = string.Empty;
        NewTaskTitle = string.Empty;
        NewTaskDate = DateTime.Today;
        SelectedProjectTitle = string.Empty;
    }
    private TaskModel _itemBeingDragged;
    [RelayCommand]
    private void ItemDragged(TaskModel user)
    {
        user.IsBeingDragged = true;
        _itemBeingDragged = user;
    }
    [RelayCommand]
    private void ItemDragLeave(TaskModel user)
    {
        user.IsBeingDraggedOver = false;
    }
    [RelayCommand]
    private void ItemDraggedOver(TaskModel user)
    {
        if (user == _itemBeingDragged)
        {
            user.IsBeingDragged = false;
        }
        user.IsBeingDraggedOver = user != _itemBeingDragged;
    }
    [RelayCommand]
    private void ItemDropped(TaskModel user)
    {
        try
        {
            var itemToMove = _itemBeingDragged;
            var itemToInsertBefore = user;
            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;
            var project = ProjectTaskList.First(x => x.Title.Equals(itemToInsertBefore.LinkedProjectName));
            int insertAtIndex = project.Tasks.IndexOf(itemToInsertBefore);
            if (insertAtIndex >= 0 && insertAtIndex < ProjectTaskList.Count)
            {
                project.Tasks.Remove(itemToMove);
                project.Tasks.Insert(insertAtIndex, itemToMove);
                itemToMove.IsBeingDragged = false;
                itemToInsertBefore.IsBeingDraggedOver = false;
            }
        }
        catch (Exception ex)
        {
            // ignored
        }
    }
}