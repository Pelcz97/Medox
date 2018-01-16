namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r die Abstraktion eines Arztes, die die IEntity Schnittstelle erweitert.
    /// </summary>
	public interface IDoctor : IEntity
	{
        /// <summary>
        /// Das Fachgebiet dieses Doktors.
        /// </summary>
		string Field { get; set; }
	}

}

