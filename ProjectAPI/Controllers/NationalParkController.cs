using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DTOs;
using ProjectAPI.Models;
using ProjectAPI.Repository.Interfaces;

namespace ProjectAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NationalParkController : Controller
    {
        private INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public NationalParkController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }

  
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var objList = _nationalParkRepository.GetNationalParks();
            var objDto = new List<NationalParkDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
            }

            return Ok(objDto);
        }


        
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var obj = _nationalParkRepository.GetNationalPark(nationalParkId);

            if(obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<NationalParkDto>(obj);
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest();
            }

            if(_nationalParkRepository.NationalParkExsists(nationalParkDto.name))
            {
                ModelState.AddModelError("", "National park exists.");
                return StatusCode(404, ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_nationalParkRepository.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Problem while saving the record {nationalParkDto.name}");
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalParkObj.id }, nationalParkObj);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNatioanlPark (int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.id)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_nationalParkRepository.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Problem while updating the record {nationalParkDto.name}");
            }

            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNatioanlPark(int nationalParkId)
        {
            if (!_nationalParkRepository.NationalParkExsists(nationalParkId))
            {
                return NotFound();
            }

            var nationalParkObj = _nationalParkRepository.GetNationalPark(nationalParkId);

            if(!_nationalParkRepository.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Problem while deleting the record {nationalParkObj.name}");
            }

            return NoContent();
        }
    }
}