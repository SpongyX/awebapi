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
        public MedicinesController(
            MedicinesService medicineService)
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
        [HttpGet]
        [Route("GetbyType")]
        public async Task<IActionResult> GetbyType(string type)
        {
            try
            {
                var items = await _medicineService.GetByType(type);

                return Ok(items);
            }
            catch (FormatException ex)
            {
                return BadRequest("Invalid type");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error while processing your request.");
            }

        }
        [HttpPost]
        [Route("GetByDate")]
        public async Task<IActionResult> GetByDate(DateTime Created_at)
        {
            var response = await _medicineService.GetByDate(Created_at);
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


            if (ParsedStartDate > ParsedEndDate)
            {
                return BadRequest("Start date is after end date");
            }

            var items = _medicineService.GetByDateRange(ParsedStartDate, ParsedEndDate);

            return Ok(items);
        }

        [HttpGet]
        [Route("GetbyExpiryDate")]
        public async Task<IActionResult> GetbyExpiryDate(string expirydate)
        {
            try
            {
                // Parse the input string to DateTime
                DateTime parsedDateTime = DateTime.ParseExact(expirydate.Replace("  ", " +"), "yyyy-MM-dd HH:mm:ss.fff zzz",
                                                    System.Globalization.CultureInfo.InvariantCulture);

                // Extract the date part and create a DateOnly object
                DateOnly parsedExpiryDate = DateOnly.FromDateTime(parsedDateTime);

                // Call the service method with the parsed date
                var items = await _medicineService.GetbyExpiryDate(parsedExpiryDate);

                return Ok(items);
            }
            catch (FormatException ex)
            {
                return BadRequest("Invalid date format");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error while processing your request.");
            }

        }
   
        [HttpPost]
        [Route("AddNewMedicine")]
        public IActionResult AddNewMedicine([FromBody] MedicineDto medicineDto)
        {
            var AddNewMedicine = new Medicines
            {
                //Create_at & last_edit value are pre-defined in the model 
                Med_id = Guid.NewGuid(),
                Name = medicineDto.Name,
                Description = medicineDto.Description,
                Stock = medicineDto.Stock,
                Is_active = true
            };

            _medicineService.CreateNew(AddNewMedicine);
            return Ok(AddNewMedicine);
        }

        [HttpPut]
        [Route("UpdateMed")]
        public IActionResult UpdateMed([FromBody] MedicineDto medicineDto)
        {
            var medToUpdate = new Medicines
            {
                Med_id = medicineDto.Med_id,
                Name = medicineDto.Name,
                Description = medicineDto.Description,
                Last_edit = DateTime.Now,
                Stock = medicineDto.Stock
            };
            _medicineService.UpdateMed(medToUpdate);
            return Ok(medToUpdate);
        }

        //TODO update route
        [HttpPut("isActiveUpdate/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] bool is_active)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }
            try
            {
                await _medicineService.UpdateActivation(id, is_active);
                return Ok(new { message = "Status updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete]
        [Route("DeleteMed")]
        public async Task<IActionResult> DeleteMed(Guid med_id)
        {
            await _medicineService.DeleteAsync(med_id);
            return Ok();
        }

        #region search engine to be corrected as noted in service

        [HttpPost]
        [Route("GetQuery")]

        public async Task<IActionResult> GetQuery(string searchedQuery)
        {
            var response = await _medicineService.searchQuery(searchedQuery);
            return Ok(response);
        }
        #endregion
    }
}