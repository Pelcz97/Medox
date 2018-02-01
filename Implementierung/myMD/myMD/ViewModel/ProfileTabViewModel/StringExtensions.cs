using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    /// <summary>
    /// Erweiterungsmehtoden der String-Klasse zum formatieren von strings zum Anzeigen
    /// </summary>
    [Preserve(AllMembers = true)]
    public static class StringExtensions
    {
        /// <summary>
        /// Ersetzt die Wörter "Minus" und "Plus" durch das entsprechende Zeichen mit vorangestelltem Leerzeichen 
        /// </summary>
        /// <param name="str">Der zu formatierende string</param>
        /// <returns>Das formatierte Ergebnis</returns>
        public static string FormatPlusAndMinus(this string str)
        {
            return Regex.Replace(Regex.Replace(str, "Plus", " +"), "Minus", " -");
        }
    }
}
