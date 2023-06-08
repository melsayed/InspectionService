using AutoMapper;
using InspectionService.Dtos;
using InspectionService.Interfaces;
using InspectionService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspectionService.Controllers
{
    //[Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class InspectionsController : CustomControllerbase
    {
        private readonly IInspectionRepo _repository;
        private readonly IMapper _mapper;

        public InspectionsController(IInspectionRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InspectionReadDto>> GetAllInspections()
        {
            // var user = GetCurrentUser();
            // Console.WriteLine(user.DisplayName);

            Console.WriteLine("----- Get All Inspections -----");
            var inspectionsItems = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<InspectionReadDto>>(inspectionsItems));
        }

        [HttpGet("{id}", Name = "GetInspectionById")]
        public ActionResult<InspectionReadDto> GetInspectionById(int id)
        {
            Console.WriteLine("----- Get Inspection -----");
            Inspection inspection = _repository.GetById(id);
            if (inspection != null)
                return Ok(_mapper.Map<InspectionReadDto>(inspection));
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<InspectionReadDto> CreateNewInpection(InspectionCreateDto inspectionCreate)
        {
            var inspection = _mapper.Map<Inspection>(inspectionCreate);
            _repository.Add(inspection);
            _repository.SaveChanges();

            var inspectionRead = _mapper.Map<InspectionReadDto>(inspection);

            return CreatedAtRoute(nameof(GetInspectionById), new { id = inspectionRead.Id }, inspectionRead);
        }

        [Route("{id}")]
        [HttpPut]
        public ActionResult<InspectionReadDto> UpdateInspection(int id, InspectionUpdateDto inspectionUpdated)
        {
            if (id == null || inspectionUpdated == null)
                return BadRequest();
            if (!_repository.CheckIfExist(id))
                return BadRequest();
            if (id != inspectionUpdated.Id)
                return BadRequest();

            var inspection = _mapper.Map<Inspection>(inspectionUpdated);

            _repository.Update(inspection);
            _repository.SaveChanges();

            var inspectionRead = _mapper.Map<InspectionReadDto>(inspection);

            return CreatedAtRoute(nameof(GetInspectionById), new { id = inspectionRead.Id }, inspectionRead);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteInspection(int id)
        {
            var inspection = _repository.GetById(id);
            if (inspection == null)
                return BadRequest();

            _repository.Delete(inspection);
            return Ok();
        }
    }
}