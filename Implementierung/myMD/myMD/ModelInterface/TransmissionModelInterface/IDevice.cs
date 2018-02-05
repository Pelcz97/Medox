namespace myMD.ModelInterface.TransmissionModelInterface
{
    /// <summary>
    /// Schnittstelle, die ein anderes Ger�t implementieren muss
    /// </summary>
	public interface IDevice
	{
        /// <summary>
        /// Methode, um zu pruefen, ob man mit einem anderen Ger�t verbunden ist
        /// </summary>
        /// <returns>true, falls man verbunden ist. Sonst false</returns>
        bool connected();

        /// <summary>
        /// Methode um den Namen eines anderen Ger�tes zu bekommen
        /// </summary>
        /// <returns>Name des anderen Ger�tes</returns>
        string getName();

        /// <summary>
        /// Methode um die UUID eines anderen Ger�tes zu bekommen
        /// </summary>
        /// <returns>UUID des anderen Ger�tes</returns>
        string getUuid();

        /// <summary>
        /// Methode, um zu pruefen, ob man mit einem anderen Ger�t gekoppelt ist
        /// </summary>
        /// <returns>true, falls man gekoppelt ist. Sonst false</returns>
        bool paired();

    }

}

