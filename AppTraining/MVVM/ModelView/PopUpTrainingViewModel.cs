using AppTraining.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTraining.MVVM.ModelView
{
    public class  PopUpTrainingViewModel
    {
        public Exercise Exercise { get; set; }
        public string cycles {  get; set; }
        public string cycles2 {  get; set; }
        
        public PopUpTrainingViewModel()
        {
            Exercise = App.BaseRepo.ListRoutineExercises.FirstOrDefault();

            cycles = $"Do minimum {Exercise.Cycles} sets.";
            cycles2 = $"- Perform at least {Exercise.Cycles} sets of each exercise.";

            
        }
    }
}
