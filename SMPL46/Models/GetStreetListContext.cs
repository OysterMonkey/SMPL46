using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMPL46.Models
{
    public class GetStreetListContext
    {
        public string Ward_Name { get; set; }
        public string District { get; set; }
        public string Street_Name { get; set; }
        public string Limits { get; set; }
        public string streetsection { get; set; }
    }
}