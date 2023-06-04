using System.ComponentModel.DataAnnotations;

namespace InspectionService.Dtos
{
    public class InspectionUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public string? Comments { get; set; }
        [Required]
        public int InspectionTypeId { get; set; }
    }
}