using ApplicationBLL.Interfaces;
using ApplicationDAL.Models;
using ApplicationPL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;

namespace ApplicationPL.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _empRepo;
        private IDepartmentRepository _departmentRepo;

        private IMapper _mapper;

        public EmployeeController(IEmployeeRepository empRepo , IDepartmentRepository dept , IMapper mapper)
        {
            _empRepo = empRepo;
            _departmentRepo = dept;
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

        //Searching Task
        //1 - Create Form that has input and search button 
        //2 - If the value is wrong or null -> show all Employee 
        //3 - if the value is not null -> search for all emps that contains this name


        public IActionResult Index()
        {
            IEnumerable<Employee> list = _empRepo.ShowAll();

            IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(list);

            return View(model: mappedEmps);
        }

        [HttpPost]
        public IActionResult Index(string searchedName)
        {
            
            //Get name from user Form
            //Check for null 
            if(string.IsNullOrEmpty(searchedName)) 
            {
                IEnumerable<Employee> list = _empRepo.ShowAll();

                IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(list);

                return View(model: mappedEmps);
            }
            else 
            {
                IEnumerable<Employee> searchedEmps = _empRepo.SearchEmps(searchedName);

                IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(searchedEmps);

                return View(mappedEmps);
            }
        }

           
        

        public IActionResult Details(int id) 
        {
            Employee emp = _empRepo.Get(id);

            EmployeeViewModel mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(mappedEmp);
        }

        public IActionResult Update(int id) 
        {
            //Get the Employee That we pressed on 
            Employee emp = _empRepo.Get(id);

            EmployeeViewModel mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            var deptsInfo = _departmentRepo.ShowAll();

        

            ViewBag.Depts = new SelectList(_departmentRepo.ShowAll(),"Id","Name");

            return View(mappedEmp);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id,EmployeeViewModel empVM) 
        {
            ViewBag.Depts = new SelectList(_departmentRepo.ShowAll(), "Id", "Name");

            if (ModelState.IsValid) 
            {

                Employee mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);
            

                _empRepo.Update(mappedEmp);
                _empRepo.Save();

                return RedirectToAction("Index");
            }

            return View("Update",empVM);
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

        public IActionResult Add() 
        {
            ViewBag.Depts = new SelectList(_departmentRepo.ShowAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel EmpVm) 
        {

            if (ModelState.IsValid) 
            {
                Employee mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmpVm);

                _empRepo.Add(mappedEmp);
                _empRepo.Save();      
                return RedirectToAction("Index");
            }
            else 
            {
                
                return View(EmpVm);
            }

            
        } 
    }
}
 