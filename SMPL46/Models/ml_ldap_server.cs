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
    
    public partial class ml_ldap_server
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ml_ldap_server()
        {
            this.ml_user_auth_policy = new HashSet<ml_user_auth_policy>();
            this.ml_user_auth_policy1 = new HashSet<ml_user_auth_policy>();
        }
    
        public int ldsrv_id { get; set; }
        public string ldsrv_name { get; set; }
        public string search_url { get; set; }
        public string access_dn { get; set; }
        public string access_dn_pwd { get; set; }
        public string auth_url { get; set; }
        public byte num_retries { get; set; }
        public int timeout { get; set; }
        public byte start_tls { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ml_user_auth_policy> ml_user_auth_policy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ml_user_auth_policy> ml_user_auth_policy1 { get; set; }
    }
}
