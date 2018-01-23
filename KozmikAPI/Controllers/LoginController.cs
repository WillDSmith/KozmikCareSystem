using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BusinessLogicLayer;
using BusinessObjectLayer;

namespace KozmikAPI.Controllers
{
    [EnableCors("*", "*", "*", "*")]
    public class LoginController : ApiController
    {
        readonly EmployeeBs _employeeObjBs;

        public LoginController()
        {
            _employeeObjBs = new EmployeeBs();
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee emp)
        {
            var employee = _employeeObjBs.GetByEmail(emp.Email);

            if (employee == null)
            {
                ModelState.AddModelError("", "Email id Does not Exist");
                return BadRequest(ModelState);
            }
            else if (employee.Password != emp.Password)
            {
                ModelState.AddModelError("", "Invalid Password");
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(employee);
            }
        }

        [ResponseType(typeof(Employee))]
        [ActionName("RecoverPassword")]
        public IHttpActionResult Get(string empEmail)
        {
            var employee = _employeeObjBs.RecoverPasswordByEmail(empEmail);

            if (employee == null)
            {
                ModelState.AddModelError("", "Email id Does not Exist");
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(employee);
            }
        }
    }
}
