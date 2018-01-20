using System.Collections.Generic;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.DatabaseModel
{
    /// <summary>
    /// 
    /// </summary>
	public interface IEntityDatabase
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
		void Insert(IEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(IEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(IEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        void Activate(IProfile profile);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IProfile> GetAllProfiles();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IDoctorsLetter> GetAllDoctorsLetters();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IDoctorsLetterGroup> GetAllDoctorsLetterGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IMedication> GetAllMedications();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        IProfile GetProfile(IProfile profile);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDoctor GetDoctor(IDoctor doctor);
    }

}

