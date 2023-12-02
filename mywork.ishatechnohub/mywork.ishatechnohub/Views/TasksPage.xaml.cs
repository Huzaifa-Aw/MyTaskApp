namespace mywork.ishatechnohub.Views;

public partial class TasksPage
{
    private readonly TasksPageViewModel _viewModel;

    public TasksPage(TasksPageViewModel viewModel)
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

    private void UserSelection_OnClicked(object sender, EventArgs e)
    {
        UserPicker.Unfocus();
        UserPicker.Focus();
        // TODO: Update when MAUI bug is fixed: https://github.com/dotnet/maui/issues/8946)
    }

    private void ProjectPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is not Picker picker)
        {
            return;
        }

        if (picker.SelectedItem is null)
        {
            return;
        }

        _viewModel.SelectedProjectTitle = picker.SelectedItem.ToString() ?? string.Empty;
    }

    private void UserPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is not Picker picker)
        {
            return;
        }

        if (picker.SelectedItem is null)
        {
            return;
        }

        _viewModel.NewUserName = picker.SelectedItem.ToString() ?? string.Empty;
    }

    private void Slider_OnValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (sender is not Slider progressSlider)
        {
            return;
        }

        if (e.NewValue % 25 is not 0)
        {
            var snappedValue = Math.Round(e.NewValue / 25) * 25;
            progressSlider.Value = snappedValue;
        }
        else
        {
            if (_viewModel.TaskProgressUpdatedCommand is not null &&
                _viewModel.TaskProgressUpdatedCommand.CanExecute((TaskModel)progressSlider.BindingContext))
            {
                _viewModel.TaskProgressUpdatedCommand.Execute((TaskModel)progressSlider.BindingContext);
            }
        }
    }
}