using Model.DataModelInterface;
using System.Collections.Generic;

namespace Model.ModelFacadeInterface
{
    /// <summary>
    /// Dies ist die Haupteinstiegsstelle für andere Subsysteme in das Model. 
    /// Als Ein- und Ausgabeparameter werden die restlichen Schnittstellen aus ModelInterface verwendet.
    /// Alle komplexeren Operationen, die nicht schon von diesen Schnittstellen übernommen werden, sind hier enthalten.
    /// </summary>
	public interface IModelFacade
	{
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IProfile CreateEmptyProfile();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IList<IDoctorsLetter> GetAllDoctorsLetters();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IList<IMedication> GetAllMedications();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IMedication CreateEmptyMedication();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IList<IDoctorsLetterGroup> GetAllGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IDoctorsLetterGroup CreateEmptyGroup();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="letter"></param>
		void SendLetter(IDoctorsLetter letter);

	}

}

