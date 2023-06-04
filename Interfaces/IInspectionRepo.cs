using InspectionService.Models;

namespace InspectionService.Interfaces
{
    public interface IInspectionRepo : IGenericRepo<Inspection>
    {
        Inspection GetById(int id);
        bool CheckIfExist(int id);
    }
}