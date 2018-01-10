using Model.DataModelInterface;
using zUtilities;

namespace Model.DataModelInterface
{
	public interface IDoctorsLetterGroup : IData
	{
		IList<IDoctorsLetter> GetAll();

		void Add(IDoctorsLetter letter);

		void Remove(IDoctorsLetter letter);

		DateTime GetLastDate();

	}

}

