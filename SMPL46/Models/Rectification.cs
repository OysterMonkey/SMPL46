using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMPL46.Models
{
    using System;
    using System.Collections.Generic;

    public class Rectification
    {
        public int seq_id { get; set; }
        public Guid pkuid { get; set; }
        public DateTime inspected_date { get; set; }
        public string ward_name { get; set; }
        public string colour { get; set; }
        public string freq_code { get; set; }
        public string street_name { get; set; }
        public string limits { get; set; }
        public string grade { get; set; }
        public string reason_code { get; set; }
        public string rectification { get; set; }
        public int disable_twiceweekly { get; set; }
    }

    //public class Rectification : IEnumerable
    //{
    //    public int seq_id { get; set; }
    //    public Guid pkuid { get; set; }
    //    public DateTime inspected_date { get; set; }
    //    public string ward_name { get; set; }
    //    public string colour { get; set; }
    //    public string freq_code { get; set; }
    //    public string street_name { get; set; }
    //    public string limits { get; set; }
    //    public string grade { get; set; }
    //    public string reason_code { get; set; }
    //    public string rectification { get; set; }
    //    public int disable_twiceweekly { get; set; }

    //    public IEnumerator GetEnumerator()
    //    {
    //        yield return seq_id;
    //        yield return pkuid;
    //        yield return inspected_date;
    //        yield return ward_name;
    //        yield return colour;
    //        yield return freq_code;
    //        yield return street_name;
    //        yield return limits;
    //        yield return grade;
    //        yield return reason_code;
    //        yield return rectification;
    //        yield return disable_twiceweekly;

    //    }
    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return this.GetEnumerator();
    //    }
    //}
}