using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallyAssignment4.Models;

namespace TallyAssignment4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly TallyDbContext _context;

        public StudentController(TallyDbContext context)
        {
            _context = context;
        }


       
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudent()
        {

            return _context.Students.Include(s => s.Subjects).ToList();

        }


       
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.Include(s => s.Subjects)
                                          .Where(s => s.StudentId == id)
                                          .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound("Student ID Does not found");
            }

            return student;
        }



       
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }



        

        [HttpPut("{Studentid}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

           
            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else { throw; }
            }


            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);

        }



       
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Id does not found");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }


        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
