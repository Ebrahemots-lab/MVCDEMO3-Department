using ApplicationBLL.Interfaces;
using ApplicationDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationPL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Department> result = _unitOfWork.DepartmentRepository.ShowAll();
            return View(model: result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {
            //Check if the Model is not Null

            if (ModelState.IsValid)
            {
                int resultNumber = _unitOfWork.DepartmentRepository.Add(dept);
                if (resultNumber > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(dept);
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                Department dept = _unitOfWork.DepartmentRepository.Get(id.Value);

                if (dept is null) return NotFound();

                return View(dept);
            }
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            Department dept = _unitOfWork.DepartmentRepository.Get(id.Value);
            return View(model: dept);
        }

       
        public IActionResult Edit(int id, Department dept)

        {
            Department returnedDept = _unitOfWork.DepartmentRepository.Get(id);

            if (returnedDept != null)
            {
                if (ModelState.IsValid)
                {
                    returnedDept.Code = dept.Code;
                    returnedDept.Name = dept.Name;
                    returnedDept.DateOfCreation = dept.DateOfCreation;
                    _unitOfWork.DepartmentRepository.Save();
                    
                    return RedirectToAction("Index");

                }
            }

            return View("Update", dept);
    
        }

        public IActionResult Delete(int id) 
        {
           Department dept = _unitOfWork.DepartmentRepository.Get(id);
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
