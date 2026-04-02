using FinanceBackend.DTOs;
using FinanceBackend.Models;
using FinanceBackend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinanceBackend.Controllers
{
    [ApiController]
    [Route("api/FinancialRecord")]

    public class FinancialRecordController : ControllerBase
    {
        private readonly FinancialRecordService _financialRecordService;

        public FinancialRecordController(FinancialRecordService financialRecordService)
        {
            _financialRecordService = financialRecordService;
        }

        [HttpPost]
        public IActionResult CreateFinancialRecord(CreateFinancialRecordDto record)
        {
            try
            {
                var financialRecord = _financialRecordService.CreateFinancialRecord(record);
                return Created("", financialRecord);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("financialRecordId")]
        public IActionResult GetFinancialRecordById(int financialRecordId)
        {
            var financialRecord = _financialRecordService.GetFinancialRecordById(financialRecordId);
            if (financialRecord == null) 
                return NotFound();
            return Ok(financialRecord);
        }

        [HttpGet("userId")]
        public IActionResult GetFinancialRecordsByUserId(int userId)
        {
            try
            {
                var financialRecord = _financialRecordService.GetFinancialRecordsByUserId(userId);
                if (financialRecord == null)
                    return NotFound();
                return Ok(financialRecord);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("categoryId")]
        public IActionResult GetFinancialRecordsByCategoryId(int categoryId)
        {
            try
            {
                var financialRecord = _financialRecordService.GetFinancialRecordsByCategoryId(categoryId);
                if (financialRecord == null)
                    return NotFound();
                return Ok(financialRecord);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("minInclusive/maxInclusive")]
        public IActionResult GetFinancialRecordsByDateRange(DateTime minInclusive, DateTime maxInclusive)
        {
            try
            {
                var financialRecord = _financialRecordService.GetFinancialRecordsByDateRange(minInclusive, maxInclusive);
                if (financialRecord == null)
                    return NotFound();
                return Ok(financialRecord);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("categoryType")]
        public IActionResult GetFinancialRecordsByCategoryType(string categoryType)
        {
            try
            {
                var financialRecord = _financialRecordService.GetFinancialRecordsByCategoryType(categoryType);
                if (financialRecord == null)
                    return NotFound();
                return Ok(financialRecord);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateFinancialRecord(int financialRecordId, CreateFinancialRecordDto record)
        {
            try
            {
                var financialRecord = _financialRecordService.UpdateFinancialRecord(financialRecordId, record);
                return Ok(financialRecord);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
