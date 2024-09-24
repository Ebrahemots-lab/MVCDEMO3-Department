using ApplicationBLL.Interfaces;
using ApplicationDAL.Models;
using ApplicationPL.Helpers;
using ApplicationPL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApplicationPL.Controllers
{
    public class EmployeeController : Controller
    {

        private IMapper _mapper;

        private IUnitOfWork _unit { get; }

        public EmployeeController(IUnitOfWork unit , IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public IActionResult UniqueName(string Name) 
        {
            if (Name.Contains("route")) 
            {
                return Json(true);
            }
            else 
            { 
                return Json(false);
            }
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> list = await _unit.EmployeeRepository.ShowAllAsync();

            IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(list);

            return View(model: mappedEmps);
        }

        [HttpPost]
        public async Task<IActionResult>  Index(string searchedName)
        {

            //Searching Task
            //1 - Create Form that has input and search button 
            //2 - If the value is wrong or null -> show all Employee 
            //3 - if the value is not null -> search for all emps that contains this name
            //Get name from user Form
            //Check for null 
            if (string.IsNullOrEmpty(searchedName)) 
            {
                IEnumerable<Employee> list = await _unit.EmployeeRepository.ShowAllAsync();

                IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(list);

                return View(model: mappedEmps);
            }
            else 
            {
                IEnumerable<Employee> searchedEmps = await _unit.EmployeeRepository.SearchEmpsAsync(searchedName);

                IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(searchedEmps);

                return View(mappedEmps);
            }
        }
        

        public async Task<IActionResult>  Details(int id) 
        {
            Employee emp = await _unit.EmployeeRepository.GetAsync(id);

            EmployeeViewModel mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(mappedEmp);
        }

        public async Task<IActionResult> Update(int id) 
        {
            //Get the Employee That we pressed on 
            Employee emp = await _unit.EmployeeRepository.GetAsync(id);

            EmployeeViewModel mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            var deptsInfo = _unit.DepartmentRepository.ShowAllAsync();


            ViewBag.Depts = new SelectList(await _unit.DepartmentRepository.ShowAllAsync(),"Id","Name");


            //Send session Informations 
            HttpContext.Session.SetString("Name", mappedEmp.Name);
            HttpContext.Session.SetInt32("Age", mappedEmp.Age);


            return View(mappedEmp);
        }

        [HttpPost]
        public async Task<IActionResult>  Edit([FromRoute] int id,EmployeeViewModel empVM) 
        {
            ViewBag.Depts = new SelectList(_unit.DepartmentRepository.ShowAllAsync().Result, "Id", "Name");


            //Get Img name from Database Based on Emp
            Employee Emp =await _unit.EmployeeRepository.GetAsync(empVM.Id);
            string oldImgName = Emp.Img;

          
            if (ModelState.IsValid) 
            {

                //Delete old image
                if(empVM.Image != null) 
                {

                DocumentHelper.Delete(oldImgName, "Imgs");

                string newFileName = DocumentHelper.Upload(empVM.Image,"Imgs");

                empVM.Img = newFileName;
                }
                //Upload new Image

                Employee mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);
                

                _unit.EmployeeRepository.Update(mappedEmp);


                return RedirectToAction("Index");
            }

            return View("Update",empVM);
         }

        public async Task<IActionResult> Delete(int id) 
        {
            Employee emp = await _unit.EmployeeRepository.GetAsync(id);

            if(emp != null) 
            {
                int statusNumber = _unit.EmployeeRepository.Delete(emp);

                  _unit.Save();

                if(statusNumber > 0 && emp.Img != null) 
                {
                    DocumentHelper.Delete(emp.Img,"Imgs");
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Add() 
        {
            ViewBag.Depts = new SelectList(_unit.DepartmentRepository.ShowAllAsync().Result, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel EmpVm) 
        {

            if (ModelState.IsValid) 
            {

               if(EmpVm.Image != null) 
                {
                    string fileName = DocumentHelper.Upload(EmpVm.Image, "imgs");
                    EmpVm.Img = fileName;
                }

                Employee mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmpVm);

                await _unit.EmployeeRepository.AddAsync(mappedEmp);
                _unit.Save();      
                return RedirectToAction("Index");
            }
            else 
            {
                
                return View(EmpVm);
            }

            
        }


        public IActionResult Test() 
        {
            //Catch session 

            string userName = HttpContext.Session.GetString("Name");
            int? userAge = HttpContext.Session.GetInt32("Age");

            return Content($"Name: {userName} - Age: {userAge}");
        }
    }
}
 