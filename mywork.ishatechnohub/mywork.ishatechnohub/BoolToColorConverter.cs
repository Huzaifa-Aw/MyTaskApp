namespace mywork.ishatechnohub;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isBeingDragged = (bool?)value;
        var result = (isBeingDragged ?? false) ? Color.FromArgb("#bcacdc") : Color.FromArgb("#00FFFFFF");
        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}