using SQLiteNetExtensions.Attributes;

namespace myMD.Model.DataModel
{
    public class DoctorsLetterGroupDoctorsLetter
    {
        [ForeignKey(typeof(DoctorsLetter))]
        public int DoctorsLetterID { get; set; }

        [ForeignKey(typeof(DoctorsLetterGroup))]
        public int DoctorsLetterGroupID { get; set; }
    }
}
