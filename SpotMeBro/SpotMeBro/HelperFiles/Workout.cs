using SQLite;

namespace SpotMeBro.HelperFiles
{
    public class WorkoutModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Date { get; set; }
        public string ExerciseName { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }


    }
}
