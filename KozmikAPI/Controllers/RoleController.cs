using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using BusinessLogicLayer;
using BusinessObjectLayer;

namespace KozmikAPI.Controllers
{
    public class RoleController : ApiController
    {
        readonly RoleBs _roleObjBs;

        public RoleController()
        {
            _roleObjBs = new RoleBs();
        }

        [ResponseType(typeof(IEnumerable<Role>))]
        public IHttpActionResult Get()
        {
            return Ok(_roleObjBs.GetAll());
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Get(int id)
        {
            var role = _roleObjBs.GetById(id);
            if (role != null)
                return Ok(role);
            else
                return NotFound();
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Post(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleObjBs.Insert(role);
                return CreatedAtRoute("DefaultApi", new { id = role.RoleId }, role);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Put(int id, Role role)
        {
            if (ModelState.IsValid)
            {
                _roleObjBs.Update(role);
                return Ok(role);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Delete(int id)
        {
            var role = _roleObjBs.GetById(id);
            if (role != null)
            {
                _roleObjBs.Delete(id);
                return Ok(role);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
