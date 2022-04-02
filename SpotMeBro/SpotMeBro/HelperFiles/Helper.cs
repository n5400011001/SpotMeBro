using System;
using System.Collections.ObjectModel;
using SpotMeBro.HelperFiles;
using System.IO;

namespace WorkoutWatcher
{
    public static class Helper
    {
        public static void addToLog(string text)
        {
            string logPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (!File.Exists(logPath + "\\logFile.txt"))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(logPath + "\\logFile.txt"))
                {
                    sw.WriteLine(text + DateTime.Now);
                }
            }
        }

        public static ObservableCollection<WorkoutModel> Workouts = new ObservableCollection<WorkoutModel>();
        public static ObservableCollection<ExerciseModel> Exercises = new ObservableCollection<ExerciseModel>();
      
    }
}
