using myMD.Model.DataModel;
using ModelInterface.DataModelInterface;
using System;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace myMD.Model.DataModel
{
	public class Medication : Data, IMedication, IEquatable<Medication>

    {
		public void AttachToLetter(DoctorsLetter letter)
		{
            if (DatabaseDoctorsLetter != letter)
            {
                this.DatabaseDoctorsLetter = letter;
                letter.AttachMedication(this);
            }
		}

        public void DisattachFromLetter(DoctorsLetter letter)
        {
            if (this.DatabaseDoctorsLetter == letter)
            {
                this.DatabaseDoctorsLetter = null;
                letter.DisattachMedication(this);
            }
        }

        public void Delete()
        {
            DisattachFromLetter(DatabaseDoctorsLetter);
        }

        /// <see>Model.DataModelInterface.IMedication#Frequency</see>
        public int Frequency { get; set; }

        /// <see>Model.DataModelInterface.IMedication#Interval</see>
        public Interval Interval { get; set; }

        /// <see>Model.DataModelInterface.IMedication#EndDate</see>
        public DateTime EndDate { get; set; }

        [ForeignKey(typeof(DoctorsLetter))]
        public int LetterId { get; set; }

        [ManyToOne]
        public DoctorsLetter DatabaseDoctorsLetter { get; set; }

        public IDoctorsLetter DoctorsLetter => DatabaseDoctorsLetter;

        /// <see>Model.DataModelInterface.IMedication#DisattachFromLetter(Model.DataModelInterface.IDoctorsLetter)</see>
        public void DisattachFromLetter(IDoctorsLetter letter) => DisattachFromLetter(letter.ToDoctorsLetter());

        /// <see>Model.DataModelInterface.IMedication#AttachToLetter(Model.DataModelInterface.IDoctorsLetter)</see>
        public void AttachToLetter(IDoctorsLetter letter) => AttachToLetter(letter.ToDoctorsLetter());

        public bool Equals(Medication other)
        {
            return ID.Equals(other.ID);
        }

        public override bool Equals(Object obj)
        {
            Medication med = obj as Medication;
            return med != null && Equals(med);
        }

        public Medication ToMedication() => this;
    }

}

