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
    
    public partial class ConMon_Schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConMon_Schedule()
        {
            this.ConMonInspection_notice = new HashSet<ConMonInspection_notice>();
        }
    
        public string ID { get; set; }
        public string Grade { get; set; }
        public string Ward_Name { get; set; }
        public string District { get; set; }
        public string Street_Name { get; set; }
        public string Limits { get; set; }
        public string Zone_Nr { get; set; }
        public string Cleaning_Day { get; set; }
        public string Freq_Code { get; set; }
        public string Std_or_Deep { get; set; }
        public Nullable<float> Length { get; set; }
        public Nullable<System.DateTime> Sched_Cleaning_Date_Week_Commencing { get; set; }
        public string Inspected_By { get; set; }
        public Nullable<System.DateTime> Inspected_Date { get; set; }
        public byte[] Inspection_Photo { get; set; }
        public string Reason_Code { get; set; }
        public string Inspection_Comments { get; set; }
        public Nullable<double> GPS_Northing { get; set; }
        public Nullable<double> GPS_Easting { get; set; }
        public string Seen_by_Public { get; set; }
        public System.Guid pkUID { get; set; }
        public System.DateTime last_modified { get; set; }
        public int record_id { get; set; }
        public Nullable<int> Seq_ID { get; set; }
        public Nullable<System.Guid> CleaningSchedulePKUID { get; set; }
        public Nullable<System.Guid> RectificationPKUID { get; set; }
        public string IsRectification { get; set; }
        public string random_inspection { get; set; }
        public string Land_Use_Desc { get; set; }
        public string OriginalReasonCode { get; set; }
        public Nullable<System.DateTime> submission_datetime { get; set; }
        public string submission_UID { get; set; }
        public string submission_source { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConMonInspection_notice> ConMonInspection_notice { get; set; }
    }
}
