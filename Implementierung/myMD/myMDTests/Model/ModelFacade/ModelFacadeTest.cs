using myMD.ModelInterface.ModelFacadeInterface;
using myMD.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMD.Model.DatabaseModel;
using myMD.Model.TransmissionModel;
using myMD.Model.EntityFactory;
using myMD.Model.ParserModel;
using myMDTests.Model.DependencyService;
using myMD.Model.DependencyService;

namespace myMDTests.Model.ModelFacade
{
    [TestFixture]
    public class ModelFacadeTest
    {
        private IModelFacade model;
        private IBluetooth bluetooth;
        private IEntityDatabase database;
        private IEntityFactory factory;
        private IParserFacade parser;
        private IDependencyService service;

        [SetUp]
        public void SetUp()
        {
            DependencyServiceWrapper.Service = new TestDependencyService();
            bluetooth = new Bluetooth();
            database = new EntityDatabase();
            factory = new myMD.Model.EntityFactory.EntityFactory();
            parser = new ParserFacade();
            service = new TestDependencyService();       
            model = new myMD.Model.ModelFacade.ModelFacade(database, factory, parser, bluetooth);
        }
    }
}
