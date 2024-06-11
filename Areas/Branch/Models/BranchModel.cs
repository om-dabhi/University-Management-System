using System.ComponentModel.DataAnnotations;

namespace UMS.Areas.Branch.Models
{
    public class BranchModel
    {
        [Required(ErrorMessage = "BranchID is required")]
        public int BranchID { get; set; }

        [Required(ErrorMessage = "BranchName is required")]
        [StringLength(100, ErrorMessage = "BranchName length cannot exceed 100 characters")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "DeanName is required")]
        [StringLength(50, ErrorMessage = "DeanName length cannot exceed 50 characters")]
        public string DeanName { get; set; }

        [Required(ErrorMessage = "CourseID is required")]
        public int CourseID { get; set; }
    }
}
