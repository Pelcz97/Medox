using ModelInterface.DataModelInterface;
using System;

namespace myMD.Model.DataModel
{
	public abstract class Data : Entity, IEntity, IData
	{
		private DateTime date;

        private Sensitivity sensitivity;

        /// <see>ModelInterface.DataModelInterface.IData#Date</see>
        public DateTime Date => date;

        /// <see>ModelInterface.DataModelInterface.IData#Sensitivity</see>
        public Sensitivity Sensitivity { get => sensitivity; set => this.sensitivity = value; }
    }

}

