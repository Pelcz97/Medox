using myMD.Model.DatabaseModel;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using System;
using System.Collections.Generic;

namespace myMDTests.Model.ParserModel
{
    internal class EntityDatabaseStub : IEntityDatabase
    {
        public DoctorsLetter Letter { get; private set; }

        public Profile Profile { get; private set; }

        public Doctor Doctor { get; private set; }

        public List<Medication> Meds { get; private set; } = new List<Medication>();

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

        public IDoctor GetDoctor(IDoctor doctor) => null;

        public IProfile GetProfile(IProfile profile)
        {
            Insert(profile);
            return profile;
        }

        public void Insert(IEntity entity)
        {
            if (entity is Doctor)
            {
                Doctor = (Doctor)entity;
            }
            else if (entity is DoctorsLetter)
            {
                Letter = (DoctorsLetter)entity;
            }
            else if (entity is Profile)
            {
                Profile = (Profile)entity;
            }
            else if (entity is Medication med)
            {
                if (!Meds.Contains(med))
                {
                    Meds.Add(med);
                }
            }
            ((Entity)entity).Profile = Profile;
        }

        public void Update(IEntity entity) => Insert(entity);
    }
}