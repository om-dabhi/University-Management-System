using System.ComponentModel.DataAnnotations;

namespace UMS.Areas.Faculty.Models
{
    public class FacultyModel
    {
        public int FacultyID { get; set; }

        [Required(ErrorMessage = "Faculty Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Faculty Name must be between 2 and 100 characters")]
        public string FacultyName { get; set; }

        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years")]
        public int ExperienceID { get; set; }
        public string Experience { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course ID is required")]
        public int CourseID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
    public class ExperienceDropDown
    {
        public int ExperienceID { get; set; }
        public string? Experience { get; set; }
    }
}
