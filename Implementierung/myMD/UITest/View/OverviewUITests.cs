using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    public class OverviewUITests
    {

        Platform platform;
        IApp app;

        public OverviewUITests(Platform platform)
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
            string PathToAPK = Path.Combine(dir, "Droid", "bin", "Release", "myMD.apk");
            app = ConfigureApp.Android.ApkFile(PathToAPK).StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void Repl()
        {
            app.Repl();
        }
    }
}

