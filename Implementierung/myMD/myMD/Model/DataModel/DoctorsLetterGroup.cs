using Model.DataModel;
using Model.DataModelInterface;
using zUtilities;

namespace Model.DataModel
{
	public class DoctorsLetterGroup : Data, IDoctorsLetterGroup
	{
		private Profile profile;

		public void Add(DoctorsLetter letter)
		{

		}

		public void Remove(DoctorsLetter letter)
		{

		}


		/// <see>Model.DataModelInterface.IDoctorsLetterGroup#GetAll()</see>
		public IList<IDoctorsLetter> GetAll()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IDoctorsLetterGroup#Add(Model.DataModelInterface.IDoctorsLetter)</see>
		public void Add(IDoctorsLetter letter)
		{

		}


		/// <see>Model.DataModelInterface.IDoctorsLetterGroup#Remove(Model.DataModelInterface.IDoctorsLetter)</see>
		public void Remove(IDoctorsLetter letter)
		{

		}


		/// <see>Model.DataModelInterface.IDoctorsLetterGroup#GetLastDate()</see>
		public DateTime GetLastDate()
		{
			return null;
		}

	}

}

