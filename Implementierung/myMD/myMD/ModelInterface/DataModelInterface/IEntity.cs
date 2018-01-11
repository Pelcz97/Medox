namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r eine Entit�t. Entit�ten sind Abstraktionen von medizinischen Daten oder Akteuren. 
    /// </summary>
	public interface IEntity
	{
        /// <summary>
        /// Gibt den Namen der Entit�t zur�ck.
        /// </summary>
        /// <returns>Name der Entit�t</returns>
		string GetName();

        /// <summary>
        /// �ndert den Namen der Entit�t.
        /// </summary>
        /// <param name="name">Neuer Name der Entit�t</param>
		void SetName(string name);

        /// <summary>
        /// L�scht die Entit�t und l�st alle Assoziationen von ihr auf.
        /// </summary>
		void Delete();

	}

}

