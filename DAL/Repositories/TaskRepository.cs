using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TaskRepository
    {
        private Field2BaseDataContext _context;

        public TaskRepository()
        {
            _context = new Field2BaseDataContext();
        }

        public TaskEntry Add(TaskEntry model)
        {
            var nextOrder = 1;
            if (_context.TaskEntry.Any())
                nextOrder = _context.TaskEntry.Max(t => t.Order + 1);

            model.Order = nextOrder;
            _context.TaskEntry.Add(model);
            _context.SaveChanges();
            return model;
        }

        public void ChangeTaskOrder(int id, int newOrder)
        {
            var oldOrder = _context.Database.SqlQuery<int>("SELECT [Order] FROM [TaskEntries] WHERE [Id] = " + id).FirstOrDefault();

            //ReOrder bigger ones
            _context.Database.ExecuteSqlCommand("UPDATE [TaskEntries] " +
                                               " SET [Order] = [Order] + 1" +
                                               " WHERE [Order] >= " + newOrder +
                                               "   AND [Order] < " + oldOrder);

            //ReOrder lesser one
            _context.Database.ExecuteSqlCommand("UPDATE[TaskEntries] " +
                                               " SET [Order] = [Order] - 1" +
                                               " WHERE [Order] <= " + newOrder +
                                               "   AND [Order] > " + oldOrder);

            _context.Database.ExecuteSqlCommand("UPDATE[TaskEntries] " +
                                               " SET [Order] = " + newOrder +
                                               " WHERE [Id]= " + id);
        }

        public TaskEntry Edit(TaskEntry model)
        {
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return model;
        }

        public bool Delete(int id)
        {
            var taskEntry = Get(id);
            if (taskEntry != null)
            {
                _context.TaskEntry.Remove(taskEntry);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public IList<TaskEntry> GetAll()
        {
            var list = _context.TaskEntry.OrderBy(t => t.Order).ToList();
            return list;
        }

        public TaskEntry Get(int id)
        {
            var taskEntry = _context.TaskEntry.Where(t => t.Id == id).FirstOrDefault();
            return taskEntry;
        }

        public TaskEntry ChangeStatus(int id)
        {
            var taskEntry = Get(id);
            if (taskEntry != null)
            {
                taskEntry.Completed = !taskEntry.Completed;
                _context.Entry(taskEntry).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return taskEntry;
            }
            return null;
        }
    }
}
