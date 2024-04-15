using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Migrations;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [Authorize]
    public class FacuiltyController : Controller
    {
        private readonly StudentDbContext studentDbContext;

        public FacuiltyController(StudentDbContext studentDbContext)
        {
            this.studentDbContext = studentDbContext;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var faculties = await studentDbContext.Facuilty.Include(f => f.courses).Include(s => s.students).ToListAsync();
     

            return View(faculties);
        }

        [HttpGet]
        public async Task<IActionResult> add()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> add(Facuilty facuilty)
        {
            facuilty = new Facuilty
            {
                FacuiltyName = facuilty.FacuiltyName,

            };
            await studentDbContext.Facuilty.AddAsync(facuilty);
            await studentDbContext.SaveChangesAsync();


            return View(facuilty);
        }



        public async Task<IActionResult> Courses_Faculty(int facultyID)
        {
           
        var Facuilty = await studentDbContext.Facuilty
        .Include(x => x.courses)
        .FirstOrDefaultAsync(f => f.Id == facultyID);
            return View(Facuilty);
        }
    }
}
