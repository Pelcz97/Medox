using ModelInterface.DataModelInterface;
using myMD.Model.EntityObserver;
using SQLite;
using System.Collections.Generic;
using System.Collections;
using SQLiteNetExtensions.Attributes;

namespace myMD.Model.DataModel
{
	public abstract class Entity : IEntity
    {
        public Entity()
        {
            observers = new List<IEntityObserver>();
        }

        private IList<IEntityObserver> observers;

        /// <see>ModelInterface.DataModelInterface.IEntity#Name</see>
        public string Name { get; set; }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ManyToOne]
        public Profile Profile { get; set; }

        [ForeignKey(typeof(Profile))]
        public int ProfileID { get; set; }

        public Entity ToEntity() => this;
    }

}

