using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

namespace SMPL46.Models
{
    [DelimitedRecord(",")]
    public class Transect
    {
        //[FieldConverter(ConverterKind.Int32)]
        public string Cleaning_ID;

        public string Street_Name;

        public string Limits;

        public string Ward_Name;

        public string District;

        public string Freq_Code;

        //[FieldConverter(ConverterKind.Int32)]
        public string Cleaning_Day;

        //[FieldConverter(ConverterKind.Int32)]
        public string Cleaning_Week;

        //[FieldConverter(ConverterKind.Int32)]
        public string Deep_Week;

        //[FieldConverter(ConverterKind.Int32)]
        public string Deep_Day;

        public string Category;

        public string Land_Use_Desc;

        public string Zone_Nr;

        //[FieldConverter(ConverterKind.Double)]
        public string Length;

        public string USRN;

        public string DUMMY;
    }
}