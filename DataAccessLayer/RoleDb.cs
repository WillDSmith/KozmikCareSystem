using BusinessObjectLayer;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer
{
    public class RoleDb
    {
        private readonly KozmikCareDbContext _db;

        public RoleDb()
        {
            _db = new KozmikCareDbContext();
        }
        public IEnumerable<Role> GetAll()
        {
            return _db.Roles.ToList();
        }
        public Role GetById(int id)
        {
            return _db.Roles.Find(id);
        }
        public void Insert(Role role)
        {
            _db.Roles.Add(role);
            Save();
        }
        public void Delete(int id)
        {
            var role = _db.Roles.Find(id);
            if (role != null) _db.Roles.Remove(role);
            Save();
        }
        public void Update(Role role)
        {
            _db.Entry(role).State = EntityState.Modified;
            _db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
