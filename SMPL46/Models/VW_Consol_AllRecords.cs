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
    
    public partial class VW_Consol_AllRecords
    {
        public int Category { get; set; }
        public string Ward_name { get; set; }
        public string Street_Name { get; set; }
        public string Cleaning_ID { get; set; }
        public Nullable<System.DateTime> Scheduled_Cleaning_Date { get; set; }
        public Nullable<System.DateTime> InitialScheduled_Cleaning_Date { get; set; }
        public string Inspected_by { get; set; }
        public string InitialInspected_by { get; set; }
        public Nullable<System.DateTime> InitialDate { get; set; }
        public string InitialGrade { get; set; }
        public string InitialReasonCode { get; set; }
        public byte[] InitialPhoto { get; set; }
        public string InitialInspection_Comments { get; set; }
        public string Inspection_Comments { get; set; }
        public Nullable<System.DateTime> ReInspectionScheduled_Cleaning_Date { get; set; }
        public Nullable<System.DateTime> ReInspectionDate { get; set; }
        public string ReInspectionGrade { get; set; }
        public string ReInspectionReasonCode { get; set; }
        public byte[] ReInspectionPhoto { get; set; }
        public string ReInspection_Comments { get; set; }
        public string ReInspectionInspected_by { get; set; }
        public string Limits { get; set; }
        public string District { get; set; }
        public string Freq_Code { get; set; }
        public string Std_or_Deep { get; set; }
        public System.Guid InitialpkUID { get; set; }
        public Nullable<System.Guid> ReInspectionpkUID { get; set; }
    }
}
