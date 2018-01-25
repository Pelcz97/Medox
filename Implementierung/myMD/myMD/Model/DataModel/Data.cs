using myMD.ModelInterface.DataModelInterface;
using System;
using Xamarin.Forms.Internals;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// Diese Klasse implementiert die IData Schnittstelle und erweitert die abstrakte Enitity Klasse,
    /// um Information über medizinische Daten in einer SQLite-Datenbank speichern zu können.
    /// Dabei ist diese Klasse nur ein Strukturelement, dass von anderen Klassen erweitert werden sollte,
    /// um medizinische Daten in einer SQLite-Datenbank speichern zu können und daher abstrakt.
    /// </summary>
    /// <see>myMD.ModelInterface.DataModelInterface.IData</see>
    /// <see>myMD.Model.DataModelInterface.Entity</see>
    [Preserve(AllMembers = true)]
    public abstract class Data : Entity, IEntity, IData, IComparable<Data>, IEquatable<Data>
    {
        /// <see>myMD.ModelInterface.DataModelInterface.IData#Date</see>
        public virtual DateTime Date { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IData#Sensitivity</see>
        public Sensitivity Sensitivity { get; set; }

        /// <summary>
        /// Daten werden basierend auf ihrem Datum verglichen.
        /// </summary>
        /// <see>System.IComparable#CompareTo()</see>
        public int CompareTo(Data other)
        {
            return Date.CompareTo(other.Date);
        }

        /// <summary>
        /// Zwei Daten sind genau dann gleich, wenn sie als Entitäten gleich sind, ihre Sensitivitätsstufe übereinstimmt
        /// und ihr Datum gleich ist.
        /// </summary>
        /// <see>System.IEquatable<T>#Equals(T)</see>
        public bool Equals(Data other)
        {
            return base.Equals(other)
                && Sensitivity.Equals(other.Sensitivity)
                && Date.Equals(other.Date);
        }

        /// <see>System.Object#Equals(System.Object)</see>
        public override bool Equals(object obj) => Equals(obj as Data);

        /// <see>System.Object#GetHashCode()</see>
        public override int GetHashCode()
        {
            var hashCode = 914629901;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + Sensitivity.GetHashCode();
            return hashCode;
        }
    }
}