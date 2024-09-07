using AppTraining.MVVM.Model;
using AppTraining.MVVM.View;
using CommunityToolkit.Maui.Alerts;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppTraining.MVVM.ModelView
{
    [AddINotifyPropertyChangedInterface]
    public class RoutinesViewModel
    {
        public List<Routine> RoutinesList { get; set; }
        public List<string> TypeOfExercises { get; set; }

        public string sourceFav { get; set; }
        public ICommand FavoriteRoutine => new Command(IsFavoriteRoutine);


        public ICommand SelectedFilter => new Command(FilterRoutines);
        public ICommand GenerateTraining => new Command(GenerateTrainingCommand);


        public RoutinesViewModel()
        {
            Refresh();
            
            TypeOfExercises = new List<string> { "#abs", "#cardio","#legs","#pull", "#push" };

        }

        public void Refresh()
        {
            RoutinesList = App.BaseRepo.GetAllRoutines();

        }

        private  void GenerateTrainingCommand(object obj)
        {
            

            var routine = obj as Routine;

            App.BaseRepo.GenerateOneTraining(routine);

            
            (Application.Current.MainPage as NavigationPage)?.PushAsync(new TrainingView());
        }

        private void FilterRoutines(object obj)
        {
            
            if (obj as string == "All")
            {
                
                RoutinesList = App.BaseRepo.GetAllRoutines();
            }
            else
            {
                RoutinesList = App.BaseRepo.FilterRoutine(obj as string);
                
            }
            
            
        }


        private async void IsFavoriteRoutine(object obj)
        {

            
            var routine = obj as Routine;
            var toast = Toast.Make($"{routine.RoutineName} added to Favorites", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);

            if (routine.Favorite == true)
            {
                toast = Toast.Make($"{routine.RoutineName} removed from Favorites", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
            }

            App.BaseRepo.FavoriteTraining(routine);
            Refresh();

            
            
            
            await toast.Show();

            //Application.Current.MainPage.DisplayAlert("Favorites", "Added to Favorites", "cnc");
            Console.WriteLine("IsFavorite Done");

           
        }


    }
}
