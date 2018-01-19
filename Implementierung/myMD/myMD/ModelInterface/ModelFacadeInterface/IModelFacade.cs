using myMD.ModelInterface.DataModelInterface;
using System.Collections.Generic;
using myMD.Model.DataModel;

namespace myMD.ModelInterface.ModelFacadeInterface
{
    /// <summary>
    /// Dies ist die Haupteinstiegsstelle für andere Subsysteme in das Model. 
    /// Als Ein- und Ausgabeparameter werden die restlichen Schnittstellen aus ModelInterface verwendet.
    /// Alle komplexeren Operationen, die nicht schon von diesen Schnittstellen übernommen werden, sind hier enthalten.
    /// </summary>
	public interface IModelFacade
	{
        /// <summary>
        /// Legt ein neues Profil an, das noch keine Informationen und Daten enthält und gibt dieses zurück. 
        /// Wechselt außerdem das aktive Profil zu diesem Profil.
        /// </summary>
        /// <returns>Das neu erstellte Profil</returns>
		IProfile CreateEmptyProfile();

        /// <summary>
        /// Fordert alle Arztbriefe des aktiven Profils an und gibt diese zurück.
        /// </summary>
        /// <returns>Liste aller Arztbriefe des aktiven Profils</returns>
		IList<IDoctorsLetter> GetAllDoctorsLetters();

        /// <summary>
        /// Fordert alle Medikationen des aktiven Profils an und gibt diese zurück.
        /// </summary>
        /// <returns>Liste aller Medikationen des aktiven Profils</returns>
		IList<IMedication> GetAllMedications();

        /// <summary>
        /// Legt eine neue Medikation an, die noch keine Informationen enthält und gibt diese zurück.
        /// </summary>
        /// <returns>Die neu erstellte, leere Medikation</returns>
		IMedication CreateEmptyMedication();

        /// <summary>
        /// Fordert alle Arztbriefgruppen des aktiven Profils an und gibt diese zurück.
        /// </summary>
        /// <returns>Liste aller Arztbriefgruppen des aktiven Profils</returns>
		IList<IDoctorsLetterGroup> GetAllGroups();

        /// <summary>
        /// Legt eine neue Arztbriefgruppe an, die noch keine Arztbriefe enthält und gibt diese zurück.
        /// </summary>
        /// <returns>Die neu erstellte, leere Arztbriefgruppe</returns>
		IDoctorsLetterGroup CreateEmptyGroup();

        void Update(IEntity entity);

        void Delete(IEntity entity);

        void Activate(IProfile profile);

        /// <summary>
        /// Bereitet den gewünschten Arztbrief für die Datenübertragung vor und veranlasst diese anschließend.
        /// </summary>
        /// <param name="letter">Der zu sendende Arztbrief</param>
		void SendLetter(IDoctorsLetter letter);
	}

}

