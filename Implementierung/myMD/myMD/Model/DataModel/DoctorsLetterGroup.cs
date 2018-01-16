using ModelInterface.DataModelInterface;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;


namespace myMD.Model.DataModel
{
    public class DoctorsLetterGroup : Data, IDoctorsLetterGroup
    {
        public DoctorsLetterGroup()
        {
            DatabaseLetters = new List<DoctorsLetter>();
        }

        public void Add(DoctorsLetter letter)
        {
            if (!DatabaseLetters.Contains(letter))
            {
                if (DatabaseLetters.Any())
                {
                    int i = 0;
                    ///Suche nach passendem Index um die Liste sortiert zu halten
                    for (IEnumerator<DoctorsLetter> enumerator = DatabaseLetters.GetEnumerator(); 
                        enumerator.MoveNext() && letter.CompareTo(enumerator.Current) > 0; ++i) ;
                    DatabaseLetters.Insert(i, letter);
                }
                else
                {
                    DatabaseLetters.Add(letter);
                }
            }
        }

        public void Remove(DoctorsLetter letter)
        {
            if (DatabaseLetters.Contains(letter))
            {
                DatabaseLetters.Remove(letter);
                //letter.RemoveFromGroup(this);
            }
        }

        /// <see>Model.DataModel.Entity#Delete()</see>
        public void Delete()
        {
            while (DatabaseLetters.Any())
            {
                Remove(DatabaseLetters.First());
            }
        }

        [ManyToMany(typeof(DoctorsLetterGroupDoctorsLetter), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<DoctorsLetter> DatabaseLetters { get; set; }

        /// <see>ModelInterface.DataModelInterface.IDoctorsLetterGroup#DoctorsLetters</see>
        public IList<IDoctorsLetter> DoctorsLetters
        {
            get
            {
                List<IDoctorsLetter> list = new List<IDoctorsLetter>();
                foreach (DoctorsLetter letter in DatabaseLetters)
                {
                    list.Add(letter);
                }
                return list;
            }
        }


        /// <see>Modelnterface.DataModelInterface.IDoctorsLetterGroup#Add(Model.DataModelInterface.IDoctorsLetter)</see>
        public void Add(IDoctorsLetter letter) => Add(letter.ToDoctorsLetter());


        /// <see>ModelInterface.DataModelInterface.IDoctorsLetterGroup#Remove(Model.DataModelInterface.IDoctorsLetter)</see>
        public void Remove(IDoctorsLetter letter) => Remove(letter.ToDoctorsLetter());

        /// <see>Model.DataModel.Data#Date</see>
        public override DateTime Date => DatabaseLetters.Any() ? DatabaseLetters.First().Date : default(DateTime);

        /// <see>ModelInterface.DataModelInterface.IDoctorsLetterGroup#GetLastDate()</see>
        public DateTime LastDate => DatabaseLetters.Any() ? DatabaseLetters.Last().Date : default(DateTime);

        public bool Equals(DoctorsLetterGroup other)
        {
            return ID.Equals(other.ID);
        }

        public override bool Equals(Object obj)
        {
            DoctorsLetterGroup group = obj as DoctorsLetterGroup;
            return group != null && Equals(group);
        }

        public DoctorsLetterGroup ToDoctorsLetterGroup() => this;
    }

}

