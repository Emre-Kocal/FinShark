using System;
using System.Collections.Generic;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync(QueryObject query);
        public Task<Stock?> GetByIdAsync(int id);
        public Task<Stock> CreateAsync(CreateStockDto stockDto);
        public Task<Stock?> UpdateAsync(int id,UpdateStockDto updateStockDto);
        public Task<Stock?> DeleteAsync(int id);
    }
}