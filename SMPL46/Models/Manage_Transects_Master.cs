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
    
    public partial class Manage_Transects_Master
    {
        public int Cleaning_ID { get; set; }
        public string Street_Name { get; set; }
        public string Limits { get; set; }
        public string Ward_Name { get; set; }
        public string District { get; set; }
        public string Freq_Code { get; set; }
        public Nullable<int> Cleaning_Day { get; set; }
        public Nullable<int> Cleaning_Week { get; set; }
        public Nullable<int> Deep_Week { get; set; }
        public Nullable<int> Deep_Day { get; set; }
        public string Category { get; set; }
        public string Land_Use_Desc { get; set; }
        public string Zone_Nr { get; set; }
        public Nullable<double> Length { get; set; }
        public string USRN { get; set; }
        public Nullable<System.DateTime> date_last_modified { get; set; }
        public string user_last_modified { get; set; }
    }
}
