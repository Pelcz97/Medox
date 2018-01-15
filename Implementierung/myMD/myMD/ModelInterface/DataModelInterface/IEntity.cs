using myMD.Model.DataModel;

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

        Entity ToEntity();
	}

}

