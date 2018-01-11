using Model.DataModel;
using ModelInterface.DataModelInterface;
using System;

namespace Model.DataModel
{
	public class Medication : Data, IMedication
	{
		private DateTime endDate;

		private int frequency;

		private Profile profile;

		public void AttachToLetter(DoctorsLetter letter)
		{

		}


		/// <see>Model.DataModelInterface.IMedication#SetDate(zUtilities.DateTime)</see>
		public void SetDate(DateTime date)
		{

		}


		/// <see>Model.DataModelInterface.IMedication#GetFrequency()</see>
		public int GetFrequency()
		{
			return 0;
		}


		/// <see>Model.DataModelInterface.IMedication#SetFrequency(int)</see>
		public void SetFrequency(int freq)
		{

		}


		/// <see>Model.DataModelInterface.IMedication#GetInterval()</see>
		public Interval GetInterval()
		{
			return default(Interval);
		}


		/// <see>Model.DataModelInterface.IMedication#SetInterval(Model.DataModelInterface.Interval)</see>
		public void SetInterval(Interval interval)
		{

		}


		/// <see>Model.DataModelInterface.IMedication#GetEndDate()</see>
		public DateTime GetEndDate()
		{
			return endDate;
		}


		/// <see>Model.DataModelInterface.IMedication#SetEndDate(zUtilities.DateTime)</see>
		public void SetEndDate(DateTime date)
		{

		}


		/// <see>Model.DataModelInterface.IMedication#DisattachFromLetter()</see>
		public void DisattachFromLetter()
		{

		}


		/// <see>Model.DataModelInterface.IMedication#AttachToLetter(Model.DataModelInterface.IDoctorsLetter)</see>
		public void AttachToLetter(IDoctorsLetter letter)
		{

		}

	}

}

