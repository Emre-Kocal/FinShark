using System;
using System.Collections.Generic;
using System.Linq;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock model){
            return new StockDto{
                Id=model.Id,
                CompanyName=model.CompanyName,
                Industry=model.Industry,
                LastDiv=model.LastDiv,
                MarketCap=model.MarketCap,
                Purchase=model.Purchase,
                Symbol=model.Symbol,
                Comments=model.Comments.Select(x=>x.ToCommentDto()).ToList()
            };
        }
        public static Stock FromCreateStockDto(this CreateStockDto stockDto){
            return new Stock{
                CompanyName=stockDto.CompanyName,
                Industry=stockDto.Industry,
                LastDiv=stockDto.LastDiv,
                MarketCap=stockDto.MarketCap,
                Purchase=stockDto.Purchase,
                Symbol=stockDto.Symbol
            };
        }
        public static Stock ToUpdateStockDto(this UpdateStockDto updateStockDto){
            return new Stock{
                CompanyName=updateStockDto.CompanyName,
                Industry=updateStockDto.Industry,
                LastDiv=updateStockDto.LastDiv,
                MarketCap=updateStockDto.MarketCap,
                Purchase=updateStockDto.Purchase,
                Symbol=updateStockDto.Symbol
            };
        }
    }
}