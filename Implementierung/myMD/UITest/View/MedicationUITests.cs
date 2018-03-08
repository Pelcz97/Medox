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
    class MedicationUITests
    {
        Platform platform;
        IApp app;

        public MedicationUITests(Platform platform)
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
        public void CreateNewMedication()
        {
            app.SwipeRightToLeft();
            app.Query(e => e.Marked("Hinzufügen"));
            app.Tap("Hinzufügen");
            app.EnterText("Name_Container", "Aspirin");
            app.EnterText("Dosis_Container", "400mg");
            app.Tap("+");
            app.Tap("+");
            app.Tap("+");
            app.Tap("StartDatePicker_Container");
            app.Tap("date_picker_header_year");
            app.Tap("2012");
            app.Tap("date_picker_header_date");
            app.Tap("prev");
            app.Tap("date_picker_day_picker");
            app.Tap("OK");
            app.Tap("EndDatePicker_Container");
            app.Tap("date_picker_header_year");
            app.Tap("2020");
            app.Tap("date_picker_header_date");
            app.Tap("next");
            app.Tap("next");
            app.Tap("date_picker_day_picker");
            app.Tap("OK");
            app.Tap("Fertig");
        }
    }
}
