using AppTraining.MVVM.Model;
using AppTraining.Repositories;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppTraining.MVVM.ModelView
{
    public class PopUpJournalViewModel
    {
        //public JournalViewModel JournalViewModel { get; set; }
        public WorkoutNote CurrentWorkoutNote { get; set; }
        public ICommand AddOrUpdateCommand => new Command(AddOrUpdateC);

        public PopUpJournalViewModel()
        {
            
            CurrentWorkoutNote = App.BaseRepo.WorkoutNoteToUpdate;
            if (CurrentWorkoutNote != null )
            {
                _workoutName = App.BaseRepo.WorkoutNoteToUpdate.WorkoutName;
                _workoutDate = DateTime.Parse(App.BaseRepo.WorkoutNoteToUpdate.WorkoutDate);
                _note = App.BaseRepo.WorkoutNoteToUpdate.Note;
                BtnText = "Update";
            }
            

        }


        private void AddOrUpdateC()
        {
            var toast = Toast.Make("Updated", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);

            

            if (CurrentWorkoutNote == null)
            {
                CurrentWorkoutNote = new WorkoutNote();
                toast = Toast.Make("Added", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
            }



            CurrentWorkoutNote.WorkoutName = _workoutName;
            CurrentWorkoutNote.WorkoutDate = _workoutDate.ToString("yyyy-MM-dd");
            CurrentWorkoutNote.Note = _note;
            App.BaseRepo.AddOrUpdate(CurrentWorkoutNote);

            CurrentWorkoutNote = null;
            toast.Show();

        }
        #region EntryInformations

        private string _workoutName;
        public string WorkoutNameNote
        {
            get => _workoutName;
            set
            {

                _workoutName = value;

            }
        }

        private DateTime _workoutDate = DateTime.Today;
        public DateTime WorkoutDateNote
        {
            get => _workoutDate;
            set
            {

                _workoutDate = value;

            }
        }
        private string _note;
        public string Note
        {
            get => _note;
            set
            {

                _note = value;

            }
        }

        private string _btnText = "Add";
        public string BtnText
        {
            get => _btnText;
            set
            {

                _btnText = value;

            }
        }

        #endregion
    }
}
