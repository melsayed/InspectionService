using InspectionService.Models;

namespace InspectionService.Interfaces
{
    public interface IStatusRepo : IGenericRepo<Status>
    {
        Status GetById(int id);
    }
}