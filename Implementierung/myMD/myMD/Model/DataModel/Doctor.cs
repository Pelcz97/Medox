using myMD.ModelInterface.DataModelInterface;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// Diese Klasse implementiert die IDoctor Schnittstelle und erweitert die abstrakte Enitity Klasse,
    /// um Information über einen Arzt in einer SQLite-Datenbank speichern zu können
    /// </summary>
    /// <see>myMD.ModelInterface.DataModelInterface.IDoctor</see>
    /// <see>myMD.Model.DataModel.Entity</see>
    [Preserve(AllMembers = true)]
    public class Doctor : Entity, IDoctor, IEquatable<Doctor>
    {
        /// <see>myMD.ModelInterface.DataModelInterface.IDoctor#Field</see>
        public string Field { get; set; }

        /// <summary>
        /// Zwei Doktoren sind genau dann gleich sie als Entitäten gleich sind und ihr Spezialgebiet gleich ist.
        /// </summary>
        /// <see>System.IEquatable<T>#Equals(T)</see>
        public bool Equals(Doctor other)
        {
            return base.Equals(other)
                && Field.Equals(other.Field);
        }

        /// <see>System.Object#Equals(System.Object)</see>
        public override bool Equals(object obj) => Equals(obj as Doctor);

        /// <see>System.Object#GetHashCode()</see>
        public override int GetHashCode()
        {
            return 1998067999 + EqualityComparer<string>.Default.GetHashCode(Field);
        }

        public Doctor ToDoctor() => this;
    }
}