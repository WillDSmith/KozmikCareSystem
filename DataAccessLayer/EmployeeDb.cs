using BusinessObjectLayer;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer
{
    public class EmployeeDb
    {
        private readonly KozmikCareDbContext _db;

        public EmployeeDb()
        {
            _db = new KozmikCareDbContext();
        }
        public IEnumerable<Employee> GetAll()
        {
            return _db.Employees.ToList();
        }
        public Employee GetById(string id)
        {
            return _db.Employees.Find(id);
        }
        public void Insert(Employee emp)
        {
            _db.Employees.Add(emp);
            Save();
        }
        public void Delete(string id)
        {
            Employee emp = _db.Employees.Find(id);
            if (emp != null) _db.Employees.Remove(emp);
            Save();
        }
        public void Update(Employee emp)
        {
            _db.Entry(emp).State = EntityState.Modified;
            _db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }

        public Employee GetByEmail(string email)
        {
            return _db.Employees.FirstOrDefault(x => x.Email == email);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
