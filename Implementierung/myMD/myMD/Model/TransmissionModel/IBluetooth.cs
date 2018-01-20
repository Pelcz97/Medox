using myMD.ModelInterface.TransmissionModelInterface;
using System.Collections.Generic;

namespace myMD.Model.TransmissionModel
{
	public interface IBluetooth
	{
		IList<IDevice> scanForDevices();

		void send(string filePath);

		void cancelSend();

		bool pair(IDevice device, string pin);

		bool connect(IDevice device);

		string receive();

	}

}

