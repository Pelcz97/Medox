using NUnit.Framework;
using myMD.Model.DataModel;
using myMD.Model.EntityObserver;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    class ProfileTest
    {
        private Profile profile;
        private MockProfileObserver obs;

        [SetUp]
        public void SetUp()
        {
            profile = new Profile();
            obs = new MockProfileObserver();
            profile.Subscribe(obs);
        }

        /// <summary>
        /// Teste ob ein Profil seinen Beobachter korrekt über eine Aktivierung benachrichtigt
        /// </summary>
        [Test]
        public void TestObservableActivate()
        {
            Assert.False(obs.Activated);
            Assert.Null(obs.Profile);
            profile.SetActive();
            Assert.IsTrue(obs.Activated);
            Assert.AreEqual(obs.Profile, profile);
            profile.Unsubscribe(obs);
            obs.Activated = false;
            obs.Profile = null;
            profile.SetActive();
            Assert.False(obs.Activated);
            Assert.AreNotEqual(obs.Profile, profile);
        }

        [TearDown]
        public void TearDown()
        {
            profile = null;
            obs = null;
        }

        /// <summary>
        /// Mock Objekt zur Überprüfung, ob Beobachter korrekt benachrichtigt werden
        /// </summary>
        private class MockProfileObserver : IProfileObserver
        {
            private bool activated = false;
            private Profile profile;
 
            public bool Activated { get => activated; set => activated = value; }

            public Profile Profile { get => profile; set => profile = value; }

            public void OnActivation(Profile profile)
            {
                activated = true;
                this.profile = profile;
            }
        }
    }
}
