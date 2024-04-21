using System;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; }=null;
        public string? CompanyName { get; set; }=null;
        public string? OrderBy { get; set; }
        public bool IsDescending { get; set; }=false;
        public int PageNumber { get; set; }=1;
        public int PageSize { get; set; }=20;
    }
}