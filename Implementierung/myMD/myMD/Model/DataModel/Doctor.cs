using Model.DataModel;
using Model.DataModelInterface;
using System.Collections.Generic;

namespace Model.DataModel
{
	public class Doctor : Entity, IDoctor
	{
		private string field;

		private ICollection<Profile> profile;


		/// <see>Model.DataModelInterface.IDoctor#GetField()</see>
		public string GetField()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IDoctor#SetField(string)</see>
		public void SetField(string field)
		{

		}

	}

}

