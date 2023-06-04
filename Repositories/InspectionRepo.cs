using InspectionService.Data;
using InspectionService.Interfaces;
using InspectionService.Models;

namespace InspectionService.Repositories
{
    public class InspectionRepo : GenericRepo<Inspection>, IInspectionRepo
    {
        private readonly AppDbContext _context;

        public InspectionRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckIfExist(int id) => CheckIfExist(p => p.Id == id);

        public Inspection GetById(int id) => FindByCondition(p => p.Id == id);
    }
}