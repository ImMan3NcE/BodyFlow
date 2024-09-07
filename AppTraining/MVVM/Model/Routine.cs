using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTraining.MVVM.Model
{
    [Table("Routines")]

    public class Routine
    {
        [PrimaryKey]
        public string RoutineId { get; set; }
        public string RoutineName { get; set; }
        
        public string Level { get; set; }
        public string Base { get; set; }
        public bool Favorite { get; set; }
        public string ImgFavorite { get; set; }
        public string TypeOfExercises { get; set; }

    }
}
