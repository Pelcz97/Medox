using myMD.Model.DataModel;

namespace myMD.ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für die Abstraktion eines Arztes, die die IEntity Schnittstelle erweitert.
    /// </summary>
	public interface IDoctor : IEntity
    {
        /// <summary>
        /// Das Fachgebiet dieses Doktors.
        /// </summary>
		string Field { get; set; }

        /// <summary>
        /// Konvertiert die Schnittstelle zu ihrer Implementierung
        /// </summary>
        /// <returns>Konkreter Arzt</returns>
        Doctor ToDoctor();
    }
}