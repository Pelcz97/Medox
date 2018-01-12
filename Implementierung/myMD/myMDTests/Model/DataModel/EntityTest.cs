using NUnit.Framework;
using myMD.Model.DataModel;
using myMD.Model.EntityObserver;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    public class EntityTest
    {
        private static string TEST_NAME = "Test-Entität";
        private static string ALT_TEST_NAME = "Alternative Test-Entität";
        private static int TEST_ID = 1234;
        private Entity entity;
        private MockEntityObserver obs;

        [SetUp]
        public void SetUp()
        {
            entity = new Doctor
            {
                ID = TEST_ID,
                Name = TEST_NAME
            };
            obs = new MockEntityObserver();
            entity.Subscribe(obs);
        }

        [Test]
        public void TestName() => Assert.AreEqual(entity.Name, TEST_NAME);

        [Test]
        public void TestID() => Assert.AreEqual(entity.ID, TEST_ID);

        /// <summary>
        /// Teste ob eine Entität ihren Beobachter korrekt über ein Update benachrichtigt
        /// </summary>
        [Test]
        public void TestObservableUpdate()
        {           
            Assert.False(obs.Updated);
            Assert.Null(obs.Name);
            entity.Name = ALT_TEST_NAME;
            Assert.IsTrue(obs.Updated);
            Assert.AreEqual(ALT_TEST_NAME, obs.Name);
            entity.Unsubscribe(obs);
            obs.Updated = false;
            entity.Name = TEST_NAME;
            Assert.False(obs.Updated);
            Assert.AreNotEqual(TEST_NAME, obs.Name);
        }

        /// <summary>
        /// Teste ob eine Entität ihren Beobachter korrekt über eine Löschung benachrichtigt
        /// </summary>
        [Test]
        public void TestObservableDeleted()
        {
            Assert.False(obs.Deleted);
            Assert.Null(obs.Name);
            entity.Delete();
            Assert.IsTrue(obs.Deleted);
            Assert.AreEqual(TEST_NAME, obs.Name);
            entity.Unsubscribe(obs);
            obs.Deleted = false;
            entity.Name = ALT_TEST_NAME;
            entity.Delete();
            Assert.False(obs.Deleted);
            Assert.AreNotEqual(ALT_TEST_NAME, obs.Name);
        }

        [TearDown]
        public void TearDown()
        {
            entity = null;
            obs = null;
        }

        /// <summary>
        /// Mock Objekt zur Überprüfung, ob Beobachter korrekt benachrichtigt werden
        /// </summary>
        private class MockEntityObserver : IEntityObserver
        {
            private bool deleted = false;
            private bool updated = false;
            private string name;

            public bool Deleted { get => deleted; set => deleted = value; }

            public bool Updated { get => updated; set => updated = value; }

            public string Name { get => name; set => name = value; }

            public void OnDeletion(Entity entity)
            {
                deleted = true;
                name = entity.Name;
            }

            public void OnUpdate(Entity entity)
            {
                updated = true;
                name = entity.Name;
            }
        }
    }
}

