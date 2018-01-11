namespace Model.TransmissionModelInterface
{
	public interface IDevice
	{
		string getName();

		string getUuid();

		bool connected();

		bool paired();

	}

}

