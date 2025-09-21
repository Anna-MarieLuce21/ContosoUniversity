using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models.Validation;

namespace ContosoUniversity.Models.StudentViewModels
{
    public class StudentVM
    {
        public int      ID             { get; set; }

        [Required, StringLength(50)]
        public string   FirstName      { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string   LastName       { get; set; } = string.Empty;


        [DataType(DataType.Date)]
        [NotFutureDate]
        public DateTime EnrollmentDate { get; set; }
    }
}
