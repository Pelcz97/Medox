using myMD.View.AbstractPages;
using ViewModel.OverallViewModel;
using Xamarin.Forms;

namespace ViewModel.MedicationTabViewModel
{
	public class MedicationViewModel : OverallViewModel
	{
		private ToolbarItem AddEntryButton;

        public MedicationViewModel(INavigation Navigation){
            
        }

		public ICommand ShowMedicationDetails()
		{
			return null;
		}

		public void addNewMedication()
		{

		}

	}

}

