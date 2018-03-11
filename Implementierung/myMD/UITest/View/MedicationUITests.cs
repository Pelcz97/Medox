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
            string PathToAPK = Path.Combine(dir, "Droid", "bin", "Release", "com.team3.myMD.apk");
            app = ConfigureApp.Android.ApkFile(PathToAPK).StartApp();
        }

        [Test]
        public void CreateNewMedication()
        {
            app.SwipeRightToLeft();
            app.Tap("Hinzufügen");
            app.EnterText("Name", "Aspirin");
            app.EnterText("EditDosis", "400mg");
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
            app.Query("Dosis");
            AppResult[] result = app.Query("Dosis");
            Assert.AreEqual(result[0].Text, "400mg");
            result = app.Query("Frequency");
            Assert.AreEqual(result[0].Text, "4 mal täglich");
            result = app.Query("MedicationName");
            Assert.AreEqual(result[0].Text, "Aspirin");
            result = app.Query("StartDate");
            Assert.NotNull(result[0].Text);
            result = app.Query("Duration");
            Assert.NotNull(result[0].Text);
        }

        [Test]
        public void DeleteMedication()
        {
            app.SwipeRightToLeft();
            app.Tap("Hinzufügen");
            app.EnterText("Name", "Aspirin");
            app.EnterText("EditDosis", "400mg");
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
            app.TouchAndHold("Dosis");
            app.Tap("Löschen");
            AppResult[] result = app.Query("Dosis");
            Assert.IsEmpty(result);
            result = app.Query("Frequency");
            Assert.IsEmpty(result);
            result = app.Query("MedicationName");
            Assert.IsEmpty(result);
            result = app.Query("StartDate");
            Assert.IsEmpty(result);
            result = app.Query("Duration");
            Assert.IsEmpty(result);
        }

        [Test]
        public void EditMedication()
        {
            app.SwipeRightToLeft();
            app.Tap("Hinzufügen");
            app.EnterText("Name", "Heroin");
            app.EnterText("EditDosis", "3 Spritzen");
            app.Tap("+");
            app.Tap("Fertig");
            app.Query("MedicationName");
            AppResult[] result = app.Query("MedicationName");
            string oldName = result[0].Text;
            result = app.Query("Dosis");
            string oldDosis = result[0].Text;
            result = app.Query("Frequency");
            string oldFrequency = result[0].Text;
            result = app.Query("StartDate");
            string oldStartDate = result[0].Text;
            result = app.Query("Duration");
            string oldDuration = result[0].Text;

            app.Tap("Dosis");
            app.ClearText("Name");
            app.EnterText("Name", "Globuli");
            app.ClearText("EditDosis");
            app.EnterText("EditDosis", "20 Stück");
            app.Tap("+");
            app.Tap("+");
            app.Tap("+");
            app.Tap("+");
            app.Tap("+");
            app.Tap("+");
            app.Tap("+");
            app.Tap("+");
            app.Tap("+");
            app.Tap("Fertig");
            result = app.Query("MedicationName");
            string newName = result[0].Text;
            result = app.Query("Dosis");
            string newDosis = result[0].Text;
            result = app.Query("Frequency");
            string newFrequency = result[0].Text;
            result = app.Query("StartDate");
            string newStartDate = result[0].Text;
            result = app.Query("Duration");
            string newDuration = result[0].Text;
            Assert.AreNotEqual(oldName, newName);
            Assert.AreNotEqual(oldDosis, newDosis);
            Assert.AreEqual(oldDuration, newDuration);
            Assert.AreNotEqual(oldFrequency, newFrequency);
            Assert.AreEqual(oldStartDate, newStartDate);
        }
    }
}
