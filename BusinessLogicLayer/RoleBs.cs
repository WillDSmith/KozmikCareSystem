using BusinessObjectLayer;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    public class RoleBs
    {
        private readonly RoleDb _roleObject;

        public RoleBs()
        {
            _roleObject = new RoleDb();
        }
        public IEnumerable<Role> GetAll()
        {
            return _roleObject.GetAll().ToList();
        }
        public Role GetById(int id)
        {
            return _roleObject.GetById(id);
        }
        public void Insert(Role role)
        {
            _roleObject.Insert(role);
        }
        public void Delete(int id)
        {
            _roleObject.Delete(id);
        }
        public void Update(Role role)
        {
            _roleObject.Update(role);
        }
    }
}
