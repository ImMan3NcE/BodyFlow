using AppTraining.MVVM.Model;
using AppTraining.MVVM.View;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using static Microsoft.Maui.ApplicationModel.Permissions;


namespace AppTraining.MVVM.ModelView
{
    [AddINotifyPropertyChangedInterface]
    public class TrainingViewModel
    {

        public List<Exercise> ListRoutineExercises { get; set; }
        public string routineName { get; set; }
        public string levelRoutine { get; set; }
        public string cyclesRoutine { get; set; }
        public string valueExercise { get; set; }


        public ICommand BackToRoutines => new Command(BackToViewRoutines);
        public ICommand CounterOfExercises => new Command(CountOfExercises);

        public string sourceFavorite { get; set; }
        public ICommand FavoriteRoutine => new Command(IsFavoriteRoutine);


        public ICommand OpenPopupTrainingViewInstr => new Command(DisplayPopup);

        #region StopWatch
        //stopwatch
        public string timeStopWatch { get; set; }
        public string sourceStopwatch { get; set; } = "buttonplay";
        private bool isRunning;
        private Stopwatch stopwatch = new Stopwatch();
        public ICommand OnStartStopStopwatch => new Command(StartStopCommand);
        public ICommand OnResetStopwatch => new Command(OnReset);
        // 
        #endregion


        //public event PropertyChangedEventHandler PropertyChanged;

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {

                _currentIndex = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentIndex)));
                CountOfExercises();
            }
        }
        public TrainingViewModel()
        {

            ListRoutineExercises = App.BaseRepo.ListRoutineExercises;
            timeStopWatch = "00:00:00";
            routineName = ListRoutineExercises.FirstOrDefault().RoutineName;
            cyclesRoutine = $"Do minimum {ListRoutineExercises.FirstOrDefault().Cycles} sets. (Rest: 3min)";
            levelRoutine = ListRoutineExercises.FirstOrDefault().Level;
            valueExercise = $"Exercise: {_currentIndex + 1}/{ListRoutineExercises.Count()}.";
            sourceFavorite = App.BaseRepo.FavoriteImgSource;
            

            
        }

        public void DisplayPopup()
        {
            App.Current.MainPage.ShowPopupAsync(new PopUpTrainingViewInstr());
        }

        private void BackToViewRoutines()
        {
            stopwatch.Stop();
            (Application.Current.MainPage as NavigationPage)?.PopAsync();
        }

        private void CountOfExercises()
        {
            //int counter = 1;
            valueExercise = $"Exercise: {_currentIndex + 1}/{ListRoutineExercises.Count()}";
        }

        private void IsFavoriteRoutine(object obj)
        {


            var routine = App.BaseRepo.GetOneRoutine(routineName);
            var toast = Toast.Make($"{routine.RoutineName} added to Favorites", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);

            if (routine.Favorite == true)
            {
                toast = Toast.Make($"{routine.RoutineName} removed from Favorites", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
            }


            App.BaseRepo.FavoriteTraining(routine);

            sourceFavorite = App.BaseRepo.FavoriteImgSource;

            toast.Show();
        }


        #region Stopwatch
        private void StartStopCommand()
        {
            isRunning = !isRunning;
            sourceStopwatch = isRunning ? "buttonstop" : "buttonplay";

            if (isRunning)
            {
                stopwatch.Start();
                //Device.StartTimer(TimeSpan.FromMilliseconds(10), UpdateTime);

                Application.Current.Dispatcher.StartTimer(TimeSpan.FromMilliseconds(10), UpdateTime);
            }
            else
            {
                stopwatch.Stop();
            }
        }


        private void OnReset()
        {
            isRunning = false;
            stopwatch.Stop();
            stopwatch.Reset();

            //isRunning = false;
            sourceStopwatch = "buttonplay";
            timeStopWatch = "00:00:00";


        }

        private bool UpdateTime()
        {
            if (isRunning)
            {
                TimeSpan elapsed = stopwatch.Elapsed;

                string minutes = elapsed.Minutes.ToString();
                string seconds = elapsed.Seconds.ToString();
                string milliseconds = elapsed.Milliseconds.ToString().PadLeft(3, '0').Substring(0, 2);

                if (elapsed.Minutes < 10 && elapsed.Seconds < 10)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        timeStopWatch = $"0{minutes}:0{seconds}:{milliseconds}";
                    });
                }
                else if (elapsed.Seconds < 10)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        timeStopWatch = $"{minutes}:0{seconds}:{milliseconds}";
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        timeStopWatch = $"0{minutes}:{seconds}:{milliseconds}";
                    });
                }

            }
            //else
            //{
            //    TimeSpan elapsed = stopwatch.Elapsed;
            //    string seconds = elapsed.Seconds.ToString();
            //    string milliseconds = elapsed.Milliseconds.ToString().PadLeft(3, '0').Substring(0, 2);
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        timeStopWatch = $"{seconds}:{milliseconds}";
            //    });
            //}


            return isRunning;
        }


        #endregion



    }
}
