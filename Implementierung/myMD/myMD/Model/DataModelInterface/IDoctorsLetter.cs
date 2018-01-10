using Model.DataModelInterface;
using zUtilities;

namespace Model.DataModelInterface
{
	public interface IDoctorsLetter : IData
	{
		string GetDiagnosis();

		IDoctor GetDoctor();

		IList<IMedication> GetMedication();

		void RemoveMedication(IMedication med);

		void AttachMedication(IMedication med);

		IList<DoctorsLetterGroup> GetGroups();

	}

}

