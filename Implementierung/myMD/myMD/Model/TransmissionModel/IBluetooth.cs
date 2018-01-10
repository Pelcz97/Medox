using zUtilities;
using Model.TransmissionModelInterface;

namespace Model.TransmissionModel
{
	public interface IBluetooth
	{
		IList<IDevice> scanForDevices();

		void send(string filePath);

		void cancelSend();

		boolean pair(IDevice device, String pin);

		boolean connect(IDevice device);

		File receive();

	}

}

