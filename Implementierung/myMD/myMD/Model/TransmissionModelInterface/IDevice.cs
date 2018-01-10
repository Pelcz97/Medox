namespace Model.TransmissionModelInterface
{
	public interface IDevice
	{
		string getName();

		string getUuid();

		boolean connected();

		boolean paired();

	}

}

