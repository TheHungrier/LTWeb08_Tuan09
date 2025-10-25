using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bai3_LTWeb.Models
{
    public class NV_PBViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public int DepId { get; set; }
        public string DepName { get; set; }
    }
}