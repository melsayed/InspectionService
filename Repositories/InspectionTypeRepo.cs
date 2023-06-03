using InspectionService.Data;
using InspectionService.Interfaces;
using InspectionService.Models;

namespace InspectionService.Repositories
{
    public class InspectionTypeRepo : GenericRepo<InspectionType>, IInspectionTypeRepo
    {
        private readonly AppDbContext _context;
        public InspectionTypeRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public InspectionType GetById(int id) => FindByCondition(p => p.Id == id);
    }
}