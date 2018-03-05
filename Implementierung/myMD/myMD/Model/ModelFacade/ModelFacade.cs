using myMD.Model.DatabaseModel;
using myMD.Model.DependencyService;
using myMD.Model.EntityFactory;
using myMD.Model.FileHelper;
using myMD.Model.ParserModel;
using myMD.Model.TransmissionModel;
using myMD.ModelInterface.DataModelInterface;
using myMD.ModelInterface.ModelFacadeInterface;
using System.Collections.Generic;
using Xamarin.Forms;
using myMD.ModelInterface.TransmissionModelInterface;
using nexus.protocols.ble;
using System.Linq;
using System.Threading.Tasks;

namespace myMD.Model.ModelFacade
{
    /// <summary>
    /// Fassade zum Einstieg ins Model. Enthält Schnittstellen zu den anderen Paketen im Model an die beim Methodenaufruf delegiert wird.
    /// </summary>
    /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade</see>
	public class ModelFacade : IModelFacade
    {
        /// <summary>
        /// Die verwendete Datenübertragungsmöglichkeit
        /// </summary>
		private IBluetooth bluetooth;

        /// <summary>
        /// Die verwendete Datenbank
        /// </summary>
		private IEntityDatabase database;

        /// <summary>
        /// Die verwendete Fabrik
        /// </summary>
		private IEntityFactory factory;

        /// <summary>
        /// Der verwendete Dateihelfer
        /// </summary>
		private IFileHelper fileHelper;

        /// <summary>
        /// Der verwendete Parser
        /// </summary>
		private IParserFacade parser;

        /// <summary>
        /// Konstruktor in dem die Schnittstellen, von dem die Klasse abhänig ist injiziert werden können.
        /// </summary>
        /// <param name="database">Die zu verwendende Datenbank</param>
        /// <param name="factory">Die zu verwendende Fabrik</param>
        /// <param name="parser">Der zu verwendende Parser</param>
        /// <param name="bluetooth">Die zu verwendende Datenübertragungsmöglichkeit</param>
        public ModelFacade(IEntityDatabase database, IEntityFactory factory, IParserFacade parser, IBluetooth bluetooth)
        {
            this.database = database;
            this.factory = factory;
            this.parser = parser;
            this.bluetooth = bluetooth;
            this.fileHelper = DependencyServiceWrapper.Get<IFileHelper>();
            //Create new Profile, if none exists yet
            if (database.GetAllProfiles().Count == 0)
            {
                CreateEmptyProfile();
            }
        }

        public void SetConnectedServer(IBleGattServerConnection server) => bluetooth.ConnectedGattServer = server;
        public IBleGattServerConnection GetConnectedServer() => bluetooth.ConnectedGattServer;

        public async Task GetFilesFromServer(){
            List<List<byte[]>> AllFiles = await bluetooth.ReadAllFilesOnServer();

            List<byte[]> files = new List<byte[]>();
            foreach (List<byte[]> file in AllFiles){
                var concatenated = new byte[file.Count];
                foreach (byte[] array in file){
                    concatenated.Concat(array);
                }
                files.Add(concatenated);
            }

            foreach (byte[] file in files){
                var path = fileHelper.WriteLocalFileFromBytes(".hl7", file);
                parser.ParseFileToDatabase(path, database);
            }

        }

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#Activate(Model.DataModelInterface.IProfile)</see>
        public void Activate(IProfile profile) => database.Activate(profile);

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#CreateEmptyGroup()</see>
        public IDoctorsLetterGroup CreateEmptyGroup()
        {
            IDoctorsLetterGroup group = factory.CreateEmptyGroup();
            database.Insert(group);
            return group;
        }

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#CreateEmptyMedication()</see>
        public IMedication CreateEmptyMedication()
        {
            IMedication med = factory.CreateEmptyMedication();
            database.Insert(med);
            return med;
        }

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#CreateEmptyProfile()</see>
        public IProfile CreateEmptyProfile()
        {
            IProfile profile = factory.CreateEmptyProfile();
            Activate(profile);
            database.Insert(profile); 
            return profile;
        }

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#Delete(Model.DataModelInterface.IEntity)</see>
        public void Delete(IEntity entity) => database.Delete(entity);

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#GetActiveProfile()</see>
        public IProfile GetActiveProfile() => database.GetActiveProfile();

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#GetAllDoctorsLetters()</see>
        public IList<IDoctorsLetter> GetAllDoctorsLetters() => database.GetAllDoctorsLetters();

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#getAllGroups()</see>
        public IList<IDoctorsLetterGroup> GetAllGroups() => database.GetAllDoctorsLetterGroups();

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#GetAllMedications()</see>
        public IList<IMedication> GetAllMedications() => database.GetAllMedications();

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#SendLetter(Model.DataModelInterface.IDoctorsLetter)</see>
        //public void SendLetter(IDoctorsLetter letter) => bluetooth.send(parser.ParseLetterToOriginalFile(letter));

        /// <see>myMD.ModelInterface.ModelFacadeInterface.IModelFacade#Update(Model.DataModelInterface.IEntity)</see>
        public void Update(IEntity entity) => database.Update(entity);


    }
}