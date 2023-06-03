using System.ComponentModel.DataAnnotations;

namespace InspectionService.Models
{
    public class Inspection
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public string? Comments { get; set; }
        [Required]
        public int InspectionTypeId { get; set; }

        public InspectionType? InspectionType { get; set; }
    }
}