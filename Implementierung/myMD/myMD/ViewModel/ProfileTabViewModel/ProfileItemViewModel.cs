﻿using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    /// <summary>
    /// Profile item view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfileItemViewModel : OverallViewModel.OverallViewModel
    {
        /// <summary>
        /// Das Profil des ProfileItemViewModels
        /// </summary>
        /// <value>The profile.</value>
        public IProfile Profile { get; set; }

        /// <summary>
        /// Der Nachname des Profils.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { 
            get => Profile.LastName; 
            set { Profile.LastName = value; }  
        }

        /// <summary>
        /// Der Vorname des Profils.
        /// </summary>
        /// <value>The name.</value>
        public string Name { 
            get => Profile.Name; 
            set { Profile.Name = value; } 
        }

        /// <summary>
        /// Das Geburtsdatum des Profils.
        /// </summary>
        /// <value>The birthdate.</value>
        public DateTime Birthdate { 
            get => Profile.BirthDate; 
            set { Profile.BirthDate = value; }
        }

        /// <summary>
        /// Die Versicherungsnummer des Profils.
        /// </summary>
        /// <value>The insurance number.</value>
        public string InsuranceNumber { 
            get => Profile.InsuranceNumber; 
            set { Profile.InsuranceNumber = value; }
        }

        /// <summary>
        /// Die Blutgruppe des Profils.
        /// </summary>
        /// <value>The type of the blood.</value>
        public BloodType BloodType { 
            get => Profile.BloodType; 
            set => Profile.BloodType = value; 
        }

        /// <summary>
        /// Erzeugt ein ProfilItemViewModel und setzt das Profil auf das momentan aktive Profil.
        /// </summary>
        public ProfileItemViewModel()
        {
            Profile = ModelFacade.GetActiveProfile();
        }
    }
}
