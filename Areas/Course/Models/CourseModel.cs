using System.ComponentModel.DataAnnotations;

namespace UMS.Areas.Course.Models
{
    public class CourseModel
    {
        [Required(ErrorMessage = "Please select Course.")]
        public int CourseID { get; set; }
        [Required(ErrorMessage = "Course Name is required.")]
        [StringLength(50, ErrorMessage = "Course Name must not exceed 50 characters.")]
        public string CourseName { get; set; }    
    }
}
