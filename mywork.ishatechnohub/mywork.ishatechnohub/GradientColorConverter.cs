namespace mywork.ishatechnohub;

public class GradientColorConverter : IValueConverter
{
    private static Color MediumRed => Color.FromArgb("#DC3824");

    private static Color MediumYellow => Color.FromArgb("#FF8C00");

    private static Color MediumGreen => Color.FromArgb("#00994E");

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not double completedPercentage) return Colors.Transparent;
        var gradientStops = new GradientStopCollection();

        switch (completedPercentage)
        {
            case <= 25:
                // Medium Red Gradient
                gradientStops.Add(new GradientStop(MediumRed, 0.0f));
                gradientStops.Add(new GradientStop(Colors.OrangeRed, 1.0f));
                break;
            case <= 75:
                // Medium Yellow Gradient
                gradientStops.Add(new GradientStop(MediumYellow, 0.0f));
                gradientStops.Add(new GradientStop(Colors.Goldenrod, 1.0f));
                break;
            default:
                // Medium Green Gradient
                gradientStops.Add(new GradientStop(MediumGreen, 0.0f));
                gradientStops.Add(new GradientStop(Colors.DarkSeaGreen, 1.0f));
                break;
        }

        return new LinearGradientBrush(gradientStops, new Point(0, 0), new Point(1, 0));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}