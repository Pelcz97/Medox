namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r eine Entit�t. Entit�ten sind Abstraktionen von medizinischen Daten oder Akteuren. 
    /// </summary>
	public interface IEntity
	{
        /// <summary>
        /// Der Name der Entit�t.
        /// </summary>
        /// <returns>Name der Entit�t</returns>
		string Name { get; set; }

        /// <summary>
        /// L�scht die Entit�t und l�st alle Assoziationen von ihr auf.
        /// </summary>
		void Delete();

	}

}

