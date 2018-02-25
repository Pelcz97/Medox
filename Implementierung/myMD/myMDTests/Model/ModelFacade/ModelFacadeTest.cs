﻿using myMD.ModelInterface.ModelFacadeInterface;
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
using myMD.ModelInterface.DataModelInterface;

namespace myMDTests.Model.ModelFacade
{
    [TestFixture]
    public class ModelFacadeTest
    {
        private IModelFacade model;
        private IBluetooth bluetooth;
        private EntityDatabase database;
        private IEntityFactory factory;
        private IParserFacade parser;
        private IDependencyService service;

        [OneTimeSetUp]
        public void SetUpBefore()
        {
            DependencyServiceWrapper.Service = new TestDependencyService();
            bluetooth = new Bluetooth();
            database = new EntityDatabase();
            factory = new myMD.Model.EntityFactory.EntityFactory();
            parser = new ParserFacade();
            service = new TestDependencyService();
            database.Destroy();
            database.Create();
        }

        [SetUp]
        public void SetUp()
        {              
            model = new myMD.Model.ModelFacade.ModelFacade(database, factory, parser, bluetooth);
        }

        [Test]
        public void ProfileTest()
        {
            IProfile profile = model.CreateEmptyProfile();
            model.Activate(profile);
            Assert.AreEqual(profile, model.GetActiveProfile());
        }

        [Test]
        public void MedicationTest()
        {
            IMedication med = model.CreateEmptyMedication();
            IList<IMedication> meds = model.GetAllMedications();
            Assert.IsTrue(meds.Contains(med));
        }

        [Test]
        public void GroupTest()
        {
            IDoctorsLetterGroup group = model.CreateEmptyGroup();
            Assert.IsTrue(model.GetAllGroups().Contains(group));
        }

        [Test]
        public void UpdateTest()
        {
            IDoctorsLetterGroup group = model.CreateEmptyGroup();
            database.Insert(factory.CreateEmptyDoctorsLetter());
            foreach (IDoctorsLetter letter in model.GetAllDoctorsLetters())
            {
                group.Add(letter);
            }
            model.Update(group);
            IList<IDoctorsLetterGroup> groups = model.GetAllGroups();
            Assert.IsTrue(groups.Contains(group));
        }
    }
}