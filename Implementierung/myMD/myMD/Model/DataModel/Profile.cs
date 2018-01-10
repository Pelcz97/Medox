using Model.DataModel;
using Model.DataModelInterface;
using Model.EntityObserver;
using zUtilities;

namespace Model.DataModel
{
	public class Profile : Entity, IProfile, IProfileObservable
	{
		private string firstName;

		private DateTime birthDate;

		private string insuranceNumber;

		private BloodType bloodType;


		/// <see>Model.DataModelInterface.IProfile#GetInsuranceNumber()</see>
		public string GetInsuranceNumber()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IProfile#SetInsuranceNumber(string)</see>
		public void SetInsuranceNumber(string number)
		{

		}


		/// <see>Model.DataModelInterface.IProfile#GetLastName()</see>
		public string GetLastName()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IProfile#SetLastName(string)</see>
		public void SetLastName(string name)
		{

		}


		/// <see>Model.DataModelInterface.IProfile#GetBloodType()</see>
		public BloodType GetBloodType()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IProfile#SetBloodType(Model.DataModelInterface.BloodType)</see>
		public void SetBloodType(BloodType bloodType)
		{

		}


		/// <see>Model.DataModelInterface.IProfile#GetBirthDate()</see>
		public DateTime GetBirthDate()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IProfile#SetBirthDate(zUtilities.DateTime)</see>
		public void SetBirthDate(DateTime date)
		{

		}


		/// <see>Model.DataModelInterface.IProfile#SetActive()</see>
		public void SetActive()
		{

		}


		/// <see>Model.EntityObserver.IProfileObservable#Subscribe(Model.EntityObserver.IProfileObserver)</see>
		public void Subscribe(IProfileObserver observer)
		{

		}


		/// <see>Model.EntityObserver.IProfileObservable#Unsubscribe(Model.EntityObserver.IProfileObserver)</see>
		public void Unsubscribe(IProfileObserver observer)
		{

		}

	}

}

