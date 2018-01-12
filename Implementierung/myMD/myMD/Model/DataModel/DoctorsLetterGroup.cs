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

        IList<DoctorsLetter> letters;

		public void Add(DoctorsLetter letter)
		{
            if(!letters.Contains(letter))
            {
                letters.Add(letter);
                letter.AddToGroup(this);
            }
		}

		public void Remove(DoctorsLetter letter)
		{
            if (letters.Contains(letter))
            {
                letters.Remove(letter);
                letter.RemoveFromGroup(this);
            }
        }

        /// <see>Model.DataModelInterface.IDoctorsLetterGroup#GetAll()</see>
        public IList<IDoctorsLetter> DoctorsLetters => letters.Cast<IDoctorsLetter>().ToList();


        /// <see>Model.DataModelInterface.IDoctorsLetterGroup#Add(Model.DataModelInterface.IDoctorsLetter)</see>
        public void Add(IDoctorsLetter letter) => letter.AddToGroup(this);


        /// <see>Model.DataModelInterface.IDoctorsLetterGroup#Remove(Model.DataModelInterface.IDoctorsLetter)</see>
        public void Remove(IDoctorsLetter letter) => letter.RemoveFromGroup(this);


        /// <see>Model.DataModelInterface.IDoctorsLetterGroup#GetLastDate()</see>
        public DateTime LastDate => default(DateTime);

    }

}

