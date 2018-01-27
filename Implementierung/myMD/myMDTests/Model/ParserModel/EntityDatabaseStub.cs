using myMD.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMD.ModelInterface.DataModelInterface;

namespace myMDTests.Model.ParserModel
{
    class EntityDatabaseStub : IEntityDatabase
    {
        public void Activate(IProfile profile)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public IProfile GetActiveProfile()
        {
            throw new NotImplementedException();
        }

        public IList<IDoctorsLetterGroup> GetAllDoctorsLetterGroups()
        {
            throw new NotImplementedException();
        }

        public IList<IDoctorsLetter> GetAllDoctorsLetters()
        {
            throw new NotImplementedException();
        }

        public IList<IMedication> GetAllMedications()
        {
            throw new NotImplementedException();
        }

        public IList<IProfile> GetAllProfiles()
        {
            throw new NotImplementedException();
        }

        public IDoctor GetDoctor(IDoctor doctor)
        {
            throw new NotImplementedException();
        }

        public IProfile GetProfile(IProfile profile)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
