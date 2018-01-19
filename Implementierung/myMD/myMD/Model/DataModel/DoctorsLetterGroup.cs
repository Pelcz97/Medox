using ModelInterface.DataModelInterface;
using SQLiteNetExtensions.Attributes;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// 
    /// </summary>
    public class DoctorsLetterGroup : Data, IDoctorsLetterGroup, IEquatable<DoctorsLetterGroup>
    {
        /// <summary>
        /// 
        /// </summary>
        public DoctorsLetterGroup()
        {
            DatabaseLetters = new List<DoctorsLetter>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="letter"></param>
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
                    letter.AddToGroup(this);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="letter"></param>
        public void Remove(DoctorsLetter letter)
        {
            if (DatabaseLetters.Contains(letter))
            {
                DatabaseLetters.Remove(letter);
                letter.RemoveFromGroup(this);
            }
        }

        /// <see>Model.DataModel.Entity#Delete()</see>
        public override void Delete()
        {
            while (DatabaseLetters.Any())
            {
                Remove(DatabaseLetters.First());
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// Diese Information wird nicht explizit gespeichert und kann daher von der Datenbank ignoriert werden.
        /// </summary>
        /// <see>Model.DataModel.Data#Date</see>
        [Ignore]      
        public override DateTime Date => DatabaseLetters.Any() ? DatabaseLetters.First().Date : default(DateTime);

        /// <summary>
        /// Diese Information wird nicht explizit gespeichert und kann daher von der Datenbank ignoriert werden.
        /// </summary>
        /// <see>ModelInterface.DataModelInterface.IDoctorsLetterGroup#GetLastDate()</see>
        [Ignore]
        public DateTime LastDate => DatabaseLetters.Any() ? DatabaseLetters.Last().Date : default(DateTime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DoctorsLetterGroup other)
        {
            return base.Equals(other)
                && DatabaseLetters.SequenceEqual(other.DatabaseLetters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            DoctorsLetterGroup group = obj as DoctorsLetterGroup;
            return group != null && Equals(group);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DoctorsLetterGroup ToDoctorsLetterGroup() => this;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 2029785287;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<DoctorsLetter>>.Default.GetHashCode(DatabaseLetters);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + LastDate.GetHashCode();
            return hashCode;
        }
    }

}

