using AppTraining.MVVM.Model;
using AppTraining.MVVM.View;
using AppTraining.Repositories;
using CommunityToolkit.Maui.Behaviors;

namespace AppTraining
{
    public partial class App : Application
    {
        //dotnet publish -c Release -f:net7.0-android
        //public static ExerciseRepositories ExerciseRepo { get; private set; }
        //public static WorkoutNoteRepository WorkoutNoteRepo { get; private set; }
        public static BaseRepository BaseRepo { get; private set; }
        //public App(ExerciseRepositories repo, WorkoutNoteRepository repo2)
        public App(BaseRepository repo) 
        {
            
            InitializeComponent();

            //ExerciseRepo = repo;
            //WorkoutNoteRepo = repo2;
            BaseRepo = repo;

            //// generowanie treningu do listy przy starcie aplikacji
            //App.ExerciseRepo.LoadExercises();


            MainPage =new NavigationPage( new MainView());
        }
    }
}
