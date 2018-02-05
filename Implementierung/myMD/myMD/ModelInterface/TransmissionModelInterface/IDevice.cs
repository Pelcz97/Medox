namespace myMD.ModelInterface.TransmissionModelInterface
{
    /// <summary>
    /// Schnittstelle, die ein anderes Gerät implementieren muss
    /// </summary>
	public interface IDevice
	{
        /// <summary>
        /// Methode, um zu pruefen, ob man mit einem anderen Gerät verbunden ist
        /// </summary>
        /// <returns>true, falls man verbunden ist. Sonst false</returns>
        bool connected();

        /// <summary>
        /// Methode um den Namen eines anderen Gerätes zu bekommen
        /// </summary>
        /// <returns>Name des anderen Gerätes</returns>
        string getName();

        /// <summary>
        /// Methode um die UUID eines anderen Gerätes zu bekommen
        /// </summary>
        /// <returns>UUID des anderen Gerätes</returns>
        string getUuid();

        /// <summary>
        /// Methode, um zu pruefen, ob man mit einem anderen Gerät gekoppelt ist
        /// </summary>
        /// <returns>true, falls man gekoppelt ist. Sonst false</returns>
        bool paired();

    }

}

