using System.ComponentModel.DataAnnotations;

namespace InspectionService.Models
{
    public class InspectionType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Name { get; set; }
    }
}