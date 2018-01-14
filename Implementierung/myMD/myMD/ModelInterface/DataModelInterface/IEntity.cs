using myMD.Model.DataModel;

namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für eine Entität. Entitäten sind Abstraktionen von medizinischen Daten oder Akteuren. 
    /// </summary>
	public interface IEntity
	{
        /// <summary>
        /// Der Name der Entität.
        /// </summary>
        /// <returns>Name der Entität</returns>
		string Name { get; set; }

        Entity ToEntity();
	}

}

