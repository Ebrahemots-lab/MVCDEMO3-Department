using ApplicationBLL.Interfaces;
using ApplicationDAL.Models;
using ApplicationPL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApplicationPL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;   

        public DepartmentController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            //Get all Department
            IEnumerable<Department> models = await _unitOfWork.DepartmentRepository.ShowAllAsync();

            //Convert it to Ienumrable of DepartmentViewModel
            IEnumerable<DepartmentViewModel> depts = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(models);


            return View(model: depts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            //Check if the Model is not Null

            if (ModelState.IsValid)
            {
                var resultNumber = await _unitOfWork.DepartmentRepository.AddAsync(dept);
                if (resultNumber > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(dept);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                Department dept = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

                if (dept is null) return NotFound();

                return View(dept);
            }
        }

        [HttpGet]
        public async Task<IActionResult>  Update(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            Department dept = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            return View(model: dept);
        }

       
        public async Task<IActionResult> Edit(int id, Department dept)

        {
            Department returnedDept = await _unitOfWork.DepartmentRepository.GetAsync(id);

            
           
            if (returnedDept != null)
            {
                if (ModelState.IsValid)
                {

                    returnedDept.Code = dept.Code;
                    returnedDept.Name = dept.Name;
                    returnedDept.DateOfCreation = dept.DateOfCreation;

                    _unitOfWork.Save();

                    return RedirectToAction("Index");

                }
            }

            return View("Update", dept);
    
        }

        public async Task<IActionResult> Delete(int id) 
        {
           Department dept = await _unitOfWork.DepartmentRepository.GetAsync(id);
            _unitOfWork.DepartmentRepository.Delete(dept);
            return RedirectToAction("Index");
        }

        
       //public IActionResult Test(string name, int age , string[] colors) 
       // {
       //     var result = Request.QueryString.ToString();
       //     Console.WriteLine(result);
       //     return Content($"{name} {age}");
       // }
    }

    
}
