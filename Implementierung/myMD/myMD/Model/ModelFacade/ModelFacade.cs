using Model.ModelFacadeInterface;
using Model.DatabaseModel;
using Model.EntityFactory;
using Model.ParserModel;
using Model.FileHelper;
using Model.TransmissionModel;
using Model.DataModelInterface;
using System.Collections.Generic;

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
		public IProfile CreateEmptyProfile()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#getAllDoctorsLetters()</see>
		public IList<IDoctorsLetter> GetAllDoctorsLetters()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#getAllMedications()</see>
		public IList<IMedication> GetAllMedications()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#createEmptyMedication()</see>
		public IMedication CreateEmptyMedication()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#getAllGroups()</see>
		public IList<IDoctorsLetterGroup> GetAllGroups()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#createEmptyGroup()</see>
		public IDoctorsLetterGroup CreateEmptyGroup()
		{
			return null;
		}


		/// <see>Model.ModelFacadeInterface.IModelFacade#sendLetter(Model.DataModelInterface.IDoctorsLetter)</see>
		public void SendLetter(IDoctorsLetter letter)
		{

		}

	}

}

