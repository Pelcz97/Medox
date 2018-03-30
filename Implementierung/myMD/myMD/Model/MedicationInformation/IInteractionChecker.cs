using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.MedicationInformation
{
    public interface IInteractionChecker
    {
        Task<IList<InteractionPair>> GetInteractions(IList<IMedication> medications);

        Task<IList<string>> GetRxNormIDs(IList<IMedication> medications);

        Task<string> GetRxNormID(IMedication medication);
    }
}
