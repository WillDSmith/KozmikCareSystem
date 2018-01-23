using BusinessObjectLayer;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    public class EmployeeBs
    {
        private readonly EmployeeDb _employeeObject;

        public List<string> Errors = new List<string>();

        public EmployeeBs()
        {
            _employeeObject = new EmployeeDb();
        }
        public IEnumerable<Employee> GetAll()
        {
            return _employeeObject.GetAll();
        }
        public Employee GetById(string id)
        {
            return _employeeObject.GetById(id);
        }

        public bool Insert(Employee emp)
        {
            if (IsValidOnInsert(emp))
            {
                _employeeObject.Insert(emp);
                const string subject = "Your Login Credentials On KCS";
                var body = "User Name : " + emp.Email + "\n" +
                              "Password : " + emp.Password + "\n" +
                              "Login Here : http://www.kozmiksoftware.com/kozmik/kcs.html#/Login" + "\n" +
                              "Regards," + "\n" +
                              "www.kozmiksoftware.com/kozmik/kcs.html#";
                Utilities.SendEmail(emp.Email, subject, body);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Delete(string id)
        {
            _employeeObject.Delete(id);
        }
        public bool Update(Employee emp)
        {
            //if (IsValidOnUpdate(emp))
            //{
            _employeeObject.Update(emp);
            return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        public Employee GetByEmail(string email)
        {
            return _employeeObject.GetByEmail(email);
        }
        public Employee RecoverPasswordByEmail(string email)
        {
            var emp = _employeeObject.GetByEmail(email);
            if (emp == null) return null;
            const string subject = "Your Login Credentials On KCS";
            var body = "User Name : " + emp.Email + "\n" +
                          "Password : " + emp.Password + "\n" +
                          "Login Here : http://www.kozmiksoftware.com/kozmik/kcs.html#/Login" + "\n" +
                          "Regards," + "\n" +
                          "www.Kozmiksoftware.com";
            Utilities.SendEmail(emp.Email, subject, body);
            return emp;
        }

        public bool IsValidOnInsert(Employee emp)
        {
            EmployeeBs employeeObjBs = new EmployeeBs();

            //Unique Employee Id Validation
            var employeeIdValue = emp.EmployeeId;
            var count = employeeObjBs.GetAll().Where(x => x.EmployeeId == employeeIdValue).ToList().Count();
            if (count != 0)
            {
                Errors.Add("EmployeeId Already Exist");
            }

            //Unique Email Validation
            var emailValue = emp.Email;
            count = employeeObjBs.GetAll().Where(x => x.Email == emailValue).ToList().Count();
            if (count != 0)
            {
                Errors.Add("Email Already Exist");
            }

            return !Errors.Any();
        }

        //public bool IsValidOnUpdate(Employee emp)
        //{

        //    //Total Exp should be greater than Relevant Exp
        //    var TotalExpValue = emp.TotalExp;
        //    var RelevantExpValue = emp.RelevantExp;

        //    if (RelevantExpValue > TotalExpValue)
        //    {
        //        Errors.Add("Total Exp should be greater than Relevant Exp");
        //    }

        //    if (Errors.Count() == 0)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
