using InspectionService.Models;

namespace InspectionService.Interfaces
{
    public interface IInspectionTypeRepo : IGenericRepo<InspectionType>
    {
        InspectionType GetById(int id);
    }
}