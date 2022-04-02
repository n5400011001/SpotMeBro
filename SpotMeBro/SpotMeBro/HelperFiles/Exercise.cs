using SQLite;

namespace SpotMeBro.HelperFiles
{
    public class ExerciseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ExerciseName { get; set; }

        public string ExerciseBodyPart { get; set; }    


    }
}
