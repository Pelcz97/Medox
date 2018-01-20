using myMD.ModelInterface.ModelFacadeInterface;
using myMD.Model.DatabaseModel;
using myMD.Model.EntityFactory;
using myMD.Model.ParserModel;
using myMD.Model.FileHelper;
using myMD.Model.TransmissionModel;
using myMD.ModelInterface.DataModelInterface;
using System.Collections.Generic;
using myMD.Model.DataModel;

namespace myMD.Model.ModelFacade
{
    /// <summary>
    /// 
    /// </summary>
    /// <see>ModelInterface.ModelFacadeInterface.IModelFacade</see>
	public class ModelFacade : IModelFacade
	{
        /// <summary>
        /// 
        /// </summary>
		private IEntityDatabase database;

        /// <summary>
        /// 
        /// </summary>
		private IEntityFactory factory;

        /// <summary>
        /// 
        /// </summary>
		private IParserFacade parser;

        /// <summary>
        /// 
        /// </summary>
		private IFileHelper fileHelper;

        /// <summary>
        /// 
        /// </summary>
		private IBluetooth bluetooth;

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#CreateEmptyProfile()</see>
        public IProfile CreateEmptyProfile() => factory.CreateEmptyProfile();

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#GetAllDoctorsLetters()</see>
        public IList<IDoctorsLetter> GetAllDoctorsLetters() => database.GetAllDoctorsLetters();

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#GetAllMedications()</see>
        public IList<IMedication> GetAllMedications() => database.GetAllMedications();

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#CreateEmptyMedication()</see>
        public IMedication CreateEmptyMedication() => factory.CreateEmptyMedication();

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#getAllGroups()</see>
        public IList<IDoctorsLetterGroup> GetAllGroups() => database.GetAllDoctorsLetterGroups();

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#CreateEmptyGroup()</see>
        public IDoctorsLetterGroup CreateEmptyGroup() => factory.CreateEmptyGroup();

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#SendLetter(Model.DataModelInterface.IDoctorsLetter)</see>
        public void SendLetter(IDoctorsLetter letter) => bluetooth.send(parser.ParseLetterToOriginalFile(letter));

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#Update(Model.DataModelInterface.IEntity)</see>
        public void Update(IEntity entity) => database.Update(entity);

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#Delete(Model.DataModelInterface.IEntity)</see>
        public void Delete(IEntity entity) => database.Delete(entity);

        /// <see>ModelInterface.ModelFacadeInterface.IModelFacade#Activate(Model.DataModelInterface.IProfile)</see>
        public void Activate(IProfile profile) => database.Activate(profile);

    }

}

