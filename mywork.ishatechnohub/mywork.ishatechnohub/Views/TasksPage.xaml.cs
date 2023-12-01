namespace mywork.ishatechnohub.Views;

public partial class TasksPage
{

    public TasksPage(TasksPageViewModel viewModel) 
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}