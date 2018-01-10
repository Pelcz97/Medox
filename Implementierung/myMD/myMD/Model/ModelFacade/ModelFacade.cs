using Model.ModelFacadeInterface;
using Model.DatabaseModel;
using Model.EntityFactory;
using Model.ParserModel;
using Model.FileHelper;
using Model.TransmissionModel;
using Model.DataModelInterface;
using zUtilities;

namespace Model.ModelFacade
{
	public class ModelFacade : IModelFacade
	{
		private IEntityDatabase iEntityDatabase;

		private IEntityFactory iEntityFactory;

		private IParserFacade iParserFacade;

		private IFileHelper iFileHelper;

		private IBluetooth iBluetooth;


		/// <see>Model.ModelFacadeInterface.IModelFacade#createEmptyProfile()</see>
		public IProfile createEmptyProfile()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#getAllDoctorsLetters()</see>
		public IList<IDoctorsLetter> getAllDoctorsLetters()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#getAllMedications()</see>
		public IList<IMedication> getAllMedications()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#createEmptyMedication()</see>
		public IMedication createEmptyMedication()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#getAllGroups()</see>
		public IList<IDoctorsLetterGroup> getAllGroups()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#createEmptyGroup()</see>
		public IDoctorsLetterGroup createEmptyGroup()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#sendLetter(Model.DataModelInterface.IDoctorsLetter)</see>
		public void sendLetter(IDoctorsLetter letter)
		{

		}

	}

}

