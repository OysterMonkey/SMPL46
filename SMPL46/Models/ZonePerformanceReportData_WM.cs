//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMPL46.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZonePerformanceReportData_WM
    {
        public string Zone_Nr { get; set; }
        public string Freq_Code { get; set; }
        public string Std_or_Deep { get; set; }
        public int AOriginalSum { get; set; }
        public Nullable<int> ARectifiedSum { get; set; }
        public Nullable<int> BOriginalSum { get; set; }
        public Nullable<int> BRectifiedSum { get; set; }
        public Nullable<int> COriginalSum { get; set; }
        public Nullable<int> CRectifiedSum { get; set; }
        public Nullable<int> DOriginalSum { get; set; }
        public Nullable<int> DRectifiedSum { get; set; }
        public Nullable<int> UncheckedSum { get; set; }
        public Nullable<int> GroupID { get; set; }
    }
}
