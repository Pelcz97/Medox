namespace myMD.ModelInterface.TransmissionModelInterface
{
	public interface IDevice
	{
		string getName();

		string getUuid();

		bool connected();

		bool paired();

	}

}

