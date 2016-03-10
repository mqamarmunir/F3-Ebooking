using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EBooking.Models;
using System.Data;

namespace BusinessRules
{
    public class BLFacilityAddress
    {
        private static DataSet ds = new DataSet();

        private dynamic GetAllAddresses()
        {

            List<FacilityAddress> lsFacilityAddress = new List<FacilityAddress>();
            //FacilityAddress fa = new FacilityAddress();
            
            StaticCache cache = new StaticCache();
           // DataSet ds = new DataSet();
            ds = cache.GetDropDowns(false);
            Dropdown ddhelper = new Dropdown();
            int tableindex = ddhelper.GetTableIndex("FacilityAddress", ds);
            if (tableindex != -1)
            {
                foreach (DataRow dr in ds.Tables[tableindex].Rows)
                {
                    FacilityAddress fa = new FacilityAddress();
                    fa.FacilityID = Convert.ToInt32(dr["FacilityID"].ToString());
                    fa.AddressLine1 = dr["AddressLine1"].ToString();
                    fa.AddressLine2 = dr["AddressLine2"].ToString();
                    fa.AddressLine3 = dr["AddressLine3"].ToString();
                    fa.AddressLine4 = dr["AddressLine4"].ToString();
                    fa.FullAddress = dr["FullAddress"].ToString();
                    fa.Latitude = dr["Latitude"].ToString();
                    fa.Longitude = dr["Longitude"].ToString();
                    fa.Easting = dr["Easting"].ToString();
                    fa.Northing = dr["Northing"].ToString();
                    fa.GridReference = dr["GridReference"].ToString();

                    fa.UPRN = dr["URPN"].ToString();
                    fa.PostCode = dr["PostCode"].ToString();
                    fa.TownName = dr["TownName"].ToString();
                    fa.CountryName = dr["CountyName"].ToString();
                    fa.FacilityPhoneNo = dr["FacilityPhoneNo"].ToString().Trim();

                    lsFacilityAddress.Add(fa);
                }
            }
            return lsFacilityAddress;
        }

        public dynamic GetFacilityAddress(int Facilityid, bool isFacilityDepartmentID)
        {

            List<FacilityAddress> lsfa = GetAllAddresses();

            if (isFacilityDepartmentID)
            {
                 Dropdown ddhelper = new Dropdown();
                int tableindex = ddhelper.GetTableIndex("tblHospitalDepartment", ds);
                DataView dv = new DataView(ds.Tables[tableindex], "FacilityDepartmentID=" + Facilityid, "FacilityID", DataViewRowState.CurrentRows);
                ds.Dispose();
                if (dv.Count == 0)
                {

                    return false;
                }
                Facilityid = Convert.ToInt16(dv[0]["FacilityID"].ToString().Trim());

            }
            ds.Dispose();
            //var xxx = lsfa.Find(x => x.FacilityID.Equals(Facilityid));
            return lsfa.Find(x => x.FacilityID.Equals(Facilityid));
        }
    }
}
