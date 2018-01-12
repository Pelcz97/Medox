using ModelInterface.DataModelInterface;
using myMD.Model.EntityObserver;
using SQLite;
using System.Collections.Generic;
using System.Collections;

namespace myMD.Model.DataModel
{
	public abstract class Entity : IEntity, IObservable<IEntityObserver>
	{
        public Entity()
        {
            observers = new List<IEntityObserver>();
        }

        private string name;

        private int id;

        private IList<IEntityObserver> observers;

        /// <see>ModelInterface.DataModelInterface.IEntity#Name</see>
        public string Name {
            get => name;
            set
            {
                name = value;
                Updated();
            }
        }

        [PrimaryKey, AutoIncrement]
        public int ID {
            get => id;
            set
            {
                id = value;
                Updated();
            }
        }

        /// <see>ModelInterface.DataModelInterface.IEntity#Delete()</see>
        public virtual void Delete() => Deleted();

        /// <see>Model.EntityObserver.IEntityObservable#Subscribe(Model.EntityObserver.IEntityObserver)</see>
        public void Subscribe(IEntityObserver observer) => this.observers.Add(observer);

        /// <see>Model.EntityObserver.IEntityObservable#Unsubscribe(Model.EntityObserver.IEntityObserver)</see>
        public void Unsubscribe(IEntityObserver observer) => this.observers.Remove(observer);

        protected void Updated()
        {
            foreach (IEntityObserver obs in observers) obs.OnUpdate(this);
        }

        protected void Deleted()
        {
            foreach (IEntityObserver obs in observers) obs.OnDeletion(this);
        }
    }

}

