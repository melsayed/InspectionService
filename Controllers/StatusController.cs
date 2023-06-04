using InspectionService.Interfaces;
using InspectionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InspectionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : CustomControllerbase
    {
        private readonly IStatusRepo _repository;

        public StatusController(IStatusRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Status>> GetAllStatues()
        {
            return Ok(_repository.GetAll());
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<Status> GetStatusById(int id)
        {
            return Ok(_repository.GetById(id));
        }
    }
}