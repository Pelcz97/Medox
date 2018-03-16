using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    class ProfilUITests
    {
        Platform platform;
        IApp app;

        public ProfilUITests(Platform platform)
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
        public void CreateNewProfile()
        {
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Tap("Bearbeiten");
            app.EnterText("LastNameEntry", "Philipp");
            app.ClearText("LastNameEntry");
            app.EnterText("LastNameEntry", "Pelcz");
            app.EnterText("NameEntry", "Philipp");
            app.Tap("BirthdayPicker");
            app.Tap("prev");
            app.Tap("BirthdayPicker");
            app.Tap("Cancel");
            app.EnterText("InsuranceNumberEntry");
            app.EnterText("InsuranceNumberEntry", "123Test");
            app.Tap("BloodtypePicker");
            app.Tap("text1");
            app.Tap("Fertig");
            app.Flash("Profil");
            AppResult[] result = app.Query("LastNameLabel");
            Assert.AreEqual(result[0].Text, "Pelcz");
            result = app.Query("NameLabel");
            Assert.AreEqual(result[0].Text, "Philipp");
            result = app.Query("InsuranceNumber");
            Assert.AreEqual(result[0].Text, "123Test");
            result = app.Query("Bloodtype");
            Assert.AreEqual(result[0].Text, "O -");
            result = app.Query("Birthday");
            Assert.NotNull(result[0].Text);
        }

        [Test]
        public void EditProfile()
        {
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Tap("Bearbeiten");
            app.EnterText("LastNameEntry", "Philipp");
            app.ClearText("LastNameEntry");
            app.EnterText("LastNameEntry", "Pelcz");
            app.EnterText("NameEntry", "Philipp");
            app.Tap("BirthdayPicker");
            app.Tap("prev");
            app.Tap("BirthdayPicker");
            app.Tap("Cancel");
            app.EnterText("InsuranceNumberEntry");
            app.EnterText("InsuranceNumberEntry", "123Test");
            app.Tap("BloodtypePicker");
            app.Tap("text1");
            app.Tap("Fertig");
            app.Query("LastNameLabel");
            AppResult[] result = app.Query("LastNameLabel");
            String oldLastName = result[0].Text;
            result = app.Query("NameLabel");
            String oldName = result[0].Text;
            result = app.Query("InsuranceNumber");
            String oldINumber = result[0].Text;
            result = app.Query("Bloodtype");
            String oldBloodtype = result[0].Text;
            result = app.Query("Birthday");
            Assert.NotNull(result[0].Text);
            app.Tap("Bearbeiten");
            app.ClearText("NameEntry");
            app.EnterText("NameEntry", "Jan-Luca");
            app.ClearText("LastNameEntry");
            app.EnterText("NameEntry", "Vettel");
            app.ClearText("InsuranceNumber");
            app.ClearText("InsuranceNumberEntry");
            app.ClearText("InsuranceNumberEntry");
            app.EnterText("InsuranceNumberEntry", "Test1234");
            app.Tap("BloodtypePicker");
            app.Tap("Empty");
            app.ClearText("NameEntry");
            app.EnterText("LastNameEntry", "Vettel");
            app.EnterText("NameEntry", "Jan-Luca");
            app.Tap("Fertig");
            result = app.Query("LastNameLabel");
            Assert.AreNotEqual(result[0].Text, oldLastName);
            Assert.AreEqual(result[0].Text, "Vettel");
            result = app.Query("NameLabel");
            Assert.AreNotEqual(result[0].Text, oldName);
            Assert.AreEqual(result[0].Text, "Jan-Luca");
            result = app.Query("InsuranceNumber");
            Assert.AreNotEqual(result[0].Text, oldINumber);
            Assert.AreEqual(result[0].Text, "Test1234");
            result = app.Query("Bloodtype");
            Assert.AreNotEqual(result[0].Text, oldBloodtype);
            Assert.AreEqual(result[0].Text, "Empty");
            result = app.Query("Birthday");
            Assert.NotNull(result[0].Text);
        }
    }
}
