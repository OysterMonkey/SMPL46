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
    
    public partial class Public_Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Public_Users()
        {
            this.Public_Feedback = new HashSet<Public_Feedback>();
        }
    
        public decimal User_ID { get; set; }
        public System.DateTime Datetime_Registration { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email_Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Public_Feedback> Public_Feedback { get; set; }
    }
}
