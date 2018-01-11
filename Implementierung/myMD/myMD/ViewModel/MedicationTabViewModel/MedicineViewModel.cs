using ModelInterface.DataModelInterface;

namespace ViewModel.MedicationTabViewModel
{
    public class MedicineViewModel : OverallViewModel.OverallViewModel
	{
		private string medicationName;

		private int startingDate;

		private int endingDate;

		private int frequency;

		private IMedication iMedication;

	}

}

