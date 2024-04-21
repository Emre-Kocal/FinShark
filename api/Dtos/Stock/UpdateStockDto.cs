using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class UpdateStockDto
    {
        [Required]
        [MaxLength(10,ErrorMessage ="Max 10 character")]
        public string Symbol { get; set; }=string.Empty;
        [Required]
        [MinLength(3,ErrorMessage ="Min 3 character")]
        [MaxLength(1000,ErrorMessage ="Max 100 character")]
        public string CompanyName { get; set; }=string.Empty;
        [Required]
        [Range(1,1000000)] 
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001,100)] 
        public decimal LastDiv { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="Min 3 character")]
        [MaxLength(100,ErrorMessage ="Max 100 character")]
        public string Industry { get; set; }=string.Empty;
        [Required]
        [Range(1,1000000000000000)] 
        public long MarketCap { get; set; }
    }
}