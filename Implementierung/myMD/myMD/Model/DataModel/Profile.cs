using ModelInterface.DataModelInterface;
using myMD.Model.EntityObserver;
using System;
using System.Collections.Generic;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// 
    /// </summary>
	public class Profile : Entity, IProfile, IEquatable<Profile>
    {
        /// <summary>
        /// 
        /// </summary>
        public Profile()
        {
            Profile = this;
            ProfileID = ID;
        }

        /// <see>Model.DataModelInterface.IProfile#InsuranceNumber</see>
        public string InsuranceNumber { get; set; }

        /// <see>Model.DataModelInterface.IProfile#LastName(string)</see>
        public string LastName { get; set; }

        /// <see>Model.DataModelInterface.IProfile#BloodType</see>
        public BloodType BloodType { get; set; }

        /// <see>Model.DataModelInterface.IProfile#BirthDate()</see>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => Equals(obj as Profile);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public Profile ToProfile() => this;
    }

}

