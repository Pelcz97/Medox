using myMD.ModelInterface.TransmissionModelInterface;

namespace myMD.Model.TransmissionModel
{
    /// <summary>
    /// Klasse zur Modelierung eines anderen Gerätes
    /// </summary>
    public class Device : IDevice
    {
        /// <summary>
        /// Methode, um zu pruefen, ob man mit einem anderen Gerät verbunden ist
        /// </summary>
        /// <returns>true, falls man verbunden ist. Sonst false</returns>
        public bool connected()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Methode um den Namen eines anderen Gerätes zu bekommen
        /// </summary>
        /// <returns>Name des anderen Gerätes</returns>
        public string getName()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Methode um die UUID eines anderen Gerätes zu bekommen
        /// </summary>
        /// <returns>UUID des anderen Gerätes</returns>
        public string getUuid()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Methode, um zu pruefen, ob man mit einem anderen Gerät gekoppelt ist
        /// </summary>
        /// <returns>true, falls man gekoppelt ist. Sonst false</returns>
        public bool paired()
        {
            throw new System.NotImplementedException();
        }
    }

}

