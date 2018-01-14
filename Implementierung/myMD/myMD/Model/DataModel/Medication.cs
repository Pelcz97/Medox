using myMD.Model.DataModel;
using ModelInterface.DataModelInterface;
using System;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace myMD.Model.DataModel
{
	public class Medication : Data, IMedication, IEquatable<Medication>

    {
        private DateTime startDate;

        private DateTime endDate;

		private int frequency;

		private Interval interval;

        private DoctorsLetter letter;

		public void AttachToLetter(DoctorsLetter letter)
		{
            if (this.letter != letter)
            {
                this.letter = letter;
                letter.AttachMedication(this);
                Updated();
            }
		}

        public void DisattachFromLetter(DoctorsLetter letter)
        {
            if (this.letter == letter)
            {
                this.letter = null;
                letter.DisattachMedication(this);
                Updated();
            }
        }

        public override void Delete()
        {
            base.Delete();
            DisattachFromLetter(letter);
        }

        /// <see>Model.DataModelInterface.IMedication#Frequency</see>
        public int Frequency
        {
            get => frequency;
            set
            {
                frequency = value;
                Updated();
            }
        }

        /// <see>Model.DataModelInterface.IMedication#Interval</see>
        public Interval Interval
        {
            get => interval;
            set
            {
                interval = value;
                Updated();
            }
        }

        /// <see>Model.DataModelInterface.IMedication#EndDate</see>
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                Updated();
            }
        }

        [Ignore]
        public new DateTime Date
        {
            get => startDate;
            set
            {
                startDate = value;
                Updated();
            }
        }

        [ForeignKey(typeof(DoctorsLetter))]
        public int LetterId { get; set; }

        [ManyToOne]
        public DoctorsLetter DoctorsLetter
        {
            get => letter;
            set => letter = value;
        }

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

