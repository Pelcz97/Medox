using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using System;
using SQLiteNetExtensions.Attributes;
using SQLite;
using System.Collections.Generic;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// Diese Klasse implementiert die IMedication Schnittstelle und erweitert die abstrakte Data Klasse,
    /// um Medikationen in einer SQLite-Datenbank speichern zu können.
    /// </summary>
    /// <see>ModelInterface.DataModelInterface.IDoctorsLetter</see>
    /// <see>ModelInterface.DataModelInterface.Data</see>
	public class Medication : Data, IMedication, IEquatable<Medication>
    {
        /// <summary>
        /// Der Arztbrief, in dem diese Medikation verschrieben wurde und dem sie eindeutig zugeordnet werden kann.
        /// In einem Arztbrief können dabei mehrere Medikationen verschrieben werden.
        /// Wird automatisch beim Lesen aus der Datenbank gesetzt.
        /// </summary>
        [ManyToOne]
        public DoctorsLetter DatabaseDoctorsLetter { get; set; }

        /// <see>ModelInterface.DataModelInterface.IMedication#DoctorsLetter()</see>
        public IDoctorsLetter DoctorsLetter => DatabaseDoctorsLetter;

        /// <see>ModelInterface.DataModelInterface.IMedication#EndDate</see>
        public DateTime EndDate { get; set; }

        /// <see>ModelInterface.DataModelInterface.IMedication#Frequency</see>
        public int Frequency { get; set; }

        /// <see>ModelInterface.DataModelInterface.IMedication#Interval</see>
        public Interval Interval { get; set; }

        /// <summary>
        /// Fremdschlüssel zum Arztbrief dieser Medikation für die Datenbank.
        /// </summary>
        [ForeignKey(typeof(DoctorsLetter))]
        public int LetterId { get; set; }

        /// <summary>
        /// Überladung für konkrete Arztbriefe.
        /// </summary>
        /// <see>Model.DataModel.DoctorsLetter#AttachToLetter(ModelInterface.DataModelInterface.IDoctorsLetter)</see>
		public void AttachToLetter(DoctorsLetter letter)
		{
            if (DatabaseDoctorsLetter != letter)
            {
                this.DatabaseDoctorsLetter = letter;
                letter.AttachMedication(this);
            }
		}

        /// <see>ModelInterface.DataModelInterface.IMedication#AttachToLetter(Model.DataModelInterface.IDoctorsLetter)</see>
        public void AttachToLetter(IDoctorsLetter letter) => AttachToLetter(letter.ToDoctorsLetter());

        /// <summary>
        /// Löst alle der Klasse bekannten Assoziatonen auf.
        /// </summary>
        /// <see>Model.DataModel.Entity#Delete()</see>
        public override void Delete()
        {
            DisattachFromLetter(DatabaseDoctorsLetter);
        }

        /// <summary>
        /// Überladung für konkrete Arztbriefe.
        /// </summary>
        /// <see>Model.DataModel.DoctorsLetter#DisattachFromLetter(ModelInterface.DataModelInterface.IDoctorsLetter)</see>
        public void DisattachFromLetter(DoctorsLetter letter)
        {
            if (this.DatabaseDoctorsLetter == letter && letter != null)
            {
                this.DatabaseDoctorsLetter = null;
                letter.DisattachMedication(this);
            }
        }

        /// <see>ModelInterface.DataModelInterface.IMedication#DisattachFromLetter(Model.DataModelInterface.IDoctorsLetter)</see>
        public void DisattachFromLetter(IDoctorsLetter letter) => DisattachFromLetter(letter.ToDoctorsLetter());

        /// <summary>
        /// Zwei Medikation sind genau dann gleich, wenn sie als Daten gleich sind und ihr Enddatum, Interval und ihre Frequenz gleich sind.
        /// </summary>
        /// <see>System.IEquatable<T>#Equals(T)</see>
        public bool Equals(Medication other)
        {
            return base.Equals(other)
                && Frequency.Equals(other.Frequency)
                && Interval.Equals(other.Interval)
                && EndDate.Equals(other.EndDate);
        }

        /// <see>System.Object#Equals(System.Object)</see>
        public override bool Equals(Object obj) => Equals(obj as Medication);

        /// <see>System.Object#GetHashCode()</see>
        public override int GetHashCode()
        {
            var hashCode = 362796327;
            hashCode = hashCode * -1521134295 + Frequency.GetHashCode();
            hashCode = hashCode * -1521134295 + Interval.GetHashCode();
            hashCode = hashCode * -1521134295 + EndDate.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Da diese Klasse bereits den verlangten Rückgabetyp hab, ist keine Konvertierung nötig.
        /// </summary>
        /// <see>ModelInterface.DataModelInterface.IMedication#ToMedication()</see>
        public Medication ToMedication() => this;
    }

}

