using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Text.Json;

namespace AppTraining.MVVM.Model
{
    [Table("Exercises")]
    public class Exercise
    {

        
        public string ExerciseId { get; set; }
        //[Indexed, MaxLength(30)]
        public string ExerciseName { get; set; }

        public string Quantity { get; set; }
        public string Unit { get; set; }
        public int Cycles { get; set; }
        //[MaxLength(30)]
        public string RoutineName { get; set; }
        public string RoutineId { get; set; }
        //public string NameOfImg1 { get; set; }
        //public string NameOfImg2 { get; set; }
        public string Level { get; set; }
        public string Base { get; set; }
    }
}
