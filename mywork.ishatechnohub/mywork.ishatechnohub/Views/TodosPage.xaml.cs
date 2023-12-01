namespace mywork.ishatechnohub.Views;

public partial class TodosPage
{
    public TodosPage(TodosPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}