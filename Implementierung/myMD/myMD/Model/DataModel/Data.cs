using Model.DataModelInterface;
using System;

namespace Model.DataModel
{
	public abstract class Data : Entity, IEntity, IData
	{
		private DateTime date;

		/// <see>Model.DataModelInterface.IData#GetDate()</see>
		public DateTime GetDate()
		{
			return date;
		}


		/// <see>Model.DataModelInterface.IData#GetSensitivity()</see>
		public Sensitivity GetSensitivity()
		{
			return default(Sensitivity);
		}

		/// <see>Model.DataModelInterface.IData#SetSensitivity(Model.DataModelInterface.Sensitivity)</see>
		public void SetSensitivity(Sensitivity sensitivity)
		{

		}

	}

}

