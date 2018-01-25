using myMD.ModelInterface.DataModelInterface;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// Die Entity Klasse implementiert die IEntity-Schnittstelle.
    /// Dabei dient die Klasse nur als Strukturelement f�r andere Klassen, die eine Vorlage f�r eine SQLite-Datenbank-Tabelle,
    /// unter Verwendung der SQLite-Net-Extensions Library, sein sollen und ist daher abstrakt.
    /// Diese Klassen sollten Entity erweitern.
    /// </summary>
    /// <see>myMD.ModelInterface.DataModelInterface.IEntity</see>
    [Preserve(AllMembers = true)]
    public abstract class Entity : IEntity, IEquatable<Entity>
    {
        /// <summary>
        /// Prim�rer Schl�ssel um Zeilen in der korrespondierenden Tabelle zu identifizieren.
        /// Wird automatisch beim Einf�gen in die Datenbank gesetzt und sollte von sonst niemandem ver�ndert werden.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IEntity#Name</see>
        public string Name { get; set; }

        /// <summary>
        /// Das Nutzerprofil dem diese Entit�t eindeutig zugeordnet werden kann.
        /// Ein Nutzerprofil kann dabei mehrere Entit�ten enthalten.
        /// Wird automatisch beim Lesen aus der Datenbank gesetzt.
        /// </summary>
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Profile Profile { get; set; }

        /// <summary>
        /// Fremdschl�ssel zum Profil dieser Entit�t f�r die Datenbank.
        /// </summary>
        [ForeignKey(typeof(Profile))]
        public int ProfileID { get; set; }

        /// <summary>
        /// F�hrt die n�tigen Aktionen aus um diese Entit�t zu l�schen.
        /// Sollte immer zusammen mit einer L�schung aus der Datenbank aufgerufen werden.
        /// Sollte von Unterklassen �berschrieben werden, bei denen zus�tzliche Aktionen zur L�schung n�tig sind
        /// </summary>
        public virtual void Delete() { }

        /// <summary>
        /// Zwei Entit�ten sind genau dann gleich, wenn ihre Id, ihr Name und ihr Profil gleich sind.
        /// </summary>
        /// <see>System.IEquatable<T>#Equals(T)</see>
        public bool Equals(Entity other)
        {
            return other != null
                && (ID == other.ID || ID.Equals(other.ID))
                && (Name == other.Name || Name.Equals(other.Name))
                && (Profile == other.Profile || Profile.Equals(other.Profile));
        }

        /// <see>System.Object#Equals(System.Object)</see>
        public override bool Equals(object obj) => Equals(obj as Entity);

        /// <see>System.Object#GetHashCode()</see>
        public override int GetHashCode()
        {
            var hashCode = 262302548;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Profile>.Default.GetHashCode(Profile);
            return hashCode;
        }

        /// <summary>
        /// Da diese Klasse bereits den verlangten R�ckgabetyp hab, ist keine Konvertierung n�tig.
        /// </summary>
        /// <see>myMD.ModelInterface.DataModelInterface.IEntity#ToEntity()</see>
        public Entity ToEntity() => this;
    }
}