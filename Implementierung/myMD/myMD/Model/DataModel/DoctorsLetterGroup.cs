using ModelInterface.DataModelInterface;
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
        private readonly IList<DoctorsLetter> letters;

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
                letter.AddToGroup(this);
                Updated();
            }
        }

        public void Remove(DoctorsLetter letter)
        {
            if (letters.Contains(letter))
            {
                letters.Remove(letter);
                letter.RemoveFromGroup(this);
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

        /// <see>ModelInterface.DataModelInterface.IDoctorsLetterGroup#DoctorsLetters</see>
        public IList<IDoctorsLetter> DoctorsLetters => letters.Cast<IDoctorsLetter>().ToList();


        /// <see>Modelnterface.DataModelInterface.IDoctorsLetterGroup#Add(Model.DataModelInterface.IDoctorsLetter)</see>
        public void Add(IDoctorsLetter letter) => letter.AddToGroup(this);


        /// <see>ModelInterface.DataModelInterface.IDoctorsLetterGroup#Remove(Model.DataModelInterface.IDoctorsLetter)</see>
        public void Remove(IDoctorsLetter letter) => letter.RemoveFromGroup(this);

        /// <see>Model.DataModel.Data#Date</see>
        public override DateTime Date => letters.First().Date;

        /// <see>ModelInterface.DataModelInterface.IDoctorsLetterGroup#GetLastDate()</see>
        public DateTime LastDate => letters.Last().Date;

    }

}

