using CommunityToolkit.Maui;

namespace mywork.ishatechnohub;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();
        
        // Registering Route, Views and ViewModels
        
        builder.Services.AddSingletonWithShellRoute<TodosPage, TodosPageViewModel>(nameof(TodosPage));
        builder.Services.AddSingletonWithShellRoute<TasksPage, TasksPageViewModel>(nameof(TasksPage));
        builder.Services.AddSingletonWithShellRoute<ProjectsPage, ProjectsPageViewModel>(nameof(ProjectsPage));


#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}