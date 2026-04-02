using FinanceBackend.Controllers;
using FinanceBackend.Data;
using FinanceBackend.DTOs;
using FinanceBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinanceBackend.Services
{
    public class FinancialRecordService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FinancialRecordService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public FinancialRecord CreateFinancialRecord(CreateFinancialRecordDto record)
        {
            if (record == null)
                throw new ArgumentException("Invalid Financial Record");
            if (_applicationDbContext.Users.Find(record.userId) == null)
                throw new ArgumentException("Invalid User Id");
            if (_applicationDbContext.Categories.Find(record.categoryId) == null)
                throw new ArgumentException("Invalid Category Id");
            if (record.amount <= 0)
                throw new ArgumentException("Amount should be Greater than 0");
            if (record.date == default(DateTime))
                throw new ArgumentException("Date can't be a default one");
            if (record.date > DateTime.Now)
                throw new ArgumentException("Date should not be in Future");
            
            FinancialRecord newFinancialRecord= new FinancialRecord();
            newFinancialRecord.userId = record.userId;
            newFinancialRecord.categoryId = record.categoryId;
            newFinancialRecord.amount = record.amount;
            newFinancialRecord.date= record.date;
            newFinancialRecord.note = record.note;
            newFinancialRecord.createdAt=DateTime.Now;

            _applicationDbContext.FinancialRecords.Add(newFinancialRecord);
            _applicationDbContext.SaveChanges();

            return newFinancialRecord;
        }

        public FinancialRecord? GetFinancialRecordById(int financialRecordId)
        {
            return _applicationDbContext.FinancialRecords.Find(financialRecordId);
        }

        public List<FinancialRecord> GetFinancialRecordsByUserId(int userId)
        {
            if (_applicationDbContext.Users.Find(userId) == null)
                throw new ArgumentException("Invalid User Id");
            return _applicationDbContext.FinancialRecords.Where(u=>u.userId==userId).ToList();
        }
        public List<FinancialRecord> GetFinancialRecordsByCategoryId(int categoryId)
        {
            if (_applicationDbContext.Categories.Find(categoryId) == null)
                throw new ArgumentException("Invalid Category Id");
            return _applicationDbContext.FinancialRecords.Where(u => u.categoryId == categoryId).ToList();
        }

        public List<FinancialRecord> GetFinancialRecordsByDateRange(DateTime minInclusive, DateTime maxInclusive)
        {
            if (minInclusive > maxInclusive)
                throw new ArgumentException("Invalid Date Range");
            
            return _applicationDbContext.FinancialRecords.Where(d=>d.date>=minInclusive && d.date<=maxInclusive).ToList(); 
        }

        public List<FinancialRecord> GetFinancialRecordsByCategoryType(string categoryType)
        {
            if (string.IsNullOrWhiteSpace(categoryType))
                throw new ArgumentException("Category type can't be empty");
            CategoryType parsedCategoryType;
            if (!Enum.TryParse<CategoryType>(categoryType, true, out parsedCategoryType))
                throw new ArgumentException("Invalid Category Type");

            var records=_applicationDbContext.FinancialRecords.Include(r=>r.Category).Where(r=>r.Category.type==parsedCategoryType).ToList();
            return records;
        }

        public FinancialRecord UpdateFinancialRecord(int financialRecordId, CreateFinancialRecordDto record)
        {
            if (record == null)
                throw new ArgumentException("Invalid Financial Record");
            if (_applicationDbContext.Users.Find(record.userId) == null)
                throw new ArgumentException("Invalid User Id");
            if (_applicationDbContext.Categories.Find(record.categoryId) == null)
                throw new ArgumentException("Invalid Category Id");
            if (record.amount <= 0)
                throw new ArgumentException("Amount should be Greater than 0");
            if (record.date == default(DateTime))
                throw new ArgumentException("Date can't be a default one");
            if (record.date > DateTime.Now)
                throw new ArgumentException("Date should not be in Future");

            FinancialRecord previousFinancialRecord = _applicationDbContext.FinancialRecords.Find(financialRecordId);
            if (previousFinancialRecord == null)
                throw new ArgumentException("Invalid Financial Record Id");

            previousFinancialRecord.categoryId = record.categoryId;
            previousFinancialRecord.amount = record.amount;
            previousFinancialRecord.date = record.date;
            previousFinancialRecord.note = record.note;

            _applicationDbContext.SaveChanges();
            return previousFinancialRecord;
        }
    }
}
