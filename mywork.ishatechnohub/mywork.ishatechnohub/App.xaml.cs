namespace mywork.ishatechnohub;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        Current!.UserAppTheme = AppTheme.Light;
    }
}