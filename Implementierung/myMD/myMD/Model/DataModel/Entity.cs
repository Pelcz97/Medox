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
    /// Dabei dient die Klasse nur als Strukturelement für andere Klassen, die eine Vorlage für eine SQLite-Datenbank-Tabelle,
    /// unter Verwendung der SQLite-Net-Extensions Library, sein sollen und ist daher abstrakt.
    /// Diese Klassen sollten Entity erweitern.
    /// </summary>
    /// <see>myMD.ModelInterface.DataModelInterface.IEntity</see>
    [Preserve(AllMembers = true)]
    public abstract class Entity : IEntity, IEquatable<Entity>
    {
        /// <summary>
        /// Primärer Schlüssel um Zeilen in der korrespondierenden Tabelle zu identifizieren.
        /// Wird automatisch beim Einfügen in die Datenbank gesetzt und sollte von sonst niemandem verändert werden.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IEntity#Name</see>
        public string Name { get; set; }

        /// <summary>
        /// Das Nutzerprofil dem diese Entität eindeutig zugeordnet werden kann.
        /// Ein Nutzerprofil kann dabei mehrere Entitäten enthalten.
        /// Wird automatisch beim Lesen aus der Datenbank gesetzt.
        /// </summary>
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Profile Profile { get; set; }

        /// <summary>
        /// Fremdschlüssel zum Profil dieser Entität für die Datenbank.
        /// </summary>
        [ForeignKey(typeof(Profile))]
        public int ProfileID { get; set; }

        /// <summary>
        /// Führt die nötigen Aktionen aus um diese Entität zu löschen.
        /// Sollte immer zusammen mit einer Löschung aus der Datenbank aufgerufen werden.
        /// Sollte von Unterklassen überschrieben werden, bei denen zusätzliche Aktionen zur Löschung nötig sind
        /// </summary>
        public virtual void Delete() { }

        /// <summary>
        /// Zwei Entitäten sind genau dann gleich, wenn ihre Id, ihr Name und ihr Profil gleich sind.
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
        /// Da diese Klasse bereits den verlangten Rückgabetyp hab, ist keine Konvertierung nötig.
        /// </summary>
        /// <see>myMD.ModelInterface.DataModelInterface.IEntity#ToEntity()</see>
        public Entity ToEntity() => this;
    }
}