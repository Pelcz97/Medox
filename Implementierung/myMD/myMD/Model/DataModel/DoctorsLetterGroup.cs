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
            letters = new List<DoctorsLetter>();
        }

        /// <summary>
        /// Liste aller Arztbriefe in dieser Gruppe.
        /// Diese Liste sollte immer sortiert sein (Sortierung folgt aus IComparable Implementierung von DoctorsLetter)
        /// </summary>
        private List<DoctorsLetter> letters;

        public void Add(DoctorsLetter letter)
        {
            if (!letters.Contains(letter))
            {
                if (letters.Any())
                {
                    int i = 0;
                    ///Suche nach passendem Index um die Liste sortiert zu halten
                    for (IEnumerator<DoctorsLetter> enumerator = letters.GetEnumerator(); 
                        enumerator.MoveNext() && letter.CompareTo(enumerator.Current) > 0; ++i) ;
                    letters.Insert(i, letter);
                }
                else
                {
                    letters.Add(letter);
                }
                //letter.AddToGroup(this);
                Updated();
            }
        }

        public void Remove(DoctorsLetter letter)
        {
            if (letters.Contains(letter))
            {
                letters.Remove(letter);
                //letter.RemoveFromGroup(this);
                Updated();
            }
        }

        /// <see>Model.DataModel.Entity#Delete()</see>
        public override void Delete()
        {
            base.Delete();
            while (letters.Any())
            {
                Remove(letters.First());
            }
        }

        [ManyToMany(typeof(DoctorsLetterGroupDoctorsLetter), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<DoctorsLetter> DatabaseLetters
        {
            get => letters;
            set => letters = value;
        }

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
        public override DateTime Date => letters.Any() ? letters.First().Date : default(DateTime);

        /// <see>ModelInterface.DataModelInterface.IDoctorsLetterGroup#GetLastDate()</see>
        public DateTime LastDate => letters.Any() ? letters.Last().Date : default(DateTime);

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

