using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using SMPL46.Models;

namespace SMPL46.ViewModels
{
    public class RectificationEditorViewModel 
    {
        //public RectificationEditorViewModel ()
        //{
        //    Selected = false;
        //    Enabled = true;
        //    BackgroundColour = Color.White;
        //    Rectifications = new List<Rectification>();
        //}

        public bool Selected { get; set; }
        public bool Enabled { get; set; }
        public string HiddenEnabled { get; set; }
        public string BackgroundColour { get; set; }

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

        //public List<Rectification> Rectifications { get; set; }
    }
}