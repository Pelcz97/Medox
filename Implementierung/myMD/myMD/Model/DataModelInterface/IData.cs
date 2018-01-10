using Model.DataModelInterface;
using zUtilities;

namespace Model.DataModelInterface
{
	public interface IData : IEntity
	{
		DateTime GetDate();

		Sensitivity GetSensitivity();

		void SetSensitivity(Sensitivity sensitivity);

	}

}

