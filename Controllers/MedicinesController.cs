using AutoMapper;
using awebapi.DTOs;
using awebapi.Entities;
using awebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace awebapi.Controllers
{

    [Route("api/[controller]")]
    public class MedicinesController : Controller
    {
        private readonly MedicinesService _medicineService;
        public MedicinesController(MedicinesService medicineService)
        {
            _medicineService = medicineService;
            

        }

        [HttpGet]
        [Route("GetMedicines")]

        public async Task<IActionResult> GetMedicines()
        {
            var response = await _medicineService.FetchAllAsync();
            return Ok(response);
        }

        [HttpPost]
        [Route("GetQuery")]

        public async Task<IActionResult> GetQuery(string searchedQuery)
        {
            var response = await _medicineService.searchQuery(searchedQuery);
            return Ok(response);
        }



        // [HttpPut("isActiveUpdate/{id}")]
        // public async Task<IActionResult> UpdateStatus(Guid id, bool is_active)
        // {
        //     if (id == Guid.Empty)
        //     {
        //         return BadRequest("Invalid ID");
        //     }

        //     try
        //     {
        //         await _medicineService.isActiveUpdate(id, is_active);
        //         return Ok(new { message = "Status updated" });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, "Internal server error");
        //     }
        // }
        [HttpPut("isActiveUpdate/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] bool is_active)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            try
            {
                await _medicineService.isActiveUpdate(id, is_active);
                return Ok(new { message = "Status updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost]
        [Route("GetByDate")]

        public async Task<IActionResult> getByDate(DateTime Created_at)
        {
            var response = await _medicineService.getByDate(Created_at);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetByDateRange")]
        public IActionResult GetItemsBetweenDates(string startDate, string endDate)
        {

            DateTime ParsedStartDate = DateTime.ParseExact(startDate.Replace("  ", " +"), "yyyy-MM-dd HH:mm:ss.fff zzzz",
                                       System.Globalization.CultureInfo.InvariantCulture).ToUniversalTime();
            DateTime ParsedEndDate = DateTime.ParseExact(endDate.Replace("  ", " +"), "yyyy-MM-dd HH:mm:ss.fff zzzz",
           System.Globalization.CultureInfo.InvariantCulture).ToUniversalTime();

            // DateTime utcStartDate = ParsedStartDate.ToUniversalTime();
            // DateTime utcEndDate = ParsedEndDate.ToUniversalTime();

            if (ParsedStartDate > ParsedEndDate)
            {
                return BadRequest("Start date is after end date");
            }

            var items = _medicineService.GetByDateRange(ParsedStartDate, ParsedEndDate);

            return Ok(items);
        }



        [HttpPost]
        [Route("AddNewMedicine")]
        public IActionResult AddNewMedicine( [FromBody] MedicineDto medicineDto)
        {
            var AddNewMedicine = new Medicines {
                Med_id = Guid.NewGuid(),  
                Name = medicineDto.Name,
                Description = medicineDto.Description,
                // Created_at = DateTime.UtcNow,
                // Last_edit = DateTime.UtcNow,
                Stock = medicineDto.Stock,
                Is_active = true
            };

            // var model = _mapper.Map<Medicines>(medicineDto);
            _medicineService.CreateNewAsync(AddNewMedicine);
            return Ok(AddNewMedicine);
        }


        [HttpPut]
        [Route("UpdateMed")]
        public IActionResult UpdateMed( [FromBody] MedicineDto medicineDto)
        {
            _medicineService.UpdateMed(medicineDto);
            return Ok();
        }
    }
}