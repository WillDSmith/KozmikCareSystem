using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BusinessLogicLayer;
using BusinessObjectLayer;

namespace KozmikAPI.Controllers
{

    [EnableCors("*", "*", "*")]
    public class EmployeeController : ApiController
    {
        readonly EmployeeBs _employeeObjBs;

        public EmployeeController()
        {
            _employeeObjBs = new EmployeeBs();
        }

        [ResponseType(typeof(IEnumerable<Employee>))]
        public IHttpActionResult Get()
        {
            var emps = _employeeObjBs.GetAll().OrderByDescending(x => x.DateCreated);
            return Ok(emps);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Get(string id)
        {
            var employee = _employeeObjBs.GetById(id);
            if (employee != null)
                return Ok(employee);
            else
                return NotFound();
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee employee)
        {
            if (!(ModelState.IsValid && _employeeObjBs.Insert(employee)))
                return SendBadRequest();

            return CreatedAtRoute("DefaultApi", new {id = employee.EmployeeId}, employee);
        }

        private IHttpActionResult SendBadRequest()
        {
            foreach (var error in _employeeObjBs.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Put(Employee employee)
        {
            if (!(ModelState.IsValid && _employeeObjBs.Update(employee)))
                return SendBadRequest();

            return Ok(employee);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Delete(string id)
        {
            var employee = _employeeObjBs.GetById(id);
            if (employee != null)
            {
                _employeeObjBs.Delete(id);
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }
    }
}