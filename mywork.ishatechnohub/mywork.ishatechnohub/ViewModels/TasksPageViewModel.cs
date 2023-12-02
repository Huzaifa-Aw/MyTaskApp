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
                CompletedPercentage = 20,
                Tasks = new List<TaskModel>()
                {
                    new TaskModel()
                    {
                        Title = "Task 1.1",
                        CompletedPercentage = 0.5,
                        Username = "user1"
                    },
                    new TaskModel()
                    {
                        Title = "Task 1.2",
                        CompletedPercentage = 1,
                        Username = "user2"
                    }
                },
                Date = DateTime.Now
            },
            new ProjectModel()
            {
                Title = "Project 2",
                CompletedPercentage = 55,
                Tasks = new List<TaskModel>()
                {
                    new TaskModel()
                    {
                        Title = "Task 2.1",
                        CompletedPercentage = .25,
                        Username = "user3"
                    },
                    new TaskModel()
                    {
                        Title = "Task 2.2",
                        CompletedPercentage = .75,
                        Username = "user4"
                    }
                },
                Date = DateTime.Now
            },
            new ProjectModel()
            {
                Title = "Project 3",
                CompletedPercentage = 96,
                Tasks = new List<TaskModel>()
                {
                    new TaskModel()
                    {
                        Title = "Task 3.1",
                        CompletedPercentage = 0,
                        Username = "user5"
                    },
                    new TaskModel()
                    {
                        Title = "Task 3.2",
                        CompletedPercentage = .9,
                        Username = "user6"
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
    private void DateSelected(DateTime date)
    {
        NewTaskDate = date;
    }

    [RelayCommand]
    private void AddTask()
    {
        var projects = new List<ProjectModel>(ProjectTaskList);
            var project = projects.Find(x =>
            x.Title.Equals(SelectedProjectTitle, StringComparison.InvariantCultureIgnoreCase));
        project.Date = NewTaskDate;
        var tasks = project.Tasks.ToList();
           tasks.Add(
                new TaskModel
                {
                    Title = NewTaskTitle,
                    IsDone = false,
                    Username = NewUserName,
                    CompletedPercentage = 0,
                });
           projects.First(x => x.Title.Equals(SelectedProjectTitle, StringComparison.InvariantCultureIgnoreCase))
               .Tasks = new ObservableCollection<TaskModel>(tasks);

        ProjectTaskList = new ObservableCollection<ProjectModel>(projects);
        NewUserName = string.Empty;
        NewTaskTitle = string.Empty;
        NewTaskDate = DateTime.Today;
        SelectedProjectTitle = string.Empty;
    }
    [RelayCommand]
    private void AddToDo()
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
                Username = NewUserName,
                CompletedPercentage = 0
            });

            project.Tasks = tasksList; 
        }

        NewUserName = string.Empty;
        NewTaskTitle = string.Empty;
        NewTaskDate = DateTime.Today;
        SelectedProjectTitle = string.Empty;
    }
}