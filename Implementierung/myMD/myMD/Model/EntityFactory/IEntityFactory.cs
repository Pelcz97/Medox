using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.EntityFactory
{
    /// <summary>
    /// 
    /// </summary>
	public interface IEntityFactory
	{
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IMedication CreateEmptyMedication();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IDoctorsLetterGroup CreateEmptyGroup();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IProfile CreateEmptyProfile();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		IDoctorsLetter CreateEmptyDoctorsLetter();

	}

}

