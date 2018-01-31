using System;
using System.Globalization;
using Xamarin.Forms;

namespace myMD.ViewModel.ProfileTabViewModel
{
    /// <summary>
    /// Konverter zwischen Enum und Integer, zur Verwendung in XAML-Seiten.
    /// </summary>
    public class IntEnumConverter : IValueConverter
    {
        /// <summary>
        /// Konvertiert das gegebene Objekt zu einem integer.
        /// </summary>
        /// <param name="value">Das zu konvertierende Objekt</param>
        /// <param name="targetType">hier nicht benutzt</param>
        /// <param name="parameter">hier nicht benutzt</param>
        /// <param name="culture">hier nicht benutzt</param>
        /// <returns>Der zu value gehörende Integer Wert</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
            {
                return (int)value;
            }
            return 0;
        }

        /// <summary>
        /// Konvertiert das gegebene Objekt zum gegebenen Enum-Typ.
        /// </summary>
        /// <param name="value">Der Integer der zu einem Enum-Wert konvertiert werden soll</param>
        /// <param name="targetType">Der Enum Typ zu dem value konvertiert werden soll</param>
        /// <param name="parameter">hier nicht benutzt</param>
        /// <param name="culture">hier nicht benutzt</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return Enum.ToObject(targetType, value);
            }
            return 0;
        }
    }
}
