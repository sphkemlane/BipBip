using BipBip.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BipBip.Views
{
    public class SenderToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int userIdSender = (int)value;
            // Replace with actual check for current user ID
            bool isCurrentUser = userIdSender == UserSession.Id;
            return isCurrentUser ? Color.LightGreen : Color.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
