using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest.View
{
    [TestFixture(Platform.Android)]
    class SendDataUITests
    {
        Platform platform;
        IApp app;

        public SendDataUITests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            FileInfo fi = new FileInfo(currentFile);
            string dir = fi.Directory.Parent.Parent.Parent.FullName;

            // PathToAPK is a property or an instance variable in the test class
            string PathToAPK = Path.Combine(dir, "Droid", "bin", "Release", "com.team3.myMD.apk");
            app = ConfigureApp.Android.ApkFile(PathToAPK).StartApp();
        }

        [Test]
        public void AllButtonsThere()
        {
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Flash("Hinweis");
            AppResult[] result = app.Query("Hinweis");
            Assert.IsNotNull(result[0]);
            app.Flash("ReceiveDataButton");
            app.Tap("ReceiveDataButton");
            app.Flash("Geräte suchen");
            app.Flash("0 Dateien verfügbar");
            app.Flash("0 Arztbriefe laden");
            result = app.Query("Geräte suchen");
            Assert.IsNotNull(result[0]);
            Assert.IsTrue(result[0].Enabled);
            result = app.Query("0 Dateien verfügbar");
            Assert.IsNotNull(result[0]);
            Assert.IsTrue(result[0].Enabled);
            result = app.Query("0 Arztbriefe laden");
            Assert.IsNotNull(result[0]);
            Assert.IsFalse(result[0].Enabled);
        }
    }
}
