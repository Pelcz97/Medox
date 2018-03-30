using System;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.MedicationInformation
{
    public class InteractionPair
    {

        public string med1 { get; set; }
        public string med2 { get; set; }

        public string InteractionDescription { get; set; }

        public InteractionPair(string med1, string med2, string description){
            this.med1 = med1;
            this.med2 = med2;
            InteractionDescription = description;
        }
    }
}
