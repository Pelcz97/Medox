using ModelInterface.ModelFacadeInterface;
using myMD.Model.DatabaseModel;
using myMD.Model.EntityFactory;
using myMD.Model.ParserModel;
using myMD.Model.FileHelper;
using myMD.Model.TransmissionModel;
using ModelInterface.DataModelInterface;
using System.Collections.Generic;
using myMD.Model.DataModel;

namespace myMD.Model.ModelFacade
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

        public void Update(IEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(IEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Activate(IProfile profile)
        {
            throw new System.NotImplementedException();
        }

        public Entity ToEntity()
        {
            throw new System.NotImplementedException();
        }
    }

}

