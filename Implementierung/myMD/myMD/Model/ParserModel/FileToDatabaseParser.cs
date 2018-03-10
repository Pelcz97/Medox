using myMD.Model.DatabaseModel;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Abstrakte Klasse zum Parsen von Dateien in eine Datenbank.
    /// Sollte dateiformatspezifisch erweitert werden.
    /// </summary>
	public abstract class FileToDatabaseParser
    {
        /// <summary>
        /// Fügt Informationen aus der Datei in die Datenbank ein.
        /// </summary>
        /// <param name="filename">Die zu parsende Datei</param>
        /// <param name="db">Die Datenbank in die die geparsten Informationen eingetragen werden</param>
        /// <exception cref="InvalidOperationException">Werfe, wenn kein passendes Profil für die Datei gefunden wurde</exception>
		public void ParseFile(string filename, IEntityDatabase db)
        {
            Init(filename);
            //Werfe Ausnahme falls kein passendes Profil existiert
            var pr = ParseProfile();
            Debug.WriteLine("LastName: " + pr.LastName);
            Debug.WriteLine("Name: " + pr.Name);
            Debug.WriteLine("InsuranceNumber: " + pr.InsuranceNumber);

            IProfile iProfile = db.GetProfile(ParseProfile()) ?? throw new InvalidOperationException("No matching Profile found");
            Profile profile = iProfile.ToProfile();
            Doctor eqDoc = ParseDoctor();
            IDoctor doc = db.GetDoctor(eqDoc);
            Doctor doctor;
            //Füge neuen Arzt in die Datenbank ein, falls dieser dort noch nicht existiert
            if (doc == null)
            {
                doctor = eqDoc;
                db.Insert(doctor);
            } else
            {
                doctor = doc.ToDoctor();
            }
            IList<Medication> meds = ParseMedications();
            DoctorsLetter letter = ParseLetter();
            db.Insert(letter);
            //Erstelle Beziehungen zwischen den geparsten Entitäten
            letter.Profile = profile;
            letter.DatabaseDoctor = doctor;
            foreach (Medication med in meds)
            {
                db.Insert(med);
                letter.AttachMedication(med);
            }
            db.Update(letter);
        }

        /// <summary>
        /// Rufe diese Methode auf um den Parser zu initialisieren um danach über die Parse-Methoden die relevanten Informationen zu erhalten.
        /// </summary>
        /// <param name="file">Der Pfad zu der Datei die geparst werden soll</param>
        protected abstract void Init(string file);

        /// <summary>
        /// Erstelle den in der Datei spezifizierten Arzt.
        /// </summary>
        /// <returns>Den erstellten Arzt</returns>
		protected abstract Doctor ParseDoctor();

        /// <summary>
        /// Erstelle einen Arztbrief mit den Informationen aus der Datei.
        /// </summary>
        /// <returns>Den erstellten Arztbrief</returns>
		protected abstract DoctorsLetter ParseLetter();

        /// <summary>
        /// Erstelle die in der Datei spezifizierten Medikationen.
        /// </summary>
        /// <returns>Liste der erstellten Medikationen</returns>
		protected abstract IList<Medication> ParseMedications();

        /// <summary>
        /// Erstelle das in der Datei spezifizierten Profil.
        /// </summary>
        /// <returns>Das erstellte Profil</returns>
		protected abstract Profile ParseProfile();
    }
}