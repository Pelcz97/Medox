using ViewModel.OverallViewModel;
using Model.DataModelInterface;

namespace ViewModel.MedicationTabViewModel
{
	public class MedicineViewModel : OverallViewModel
	{
		private string medicationName;

		private int startingDate;

		private int endingDate;

		private int frequency;

		private IMedication iMedication;

	}

}

