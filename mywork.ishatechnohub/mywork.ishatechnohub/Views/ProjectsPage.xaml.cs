using CommunityToolkit.Maui.Core;

namespace mywork.ishatechnohub.Views;

public partial class ProjectsPage
{
    public ProjectsPage(ProjectsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
    }
}