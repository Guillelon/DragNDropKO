using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TaskEntry
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        [NotMapped]
        public string DueDateFormated
        {
            get
            {
                return DueDate.ToShortDateString();
            }
        }
        public string Name { get; set; }
        public bool Completed { get; set; }

        public TaskEntry()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
