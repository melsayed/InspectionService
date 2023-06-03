namespace InspectionService.Dtos
{
    public class InspectionReadDto
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public string? Comments { get; set; }
        public int InspectionTypeId { get; set; }
    }
}