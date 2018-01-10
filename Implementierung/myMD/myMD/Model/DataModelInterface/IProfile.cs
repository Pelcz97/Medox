using Model.DataModelInterface;
using zUtilities;

namespace Model.DataModelInterface
{
	public interface IProfile : IEntity
	{
		string GetInsuranceNumber();

		void SetInsuranceNumber(string number);

		string GetLastName();

		void SetLastName(string name);

		BloodType GetBloodType();

		void SetBloodType(BloodType bloodType);

		DateTime GetBirthDate();

		void SetBirthDate(DateTime date);

		void SetActive();

	}

}

