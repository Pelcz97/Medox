using View.AbstractPages;
using ViewModel.OverallViewModel;
using zUtilities;

namespace ViewModel.OverviewTabViewModel
{
	public class OverviewViewModel : CustomContentPage, OverallViewModel
	{
		private ToolbarItem EditButton;

		public ICommand ShowDoctorsLetter()
		{
			return null;
		}

		public void EditList(IList<DoctorsLetter> list)
		{

		}

	}

}

