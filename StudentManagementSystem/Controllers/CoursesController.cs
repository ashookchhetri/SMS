using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly StudentDbContext studentDbContext;

        public CoursesController(StudentDbContext studentDbContext)
        {
            this.studentDbContext = studentDbContext;
        }

        [AllowAnonymous]
        public async Task< IActionResult> Index()
        {
            var courses = await studentDbContext.courses.Include(x => x.facuilty).ToListAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> add()
        {
            var faculties = await studentDbContext.Facuilty.ToListAsync();
            ViewBag.Faculties = new SelectList(faculties, "Id", "FacuiltyName");

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> add( Courses courses)
        {            
            Courses course = new Courses()
            {
                Name = courses.Name,
                FacuiltyId = courses.FacuiltyId,

            };
            await studentDbContext.courses.AddAsync(course);
            await studentDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
