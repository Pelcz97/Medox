using Model.EntityFactory;
using Model.DataModelInterface;
using zUtilities;

namespace Model.ModelFacadeInterface
{
	public interface IModelFacade
	{
		private IEntityFactory iEntityFactory;

		IProfile createEmptyProfile();

		IList<IDoctorsLetter> getAllDoctorsLetters();

		IList<IMedication> getAllMedications();

		IMedication createEmptyMedication();

		IList<IDoctorsLetterGroup> getAllGroups();

		IDoctorsLetterGroup createEmptyGroup();

		void sendLetter(IDoctorsLetter letter);

	}

}

