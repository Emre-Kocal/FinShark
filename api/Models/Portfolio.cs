using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public int AppUserId { get; set; }
        public int StockId { get; set; }
        public AppUser AppUser { get; set; }=null;
        public Stock Stock { get; set; }
    }
}