using Model.DataModelInterface;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ViewModel.OverviewTabViewModel
{
	public class OverviewViewModel : OverallViewModel.OverallViewModel
	{
		private ToolbarItem EditButton;

		public ICommand ShowDoctorsLetter()
		{
			return null;
		}

		public void EditList(IList<IDoctorsLetter> list)
		{

		}

	}

}

