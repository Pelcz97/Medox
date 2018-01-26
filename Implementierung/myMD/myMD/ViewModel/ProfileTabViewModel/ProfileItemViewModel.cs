using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ProfileItemViewModel : OverallViewModel.OverallViewModel
    {

        public IProfile Profile { get; set; }
        public string LastName { 
            get => Profile.LastName; 
            set {
                Profile.LastName = value;
                Debug.WriteLine("Nachname: " + LastName);
            }  
        }
        
        public string Name { 
            get => Profile.Name; 
            set {
                Profile.Name = value;
                Debug.WriteLine("Vorname: " + Name);
            } 
        }

        public DateTime Birthdate { 
            get => Profile.BirthDate; 
            set {
                Profile.BirthDate = value; 
                Debug.WriteLine("Geburtstag: " + Birthdate);
            }
        }

        public string InsuranceNumber { 
            get => Profile.InsuranceNumber; 
            set {
                Profile.InsuranceNumber = value; 
                Debug.WriteLine("Versicherungsnummer: " + InsuranceNumber);
            }
        }

        public BloodType BloodType { 
            get => Profile.BloodType; 
            set => Profile.BloodType = value; 
        }

        public bool LastNameVisible { get; set; }
        public bool NameVisible { get; set; }
        public bool BirthdateVisible { get; set; }
        public bool InsuranceNumberVisible { get; set; }
        public bool BloodTypeVisible { get; set; }

        public ProfileItemViewModel()
        {
            Profile = ModelFacade.GetActiveProfile();
        }
    }
}
