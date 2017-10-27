using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMPL46.Models
{
    public class ManageTransectsHistoryModel
    {
        public int Manage_Transects_Audit_ID { get; set; }
        public string User_ID { get; set; }
        public string action { get; set; }
        public DateTime date_action { get; set; }
        public int Orig_Cleaning_ID { get; set; }
        public string Orig_Street_Name { get; set; }
        public string Orig_Limits { get; set; }
        public string Orig_Ward_Name { get; set; }
        public string Orig_District { get; set; }
        public string Orig_Freq_Code { get; set; }
        public Nullable<int> Orig_Cleaning_Day { get; set; }
        public Nullable<int> Orig_Cleaning_Week { get; set; }
        public Nullable<int> Orig_Deep_Week { get; set; }
        public Nullable<int> Orig_Deep_Day { get; set; }
        public string Orig_Category { get; set; }
        public string Orig_Land_Use_Desc { get; set; }
        public string Orig_Zone_Nr { get; set; }
        public Nullable<double> Orig_Length { get; set; }
        public string Orig_USRN { get; set; }
        public int New_Cleaning_ID { get; set; }
        public string New_Street_Name { get; set; }
        public string New_Limits { get; set; }
        public string New_Ward_Name { get; set; }
        public string New_District { get; set; }
        public string New_Freq_Code { get; set; }
        public Nullable<int> New_Cleaning_Day { get; set; }
        public Nullable<int> New_Cleaning_Week { get; set; }
        public Nullable<int> New_Deep_Week { get; set; }
        public Nullable<int> New_Deep_Day { get; set; }
        public string New_Category { get; set; }
        public string New_Land_Use_Desc { get; set; }
        public string New_Zone_Nr { get; set; }
        public Nullable<double> New_Length { get; set; }
        public string New_USRN { get; set; }
    }
}