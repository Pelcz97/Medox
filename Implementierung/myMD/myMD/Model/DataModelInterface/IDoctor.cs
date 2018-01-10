using Model.DataModelInterface;

namespace Model.DataModelInterface
{
	public interface IDoctor : IEntity
	{
		string GetField();

		void SetField(string field);

	}

}

