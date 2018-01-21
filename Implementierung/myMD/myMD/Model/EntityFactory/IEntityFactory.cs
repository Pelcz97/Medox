using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.EntityFactory
{
    /// <summary>
    /// Fabrik zum instanziieren von Objekten aus dem DataModelInterface Paket.
    /// </summary>
    /// <seealso>myMD.ModelInterface.DataModelInterface</seealso>
	public interface IEntityFactory
    {
        /// <summary>
        /// Erstellt eine neue, leere Medikation.
        /// </summary>
        /// <returns>Die neue Medikation</returns>
		IMedication CreateEmptyMedication();

        /// <summary>
        /// Erstellt eine neue, leere Arztbriefgruppe.
        /// </summary>
        /// <returns>Die neue Arztbriefgruppe</returns>
		IDoctorsLetterGroup CreateEmptyGroup();

        /// <summary>
        /// Erstellt ein neues, leeres Profil.
        /// </summary>
        /// <returns>Das neue Profil</returns>
		IProfile CreateEmptyProfile();

        /// <summary>
        /// Erstellt einen neuen, leeren Arztbrief.
        /// </summary>
        /// <returns>Der neue Arztbrief</returns>
		IDoctorsLetter CreateEmptyDoctorsLetter();
    }
}