using Model.DataModel;
using Model.DataModelInterface;
using zUtilities;

namespace Model.DataModel
{
	public class DoctorsLetter : Data, IDoctorsLetter
	{
		private string diagnosis;

		private string filepath;

		private Profile profile;

		public void SetFilepath(string path)
		{

		}

		public void DisattachMedication(Medication med)
		{

		}

		public void AddToGroup(DoctorsLetterGroup group)
		{

		}

		public void RemoveFromGroup(DoctorsLetterGroup group)
		{

		}

		public void AttachMedication(Medication med)
		{

		}


		/// <see>Model.DataModelInterface.IDoctorsLetter#GetDiagnosis()</see>
		public string GetDiagnosis()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IDoctorsLetter#GetDoctor()</see>
		public IDoctor GetDoctor()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IDoctorsLetter#GetMedication()</see>
		public IList<IMedication> GetMedication()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IDoctorsLetter#RemoveMedication(Model.DataModelInterface.IMedication)</see>
		public void RemoveMedication(IMedication med)
		{

		}


		/// <see>Model.DataModelInterface.IDoctorsLetter#AttachMedication(Model.DataModelInterface.IMedication)</see>
		public void AttachMedication(IMedication med)
		{

		}


		/// <see>Model.DataModelInterface.IDoctorsLetter#GetGroups()</see>
		public IList<DoctorsLetterGroup> GetGroups()
		{
			return null;
		}

	}

}

