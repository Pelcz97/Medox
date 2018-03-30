using System;
using System.Collections.Generic;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.MedicationInformation
{
    public interface IInteractionChecker
    {
        IList<string> GetInteractions(IList<IMedication> medications);

        IList<string> GetRxNormIDs(IList<IMedication> medications);

        void GetRxNormID(IMedication medication);
    }
}
