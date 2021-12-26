using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThucPhamSach.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int Page { set; get; }
        public int Count { get; set; }
        public int TotalPages { set; get; }
        public int TotalCount { set; get; }
        public int MaxPage { set; get; }
        public IEnumerable<T> Items { set; get; }
    }
}