using myMD.Model.DataModel;
using ModelInterface.DataModelInterface;
using System;
using SQLiteNetExtensions.Attributes;
using SQLite;
using System.Collections.Generic;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// 
    /// </summary>
	public class Medication : Data, IMedication, IEquatable<Medication>

    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="letter"></param>
		public void AttachToLetter(DoctorsLetter letter)
		{
            if (DatabaseDoctorsLetter != letter)
            {
                this.DatabaseDoctorsLetter = letter;
                letter.AttachMedication(this);
            }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="letter"></param>
        public void DisattachFromLetter(DoctorsLetter letter)
        {
            if (this.DatabaseDoctorsLetter == letter)
            {
                this.DatabaseDoctorsLetter = null;
                letter.DisattachMedication(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Delete()
        {
            DisattachFromLetter(DatabaseDoctorsLetter);
        }

        /// <see>Model.DataModelInterface.IMedication#Frequency</see>
        public int Frequency { get; set; }

        /// <see>Model.DataModelInterface.IMedication#Interval</see>
        public Interval Interval { get; set; }

        /// <see>Model.DataModelInterface.IMedication#EndDate</see>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(typeof(DoctorsLetter))]
        public int LetterId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ManyToOne]
        public DoctorsLetter DatabaseDoctorsLetter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDoctorsLetter DoctorsLetter => DatabaseDoctorsLetter;

        /// <see>Model.DataModelInterface.IMedication#DisattachFromLetter(Model.DataModelInterface.IDoctorsLetter)</see>
        public void DisattachFromLetter(IDoctorsLetter letter) => DisattachFromLetter(letter.ToDoctorsLetter());

        /// <see>Model.DataModelInterface.IMedication#AttachToLetter(Model.DataModelInterface.IDoctorsLetter)</see>
        public void AttachToLetter(IDoctorsLetter letter) => AttachToLetter(letter.ToDoctorsLetter());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Medication other)
        {
            return base.Equals(other)
                && Frequency.Equals(other.Frequency)
                && Interval.Equals(other.Interval)
                && EndDate.Equals(other.EndDate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj) => Equals(obj as Medication);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Medication ToMedication() => this;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 362796327;
            hashCode = hashCode * -1521134295 + Frequency.GetHashCode();
            hashCode = hashCode * -1521134295 + Interval.GetHashCode();
            hashCode = hashCode * -1521134295 + EndDate.GetHashCode();
            return hashCode;
        }
    }

}

