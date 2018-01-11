namespace Model.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für die Abstraktion eines Arztes, die die IEntity Schnittstelle erweitert.
    /// </summary>
	public interface IDoctor : IEntity
	{
        /// <summary>
        /// Gibt das Fachgebiet dieses Doktors zurück.
        /// </summary>
        /// <returns>Das Fachgebiet dieses Doktors</returns>
		string GetField();

        /// <summary>
        /// Ändert das Fachgebiet dieses Doktors.
        /// </summary>
        /// <param name="field">Neues Fachgebiet dieses Doktors</param>
		void SetField(string field);

	}

}

