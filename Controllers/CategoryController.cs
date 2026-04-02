using FinanceBackend.DTOs;
using FinanceBackend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;


namespace FinanceBackend.Controllers
{
    [ApiController]
    [Route("api/category")]

    public class CategoryController:ControllerBase
    {
        public readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto categoryDto)
        {
            try
            {
                var createdCategory = _categoryService.CreateCategory(categoryDto);
                return Created("", createdCategory);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            if (categories == null)
                return NotFound();
            return Ok(categories);
        }

        [HttpGet("{categoryType}")]
        public IActionResult GetByCategoryType(string categoryType)
        {
            try
            {
                var categories = _categoryService.GetByCategoryType(categoryType);
                if (categories == null)
                    return NotFound();
                return Ok(categories);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{categoryId}/UsesId")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            try
            {
                var category = _categoryService.GetByCategoryId(categoryId);
                if (category == null)
                    return NotFound();
                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, CreateCategoryDto newCategoryDto)
        {
            try
            {
                var category = _categoryService.UpdateCategory(categoryId, newCategoryDto);
                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}