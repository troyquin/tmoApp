//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tmoWebAPI3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class stdn
    {
        public string stdn_prod_id { get; set; }
        public string stdn_ccy_code { get; set; }
        public double stdn_demn_price { get; set; }
        public string stdn_denm_class { get; set; }
        public Nullable<short> stdn_cyclic_no { get; set; }
        public string stdn_user_id { get; set; }
        public Nullable<System.DateTime> stdn_amend_date { get; set; }
        public string stdn_state { get; set; }
        public int unique_id { get; set; }
    }
}
