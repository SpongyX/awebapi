﻿using awebapi.DTOs;
using awebapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace awebapi.Services
{
    public class MedicinesService
    {
        private readonly HealthDbContext _healthDbContext;
        public MedicinesService(HealthDbContext healthDbContext)
        {
            _healthDbContext = healthDbContext;
        }
        public void CreateNew(Medicines model)
        {
            _healthDbContext.Medicines.Add(model);
            _healthDbContext.SaveChanges();
        }
        public void UpdateMed(Medicines model)
        {
            try
            {
                var medToUpdate = _healthDbContext.Medicines.Where(x => x.Med_id == model.Med_id).FirstOrDefault();

                if (medToUpdate != null)
                {
                    _healthDbContext.Medicines.Update(model);
                    _healthDbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Product not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all medicines", ex);
            }

        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var toDelete = await _healthDbContext.Medicines.Where(x => x.Med_id == id).FirstOrDefaultAsync();

                if (toDelete != null)
                {
                    _healthDbContext.Medicines.Remove(toDelete);
                    await _healthDbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all medicines", ex);
            }
        }
        public async Task<List<Medicines>> FetchAllAsync()
        {
            try
            {
                var allMed = await _healthDbContext.Medicines.ToListAsync();

                if (allMed != null)
                {
                    return allMed;
                }
                else
                {
                    return new List<Medicines>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all medicines", ex);
            }
        }
        public async Task<Medicines?> FetchByIdAsync(Guid id)
        {
            try
            {
                var medbyId = await _healthDbContext.Medicines.Where(x => x.Med_id == id).FirstOrDefaultAsync();

                if (medbyId != null)
                {
                    return medbyId;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all medicines", ex);
            }
        }
        
        //Not used in frontend (must be corrected to have better auto-complete and result)
        public async Task<List<Medicines>> searchQuery(string model)
        {

            try
            {
                var exactMatching = await _healthDbContext.Medicines.Where(x => x.Description.ToLower() == model.ToLower()).ToListAsync();

                if (exactMatching.Count > 0)
                {
                    return exactMatching;
                }

                // var query = await _healthDbContext.Medicines.Where(t => model.Contains(t.Description)).ToListAsync();

                var query = await _healthDbContext.Medicines.Where(t => t.Description.ToLower().Contains(model.ToLower())).ToListAsync();

                if (query.Count > 0)
                {
                    return query;
                }
                else
                {
                    var allMedicines = _healthDbContext.Medicines.Select(x => x.Description).ToList();
                    var bestMatch = FuzzySharp.Process.ExtractTop(model, allMedicines);

                    if (bestMatch != null)
                    {

                        List<Medicines> finalResult = new List<Medicines>();

                        foreach (var res in bestMatch)
                        {
                            var medResult = _healthDbContext.Medicines.FirstOrDefault(x => x.Description == res.Value);
                            finalResult.Add(medResult);
                        }
                        return finalResult;
                    }
                }
                throw new Exception("No matching medicine found");

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while fetching all medicines", ex);
            }
        }
        public async Task UpdateActivation(Guid id, bool is_active)
        {
            try
            {
                var entity = await _healthDbContext.Medicines.FindAsync(id);
                if (entity != null)
                {
                    entity.Is_active = is_active;
                    await _healthDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }
        public async Task<List<Medicines>> GetByDate(DateTime Created_at)
        {
            try
            {
                var medByDate = await _healthDbContext.Medicines.Where(x => x.Created_at == Created_at).ToListAsync();
                return medByDate ?? new List<Medicines>();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }
        public IEnumerable<Medicines> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _healthDbContext.Medicines.Where(item => item.Created_at >= startDate && item.Created_at <= endDate);
        }
        public async Task<List<Medicines>> GetbyExpiryDate(DateOnly expirydate)
        {
            try
            {
                var res = await _healthDbContext.Medicines
                                 .Where(x => x.Expiry_date < expirydate)
                                 .ToListAsync();

                return res ?? new List<Medicines>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching medicines by expiry date", ex);
            }
        }
        public async Task<List<Medicines>> GetByType(string type)
        {
            try
            {
                var medByType = await _healthDbContext.Medicines.Where(x => x.Type!.ToLower() == type.ToLower()).ToListAsync();
                return medByType ?? new List<Medicines>();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }

    }
}
