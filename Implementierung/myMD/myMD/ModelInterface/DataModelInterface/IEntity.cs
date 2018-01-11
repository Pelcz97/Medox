namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für eine Entität. Entitäten sind Abstraktionen von medizinischen Daten oder Akteuren. 
    /// </summary>
	public interface IEntity
	{
        /// <summary>
        /// Gibt den Namen der Entität zurück.
        /// </summary>
        /// <returns>Name der Entität</returns>
		string GetName();

        /// <summary>
        /// Ändert den Namen der Entität.
        /// </summary>
        /// <param name="name">Neuer Name der Entität</param>
		void SetName(string name);

        /// <summary>
        /// Löscht die Entität und löst alle Assoziationen von ihr auf.
        /// </summary>
		void Delete();

	}

}

