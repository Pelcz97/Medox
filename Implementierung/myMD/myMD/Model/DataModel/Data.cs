using ModelInterface.DataModelInterface;
using System;

namespace myMD.Model.DataModel
{
    public abstract class Data : Entity, IEntity, IData, IComparable<Data>
    {
        /// <see>ModelInterface.DataModelInterface.IData#Date</see>
        public virtual DateTime Date { get; set; }

        /// <see>ModelInterface.DataModelInterface.IData#Sensitivity</see>
        public Sensitivity Sensitivity { get; set; }

        public int CompareTo(Data other)
        {
            return Date.CompareTo(other.Date);
        }
    }
}

