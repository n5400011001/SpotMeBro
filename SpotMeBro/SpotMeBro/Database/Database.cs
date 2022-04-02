using System;
using System.IO;
using System.Collections.Generic;
using SpotMeBro.HelperFiles;
using SQLite;
using System.Linq;

namespace SpotMeBro.Database
{
    public class DatabaseClass
    {
        public static void InitializeTables()
        {
            var x = CreateConnection();
            using (x)
            {
                x.CreateTable<ExerciseModel>();
                x.CreateTable<WorkoutModel>();
                x.CreateTable<LoginModel>();
                saveAdminCreds(new LoginModel { username = "admin", usersStoredHash = "794D6BFA925A7750744F22704718E4C9" });
                saveAdminCreds(new LoginModel { username = "admin", usersStoredHash = "2c103f2c4ed1e59c0b4e2e01821770fa" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "BENCH PRESS", ExerciseBodyPart = "CHEST" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "DUMBBELL PRESS", ExerciseBodyPart = "CHEST" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "MILITARY PRESS", ExerciseBodyPart = "CHEST" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "DECLINE PRESS", ExerciseBodyPart = "CHEST" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "MACHINE PRESS", ExerciseBodyPart = "CHEST" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "LANDMINES", ExerciseBodyPart = "BACK" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "WIDE GRIP LAT PULLDOWN", ExerciseBodyPart = "BACK" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "CLOSE GRIP LAT PULLDOWN", ExerciseBodyPart = "BACK" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "CABLE ROWS", ExerciseBodyPart = "BACK" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "DUMBBELL ROW", ExerciseBodyPart = "BACK" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "WEIGHTED BACK EXTENSIONS", ExerciseBodyPart = "BACK" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "SUMO-STANCE QUATS", ExerciseBodyPart = "GLUTS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "NARROW-STANCE QUATS", ExerciseBodyPart = "QUADS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "CALF RAISES", ExerciseBodyPart = "CALF" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "ROMANIAN DEAD LIFTS", ExerciseBodyPart = "LEGS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "QUAD EXTENSIONS", ExerciseBodyPart = "QUADS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "LEG CURLS", ExerciseBodyPart = "GLUTS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "HIP THRUSTS", ExerciseBodyPart = "QUADS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "BICEP CURLS", ExerciseBodyPart = "BICEPS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "21's", ExerciseBodyPart = "BICEPS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "SKULL CRUSHERS", ExerciseBodyPart = "TRICEPS" });
                saveCurrentExercise(new ExerciseModel { ExerciseName = "TRICEP DIPS", ExerciseBodyPart = "TRICEPS" });
            }
        }

        static SQLiteConnection CreateConnection()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SPOTMEBRO.db");
            var connection = new SQLiteConnection(dbPath);
            return connection;
        }

        public static List<ExerciseModel> getAllExercises()
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Table<ExerciseModel>().ToList();
            }
        }

        public static List<WorkoutModel> getAllWorkouts()
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Table<WorkoutModel>().ToList();
            }
        }

        public static int saveCurrentExercise(ExerciseModel exercise)
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Insert(exercise);
            }
        }
        public static int saveAdminCreds(LoginModel login)
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Insert(login);
            }
        }

        public static int saveCurrentWorkoutSet(WorkoutModel workout)
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Insert(workout);
            }
        }

        public static bool duplicateExercise(string newExerciseName)
        {
            var db = CreateConnection();
            bool isItADuplicate = false;
            ExerciseModel model = db.FindWithQuery<ExerciseModel>("SELECT * FROM ExerciseModel WHERE ExerciseName = ?", newExerciseName.ToUpper());
            if (model == null)
            {
                isItADuplicate = false; //Ok to record new exercise, there are no duplicate entries
                return isItADuplicate;
            }
            else
            {
                isItADuplicate = true;//There is a duplicate record, dont record.
                return isItADuplicate;
            }
        }

        public static List<WorkoutModel> getAllSetsToAWorkout(DateTime date, string exerciseName)
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Query<WorkoutModel>("SELECT * FROM WorkoutModel WHERE Date = '" + date + "' AND ExerciseName = '" + exerciseName + "'").ToList();
                //return db.Table<WorkoutModel>().ToList();
            }
        }

        public static void clearAllExercises()
        {
            var db = CreateConnection();
            using (db)
            {
                db.Query<ExerciseModel>("DELETE FROM ExerciseModel");
            }
        }

        public static void clearAllWorkouts()
        {
            var db = CreateConnection();
            using (db)
            {
                db.Query<WorkoutModel>("DELETE FROM WorkoutModel");
            }
        }

        public static List<WorkoutModel> getExercisesDone(DateTime date)
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Query<WorkoutModel>("SELECT DISTINCT ExerciseName FROM WorkoutModel WHERE Date = ?", date.ToString());
            }
        }

        public static List<WorkoutModel> getSetsDoneForExerciseReview(DateTime date, string exerciseName)
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Query<WorkoutModel>("SELECT * from WorkoutModel WHERE Date = ? AND ExerciseName = ?", date.ToString(), exerciseName);
            }
        }

        public static List<WorkoutModel> getListOfPastExercises()
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Query <WorkoutModel> ("SELECT DISTINCT ExerciseName FROM WorkoutModel");
            }
        }

        public static List<WorkoutModel> getExerciseFromAllTime(string exerciseName)
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Query<WorkoutModel>("SELECT * FROM WorkoutModel WHERE ExerciseName = ?", exerciseName);
            }
        }
        public static List<LoginModel> lookForUserHash(string userHash)
        {
            var db = CreateConnection();
            using (db)
            {
                return db.Query<LoginModel>("SELECT username, usersStoredHash FROM LoginModel WHERE usersStoredHash = ?", userHash);
            }
        }

        public static void updateExerciseNameInWorkoutTable(string oldExerciseName, string newExerciseName)
        {
            var db = CreateConnection();
            using (db)
            {
                db.Query<WorkoutModel>("UPDATE WorkoutModel SET ExerciseName = '" + newExerciseName + "' WHERE ExerciseName = '" + oldExerciseName + "'");
            }
        }
        public static void deleteRowInExerciseModel(string name)
        {
            var db = CreateConnection();
            db.Query<ExerciseModel>("DELETE FROM ExerciseModel WHERE ExerciseName = ?", name);
        }
    }
}