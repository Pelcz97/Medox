using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myMD.Model.MedicationInformation
{
    public interface IMedicalDictionary
    {
        Task<IList<DictionaryEntry>> GetDefinitions(string term);
    }
}
