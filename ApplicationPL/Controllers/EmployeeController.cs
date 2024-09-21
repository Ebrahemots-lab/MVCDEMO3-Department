﻿using ApplicationBLL.Interfaces;
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

        public IActionResult Index()
        {
            IEnumerable<Employee> list = _unit.EmployeeRepository.ShowAll();

            IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(list);

            return View(model: mappedEmps);
        }

        [HttpPost]
        public IActionResult Index(string searchedName)
        {

            //Searching Task
            //1 - Create Form that has input and search button 
            //2 - If the value is wrong or null -> show all Employee 
            //3 - if the value is not null -> search for all emps that contains this name
            //Get name from user Form
            //Check for null 
            if (string.IsNullOrEmpty(searchedName)) 
            {
                IEnumerable<Employee> list = _unit.EmployeeRepository.ShowAll();

                IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(list);

                return View(model: mappedEmps);
            }
            else 
            {
                IEnumerable<Employee> searchedEmps = _unit.EmployeeRepository.SearchEmps(searchedName);

                IEnumerable<EmployeeViewModel> mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(searchedEmps);

                return View(mappedEmps);
            }
        }
        

        public IActionResult Details(int id) 
        {
            Employee emp = _unit.EmployeeRepository.Get(id);

            EmployeeViewModel mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(mappedEmp);
        }

        public IActionResult Update(int id) 
        {
            //Get the Employee That we pressed on 
            Employee emp = _unit.EmployeeRepository.Get(id);

            EmployeeViewModel mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            var deptsInfo = _unit.DepartmentRepository.ShowAll();

        

            ViewBag.Depts = new SelectList(_unit.DepartmentRepository.ShowAll(),"Id","Name");

            return View(mappedEmp);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id,EmployeeViewModel empVM) 
        {
            ViewBag.Depts = new SelectList(_unit.DepartmentRepository.ShowAll(), "Id", "Name");

            if (ModelState.IsValid) 
            {

                Employee mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);
            

                _unit.EmployeeRepository.Update(mappedEmp);
                _unit.EmployeeRepository.Save();

                return RedirectToAction("Index");
            }

            return View("Update",empVM);
        }

        public IActionResult Delete(int id) 
        {
            Employee emp = _unit.EmployeeRepository.Get(id);

            if(emp != null) 
            {
                _unit.EmployeeRepository.Delete(emp);

                _unit.EmployeeRepository.Save();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
            
        }

        public IActionResult Add() 
        {
            ViewBag.Depts = new SelectList(_unit.DepartmentRepository.ShowAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel EmpVm) 
        {

            if (ModelState.IsValid) 
            {
                Employee mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmpVm);

                _unit.EmployeeRepository.Add(mappedEmp);
                _unit.EmployeeRepository.Save();      
                return RedirectToAction("Index");
            }
            else 
            {
                
                return View(EmpVm);
            }

            
        } 
    }
}
 