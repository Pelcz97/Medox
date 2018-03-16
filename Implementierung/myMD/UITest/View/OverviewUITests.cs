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
            string PathToAPK = Path.Combine(dir, "Droid", "bin", "Release", "com.team3.myMD.apk");
            app = ConfigureApp.Android.ApkFile(PathToAPK).StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        /// <summary>
        /// This Test tests the whole App for the existence of every ViewElement
        /// It does not need any Assertion, since every Line would Crash if the searched Element is not found
        /// </summary>
        [Test]
        public void EveryElementInWholeApp()
        {
            app.Flash("Übersicht");
            app.Flash("Medikation");
            app.Flash("Senden");
            app.Flash("Profil");
            app.SwipeRightToLeft();
            app.Flash("Medikation");
            app.Flash("Hinzufügen");
            app.Tap("Hinzufügen");
            app.Flash("Neue Medikation");
            app.Flash("Abbrechen");
            app.Flash("Fertig");
            app.Flash("Information");
            app.Flash("Name des Medikaments");
            app.Flash("Wirkstoffmenge");
            app.Flash("Zeitraum der Einnahme");
            app.Flash("Beginn");
            app.Flash("Ende");
            app.Flash("1 mal täglich");
            app.Flash("-");
            app.Flash("+");
            app.Flash("StartDatePicker");
            app.Flash("EndDatePicker");
            app.Tap("Abbrechen");
            app.SwipeRightToLeft();
            app.Flash("Senden");
            app.Flash("Hinweis");
            app.Flash("ReceiveDataButton");
            app.Tap("ReceiveDataButton");
            app.Flash("Geräte suchen");
            app.Flash("0 Dateien verfügbar");
            app.Flash("0 Arztbriefe laden");
            app.SwipeRightToLeft();
            app.Flash("Profil");
            app.Flash("Name");
            app.Flash("Vorname");
            app.Flash("Geburtstag");
            app.Flash("Versicherungsnummer");
            app.Flash("Blutgruppe");
            app.Flash("Birthday");
            app.Flash("Bloodtype");
            app.Flash("Bearbeiten");
            app.Tap("Bearbeiten");
            app.Query("Profil bearbeiten");
            app.Flash("Profil bearbeiten");
            app.Flash("Abbrechen");
            app.Flash("Fertig");
            app.Flash("Name");
            app.Flash("Nachname");
            app.Flash("Vorname");
            app.Flash("Geburtsdatum");
            app.Flash("Versicherungsnummer");
            app.Flash("Blutgruppe");
            app.Flash("BirthdayPicker");
            app.Tap("Abbrechen");
            app.SwipeLeftToRight();
            app.SwipeLeftToRight();
            app.SwipeLeftToRight();
        }

    }
}

