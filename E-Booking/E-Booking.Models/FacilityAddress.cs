using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EBooking.Models
{
    public class FacilityAddress
    {
        public int FacilityID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string FullAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Easting { get; set; }
        public string Northing { get; set; }
        public string GridReference { get; set; }
        public string UPRN { get; set; }
        public string PostCode { get; set; }
        public string TownName { get; set; }
        public string CountryName { get; set; }
        public string FacilityPhoneNo { get; set; }
        //private static DataSet ds = new DataSet();

        

    }
}
