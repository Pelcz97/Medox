using myMD.ModelInterface.DataModelInterface;
using myMD.Model.FileHelper;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// Diese Klasse implementiert die IDoctorsLetter Schnittstelle und erweitert die abstrakte Data Klasse,
    /// um Arztbriefe in einer SQLite-Datenbank speichern zu können.
    /// </summary>
    /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter</see>
    /// <see>myMD.Model.DataModelInterface.Data</see>
    [Preserve(AllMembers = true)]
    public class DoctorsLetter : Data, IDoctorsLetter, IEquatable<DoctorsLetter>
    {
        /// <summary>
        /// Initialisiert nötige Listen.
        /// </summary>
        public DoctorsLetter()
        {
            DatabaseMedication = new List<Medication>();
            DatabaseGroups = new List<DoctorsLetterGroup>();
        }

        /// <summary>
        /// Der Doktor, der diesen Arztbrief erstellt hat und dem der Arztbrief eindeutig zugeordnet werden kann.
        /// Ein Doktor kann dabei mehrere Arztbriefe erstellt haben.
        /// Wird automatisch beim Lesen aus der Datenbank gesetzt.
        /// </summary>
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Doctor DatabaseDoctor { get; set; }

        /// <summary>
        /// Die Arztgruppen in denen dieser Arztbrief enthalten ist.
        /// Eine Arztbriefgruppe enthält dabei mehrere Arztbriefe.
        /// Wird automatisch beim Lesen aus der Datenbank gesetzt.
        /// </summary>
        [ManyToMany(typeof(DoctorsLetterGroupDoctorsLetter), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<DoctorsLetterGroup> DatabaseGroups { get; set; }

        /// <summary>
        /// Die Medikationen die in diesem Arztbrief verordnet wurde
        /// Einer Medikation kann dabei eindeutig ein Arztbrief zugeordnet werden.
        /// Wird automatisch beim Lesen aus der Datenbank gesetzt.
        /// </summary>
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<Medication> DatabaseMedication { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#Diagnosis()</see>
        public string Diagnosis { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#Doctor()</see>
        public IDoctor Doctor => DatabaseDoctor;

        /// <summary>
        /// Fremdschlüssel zum Doktor dieses Arztbriefs für die Datenbank.
        /// </summary>
        [ForeignKey(typeof(Doctor))]
        public int DoctorID { get; set; }

        /// <summary>
        /// Jede Instanz dieser Klasse basiert auf einer Datei. Dies ist der Pfad zu dieser Datei.
        /// Sollte bei der Erstellung aus der Datei gesetzt werden.
        /// </summary>
        public string Filepath { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#Groups()</see>
        public IList<IDoctorsLetterGroup> Groups
        {
            get
            {
                List<IDoctorsLetterGroup> list = new List<IDoctorsLetterGroup>();
                foreach (DoctorsLetterGroup group in DatabaseGroups)
                {
                    list.Add(group);
                }
                return list;
            }
        }

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#Medication()</see>
        public IList<IMedication> Medication
        {
            get
            {
                List<IMedication> list = new List<IMedication>();
                foreach (Medication med in DatabaseMedication)
                {
                    list.Add(med);
                }
                return list;
            }
        }

        /// <summary>
        /// Überladung für konkrete Arztbriefgruppen.
        /// </summary>
        /// <see>myMD.Model.DataModel.DoctorsLetter#AddToGroup(ModelInterface.DataModelInterface.IDoctorsLetterGroup)</see>
        public void AddToGroup(DoctorsLetterGroup group)
        {
            if (!DatabaseGroups.Contains(group))
            {
                DatabaseGroups.Add(group);
                group.Add(this);
            }
        }

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#AddToGroup(ModelInterface.DataModelInterface.IDoctorsLetterGroup)</see>
        public void AddToGroup(IDoctorsLetterGroup group) => AddToGroup(group.ToDoctorsLetterGroup());

        /// <summary>
        /// Überladung für konkrete Medikationen.
        /// </summary>
        /// <see>myMD.Model.DataModel.DoctorsLetter#AttachMedication(ModelInterface.DataModelInterface.IMedication)</see>
        public void AttachMedication(Medication med)
        {
            if (!DatabaseMedication.Contains(med))
            {
                DatabaseMedication.Add(med);
                med.AttachToLetter(this);
            }
        }

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#AttachMedication(ModelInterface.DataModelInterface.IMedication)</see>
        public void AttachMedication(IMedication med) => AttachMedication(med.ToMedication());

        /// <summary>
        /// Löst alle der Klasse bekannten Assoziatonen auf und löscht die Datei aus der dieser Arztbrief stammt.
        /// </summary>
        /// <see>myMD.Model.DataModel.Entity#Delete()</see>
        public override void Delete()
        {
            while (DatabaseMedication.Any())
            {
                DisattachMedication(DatabaseMedication.First());
            }
            while (DatabaseGroups.Any())
            {
                RemoveFromGroup(DatabaseGroups.First());
            }
            DependencyService.Get<IFileHelper>().DeleteFile(Filepath);
        }

        /// <summary>
        /// Überladung für konkrete Medikationen.
        /// </summary>
        /// <see>myMD.Model.DataModel.DoctorsLetter#DisattachMedication(ModelInterface.DataModelInterface.IMedication)</see>
        public void DisattachMedication(Medication med)
        {
            if (DatabaseMedication.Contains(med))
            {
                DatabaseMedication.Remove(med);
                med.DisattachFromLetter(this);
            }
        }

        /// <summary>
        /// Zwei Arztbriefe sind genau dann gleich, wenn sie als Daten gleich sind, ihr Dateipfad, ihr Doktor, ihre Diagnose und ihre Medikationen gleich sind.
        /// </summary>
        /// <see>System.IEquatable<T>#Equals(T)</see>
        public bool Equals(DoctorsLetter other)
        {
            return base.Equals(other)
                && Filepath.Equals(other.Filepath)
                && Diagnosis.Equals(other.Diagnosis)
                && DatabaseMedication.SequenceEqual(other.DatabaseMedication)
                && DatabaseDoctor.Equals(other.DatabaseDoctor);
        }

        /// <see>System.Object#Equals(System.Object)</see>
        public override bool Equals(Object obj) => Equals(obj as DoctorsLetter);

        /// <see>System.Object#GetHashCode()</see>
        public override int GetHashCode()
        {
            var hashCode = -551889408;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Filepath);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Diagnosis);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Medication>>.Default.GetHashCode(DatabaseMedication);
            hashCode = hashCode * -1521134295 + EqualityComparer<Doctor>.Default.GetHashCode(DatabaseDoctor);
            return hashCode;
        }

        /// <summary>
        /// Überladung für konkrete Arztbriefgruppen.
        /// </summary>
        /// <see>myMD.Model.DataModel.DoctorsLetter#RemoveFromGroup(ModelInterface.DataModelInterface.IDoctorsLetterGroup)</see>
        public void RemoveFromGroup(DoctorsLetterGroup group)
        {
            if (DatabaseGroups.Contains(group))
            {
                DatabaseGroups.Remove(group);
                group.Remove(this);
            }
        }

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#RemoveFromGroup(Model.DataModelInterface.IDoctorsLetterGroup)</see>
        public void RemoveFromGroup(IDoctorsLetterGroup group) => RemoveFromGroup(group.ToDoctorsLetterGroup());

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#RemoveMedication(Model.DataModelInterface.IMedication)</see>
        public void RemoveMedication(IMedication med) => RemoveMedication(med.ToMedication());
        
        /// <summary>
        /// Da diese Klasse bereits den verlangten Rückgabetyp hab, ist keine Konvertierung nötig.
        /// </summary>
        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter#ToDoctorsLetter()</see>
        public DoctorsLetter ToDoctorsLetter() => this;
    }
}