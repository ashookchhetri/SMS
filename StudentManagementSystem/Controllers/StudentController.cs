



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Migrations;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.Domain;
using System.Xml.Linq;

namespace StudentManagementSystem.Controllers
{
    [Authorize]
    public class StudentController : Controller

    {
        IWebHostEnvironment hostingenvironment;
        private readonly StudentDbContext studentDbContext;
        

        public StudentController(StudentDbContext studentDbContext, IWebHostEnvironment hc)
        {
            this.studentDbContext = studentDbContext;
            hostingenvironment = hc; 
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var studentList = await studentDbContext.Students
                .Include(f => f.Facuilty)
                .ToListAsync();

            var studentVmList = new List<ViewModelStudent>();
            foreach (var student in studentList)
            {
                var studentvm = new ViewModelStudent()
                {
                    Id= student.Id, 
                    Name = student.Name,
                    Email = student.Email,
                    phoneno = student.phoneno,
                    FacuiltyName = student.Facuilty.FacuiltyName,
                    Imagepath = student.imagepath,


                };
                studentVmList.Add(studentvm);

            }

            return View(studentVmList);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> add()
        {
            var faculties = await studentDbContext.Facuilty.ToListAsync();
            ViewBag.Faculties = new SelectList(faculties, "Id", "FacuiltyName"); var course = await studentDbContext.courses.ToListAsync();
            ViewBag.Course = course; return View();
        }

       
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> add(ViewModelStudent student, List<int> SelectedCourse)
        {
            if (await studentDbContext.Students.AnyAsync(s => s.Email == student.Email))
            {
                ModelState.AddModelError("Email", "Email already exists!");
                return View(student);
            }
            String filename = "";
            if (student.photo != null)
            {
                String uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "-" + student.photo.FileName;
                String filepath = Path.Combine(uploadfolder, filename);
                student.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            Student s = new Student()
            {
                Name = student.Name,
                Email = student.Email,
                phoneno = student.phoneno,
                FacuiltyId = student.FacuiltyId,
                imagepath = filename,
            }; await studentDbContext.Students.AddAsync(s);

            await studentDbContext.SaveChangesAsync(); if (SelectedCourse != null && SelectedCourse.Any())
            {
                foreach (var courseId in SelectedCourse)
                {
                    var studentCourse = new StudentCourses
                    {
                        StudentId = s.Id,
                        CourseId = courseId
                    }; studentDbContext.Studentcourses.Add(studentCourse);
                }
            }
            await studentDbContext.SaveChangesAsync(); 
            return RedirectToAction("Index"); 
            
            var faculties = await studentDbContext.Facuilty.ToListAsync();
            ViewBag.Faculties = new SelectList(faculties, "Id", "FacuiltyName");
            var course = await studentDbContext.courses.ToListAsync();
            ViewBag.Course = course;
            return View("Student");
        }


        [HttpGet]
        public async Task<IActionResult> edit(int id)
        {
            var student = await studentDbContext.Students.Include(x => x.Facuilty).FirstOrDefaultAsync(x => x.Id == id);
            if (student != null)
            {
                var viewmodel = new ViewModelStudent()
                {
                    Name = student.Name,
                    Email = student.Email,
                    phoneno = student.phoneno,
                    FacuiltyName = student.Facuilty.FacuiltyName,
                    photo = ConvertToIFormFile(student.imagepath),

                };

                return View(viewmodel);
            }
            return RedirectToAction("Index");

             IFormFile ConvertToIFormFile(string filePath)
            {
                if (System.IO.File.Exists(filePath))
                {
                    var stream = new FileStream(filePath, FileMode.Open);
                    var fileInfo = new FileInfo(filePath);
                    return new FormFile(stream, 0, stream.Length, fileInfo.Name, fileInfo.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg"
                    };
                }
                return null;
            }


        }



        [HttpPost]
        public async Task<IActionResult> edit(ViewModelStudent model)
        {
            
            
                var existingStudentWithEmail = await studentDbContext.Students .Where(s => s.Email == model.Email && s.Id != model.Id).FirstOrDefaultAsync();

                if (existingStudentWithEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already exists!");
                    return View(model);
                }

                String filename = "";
                if (model.photo != null)
                {
                    String uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                    filename = Guid.NewGuid().ToString() + "-" + model.photo.FileName;
                    String filepath = Path.Combine(uploadfolder, filename);
                    model.photo.CopyTo(new FileStream(filepath, FileMode.Create));
                }

                var student = await studentDbContext.Students.FindAsync(model.Id);
                if (student != null)
                {
                       student.Name = model.Name;
                    student.Email = model.Email;
                    student.phoneno = model.phoneno;
                    student.FacuiltyId = model.FacuiltyId;
                    student.imagepath = filename;

                    await studentDbContext.SaveChangesAsync();
                };

                return RedirectToAction("Index");
            
            
                return View(model);

            

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await studentDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
           

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student model)
        {
            var student = await studentDbContext.Students.FindAsync(model.Id);
            if(student != null)
            {
                studentDbContext.Students.Remove(student);
                await studentDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
       


       
    }
}
