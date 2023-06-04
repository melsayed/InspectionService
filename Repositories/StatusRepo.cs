using InspectionService.Data;
using InspectionService.Interfaces;
using InspectionService.Models;

namespace InspectionService.Repositories
{
    public class StatusRepo : GenericRepo<Status>, IStatusRepo
    {
        private readonly AppDbContext _context;
        public StatusRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Status GetById(int id) => FindByCondition(s => s.Id == id);
    }
}