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
    
    public partial class ml_user_auth_policy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ml_user_auth_policy()
        {
            this.ml_user = new HashSet<ml_user>();
        }
    
        public int policy_id { get; set; }
        public string policy_name { get; set; }
        public int primary_ldsrv_id { get; set; }
        public Nullable<int> secondary_ldsrv_id { get; set; }
        public int ldap_auto_failback_period { get; set; }
        public byte ldap_failover_to_std { get; set; }
    
        public virtual ml_ldap_server ml_ldap_server { get; set; }
        public virtual ml_ldap_server ml_ldap_server1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ml_user> ml_user { get; set; }
    }
}