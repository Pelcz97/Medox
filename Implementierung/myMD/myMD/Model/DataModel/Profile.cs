using ModelInterface.DataModelInterface;
using myMD.Model.EntityObserver;
using System;
using System.Collections.Generic;

namespace myMD.Model.DataModel
{
	public class Profile : Entity, IProfile, IProfileObservable
	{
        public Profile()
        {
            observers = new List<IProfileObserver>();
        }

		private string firstName;

        private string lastName;

        private DateTime birthDate;

		private string insuranceNumber;

		private BloodType bloodType;

        private IList<IProfileObserver> observers;

        /// <see>Model.DataModelInterface.IProfile#InsuranceNumber</see>
        public string InsuranceNumber
        {
            get => insuranceNumber;
            set => insuranceNumber = value;
        }

        /// <see>Model.DataModelInterface.IProfile#LastName(string)</see>
        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        /// <see>Model.DataModelInterface.IProfile#BloodType</see>
        public BloodType BloodType
        {
            get => bloodType;
            set => bloodType = value;
        }

        /// <see>Model.DataModelInterface.IProfile#BirthDate()</see>
        public DateTime BirthDate
        {
            get => birthDate;
            set => birthDate = value;
        }

        /// <see>Model.DataModelInterface.IProfile#SetActive()</see>
        public void SetActive()
		{
            foreach (IProfileObserver obs in observers) obs.OnActivation(this);
		}

        /// <see>Model.EntityObserver.IProfileObservable#Subscribe(Model.EntityObserver.IProfileObserver)</see>
        public void Subscribe(IProfileObserver observer) => observers.Add(observer);

        /// <see>Model.EntityObserver.IProfileObservable#Unsubscribe(Model.EntityObserver.IProfileObserver)</see>
        public void Unsubscribe(IProfileObserver observer) => observers.Remove(observer);

    }

}

