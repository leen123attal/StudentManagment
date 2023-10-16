using Abp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentManagment.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentManagment.Controllers
{
     
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;


        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;

            _context = context;

        }

        public IActionResult Index()
        {
            _logger.LogInformation("HomeController.Index method called!!!");
            var result = GetAll().Result.Value;

            var model = new AllStudentModel
            {
                students= (List<Student>)result
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {

            var viewModel = new CreateOrEditModel();

            if(id != null)
            {
                viewModel.student = GetStudent(id).Result.Value;

            }

            else
            {
                viewModel = new CreateOrEditModel()
                {
                    student = new Student()
                };
            }

           
            return PartialView("_CreateOrEditModal",viewModel);
        }


        
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            _logger.LogInformation("GetAll() method called!!!");
            IEnumerable<Student> result;
            try {

                result = await _context.Students.Where(x=>x.IsDeleted != true).ToListAsync();

                _logger.LogInformation(" The Results Are :"+ JsonSerializer.Serialize(result));
            }

            catch 
            {
            
                result = null;

                _logger.LogInformation("Something Wrong!!");
            }

            return result.ToList();
        }


        
        public async Task<ActionResult<Student>> GetStudent(int? id )
        {
            Student result;
            _logger.LogInformation("GetStudent Called with id" + id);

            try
            {
                result = await _context.Students.FindAsync(id);
                _logger.LogInformation("The Result For GetStudent " + JsonSerializer.Serialize(result));

            }

            catch (Exception e)
            {

                 result = null;

                _logger.LogInformation("Something Wrong!!" + JsonSerializer.Serialize(e));

            }

                return result;

        }

        public async Task<IActionResult> GetStudentSearch(string text)
        {
            List<Student> result;
            if (text != null)
            {

                 result = await _context.Students.Where(x => (x.Name.Contains(text) || x.Number.ToString().Contains(text) || x.Grade.ToString().Contains(text)) &&  x.IsDeleted== false).ToListAsync();
            }

            else
            {
                 result = (List<Student>)GetAll().Result.Value;
            }

            var model = new AllStudentModel
            {
                students = result
            };

            return PartialView("_SerachModal", model);


        }




        public async Task CreateStudent(Student student)
        {
            _logger.LogInformation(" Create Student  method called!!!");

            _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();

            _logger.LogInformation(" Create Student "+ JsonSerializer.Serialize(student));

        }


       

        public async Task UpdateStudent(Student student)
        {
            _logger.LogInformation(" Create Student  method called!!!");

            _context.Students.Update(student);

            await _context.SaveChangesAsync();

            _logger.LogInformation(" update Student " + JsonSerializer.Serialize(student));

        }

        [HttpGet]
        public async Task<IActionResult> CreateOrEditStudent(Student student)
        {
            if (student.Id == null || student.Id == 0)
            {
                await CreateStudent(student);
            }
            else
            {
                await UpdateStudent(student);
            }

            var model = new AllStudentModel
            {
                students = (List<Student>)GetAll().Result.Value
            };


            return PartialView("_SerachModal", model);

        }

      


        public async Task<IActionResult> DeleteStudent(int id)
        {

            _logger.LogInformation(" Delete Student  method called!!!");

            var result=await  _context.Students.Where(x=>x.Id== id).FirstOrDefaultAsync();

            result.IsDeleted = true;

            _context.Students.Update(result);

            await _context.SaveChangesAsync();

            _logger.LogInformation(" Student Deleted with id " + JsonSerializer.Serialize(id));

            var model = new AllStudentModel
            {
                students = (List<Student>)GetAll().Result.Value
            };

          


            return PartialView("_SerachModal", model);



        }



        }
}
