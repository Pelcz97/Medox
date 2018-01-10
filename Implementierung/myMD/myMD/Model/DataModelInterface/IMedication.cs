using Model.DataModelInterface;
using zUtilities;

namespace Model.DataModelInterface
{
	public interface IMedication : IData
	{
		void SetDate(DateTime date);

		int GetFrequency();

		void SetFrequency(int freq);

		Interval GetInterval();

		void SetInterval(Interval interval);

		DateTime GetEndDate();

		void SetEndDate(DateTime date);

		void DisattachFromLetter();

		void AttachToLetter(IDoctorsLetter letter);

	}

}

