using System.ComponentModel.DataAnnotations;

namespace UMS.Areas.Student.Models
{
    public class Studentmodel
    {
        public int? StudentID { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? DOB { get; set; }

        [Required]
        [MinLength(20)]
        public string? Address { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$")]
        public string? PhoneNumber { get; set; }

        [Required]
        [RegularExpression("[a-z0-9]+@[a-z]+\\.[a-z]{2,3}", ErrorMessage = "Please enter Valid Email Address")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$")]
        public string? EnrollmentNo { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        public int IsOnRoll { get; set; }
    }
}
