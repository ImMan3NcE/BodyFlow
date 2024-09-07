using SQLite;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTraining.MVVM.Model
{
    [Table("WorkoutNotes")]
    public class WorkoutNote
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("routineName")]
        public string WorkoutName {  get; set; }
        [Column("workoutDate")]
        public string WorkoutDate {  get; set; }
        [MaxLength(200)]
        public string Note { get; set; }

    }
}
