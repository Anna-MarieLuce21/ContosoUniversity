using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models.StudentViewModels;

namespace ContosoUniversity.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]

        public StudentVM StudentVM { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            
            var student = await _context.Students.FindAsync(id);

            if (student == null) return NotFound();

            StudentVM = new StudentVM
            {
                ID             = student.ID,
                LastName       = student.LastName,
                FirstName      = student.FirstName,
                EnrollmentDate = student.EnrollmentDate
            };

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {   
            if (!ModelState.IsValid) return Page();

            var studentToUpdate = await _context.Students.FindAsync(id);

            if (studentToUpdate == null) return NotFound();

            studentToUpdate.LastName       = StudentVM.LastName;
            studentToUpdate.FirstName      = StudentVM.FirstName;
            studentToUpdate.EnrollmentDate = StudentVM.EnrollmentDate;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Student updated successfully.";

            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
