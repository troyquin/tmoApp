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
    
    public partial class ccy
    {
        public string ccy_code { get; set; }
        public string ccy_name { get; set; }
        public Nullable<short> ccy_decm_plc { get; set; }
        public Nullable<short> ccy_ind { get; set; }
        public Nullable<short> ccy_quote_unit { get; set; }
        public Nullable<short> ccy_seq { get; set; }
        public string ccy_internal_code { get; set; }
        public Nullable<short> ccy_cyclic_no { get; set; }
        public string ccy_user_id { get; set; }
        public Nullable<System.DateTime> ccy_amend_date { get; set; }
        public string ccy_state { get; set; }
        public int unique_id { get; set; }
    }
}
