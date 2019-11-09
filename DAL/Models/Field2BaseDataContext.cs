using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Field2BaseDataContext: DbContext
    {
        public DbSet<TaskEntry> TaskEntry { get; set; }
    }
}
