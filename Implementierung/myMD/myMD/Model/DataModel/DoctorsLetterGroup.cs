using myMD.ModelInterface.DataModelInterface;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// Diese Klasse implementiert die IDoctorsLetterGroup Schnittstelle und erweitert die abstrakte Data Klasse,
    /// um Arztbriefgruppen in einer SQLite-Datenbank speichern zu können.
    /// </summary>
    /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetter</see>
    /// <see>myMD.Model.DataModelInterface.Data</see>
    [Preserve(AllMembers = true)]
    public class DoctorsLetterGroup : Data, IDoctorsLetterGroup, IEquatable<DoctorsLetterGroup>
    {
        /// <summary>
        /// Initialisiert nötige Listen.
        /// </summary>
        public DoctorsLetterGroup()
        {
            DatabaseLetters = new List<DoctorsLetter>();
        }

        /// <summary>
        /// Die Arztbriefe, die in dieser Gruppe enthalten sind.
        /// Ein Arztbrief kann dabei in mehreren Arztbriefgruppen enthalten sein.
        /// Diese Relation wird in der separaten Klasse DoctorsLetterGroupDoctorsLetter gespeichert.
        /// Wird automatisch beim Lesen aus der Datenbank gesetzt.
        /// Sollte immer sortiert sein.
        /// </summary>
        /// <see>myMD.Model.DataModel.DoctorsLetterGroupDoctorsLetter</see>
        [ManyToMany(typeof(DoctorsLetterGroupDoctorsLetter), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<DoctorsLetter> DatabaseLetters { get; set; }

        /// <summary>
        /// Das Datum einer Arztbriefgruppe ist das Datum des ältesten in ihr enthaltenen Arztbriefs.
        /// Da die Arztbriefe in DatabaseLetters nach ihrem Datum sortiert sind, muss diese Information nicht explizit gespeichert werden.
        /// </summary>
        /// <see>myMD.Model.DataModel.Data#Date</see>
        /// <see>myMD.Model.DataModel.DoctorsLetterGroup#DatabaseLetters</see>
        public override DateTime Date => DatabaseLetters.Any() ? DatabaseLetters.First().Date : default(DateTime);

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetterGroup#DoctorsLetters</see>
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

        /// <summary>
        /// Das letzte Datum einer Arztbriefgruppe ist das Datum des neuesten in ihr enthaltenen Arztbriefs.
        /// Da die Arztbriefe in DatabaseLetters nach ihrem Datum sortiert sind, muss diese Information nicht explizit gespeichert werden.
        /// </summary>
        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetterGroup#GetLastDate()</see>
        /// <see>myMD.Model.DataModel.DoctorsLetterGroup#DatabaseLetters</see>
        public DateTime LastDate => DatabaseLetters.Any() ? DatabaseLetters.Last().Date : default(DateTime);

        /// <summary>
        /// Überladung für konkrete Arztbriefe.
        /// </summary>
        /// <see>myMD.Model.DataModel.DoctorsLetterGroup#Add(ModelInterface.DataModelInterface.IDoctorsLetter)</see>
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

        /// <see>myMD.Modelnterface.DataModelInterface.IDoctorsLetterGroup#Add(Model.DataModelInterface.IDoctorsLetter)</see>
        public void Add(IDoctorsLetter letter) => Add(letter.ToDoctorsLetter());

        /// <see>myMD.Model.DataModel.Entity#Delete()</see>
        public override void Delete()
        {
            while (DatabaseLetters.Any())
            {
                Remove(DatabaseLetters.First());
            }
        }

        /// <summary>
        /// Zwei Arztbriefgruppen sind genau dann gleich, wenn sie als Daten gleich sind und die in ihr enthaltenen Arztbriefe gleich sind.
        /// </summary>
        /// <see>System.IEquatable<T>#Equals(T)</see>
        public bool Equals(DoctorsLetterGroup other)
        {
            return base.Equals(other)
                && DatabaseLetters.SequenceEqual(other.DatabaseLetters);
        }

        /// <see>System.Object#Equals(System.Object)</see>
        public override bool Equals(Object obj)
        {
            DoctorsLetterGroup group = obj as DoctorsLetterGroup;
            return group != null && Equals(group);
        }

        /// <see>System.Object#GetHashCode()</see>
        public override int GetHashCode()
        {
            var hashCode = 2029785287;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<DoctorsLetter>>.Default.GetHashCode(DatabaseLetters);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + LastDate.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Überladung für konkrete Arztbriefe.
        /// </summary>
        /// <see>myMD.Model.DataModel.DoctorsLetterGroup#Remove(ModelInterface.DataModelInterface.IDoctorsLetter)</see>
        public void Remove(DoctorsLetter letter)
        {
            if (DatabaseLetters.Contains(letter))
            {
                DatabaseLetters.Remove(letter);
                letter.RemoveFromGroup(this);
            }
        }

        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetterGroup#Remove(Model.DataModelInterface.IDoctorsLetter)</see>
        public void Remove(IDoctorsLetter letter) => Remove(letter.ToDoctorsLetter());

        /// <summary>
        /// Da diese Klasse bereits den verlangten Rückgabetyp hab, ist keine Konvertierung nötig.
        /// </summary>
        /// <see>myMD.ModelInterface.DataModelInterface.IDoctorsLetterGroup#ToDoctorsLetterGroup()</see>
        public DoctorsLetterGroup ToDoctorsLetterGroup() => this;
    }
}