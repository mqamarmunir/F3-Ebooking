using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Runtime.Caching;

namespace BusinessRules
{
    [System.ComponentModel.DataObject]
    public class StaticCache
    {
        
        public static void LoadStaticCache()
        {
            // Get suppliers - cache using application state
            //SuppliersBLL suppliersBLL = new SuppliersBLL();
           // HttpContext.Current.Application["key"] = GetDropDownsData();
            //System.Web.HttpContext.Current.Items.Add("key", GetDropDownsData());
            //var abc=System.Web.HttpContext.Current.Cache.Add("mykey", GetDropDownsData(),null,System.DateTime.Now.AddSeconds(15),TimeSpan.Zero,CacheItemPriority.Normal,CacheItemRemoved);
            System.Web.HttpContext.Current.Cache.Insert("mykey", GetDropDownsData());
        }

        private static void CacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
        {
            //throw new NotImplementedException();
        }

        private static DataSet GetDropDownsData()
        {
            DataSet ds = new DataSet();
            clsEBooking objBooking = new clsEBooking();
            string TableNames = @"tblHospitalDepartment,Title,tblAppointmentTime,RelationShip,InfectionType,Position,Facility,FacilityType,CancelRejectReason,Facility,FacilityType,ClinicianRole,AppointmentDuration,EscortType,Escort,TransportReason,TransportRequirement,Position,ServiceType,SystemSetting,FacilityAddress,GPPractice";
            ds = objBooking.LoadCache(TableNames);
            return ds;
        }
       // [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DataSet GetDropDowns(bool BypassCache)
        {
            ObjectCache cache = MemoryCache.Default;
            object cacheitem = cache["mykey"] as DataSet;
            if ((BypassCache) || cacheitem == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                cacheitem = GetDropDownsData();
                cache.Set("mykey", cacheitem,policy);
            }
            return cacheitem as DataSet;
            //string cacheKey = "mykey";
            ////object cacheItem = System.Web.HttpContext.Current.Cache[cacheKey] as DataSet;
            //if ((BypassCache) || (System.Web.HttpContext.Current.Cache[cacheKey] == null) || System.Web.HttpContext.Current==null)
            //{
            //   // cacheItem = GetDropDownsData();
            //    try
            //    {
            //        System.Web.HttpContext.Current.Cache.Insert(cacheKey, GetDropDownsData(), null,
            //       System.Web.Caching.Cache.NoAbsoluteExpiration,
            //       TimeSpan.FromSeconds(60));
            //    }
            //    catch (NullReferenceException ee)
            //    {
            //        HttpContextWrapper context =
            //new HttpContextWrapper(HttpContext.Current);
            //        context.Cache.Insert(cacheKey, GetDropDownsData(), null,
            //       System.Web.Caching.Cache.NoAbsoluteExpiration,
            //       TimeSpan.FromSeconds(60));
            //    }



            //}
            //return (DataSet)System.Web.HttpContext.Current.Cache[cacheKey];
        }
        //public DataSet GetSystemSettings(bool ByPassCache)
        //{
        //    ObjectCache cache = MemoryCache.Default;
        //    object cacheitem = cache["SystemSettingsCachekey"] as DataSet;
        //    if ((ByPassCache) || cacheitem == null)
        //    {
        //        CacheItemPolicy policy = new CacheItemPolicy();
        //        cacheitem = GetDropDownsData();
        //        cache.Set("SystemSettingsCachekey", cacheitem, policy);
        //    }
        //    return cacheitem as DataSet;
        //}

        //private static DataSet GetSystemSettings()
        //{
        //    DataSet ds = new DataSet();
        //    clsEBooking objBooking = new clsEBooking();
        //    //string TableNames = @"tblHospitalDepartment,Title,tblAppointmentTime,RelationShip,InfectionType,Position,Facility,FacilityType,CancelRejectReason,Facility,FacilityType,ClinicianRole,AppointmentDuration,EscortType,Escort,TransportReason,TransportRequirement,Position,ServiceType";
        //    ds = objBooking.LoadSystemSettings();
        //    return ds;
        //}
        ////public static void LoadStaticCache(string keyvalue)
        //{
        //    var abc = System.Web.HttpContext.Current.Cache.Add("mykey"+keyvalue, GetDropDownsData(), null, System.DateTime.Now.AddMinutes(2), TimeSpan.Zero, CacheItemPriority.Normal, CacheItemRemoved);
        //}
    }
}
