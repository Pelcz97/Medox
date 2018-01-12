using ModelInterface.DataModelInterface;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace myMD.Model.DataModel
{
	public class DoctorsLetter : Data, IDoctorsLetter
	{
        public DoctorsLetter()
        {
            meds = new List<Medication>();
            groups = new List<DoctorsLetterGroup>();
        }

		private string diagnosis;

		private string filepath;

        private Doctor doctor;

        private IList<Medication> meds;

        private IList<DoctorsLetterGroup> groups;

        public string Filepath
        {
            get => filepath;
            set => filepath = value;
        }

        public void DisattachMedication(Medication med)
		{
            if (meds.Contains(med))
            {
                meds.Remove(med);
                med.DisattachFromLetter(this);
            }
        }

		public void AddToGroup(DoctorsLetterGroup group)
		{
            if (!groups.Contains(group))
            {
                groups.Add(group);
                group.Add(this);
            }
		}

		public void RemoveFromGroup(DoctorsLetterGroup group)
		{
            if (groups.Contains(group))
            {
                groups.Remove(group);
                group.Remove(this);
            }
        }

        
		public void AttachMedication(Medication med)
		{
            if (!meds.Contains(med))
            {
                meds.Add(med);
                med.AttachToLetter(this);
            }
        }

        /// <see>Model.DataModelInterface.IDoctorsLetter#Diagnosis()</see>
        public string Diagnosis => diagnosis; 

        /// <see>Model.DataModelInterface.IDoctorsLetter#Doctor()</see>
        public IDoctor Doctor => doctor; 

        /// <see>Model.DataModelInterface.IDoctorsLetter#Medication()</see>
        public IList<IMedication> Medication => meds.Cast<IMedication>().ToList();

        /// <see>Model.DataModelInterface.IDoctorsLetter#RemoveMedication(Model.DataModelInterface.IMedication)</see>
        public void RemoveMedication(IMedication med) => med.DisattachFromLetter(this);

        /// <see>Model.DataModelInterface.IDoctorsLetter#AttachMedication(Model.DataModelInterface.IMedication)</see>
        public void AttachMedication(IMedication med) => med.AttachToLetter(this);

        /// <see>Model.DataModelInterface.IDoctorsLetter#AddToGroup(Model.DataModelInterface.IDoctorsLetterGroup)</see>
        public void AddToGroup(IDoctorsLetterGroup group) => group.Add(this);

        /// <see>Model.DataModelInterface.IDoctorsLetter#RemoveFromGroup(Model.DataModelInterface.IDoctorsLetterGroup)</see>
        public void RemoveFromGroup(IDoctorsLetterGroup group) => group.Remove(this);

        /// <see>Model.DataModelInterface.IDoctorsLetter#Groups()</see>
        public IList<IDoctorsLetterGroup> Groups => groups.Cast<IDoctorsLetterGroup>().ToList();

    }

}

