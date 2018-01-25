using myMD.ModelInterface.DataModelInterface;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// Diese Klasse implementiert die IProfile Schnittstelle und erweitert die abstrakte Enitity Klasse,
    /// um Information über einen Nutzer in einer SQLite-Datenbank speichern zu können
    /// </summary>
    /// <see>myMD.ModelInterface.DataModelInterface.IProfile</see>
    /// <see>myMD.Model.DataModel.Entity</see>
    [Preserve(AllMembers = true)]
    public class Profile : Entity, IProfile, IEquatable<Profile>
    {
        /// <summary>
        /// Jeder Entität soll eindeutig einem Nutzerprofile zugeordnet werden können.
        /// Das Profil eines Profils, sollte dabei natürlich immer das Profil selbst sein.
        /// Dies wird hier initialisiert.
        /// </summary>
        public Profile()
        {
            Profile = this;
        }

        /// <see>myMD.ModelInterface.DataModelInterface.IProfile#BirthDate()</see>
        public DateTime BirthDate { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IProfile#BloodType</see>
        public BloodType BloodType { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IProfile#InsuranceNumber</see>
        public string InsuranceNumber { get; set; }

        /// <see>myMD.ModelInterface.DataModelInterface.IProfile#LastName(string)</see>
        public string LastName { get; set; }

        /// <summary>
        /// Zwei Profile sind genau dann gleich, wenn ihr Name, Nachname, Geburtsdatum, ihre Blutgruppe, ID und Versicherungsnummer gleich sind.
        /// </summary>
        /// <see>System.IEquatable<T>#Equals(T)</see>
        public bool Equals(Profile other)
        {
            return other != null
                && ID.Equals(other.ID)
                && Name.Equals(other.Name)
                && InsuranceNumber.Equals(other.InsuranceNumber)
                && LastName.Equals(other.LastName)
                && BloodType.Equals(other.BloodType)
                && BirthDate.Equals(other.BirthDate);
        }

        /// <see>System.Object#Equals(System.Object)</see>
        public override bool Equals(object obj) => Equals(obj as Profile);

        /// <see>System.Object#GetHashCode()</see>
        public override int GetHashCode()
        {
            var hashCode = -1813595351;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(InsuranceNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + BloodType.GetHashCode();
            hashCode = hashCode * -1521134295 + BirthDate.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Da diese Klasse bereits den verlangten Rückgabetyp hab, ist keine Konvertierung nötig.
        /// </summary>
        /// <see>myMD.ModelInterface.DataModelInterface.IProfile#ToProfile()</see>
        public Profile ToProfile() => this;
    }
}