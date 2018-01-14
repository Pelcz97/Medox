using ModelInterface.DataModelInterface;
using System;

namespace myMD.Model.DataModel
{
    public abstract class Data : Entity, IEntity, IData, IComparable<Data>
    {

        private DateTime date;

        private Sensitivity sensitivity;

        /// <see>ModelInterface.DataModelInterface.IData#Date</see>
        public virtual DateTime Date
        {
            get => date;
            set => date = value;
        }

        /// <see>ModelInterface.DataModelInterface.IData#Sensitivity</see>
        public Sensitivity Sensitivity
        {
            get => sensitivity;
            set
            {
                sensitivity = value;
                Updated();
            }
        }

        public int CompareTo(Data other)
        {
            return Date.CompareTo(other.Date);
        }
    }
}

