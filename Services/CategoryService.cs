using System;
using FinanceBackend.Data;
using FinanceBackend.DTOs;
using FinanceBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceBackend.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Category CreateCategory(CreateCategoryDto categoryDto)
        {
            if(categoryDto == null)
                throw new ArgumentException("Invalid Category Data");
            if (string.IsNullOrWhiteSpace(categoryDto.name))
                throw new ArgumentException("Invalid Category Name");

            CategoryType parsedCategoryType;
            if (!Enum.TryParse<CategoryType>(categoryDto.type, true, out parsedCategoryType))
                throw new ArgumentException("Invalid Category Type");

            Category category=new Category();
            category.name = categoryDto.name;
            category.type = parsedCategoryType;

            _applicationDbContext.Categories.Add(category);
            _applicationDbContext.SaveChanges();

            return category;
        }

        public List<Category> GetAllCategories()
        {
            return _applicationDbContext.Categories.ToList();
        }

        public List<Category> GetByCategoryType(string categoryType)
        {
            CategoryType parsedCategoryType;
            if (!Enum.TryParse<CategoryType>(categoryType, true, out parsedCategoryType))
                throw new ArgumentException("Invalid Category Type");

            return _applicationDbContext.Categories.Where(c=>c.type== parsedCategoryType).ToList();
        }

        public Category? GetByCategoryId(int categoryId)
        {
            return _applicationDbContext.Categories.Find(categoryId);
        }

        public Category UpdateCategory(int categoryId,CreateCategoryDto newCategoryDto)
        {
            if (newCategoryDto == null)
                throw new ArgumentException("Invalid Category Data");
            if (string.IsNullOrWhiteSpace(newCategoryDto.name))
                throw new ArgumentException("Invalid Category Name");

            CategoryType parsedCategoryType;
            if (!Enum.TryParse<CategoryType>(newCategoryDto.type, true, out parsedCategoryType))
                throw new ArgumentException("Invalid Category Type");

            Category category = _applicationDbContext.Categories.Find(categoryId);
            if (category == null)
                throw new ArgumentException("Invalid Category Id");
            category.name = newCategoryDto.name;
            category.type = parsedCategoryType;

            _applicationDbContext.SaveChanges();

            return category;
        }
    }
}
