using AutoMapper;
using InspectionService.Dtos;
using InspectionService.Interfaces;
using InspectionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InspectionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionTypesController : CustomControllerbase
    {
        private readonly IInspectionTypeRepo _repository;
        private readonly IMapper _mapper;

        public InspectionTypesController(IInspectionTypeRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InspectionTypeReadDto>> GetAllInspectionTypes()
        {
            var inspectionTypesItems = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<InspectionTypeReadDto>>(inspectionTypesItems));
        }

        [HttpGet("{id}", Name = "GetInspectionTypeById")]
        public ActionResult<InspectionTypeReadDto> GetInspectionTypeById(int id)
        {
            var inspectionTypeItem = _repository.GetById(id);
            if (inspectionTypeItem != null)
                return Ok(_mapper.Map<InspectionTypeReadDto>(inspectionTypeItem));
            else
                return NotFound();
        }
    }
}