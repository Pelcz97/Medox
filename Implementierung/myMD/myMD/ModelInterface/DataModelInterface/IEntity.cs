using myMD.Model.DataModel;

namespace myMD.ModelInterface.DataModelInterface
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
        /// Konvertiert die Schnittstelle zu ihrer Implementierung
        /// </summary>
        /// <returns>Konkrete Entit�t</returns>
        Entity ToEntity();
    }
}