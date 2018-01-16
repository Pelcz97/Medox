using ModelInterface.DataModelInterface;
using myMD.Model.EntityObserver;
using System;
using System.Collections.Generic;

namespace myMD.Model.DataModel
{
	public class Profile : Entity, IProfile
    {
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

    }

}

