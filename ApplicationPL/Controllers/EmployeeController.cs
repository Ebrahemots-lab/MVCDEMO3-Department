using ApplicationBLL.Interfaces;
using ApplicationDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ApplicationPL.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _empRepo;

        public EmployeeController(IEmployeeRepository empRepo)
        {
            _empRepo = empRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Employee> list = _empRepo.ShowAll();
            return View(model:list);
        }

        public IActionResult Details(int id) 
        {
            Employee emp = _empRepo.Get(id);
            return View(emp);
        }

        public IActionResult Edit(int id) 
        {
            //Get the Employee That we pressed on 
            Employee emp = _empRepo.Get(id);

            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(int id,Employee emp) 
        {
            if (ModelState.IsValid) 
            {
                //Get the old Employee 
                Employee oldEmp = _empRepo.Get(id);

                //Assign the new Value To the Object occured in the database
                oldEmp.Name = emp.Name;
                oldEmp.Age = emp.Age;
                oldEmp.Adress = emp.Adress;
                oldEmp.Phone = emp.Phone;
                oldEmp.Salary = emp.Salary;
                oldEmp.IsActive = emp.IsActive;
                oldEmp.HireDate = emp.HireDate;

                //Save changes
                _empRepo.Save();

                return RedirectToAction("Index");
            }

            return View(emp);
        }

        public IActionResult Delete(int id) 
        {
            Employee emp = _empRepo.Get(id);

            if(emp != null) 
            {
                _empRepo.Delete(emp);

                _empRepo.Save();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
            
        }


        public IActionResult Add(Employee Emp) 
        {

            if (ModelState.IsValid) 
            {
                _empRepo.Add(Emp);
                _empRepo.Save();
                return RedirectToAction("Index");
            }
            else 
            {
                
                return View(Emp);
            }

            
        } 
    }
}
 