using System.ComponentModel.DataAnnotations;

namespace UMS.Areas.Student.Models
{
    public class Studentmodel
    {
        public int? StudentID { get; set; }

        [Required(ErrorMessage = "BranchID is required.")]
        public int BranchID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MinLength(5, ErrorMessage = "Address should be at least 20 characters long.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone Number should be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enrollment Number is required.")]
        public string EnrollmentNo { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        [Required(ErrorMessage = "Course ID is required.")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "On Roll status is required.")]
        public bool IsOnRoll { get; set; }
        public string CourseName { get; set; }
        public string BranchName { get; set; }
    }
    public class CourseDropDown
    {
        public int Sem { get; set; }
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public bool IsSelected { get; internal set; }
    }
    public class BranchDropDown
    {
        public int BranchID { get; set; }
        public string? BranchName { get; set; }
    }
}
