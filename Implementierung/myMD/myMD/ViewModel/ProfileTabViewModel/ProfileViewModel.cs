using myMD.Model.ModelFacade;
using myMD.ModelInterface.DataModelInterface;
using System;
namespace myMD.ViewModel.ProfileTabViewModel
{
    public class ProfileViewModel
    {

        IProfile Profil;
        public string Name { get => this.Profil.LastName; }
        public string Vorname { get => this.Profil.Name; }
        public DateTime Geburtstag { get => this.Profil.BirthDate; }
        public string Versicherungsnummer { get => this.Profil.InsuranceNumber; }
        public BloodType Blutgruppe { get => this.Profil.BloodType; }

        public bool NameVisible { get => this.Profil.LastName != null; }
        public bool VorNameVisible { get => this.Profil.Name != null; }
        public bool GeburtstagVisible { get => this.Profil.BirthDate != null; }
        public bool VersicherungsnummerVisible { get => this.Profil.InsuranceNumber != null; }
        public bool BlutgruppeVisible { get => this.Profil.BloodType != BloodType.Empty; }

        public ProfileViewModel()
        {
            //Kann erst auskommentiert werden, wenn eine Methode, um ein aktives Profil aus der DB zu holen, da ist.
            //Profil = ModelFacade.GetActiveProfil();
        }
    }
}
