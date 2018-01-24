using SQLiteNetExtensions.Attributes;
using Xamarin.Forms.Internals;

namespace myMD.Model.DataModel
{
    /// <summary>
    /// Diese Klasse wird von der SQLite-Net-Extensions Library benötigt um die bestehenden Beziehungen zwischen Arztrbriefen und Arztbriefgruppen speichern zu können.
    /// Abseits davon hat diese Klasse keinen Nutzen und sollte daher auch von sonst niemand instanziiert oder anderweitig verwendet werden.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DoctorsLetterGroupDoctorsLetter
    {
        /// <summary>
        /// Fremdschlüssel zum Arztbrief in dieser Relation.
        /// </summary>
        [ForeignKey(typeof(DoctorsLetter))]
        public int DoctorsLetterID { get; set; }

        /// <summary>
        /// Fremdschlüssel zur Arztbriefgruppe in dieser Relation.
        /// </summary>
        [ForeignKey(typeof(DoctorsLetterGroup))]
        public int DoctorsLetterGroupID { get; set; }
    }
}
