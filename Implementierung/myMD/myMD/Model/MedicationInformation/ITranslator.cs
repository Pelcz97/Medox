using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myMD.Model.MedicationInformation
{
    public interface ITranslator
    {
        Task<IList<string>> TranslateText(IList<string> input);
    }
}
