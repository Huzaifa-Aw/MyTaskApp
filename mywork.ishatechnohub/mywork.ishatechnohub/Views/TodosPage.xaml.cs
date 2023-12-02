using System.Globalization;

namespace mywork.ishatechnohub.Views;

public partial class TodosPage
{
    private TodosPageViewModel _viewModel;

    public TodosPage(TodosPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
    {
 #if IOS
        var myDatePicker = new DatePicker { IsVisible = false };
        myDatePicker.DateSelected += MyDatePicker_OnDateSelected;
        Root.Add(myDatePicker);
        myDatePicker.Focus();
 #endif
    }

    private Timer _timer;

    private void MyDatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
    {
        if (_viewModel.DateSelectedCommand is not null && _viewModel.DateSelectedCommand.CanExecute(e.NewDate))
        {
            _viewModel.DateSelectedCommand.Execute(e.NewDate);
        }

 #if IOS


        if (_timer == null)
        {
            _timer = new Timer(OnTimerElapsed, sender, 3000, Timeout.Infinite);
        }
        else
        {
            _timer.Change(3000, Timeout.Infinite);
        }
#endif
    }

    private void OnTimerElapsed(object state)
    {
        MainThread.BeginInvokeOnMainThread(() => { Root.Remove((DatePicker)state); });
        _timer = null;
    }
}
