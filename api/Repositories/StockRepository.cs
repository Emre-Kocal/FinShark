using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        public readonly ApplicationDbContext context;

        public StockRepository(ApplicationDbContext _context)
        {
            context=_context;
        }

        public async Task<Stock> CreateAsync(CreateStockDto stockDto)
        {
            var model=stockDto.FromCreateStockDto();
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var model=await context.Stocks.FindAsync(id);
            if (model==null)
            {
                return null;
            }
            context.Remove(model);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks= context.Stocks.Include(x=>x.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks=stocks.Where(x=>x.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks=stocks.Where(x=>x.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                if (query.OrderBy=="Symbol")
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(x=>x.Symbol) : stocks.OrderBy(x=>x.Symbol) ; 
                }
                else if (query.OrderBy=="Company Name")
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(x=>x.CompanyName) : stocks.OrderBy(x=>x.CompanyName) ; 
                }
            }
            return await stocks.Skip((query.PageNumber-1)*query.PageSize).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var model=await context.Stocks.Include(x=>x.Comments).FirstOrDefaultAsync(x=>x.Id==id);
            if (model==null)
            {
                return null;
            }
            return model;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto)
        {
            var model=await context.Stocks.FindAsync(id);
            if (model==null)
            {
                return null;
            }
            model.Symbol=updateStockDto.Symbol;
            model.CompanyName=updateStockDto.CompanyName;
            model.Industry=updateStockDto.Industry;
            model.Purchase=updateStockDto.Purchase;
            model.MarketCap=updateStockDto.MarketCap;
            model.LastDiv=updateStockDto.LastDiv;
            await context.SaveChangesAsync();
            return model;
        }
    }
}