using myMD.Model.DataModel;
using ModelInterface.DataModelInterface;
using System.Collections.Generic;

namespace myMD.Model.DataModel
{
    public class Doctor : Entity, IDoctor
    {
        private string field;
    
        /// <see>Model.DataModelInterface.IDoctor#Field</see>
        public string Field
        {
            get => field;
            set
            {
                field = value;
                Updated();
            }
        }
    }

}

