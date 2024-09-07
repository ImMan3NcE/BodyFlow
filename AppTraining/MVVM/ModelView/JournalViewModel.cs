using AppTraining.MVVM.Model;
using AppTraining.MVVM.View;
using AppTraining.Repositories;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppTraining.MVVM.ModelView
{
    [AddINotifyPropertyChangedInterface]
    public class JournalViewModel
    {


        public List<WorkoutNote> WorkoutNotes { get; set; }

        public bool valueSelected;
        public WorkoutNote CurrentWorkoutNote { get; set; }
        public ICommand RefreshClcVCommand => new Command(Refresh);
        public ICommand AddNewNoteCommand => new Command(AddNewNote);
        public ICommand UpdateNoteCommand => new Command(OpenPopUp);
        public ICommand DeleteNoteCommand => new Command(DeleteNote);
        
        public JournalViewModel()
        {
            Refresh();

        }

        

        public void Refresh()
        {
            WorkoutNotes = App.BaseRepo.GetAll2();
            
        }

        private void OpenPopUp()
        {
            
             App.Current.MainPage.ShowPopupAsync(new PopUpJournal());
        }
        private void DeleteNote()
        {
            var toast = Toast.Make("Deleted", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
            App.BaseRepo.Delete();
            Refresh();
            toast.Show();
        }

        private void AddNewNote()
        {
            ClearInfo();
            OpenPopUp();
           

        }

        private void ClearInfo()
        {
            App.BaseRepo.ExistWorkoutNote(null);
            
        }
        

        public Command SelectNote
        {
            get
            {
                return new Command((sender) =>
                {
                    
                        var element = sender as WorkoutNote;
                        App.BaseRepo.ExistWorkoutNote(element);

                });
            }
        }

        #region EntryInformations

       

        private string _btnText = "Add Item";
        public string BtnText
        {
            get => _btnText;
            set
            {

                _btnText = value;

            }
        }

        private bool _btnUpdateVisible = false;
        public bool BtnUpdateVisible
        {
            get => _btnUpdateVisible;
            set
            {
                _btnUpdateVisible = value;
            }
        }

        

        #endregion
    }
}
