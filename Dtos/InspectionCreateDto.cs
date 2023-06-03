using System.ComponentModel.DataAnnotations;

namespace InspectionService.Dtos
{
    public class InspectionCreateDto
    {
        [Required]
        public string? Status { get; set; }
        [Required]
        public string? Comments { get; set; }
        [Required]
        public int InspectionTypeId { get; set; }
    }
}