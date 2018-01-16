using ModelInterface.DataModelInterface;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using myMD.Model.EntityObserver;

namespace myMD.Model.DataModel
{
    public class DoctorsLetter : Data, IDoctorsLetter, IEquatable<DoctorsLetter>
    {
        public DoctorsLetter()
        {
            DatabaseMedication = new List<Medication>();
            DatabaseGroups = new List<DoctorsLetterGroup>();
        }

        public string Filepath { get; set; }

        public void DisattachMedication(Medication med)
        {
            if (DatabaseMedication.Contains(med))
            {
                DatabaseMedication.Remove(med);
                med.DisattachFromLetter(this);
            }
        }

        public void AddToGroup(DoctorsLetterGroup group)
        {
            if (!DatabaseGroups.Contains(group))
            {
                DatabaseGroups.Add(group);
                group.Add(this);
            }
        }

        public void RemoveFromGroup(DoctorsLetterGroup group)
        {
            if (DatabaseGroups.Contains(group))
            {
                DatabaseGroups.Remove(group);
                group.Remove(this);
            }
        }


        public void AttachMedication(Medication med)
        {
            if (!DatabaseMedication.Contains(med))
            {
                DatabaseMedication.Add(med);
                med.AttachToLetter(this);
            }
        }

        public void Delete()
        {
            while (DatabaseMedication.Any())
            {
                DisattachMedication(DatabaseMedication.First());
            }
            while (DatabaseGroups.Any())
            {
                RemoveFromGroup(DatabaseGroups.First());
            }
        }


        /// <see>Model.DataModelInterface.IDoctorsLetter#Diagnosis()</see>
        public string Diagnosis { get; set; }

        /// <see>Model.DataModelInterface.IDoctorsLetter#Doctor()</see>
        public IDoctor Doctor => DatabaseDoctor;

        /// <see>Model.DataModelInterface.IDoctorsLetter#Medication()</see>
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

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        /// <see>Model.DataModelInterface.IDoctorsLetter#Medication()</see>
        public List<Medication> DatabaseMedication { get; set; }

        [ManyToOne]
        public Doctor DatabaseDoctor { get; set; }

        [ForeignKey(typeof(Doctor))]
        public int DoctorID { get; set; }

        [ManyToMany(typeof(DoctorsLetterGroupDoctorsLetter), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<DoctorsLetterGroup> DatabaseGroups { get; set; }

        /// <see>Model.DataModelInterface.IDoctorsLetter#RemoveMedication(Model.DataModelInterface.IMedication)</see>
        public void RemoveMedication(IMedication med) => RemoveMedication(med.ToMedication());

        /// <see>Model.DataModelInterface.IDoctorsLetter#AttachMedication(Model.DataModelInterface.IMedication)</see>
        public void AttachMedication(IMedication med) => AttachMedication(med.ToMedication());

        /// <see>Model.DataModelInterface.IDoctorsLetter#AddToGroup(Model.DataModelInterface.IDoctorsLetterGroup)</see>
        public void AddToGroup(IDoctorsLetterGroup group) => AddToGroup(group.ToDoctorsLetterGroup());

        /// <see>Model.DataModelInterface.IDoctorsLetter#RemoveFromGroup(Model.DataModelInterface.IDoctorsLetterGroup)</see>
        public void RemoveFromGroup(IDoctorsLetterGroup group) => RemoveFromGroup(group.ToDoctorsLetterGroup());

        /// <see>Model.DataModelInterface.IDoctorsLetter#Groups()</see>
        public IList<IDoctorsLetterGroup> Groups
        {
            get
            {
                List<IDoctorsLetterGroup> list = new List<IDoctorsLetterGroup>();
                foreach(DoctorsLetterGroup group in DatabaseGroups)
                {
                    list.Add(group);
                }
                return list;
            }          
        }

        public bool Equals(DoctorsLetter other)
        {
            return ID.Equals(other.ID);
        }

        public override bool Equals(Object obj)
        {
            DoctorsLetter letter = obj as DoctorsLetter;
            return letter != null && Equals(letter);
        }

        public DoctorsLetter ToDoctorsLetter() => this;
    }
}

