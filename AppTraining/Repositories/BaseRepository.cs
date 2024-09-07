using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AppTraining.MVVM.Model;
using Microsoft.Maui.Controls;
using SQLite;

namespace AppTraining.Repositories
{
    public class BaseRepository
    {

        SQLiteConnection connection;
        public string StatusMessage { get; set; }
        public string FavoriteImgSource { get; set; }

        public List<Exercise> ListRoutineExercises { get; set; }
        public WorkoutNote WorkoutNoteToUpdate { get; set; }

        public BaseRepository()
        {

            connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);

            
            connection.CreateTable<WorkoutNote>();
            connection.CreateTable<Exercise>();
            connection.CreateTable<Routine>();

            

            if (connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Exercises") == 0)
            {
                 LoadExercises();
            }

            //if (connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Exercises") != 0)
            //{
            //    Console.WriteLine ($"Jest {connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Exercises")}");
            //    Console.WriteLine ($"Jest rutyn {connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Routines")}");
            //}
            //else
            //{
            //    Console.WriteLine($"Nie zaladowano {connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Exercises")}");
            //}
            

        }

        #region WorkoutNote
        public void AddOrUpdate(WorkoutNote workoutNote)
        {
            int result = 0;
            try
            {
                if (workoutNote.Id != 0)
                {
                    result = connection.Update(workoutNote);
                    StatusMessage = $"{result} row(s) updated";
                }
                else
                {
                    result = connection.Insert(workoutNote);
                    StatusMessage = $"{result} row(s) added";
                }


            }
            catch (Exception ex)
            {

                StatusMessage = $"Error: {ex.Message}";
            }
        }


        public List<WorkoutNote> GetAll()
        {
            try
            {
                return connection.Table<WorkoutNote>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public List<WorkoutNote> GetAll2()
        {
            try
            {
                //name of table in WorkoutNote model
                return connection.Query<WorkoutNote>("SELECT * FROM WorkoutNotes ORDER BY workoutDate DESC").ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public WorkoutNote Get(int id)
        {
            try
            {
                return connection.Table<WorkoutNote>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public void Delete()
        {
            try
            {
                //var workoutNote = Get(workoutNoteId);
                connection.Delete(WorkoutNoteToUpdate);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public void ExistWorkoutNote(WorkoutNote currrentWorkoutNote)
        {
            WorkoutNoteToUpdate = currrentWorkoutNote;
        }
        #endregion


        #region Exercise

        public async Task LoadExercises()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("TrainingData.json");
            using var reader = new StreamReader(stream);

            var contents = reader.ReadToEnd();

            try
            {
                var exercises = JsonSerializer.Deserialize<List<Exercise>>(contents);


                foreach (var exercise in exercises)
                {
                    connection.InsertOrReplace(exercise);
                }

                //connection.InsertOrReplace(exercises);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd deserializacji JSON: {ex.Message}");
            }

            try
            {
                var routines = JsonSerializer.Deserialize<List<Routine>>(contents);
                

                foreach (var routine in routines)
                {
                    routine.Favorite = false;
                    routine.ImgFavorite = "starunfav";
                    connection.InsertOrReplace(routine);
                }
                GennerateTypeOfExercises();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        


        #endregion

        #region Routine

        public void GennerateTypeOfExercises()
        {
            //string[] typeOfExercise= new string[] { };
            List<string> typeOfExercise = new List<string>();
            string str;
            List<Routine> routines = connection.Query<Routine>("SELECT * FROM Routines").ToList();

            foreach(var routine in routines)
            {
                List<Exercise> exercises = connection.Query<Exercise>($"SELECT * FROM Exercises WHERE RoutineId = '{routine.RoutineId}' ").ToList();
                foreach (var exercise in exercises)
                {
                    if (exercise.ExerciseId.Contains( "push"))
                    {
                        str = "#push";
                    }
                    else if (exercise.ExerciseId.Contains("pull"))
                    {
                        str = "#pull";
                    }
                    else if (exercise.ExerciseId.Contains("leg"))
                    {
                        str = "#legs";
                    }
                    else if (exercise.ExerciseId.Contains("abs"))
                    {
                        str = "#abs";
                    }
                    else
                    {
                        str = "#cardio";
                    }

                    if (!typeOfExercise.Contains(str))
                    {
                        typeOfExercise.Add(str);
                    }
                    //Console.WriteLine($"{exercise.ExerciseId} {exercise.ExerciseName} {exercise.RoutineId}");
                    //Console.WriteLine($"{typeOfExercise}");

                }
                typeOfExercise.Sort();
                routine.TypeOfExercises = string.Join("",typeOfExercise);
                Console.WriteLine($"{string.Join("", typeOfExercise)}");
                connection.Update(routine);
                typeOfExercise = new List<string>();
                Console.WriteLine($"{routine.RoutineId} {routine.TypeOfExercises}");
                
            }
        }



        public List<Routine> GetAllRoutines()
        {
            try
            {
                //name of table in WorkoutNote model
                return connection.Query<Routine>("SELECT * FROM Routines ORDER BY Favorite DESC, RoutineName").ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public List<Routine> FilterRoutine(string filter)
        {

            try
            {
                //name of table in WorkoutNote model
                return connection.Query<Routine>($"SELECT * FROM Routines WHERE Level = '{filter}' OR TypeOfExercises LIKE '%{filter}%'").ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                Console.WriteLine(StatusMessage);
            }
            return null;

        }

       
        public void GenerateOneTraining(Routine routine)
        {
            //string routineId = routine.RoutineId
            FavoriteImgSource = routine.ImgFavorite;
            ListRoutineExercises = connection.Query<Exercise>($"SELECT * FROM Exercises WHERE RoutineId = '{routine.RoutineId}'").ToList();
            
        }

        public void FavoriteTraining(Routine routine)
        {
            if ( routine.Favorite == false)
            {
                routine.Favorite = true;
                routine.ImgFavorite = "starfav";
                FavoriteImgSource = "starfav";
                connection.Update(routine);
                Console.WriteLine("dodano do ulubionych");

            }
            else
            {
                routine.Favorite = false;
                routine.ImgFavorite = "starunfav";
                FavoriteImgSource = "starunfav";
                connection.Update(routine);
                Console.WriteLine("usunięto z ulubionych");
            }
        }

        public Routine GetOneRoutine(string routineName)
        {
            return connection.Query<Routine>($"SELECT * FROM Routines WHERE RoutineName = '{routineName}'").FirstOrDefault();
            

        }


        #endregion

    }


}

