namespace Model.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r die Abstraktion eines Arztes, die die IEntity Schnittstelle erweitert.
    /// </summary>
	public interface IDoctor : IEntity
	{
        /// <summary>
        /// Gibt das Fachgebiet dieses Doktors zur�ck.
        /// </summary>
        /// <returns>Das Fachgebiet dieses Doktors</returns>
		string GetField();

        /// <summary>
        /// �ndert das Fachgebiet dieses Doktors.
        /// </summary>
        /// <param name="field">Neues Fachgebiet dieses Doktors</param>
		void SetField(string field);

	}

}

