using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
//using Common;
//using BusinessRule.wsCommon;
using System.IO;

namespace BusinessRules
{
    /// Put your comments here
    public class clsAddress
    {
        //public event GetAddressFromPhoneNoCompletedEventHandler OnPhoneNoCompleted;

        private static clsAddress objclsAddress = null;
        public static clsAddress Obj
        {
            get
            {
                if (objclsAddress == null)
                    objclsAddress = new clsAddress();

                return objclsAddress;
            }
        }
        #region -- Enum --
        /// <summary>
        /// Methods of clsBank.
        /// </summary>
        /// 
        
        public enum eclsAddressMethod
        {
            Save_Int,
            Delete_Bool,
            GetRecords_List
        }
        #endregion


        public struct structAddress
        {
            public double Latitude;
            public double Longitude;

            public double Easting;
            public double Northing;

            public string OSGridReference;
            public string Error;
            public int AddressShowingFormat;
            public string DistanceUnit;

        }

        public struct MapPointStreetAddress
        {
            public string Street;
            public string PostalCode;
            public string City;
            public string OtherCity;
            public string Region;
            public string Country;







        }

        /// <summary>
        /// Get Lat  / Long Values for Given Easting / Northing
        /// </summary>
        /// <param name="pEasting"></param>
        /// <param name="pNorthing"></param>
        /// <returns></returns>
        public structAddress GetLatLongAndGridReference(double pEasting, double pNorthing)
        {
            structAddress objStructAddress = new structAddress();
            double Lat;
            double Long;

            try
            {

                objStructAddress.Easting = pEasting;
                objStructAddress.Northing = pNorthing;

                GT_OSGB objGT_OSGB = new GT_OSGB();

                #region Set Lat / Long

                objGT_OSGB.setGridCoordinates(pEasting, pNorthing);
                objGT_OSGB.getWGS84(out Lat, out Long);

                objStructAddress.Latitude = Lat;
                objStructAddress.Longitude = Long;
                #endregion


                #region Set Grid Ref
                objGT_OSGB.setGridCoordinates(pEasting, pNorthing);
                objStructAddress.OSGridReference = objGT_OSGB.getGridRef(5);
                #endregion

                return objStructAddress;
            }
            catch (Exception ex)
            {
                objStructAddress.Latitude = 0.0;
                objStructAddress.Longitude = 0.0;
                objStructAddress.Easting = 0;
                objStructAddress.Northing = 0;
                objStructAddress.OSGridReference = string.Empty;
                objStructAddress.Error = ex.Message + ex.InnerException;
                return objStructAddress;
            }

        }

        public structAddress GetEastingWestingAndGridReference(double lat, double lng)
        {
            structAddress objStructAddress = new structAddress();
            try
            {
                objStructAddress.Latitude = lat;
                objStructAddress.Longitude = lng;

                GT_WGS84 objGT_WGS84 = new GT_WGS84();
                #region Easting Northing.
                objGT_WGS84.setDegrees(lat, lng);
                double Easting;
                double Northing;
                objGT_WGS84.getOSGB(out Easting, out Northing);
                objStructAddress.Easting = Convert.ToInt32(Easting);
                objStructAddress.Northing = Convert.ToInt32(Northing);
                #endregion


                #region GridReference
                GT_OSGB objGT_OSGB = new GT_OSGB();
                objGT_OSGB.setGridCoordinates(Easting, Northing);

                objStructAddress.OSGridReference = objGT_OSGB.getGridRef(5);

                return objStructAddress;
                #endregion

            }
            catch (Exception ex)
            {
                objStructAddress.Latitude = 0.0;
                objStructAddress.Longitude = 0.0;
                objStructAddress.Easting = 0;
                objStructAddress.Northing = 0;
                objStructAddress.OSGridReference = string.Empty;
                objStructAddress.Error = ex.Message + ex.InnerException;
                return objStructAddress;
            }

        }
        
        
        //#region -- Delegate --

        ///// <summary>
        ///// Delegate for Asynchronous Methods of clsBank.
        ///// </summary>
        ///// <param name="penmBankMethod"></param>
        ///// <param name="pobjReturnValue"></param>
        //public delegate void dlgOnclsAddressMethodCompleted(eclsAddressMethod penmAddressMethod, object pobjReturnValue);

        //#endregion

        //#region -- Constants --

        ///// <summary>
        ///// Constant Table Name
        ///// </summary>
        //public const string cstrTableName = "tblAddress";

        //private static DataTable mobjAddressTable = new DataTable(cstrTableName);

        //public static string cstrAddressIDCol = "AddressID";
        //public static string cstrAddressLine1Col = "AddressLine1";
        //public static string cstrAddressLine2Col = "AddressLine2";
        //public static string cstrAddressLine3Col = "AddressLine3";
        //public static string cstrAddressLine4Col = "AddressLine4";
        //public static string cstrLongitudeCol = "Longitude";
        //public static string cstrLatitudeCol = "Latitude";
        //public static string cstrCityIDCol = "CityID";
        //public static string cstrStateOrProvinceIDCol = "StateOrProvinceID";
        //public static string cstrCountryIDCol = "CountryID";
        //public const string cstrPostCodeCol = "PostCodeID";
        //public const string cstrAreaCol = "AreaID";
        //public const string cstrIsPublicAddressCol = "IsPublicAddress";
        //private static string cstrInsertedByCol = "InsertedBy";
        //private static string cstrUpdatedByCol = "UpdatedBy";
        //private static string cstrInsertedAtCol = "InsertedAt";
        //private static string cstrUpdatedAtCol = "UpdatedAt";

        //private static string cstrPublicAddressTypeIDCol = "PublicAddressTypeID";
        //private static string cstrPublicAddressTitleCol = "PublicAddressTitle";
        //private static string cstrPublicAddressCodeCol = "PublicAddressCode";

        //private static string mstrReferenceIDCol = "ReferenceID";

        //private static string cstrIsPrimaryCol = "IsPrimary";
        //private static string cstrCountryCol = "Country";
        //private static string cstrCityNameCol = "CityName";
        //private static string cstrStateOrProvinceCol = "StateOrProvince";

        //private static int mintReferenceID = 0;

        //private wsCommon.wsCommon mobjWSCommon = null;
        //#endregion

        //#region -- Private Fields --

        //private int mintAddressID;


        //private int mintAddressPointID;


        //private string mstrWsBaseUrl = string.Empty;
        //private double mdblLatitude = 0.0;

        //private double mdblLongitude = 0.0;

        //private string mstrAddressLine1;

        //private string mstrAddressLine2;

        //private string mstrAddressLine3;

        //private string mstrAddressLine4;

        //private int mintCityID;

        //private int mintStateOrProvinceID;

        //private int mintCountryID;

        //private int mintPostCode;

        //private int mintSubPostCode;

        //private int mintArea;

        //private bool mblnIsPublicAddress;

        //private String publicAddressTitle = String.Empty;

        //private Int32 publicAddressTypeId = 0;

        //public Int32 PublicAddressTypeId
        //{
        //    get { return publicAddressTypeId; }
        //    set { publicAddressTypeId = value; }
        //}


        //private Int32 _zoneID = 0;

        //public Int32 ZoneID
        //{
        //    get { return _zoneID; }
        //    set { _zoneID = value; }
        //}


        //private String publicAddressType = String.Empty;

        //public String PublicAddressType
        //{
        //    get { return publicAddressType; }
        //    set { publicAddressType = value; }
        //}


        //public String PublicAddressTitle
        //{
        //    get { return publicAddressTitle; }
        //    set { publicAddressTitle = value; }
        //}

        //private String publicAddressCode = String.Empty;

        //public String PublicAddressCode
        //{
        //    get { return publicAddressCode; }
        //    set { publicAddressCode = value; }
        //}

        //private int mintInsertedBy;

        //private int mintUpdatedBy;

        //private System.DateTime mdtmInsertedAt;

        //private System.DateTime mdtmUpdatedAt;

        //private wsCall.wsCall mobjWSAddress = null;

        ////private clsMapClientAddress mobjMapAddress = null;

        //private clsPublicAddress mobjPublicAddress = null;

        //private List<clsAddressPoint> addressPoints = new List<clsAddressPoint>(0);
        //private bool mIsPrimary = false;
        //private string mcountryNameStr = string.Empty;
        //private string mStateStr = string.Empty;
        //private string mCityStr = string.Empty;




        //#endregion

        //#region -- Events --

        //public event dlgOnclsAddressMethodCompleted mevtOnMethodCompleted = null;

        //#endregion

        //#region -- Constructor --

        //static clsAddress()
        //{
        //    mobjAddressTable.Columns.Add(mstrReferenceIDCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrAddressIDCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrAddressLine1Col, typeof(System.String));
        //    mobjAddressTable.Columns.Add(cstrAddressLine2Col, typeof(System.String));
        //    mobjAddressTable.Columns.Add(cstrAddressLine3Col, typeof(System.String));
        //    mobjAddressTable.Columns.Add(cstrAddressLine4Col, typeof(System.String));
        //    mobjAddressTable.Columns.Add(cstrLongitudeCol, typeof(System.Double));
        //    mobjAddressTable.Columns.Add(cstrLatitudeCol, typeof(System.Double));
        //    mobjAddressTable.Columns.Add(cstrCityIDCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrStateOrProvinceIDCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrCountryIDCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrPostCodeCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrAreaCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrIsPublicAddressCol, typeof(System.Boolean));
        //    mobjAddressTable.Columns.Add(cstrInsertedByCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrUpdatedByCol, typeof(System.Int32));
        //    mobjAddressTable.Columns.Add(cstrInsertedAtCol, typeof(System.DateTime));
        //    mobjAddressTable.Columns.Add(cstrUpdatedAtCol, typeof(System.DateTime));
        //}

        //private BusinessRule.wsCall.wsCall WSAddressObj
        //{
        //    get
        //    {
        //        if (mobjWSAddress == null)
        //        {
        //            //initialize web service object
        //            mobjWSAddress = new BusinessRule.wsCall.wsCall();


        //        }

        //        mobjWSAddress.UsernamePasswordHeader = new BusinessRule.wsCall.UsernamePasswordSoapHeader();
        //        mobjWSAddress.UsernamePasswordHeader.Token = Common.Credentials.Current.TokenXml;

        //        //register required events.
        //        //mobjWSAddress.AddressDeleteCompleted += new BusinessRule.wsCall.AddressDeleteCompletedEventHandler(mobjWSAddress_AddressDeleteCompleted);
        //        //mobjWSAddress.AddressUpdateCompleted += new BusinessRule.wsCall.AddressUpdateCompletedEventHandler(mobjWSAddress_AddressUpdateCompleted);
        //        mobjWSAddress.AddressLoadCompleted += new BusinessRule.wsCall.AddressLoadCompletedEventHandler(mobjWSAddress_AddressLoadCompleted);

        //        if (WsBaseUrlStr != "")
        //            mobjWSAddress.Url = WsBaseUrlStr + "/CADServices/wsCall.asmx";
        //        else if (Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL != "")
        //            mobjWSAddress.Url = Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL + "/CADServices/wsCall.asmx";

        //        return mobjWSAddress;
        //    }
        //}

        //private BusinessRule.wsCommon.wsCommon WSCommonObj
        //{
        //    get
        //    {
        //        if (mobjWSCommon == null)
        //        {
        //            //initialize web service object
        //            mobjWSCommon = new BusinessRule.wsCommon.wsCommon();
        //        }

        //        mobjWSCommon.UsernamePasswordHeader = new BusinessRule.wsCommon.UsernamePasswordSoapHeader();
        //        mobjWSCommon.UsernamePasswordHeader.Token = Common.Credentials.Current.TokenXml;

        //        //register required events.
        //        //mobjWSAddress.AddressDeleteCompleted += new BusinessRule.wsCall.AddressDeleteCompletedEventHandler(mobjWSAddress_AddressDeleteCompleted);
        //        // mobjWSAddress.AddressUpdateCompleted += new BusinessRule.wsCall.AddressUpdateCompletedEventHandler(mobjWSAddress_AddressUpdateCompleted);
        //        // mobjWSAddress.AddressLoadCompleted += new BusinessRule.wsCall.AddressLoadCompletedEventHandler(mobjWSAddress_AddressLoadCompleted);

        //        if (WsBaseUrlStr != "")
        //            mobjWSCommon.Url = WsBaseUrlStr + "/CADServices/wsCommon.asmx";
        //        else if (Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL != "")
        //            mobjWSCommon.Url = Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL + "/CADServices/wsCommon.asmx";

        //        return mobjWSCommon;
        //    }
        //}

        //public clsAddress()
        //{
        //    if (!(Common.clsGeneral.IsInDesignMode))
        //    {
        //        WsBaseUrlStr = Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL;
        //        this.mobjPublicAddress = new clsPublicAddress();
        //    }

        //    try
        //    {
        //        WSCommonObj.GetAddressFromPhoneNoCompleted += new GetAddressFromPhoneNoCompletedEventHandler(WSCommonObj_GetAddressFromPhoneNoCompleted);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //void WSCommonObj_GetAddressFromPhoneNoCompleted(object sender, GetAddressFromPhoneNoCompletedEventArgs e)
        //{
        //    if (OnPhoneNoCompleted != null)
        //        OnPhoneNoCompleted(sender, e);
        //}

        //public clsAddress(string wsBaseUrlStr)
        //{
        //    WsBaseUrlStr = wsBaseUrlStr;
        //    WSCommonObj.GetAddressFromPhoneNoCompleted += new GetAddressFromPhoneNoCompletedEventHandler(WSCommonObj_GetAddressFromPhoneNoCompleted);
        //}

        //#region -- Web Service Event handlers --

        //void mobjWSAddress_AddressLoadCompleted(object sender, BusinessRule.wsCall.AddressLoadCompletedEventArgs e)
        //{
        //    if (mevtOnMethodCompleted != null)
        //    {
        //        List<clsAddress> objAddressList = PopulateList(e.Result);
        //        mevtOnMethodCompleted(eclsAddressMethod.GetRecords_List, objAddressList);
        //    }
        //}

        ////void mobjWSAddress_AddressUpdateCompleted(object sender, BusinessRule.wsCall.AddressUpdateCompletedEventArgs e)
        ////{
        ////    if (mevtOnMethodCompleted != null)
        ////        mevtOnMethodCompleted(eclsAddressMethod.Save_Int, e.Result);
        ////}

        ////void mobjWSAddress_AddressDeleteCompleted(object sender, BusinessRule.wsCall.AddressDeleteCompletedEventArgs e)
        ////{
        ////    if (mevtOnMethodCompleted != null)
        ////        mevtOnMethodCompleted(eclsAddressMethod.Delete_Bool, e.Result);
        ////}

        //#endregion

        //#endregion

        //#region -- Public Properties --

        //public clsPublicAddress pPublicAddress
        //{
        //    get { return this.mobjPublicAddress; }
        //    set { this.mobjPublicAddress = value; }
        //}

        //public string WsBaseUrlStr
        //{
        //    get { return this.mstrWsBaseUrl; }
        //    set { this.mstrWsBaseUrl = value; }
        //}
        //public int pAddressIDInt
        //{
        //    get
        //    {
        //        return this.mintAddressID;
        //    }
        //    set
        //    {
        //        this.mintAddressID = value;
        //    }
        //}

        //public int pAddressPointIDInt
        //{
        //    get
        //    {
        //        return this.mintAddressPointID;
        //    }
        //    set
        //    {
        //        this.mintAddressPointID = value;
        //    }
        //}

        //public string pAddressLine1Str
        //{
        //    get
        //    {
        //        return this.mstrAddressLine1;
        //    }
        //    set
        //    {
        //        this.mstrAddressLine1 = value;
        //    }
        //}

        //public string pAddressLine2Str
        //{
        //    get
        //    {
        //        return this.mstrAddressLine2;
        //    }
        //    set
        //    {
        //        this.mstrAddressLine2 = value;
        //    }
        //}

        //public string pAddressLine3Str
        //{
        //    get
        //    {
        //        return this.mstrAddressLine3;
        //    }
        //    set
        //    {
        //        this.mstrAddressLine3 = value;
        //    }
        //}


        //public double pLongitudeDbl
        //{
        //    get
        //    {
        //        return this.mdblLongitude;
        //    }
        //    set
        //    {
        //        this.mdblLongitude = value;
        //    }
        //}


        //public double pLatitudeDbl
        //{
        //    get
        //    {
        //        return this.mdblLatitude;
        //    }
        //    set
        //    {
        //        this.mdblLatitude = value;
        //    }
        //}

        //public string pAddressLine4Str
        //{
        //    get
        //    {
        //        return this.mstrAddressLine4;
        //    }
        //    set
        //    {
        //        this.mstrAddressLine4 = value;
        //    }
        //}

        //public int pCityIDInt
        //{
        //    get
        //    {
        //        return this.mintCityID;
        //    }
        //    set
        //    {
        //        this.mintCityID = value;
        //    }
        //}

        //public int pStateOrProvinceIDInt
        //{
        //    get
        //    {
        //        return this.mintStateOrProvinceID;
        //    }
        //    set
        //    {
        //        this.mintStateOrProvinceID = value;
        //    }
        //}

        //public int pCountryIDInt
        //{
        //    get
        //    {
        //        return this.mintCountryID;
        //    }
        //    set
        //    {
        //        this.mintCountryID = value;
        //    }
        //}

        //public int pPostCodeInt
        //{
        //    get { return mintPostCode; }
        //    set { mintPostCode = value; }
        //}

        //public int pSubPostCodeInt
        //{
        //    get { return mintSubPostCode; }
        //    set { mintSubPostCode = value; }
        //}

        //public int pAreaInt
        //{
        //    get { return mintArea; }
        //    set { mintArea = value; }
        //}


        //public bool pIsPublicAddressBln
        //{
        //    get { return mblnIsPublicAddress; }
        //    set { mblnIsPublicAddress = value; }
        //}

        //public int pInsertedByInt
        //{
        //    get
        //    {
        //        return this.mintInsertedBy;
        //    }
        //    set
        //    {
        //        this.mintInsertedBy = value;
        //    }
        //}

        //public int pUpdatedByInt
        //{
        //    get
        //    {
        //        return this.mintUpdatedBy;
        //    }
        //    set
        //    {
        //        this.mintUpdatedBy = value;
        //    }
        //}

        //public System.DateTime pInsertedAtDtm
        //{
        //    get
        //    {
        //        return this.mdtmInsertedAt;
        //    }
        //    set
        //    {
        //        this.mdtmInsertedAt = value;
        //    }
        //}

        //public System.DateTime pUpdatedAtDtm
        //{
        //    get
        //    {
        //        return this.mdtmUpdatedAt;
        //    }
        //    set
        //    {
        //        this.mdtmUpdatedAt = value;
        //    }
        //}

        //public List<clsAddressPoint> pAddressPoints
        //{
        //    get { return addressPoints; }
        //    set { addressPoints = value; }
        //}
        //public bool pIsPrimaryBln
        //{
        //    get { return mIsPrimary; }
        //    set { mIsPrimary = value; }
        //}
        //public string pcountryNameStr
        //{
        //    get { return mcountryNameStr; }
        //    set { mcountryNameStr = value; }
        //}
        //public string pStateStr
        //{
        //    get { return mStateStr; }
        //    set { mStateStr = value; }
        //}
        //public string pCityStr
        //{
        //    get { return mCityStr; }
        //    set { mCityStr = value; }
        //}

        //#endregion

        //#region -- Public Methods --

        //#region Delete

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pintID"></param>
        ///// <returns></returns>
        ////public bool Delete(int pintID)
        ////{  
        ////    return this.Delete(pintID.ToString());
        ////}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pstrRecIDs"></param>
        ///// <returns></returns>
        ////public bool Delete(string pstrRecIDs)
        ////{
        ////    //ws.Delete(pstrRecIDs);
        ////    return WSAddressObj.AddressDelete(pstrRecIDs,"");
        ////}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pintID"></param>
        ///// <param name="pobjTransDS"></param>
        //public void Delete(int pintID, DataSet pobjTransDS)
        //{
        //    Delete(pintID.ToString(), pobjTransDS);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pintID"></param>
        ///// <param name="pobjTransDS"></param>
        //public void Delete(string pstrRecIDs, DataSet pobjTransDS)
        //{
        //    if (!pobjTransDS.Tables.Contains(clsGeneral.cstrDeleteTableName))
        //        pobjTransDS.Tables.Add(clsGeneral.mobjDeleteTable.Copy());

        //    DataRow objDR = pobjTransDS.Tables[clsGeneral.cstrDeleteTableName].NewRow();

        //    objDR[clsGeneral.cstrCsvIDsColumnName] = pstrRecIDs;
        //    objDR[clsGeneral.cstrTableColumnName] = cstrTableName;
        //    objDR[clsGeneral.cstrIDColumnColumnName] = cstrAddressIDCol;

        //    pobjTransDS.Tables[clsGeneral.cstrDeleteTableName].Rows.Add(objDR);
        //    pobjTransDS.AcceptChanges();

        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pintID"></param>
        ///// <returns></returns>
        ////public void DeleteAsync(int pintID)
        ////{
        ////    this.DeleteAsync(pintID.ToString());
        ////}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pstrRecIDs"></param>
        ///// <returns></returns>
        ////public void DeleteAsync(string pstrRecIDs)
        ////{
        ////    WSAddressObj.AddressDeleteAsync(pstrRecIDs, "");           
        ////}

        //#endregion

        //#region Save

        ////public int Save(int pintAddressID, string pstrAddressLine1, string pstrAddressLine2, string pstrAddressLine3,
        ////    string pstrAddressLine4, int pintCityID, int pintStateOrProvinceID, int pintCountryID, string pstrZipCode, bool pblnisPublicAddress,
        ////    int pintInsertedBy, int pintUpdatedBy, System.DateTime pdtmInsertedAt, System.DateTime pdtmUpdatedAt)
        ////{
        ////    //Business Validation
        ////    pstrAddressLine1 = pstrAddressLine1.Trim();
        ////    pstrAddressLine2 = pstrAddressLine2.Trim();
        ////    pstrAddressLine3 = pstrAddressLine3.Trim();
        ////    pstrAddressLine4 = pstrAddressLine4.Trim();

        ////    return WSAddressObj.AddressUpdate(pintAddressID, 0.0, 0.0, pstrAddressLine1, pstrAddressLine2,
        ////        pstrAddressLine3, pstrAddressLine4, pintCountryID, pintStateOrProvinceID, pintCityID, pstrZipCode, pblnisPublicAddress,
        ////        pintInsertedBy, pintUpdatedBy, pdtmInsertedAt, pdtmUpdatedAt);

        ////}

        //public int Save(DataSet pobjTransDS, int pintAddressID, string pstrAddressLine1, string pstrAddressLine2, string pstrAddressLine3,
        //    string pstrAddressLine4, double pdoubleLongitiude, double pdoubleLatitude, int pintCityID, int pintStateOrProvinceID, int pintCountryID, int pintPostCode, int pintArea, bool pblnisPublicAddress,
        //    int pintInsertedBy, int pintUpdatedBy, System.DateTime pdtmInsertedAt, System.DateTime pdtmUpdatedAt)
        //{
        //    //Business Validation
        //    pstrAddressLine1 = pstrAddressLine1.Trim();
        //    pstrAddressLine2 = pstrAddressLine2.Trim();
        //    pstrAddressLine3 = pstrAddressLine3.Trim();
        //    pstrAddressLine4 = pstrAddressLine4.Trim();

        //    //if table is not already added, add it.
        //    if (!pobjTransDS.Tables.Contains(cstrTableName))
        //        pobjTransDS.Tables.Add(mobjAddressTable.Copy());

        //    DataRow objDrw = pobjTransDS.Tables[cstrTableName].NewRow();

        //    int intRefID = mintReferenceID;

        //    //if this is a new Record.
        //    if (pintAddressID == 0)
        //        intRefID = --mintReferenceID;
        //    else
        //        intRefID = pintAddressID;

        //    objDrw[mstrReferenceIDCol] = intRefID;
        //    objDrw[cstrLatitudeCol] = pdoubleLatitude;
        //    objDrw[cstrLongitudeCol] = pdoubleLongitiude;
        //    objDrw[cstrAddressIDCol] = pintAddressID <= 0 ? intRefID : pintAddressID;
        //    objDrw[cstrAddressLine1Col] = pstrAddressLine1;
        //    objDrw[cstrAddressLine2Col] = pstrAddressLine2;
        //    objDrw[cstrAddressLine3Col] = pstrAddressLine3;
        //    objDrw[cstrAddressLine4Col] = pstrAddressLine4;
        //    objDrw[cstrCityIDCol] = pintCityID;
        //    objDrw[cstrStateOrProvinceIDCol] = pintStateOrProvinceID;
        //    objDrw[cstrCountryIDCol] = pintCountryID;
        //    objDrw[cstrPostCodeCol] = pintPostCode;
        //    objDrw[cstrAreaCol] = pintArea;
        //    objDrw[cstrIsPublicAddressCol] = pblnisPublicAddress;
        //    objDrw[cstrInsertedByCol] = pintInsertedBy;
        //    objDrw[cstrUpdatedByCol] = pintUpdatedBy;
        //    objDrw[cstrInsertedAtCol] = pdtmInsertedAt;
        //    objDrw[cstrUpdatedAtCol] = pdtmUpdatedAt;

        //    pobjTransDS.Tables[cstrTableName].Rows.Add(objDrw);

        //    pobjTransDS.AcceptChanges();

        //    return intRefID;

        //}

        ////public void SaveAsync(int pintAddressID, string pstrAddressLine1, string pstrAddressLine2, string pstrAddressLine3,
        ////    string pstrAddressLine4, int pintCityID, int pintStateOrProvinceID, int pintCountryID, string pstrZipCode, bool pblnisPublicAddress,
        ////    int pintInsertedBy, int pintUpdatedBy, System.DateTime pdtmInsertedAt, System.DateTime pdtmUpdatedAt)
        ////{            
        ////    //Business Validation
        ////    pstrAddressLine1 = pstrAddressLine1.Trim();
        ////    pstrAddressLine2 = pstrAddressLine2.Trim();
        ////    pstrAddressLine3 = pstrAddressLine3.Trim();
        ////    pstrAddressLine4 = pstrAddressLine4.Trim();

        ////    WSAddressObj.AddressUpdateAsync(pintAddressID, 0.0, 0.0, pstrAddressLine1, pstrAddressLine2,
        ////        pstrAddressLine3, pstrAddressLine4, pintCountryID, pintStateOrProvinceID, pintCityID, pstrZipCode, pblnisPublicAddress, 
        ////        pintInsertedBy, pintUpdatedBy, pdtmInsertedAt, pdtmUpdatedAt);

        ////} 

        //#endregion

        //#region Get Records

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pintRecID"></param>
        ///// <returns></returns>
        //public clsAddress GetRecords(int pintRecID)
        //{
        //    List<clsAddress> objAddressList = GetRecords(pintRecID.ToString());

        //    if (objAddressList.Count == 1)
        //        return objAddressList[0];
        //    else
        //        return null;
        //}

        //public List<clsAddress> GetRecords(string pstrRecIDs)
        //{
        //    DataSet dsReturn = WSAddressObj.AddressLoad(pstrRecIDs);
        //    return PopulateList(dsReturn);
        //}

        //public List<clsAddress> GetRecordsByCustomerID(int clientID)
        //{
        //    List<clsAddress> objAddressList = new List<clsAddress>();
        //    //clsMapClientAddress objMapCustAdd = new clsMapClientAddress();
        //    //List<clsMapClientAddress> lstCustomerMap = objMapCustAdd.GetRecordsByClientID(clientID);

        //    //if (lstCustomerMap != null)
        //    //{
        //    //    string strAddressIDs = string.Empty;
        //    //    foreach (clsMapClientAddress custAdd in lstCustomerMap)
        //    //    {
        //    //        strAddressIDs += custAdd.pAddressIDInt+",";
        //    //    }

        //    //    if (strAddressIDs.EndsWith(","))
        //    //    {
        //    //        strAddressIDs = strAddressIDs.TrimEnd(',');
        //    //    }

        //    //    //Commented By Rizwan
        //    //    //return new clsAddress().GetRecords(strAddressIDs);
        //    //    if(strAddressIDs != string.Empty)
        //    //    objAddressList= GetRecords(strAddressIDs);
        //    //}


        //    return objAddressList;

        //}
        //public List<clsAddress> GetRecordsByCustomerIDForWeb(int customerID, string pstrWsBseURL)
        //{
        //    List<clsAddress> objAddressList = new List<clsAddress>();
        //    //clsMapClientAddress objMapCustAdd = new clsMapClientAddress(pstrWsBseURL);
        //    //List<clsMapClientAddress> lstCustomerMap = objMapCustAdd.GetRecordsByClientID(customerID);

        //    //if (lstCustomerMap != null)
        //    //{
        //    //    string strAddressIDs = string.Empty;
        //    //    foreach (clsMapClientAddress custAdd in lstCustomerMap)
        //    //    {
        //    //        strAddressIDs += custAdd.pAddressIDInt + ",";
        //    //    }

        //    //    if (strAddressIDs.EndsWith(","))
        //    //    {
        //    //        strAddressIDs = strAddressIDs.TrimEnd(',');
        //    //    }

        //    //    //Commented By Rizwan
        //    //    //return new clsAddress().GetRecords(strAddressIDs);
        //    //    if (strAddressIDs != string.Empty)
        //    //        objAddressList = GetRecords(strAddressIDs);
        //    //}


        //    return objAddressList;

        //}

        //public void GetRecordsAsync(int pintRecID)
        //{
        //    GetRecordsAsync(pintRecID.ToString());
        //}

        //public void GetRecordsAsync(string pstrRecIDs)
        //{
        //    WSAddressObj.AddressLoadAsync(pstrRecIDs);
        //}

        //public List<clsAddress> GetRecordWithAddressPoins(int PatientID)
        //{
        //    //To Do
        //    DataSet dsReturn = WSAddressObj.AddressWithAddressPointsLoad(PatientID);
        //    return PopulateListWithAddressPoints(dsReturn);
        //}

        //public PublicAddressData GetRecordWithAddressPoinsForPublicAddress(int pPageIndex, int pPageSize, String publicAddressTitle)
        //{
        //    //To Do
        //    DataSet dsReturn = WSAddressObj.AddressWithAddressPointsLoadByPublicAddressTitle(pPageIndex, pPageSize, publicAddressTitle);
        //    return PopulateListWithAddressPointsForPublicAddress(dsReturn);
        //}


        //public DataSet GetRecordsByCriteria(int pPageIndex, int pPageSize, string pstrLine1,
        //    string pstrLine2, string pstrLine3, string pstrLine4, int pintCountryid, String pintStateid,
        //    String pintCityid, int pintPublicAddressTypeid, String pStrPostCode, String pStrSubPostCode, String pIntArea, bool pblnIsPublicAddress,
        //    string pstrPublicAddTitle, string pstrPublicAddCode, bool pbUseORForTitleAndCode,
        //    int pSortColumnIndex, string pstrSortColumn, Boolean? pblnIsEmergencyAddress, Boolean? blnPhoneticSearch, Boolean? poi,String flagged)
        //{
        //    if (clsGeneral.blnIsOfflineMode)
        //    {
        //        return OfflineWorkManager.AddressManager.AddressDS;
        //    }
        //    DataSet dsReturn = WSAddressObj.GetPublicAddressRecordsByCriteria(pPageIndex, pPageSize,
        //        pstrLine1, pstrLine2, pstrLine3, pstrLine4, pintCountryid, pintStateid, pintCityid,
        //        pintPublicAddressTypeid, pStrPostCode, pStrSubPostCode, pIntArea, pblnIsPublicAddress, pstrPublicAddTitle,
        //        pstrPublicAddCode, pbUseORForTitleAndCode, pSortColumnIndex, pstrSortColumn, pblnIsEmergencyAddress, blnPhoneticSearch, poi,flagged);
        //    return dsReturn;
        //}

        //public DataSet SearchDuplicateAddress(string pstrLine1, string pstrLine2, string pstrLine3, string pstrLine4, string pstrPublicAddTitle, string pstrPublicAddCode,string town)
        //{
        //    return WSAddressObj.SearchDuplicateAddress(pstrLine1, pstrLine2, pstrLine3, pstrLine4, pstrPublicAddTitle, pstrPublicAddCode,town);
        //}

        //public void SaveVerifiedAddressAndFlags(int AddressId, string flagsToLinkCSVIds, string flagsToRemoveCSVIDs, string addressToMarksAsDeleteCSVIds, int userID)
        //{
        //    WSAddressObj.SaveVerifiedAddressAndFlags(AddressId, flagsToLinkCSVIds, flagsToRemoveCSVIDs, addressToMarksAsDeleteCSVIds, userID);
        //}
        //#endregion

        //public List<clsAddress> PopulateList(System.Data.DataSet pobjDS)
        //{
        //    return PopulateList(pobjDS, 0);
        //}

        //public List<clsAddress> PopulateList(System.Data.DataSet pobjDS, int pintAddressID)
        //{
        //    List<clsAddress> objCollection = new List<clsAddress>();
        //    int iCount = 0;
        //    if (pobjDS != null && pobjDS.Tables.Contains(cstrTableName))
        //    {
        //        DataTable objTbl = pobjDS.Tables[cstrTableName];
        //        clsAddress objAddress = null;

        //        foreach (DataRow objDrw in objTbl.Rows)
        //        {
        //            if (pintAddressID != 0)
        //            {
        //                if (objTbl.Columns.Contains(cstrAddressIDCol) && objDrw[cstrAddressIDCol] != DBNull.Value)
        //                {
        //                    if (pintAddressID != (int)objDrw[cstrAddressIDCol])
        //                        continue;
        //                }
        //                else
        //                    continue;
        //            }

        //            objAddress = new clsAddress();
        //            objCollection.Add(objAddress);

        //            if (objTbl.Columns.Contains(cstrAddressIDCol) && objDrw[cstrAddressIDCol] != DBNull.Value)
        //                objAddress.pAddressIDInt = (int)objDrw[cstrAddressIDCol];

        //            if (objTbl.Columns.Contains(cstrAddressLine1Col) && objDrw[cstrAddressLine1Col] != DBNull.Value)
        //                objAddress.pAddressLine1Str = objDrw[cstrAddressLine1Col].ToString();

        //            if (objTbl.Columns.Contains(cstrAddressLine2Col) && objDrw[cstrAddressLine2Col] != DBNull.Value)
        //                objAddress.pAddressLine2Str = objDrw[cstrAddressLine2Col].ToString();

        //            if (objTbl.Columns.Contains(cstrAddressLine3Col) && objDrw[cstrAddressLine3Col] != DBNull.Value)
        //                objAddress.pAddressLine3Str = objDrw[cstrAddressLine3Col].ToString();

        //            if (objTbl.Columns.Contains(cstrAddressLine4Col) && objDrw[cstrAddressLine4Col] != DBNull.Value)
        //                objAddress.pAddressLine4Str = objDrw[cstrAddressLine4Col].ToString();

        //            if (objTbl.Columns.Contains(cstrLatitudeCol) && objDrw[cstrLatitudeCol] != DBNull.Value)
        //                objAddress.pLatitudeDbl = double.Parse(objDrw[cstrLatitudeCol].ToString());

        //            if (objTbl.Columns.Contains(cstrLongitudeCol) && objDrw[cstrLongitudeCol] != DBNull.Value)
        //                objAddress.pLongitudeDbl = double.Parse(objDrw[cstrLongitudeCol].ToString());

        //            if (objTbl.Columns.Contains(cstrCityIDCol) && objDrw[cstrCityIDCol] != DBNull.Value)
        //                objAddress.pCityIDInt = (int)objDrw[cstrCityIDCol];

        //            if (objTbl.Columns.Contains(cstrStateOrProvinceIDCol) && objDrw[cstrStateOrProvinceIDCol] != DBNull.Value)
        //                objAddress.pStateOrProvinceIDInt = (int)objDrw[cstrStateOrProvinceIDCol];

        //            if (objTbl.Columns.Contains(cstrCountryIDCol) && objDrw[cstrCountryIDCol] != DBNull.Value)
        //                objAddress.pCountryIDInt = (int)objDrw[cstrCountryIDCol];

        //            if (objTbl.Columns.Contains(cstrCountryIDCol) && objDrw[cstrCountryIDCol] != DBNull.Value)
        //                objAddress.pCountryIDInt = (int)objDrw[cstrCountryIDCol];

        //            if (objTbl.Columns.Contains(cstrIsPublicAddressCol) && objDrw[cstrIsPublicAddressCol] != DBNull.Value)
        //                objAddress.pIsPublicAddressBln = (bool)objDrw[cstrIsPublicAddressCol];

        //            if (objAddress.pIsPublicAddressBln)
        //                iCount++;

        //            if (objTbl.Columns.Contains(cstrPostCodeCol) && objDrw[cstrPostCodeCol] != DBNull.Value)
        //                objAddress.pPostCodeInt = (int)objDrw[cstrPostCodeCol];

        //            if (objTbl.Columns.Contains(cstrAreaCol) && objDrw[cstrAreaCol] != DBNull.Value)
        //                objAddress.pAreaInt = (int)objDrw[cstrAreaCol];

        //            if (objTbl.Columns.Contains(cstrInsertedByCol) && objDrw[cstrInsertedByCol] != DBNull.Value)
        //                objAddress.pInsertedByInt = (int)objDrw[cstrInsertedByCol];

        //            if (objTbl.Columns.Contains(cstrUpdatedByCol) && objDrw[cstrUpdatedByCol] != DBNull.Value)
        //                objAddress.pUpdatedByInt = (int)objDrw[cstrUpdatedByCol];

        //            if (objTbl.Columns.Contains(cstrInsertedAtCol) && objDrw[cstrInsertedAtCol] != DBNull.Value)
        //                objAddress.pInsertedAtDtm = (DateTime)objDrw[cstrInsertedAtCol];

        //            if (objTbl.Columns.Contains(cstrUpdatedAtCol) && objDrw[cstrUpdatedAtCol] != DBNull.Value)
        //                objAddress.pUpdatedAtDtm = (DateTime)objDrw[cstrUpdatedAtCol];

        //            if (objAddress.pIsPublicAddressBln)
        //            {
        //                mobjPublicAddress = new clsPublicAddress();

        //                if (objTbl.Columns.Contains(cstrAddressIDCol) && objDrw[cstrAddressIDCol] != DBNull.Value)
        //                    mobjPublicAddress.pAddressIDInt = (int)objAddress.pAddressIDInt;
        //                if (objTbl.Columns.Contains(cstrPublicAddressTypeIDCol) && objDrw[cstrPublicAddressTypeIDCol] != DBNull.Value)
        //                    mobjPublicAddress.pPublicAddressTypeIDInt = int.Parse(objDrw[cstrPublicAddressTypeIDCol].ToString());
        //                if (objTbl.Columns.Contains(cstrPublicAddressTitleCol) && objDrw[cstrPublicAddressTitleCol] != DBNull.Value)
        //                    mobjPublicAddress.pPublicAddressTitleStr = (string)objDrw[cstrPublicAddressTitleCol];
        //                if (objTbl.Columns.Contains(cstrPublicAddressCodeCol) && objDrw[cstrPublicAddressCodeCol] != DBNull.Value)
        //                    mobjPublicAddress.pPublicAddressCodeStr = (string)objDrw[cstrPublicAddressCodeCol];

        //                objAddress.pPublicAddress = mobjPublicAddress;
        //            }

        //        }
        //    }

        //    return objCollection;

        //}

        //public static List<clsAddress> PopulateListWithAddressPoints(DataSet DSAddresses)
        //{
        //    List<clsAddress> addresses = new List<clsAddress>();

        //    //If data set contains tables
        //    if (DSAddresses != null && DSAddresses.Tables.Count > 0)
        //    {
        //        foreach (DataRow aAddressRow in DSAddresses.Tables[0].Rows)
        //        {
        //            clsAddress aAddress = new clsAddress();
        //            aAddress.pIsPublicAddressBln = bool.Parse(Convert.ToString(aAddressRow[cstrIsPublicAddressCol]));
        //            aAddress.pCityStr = Convert.ToString(aAddressRow[cstrCityNameCol]);
        //            aAddress.pcountryNameStr = Convert.ToString(aAddressRow[cstrCountryCol]);
        //            aAddress.pLatitudeDbl = aAddressRow[cstrLatitudeCol] == DBNull.Value ? 0 : double.Parse(Convert.ToString(aAddressRow[cstrLatitudeCol]));
        //            aAddress.pLongitudeDbl = aAddressRow[cstrLongitudeCol] == DBNull.Value ? 0 : double.Parse(Convert.ToString(aAddressRow[cstrLongitudeCol]));
        //            aAddress.pAddressIDInt = aAddressRow[cstrAddressIDCol] == DBNull.Value ? 0 : int.Parse(Convert.ToString(aAddressRow[cstrAddressIDCol]));
        //            aAddress.pCityIDInt = aAddressRow[cstrCityIDCol] == DBNull.Value ? 0 : int.Parse(Convert.ToString(aAddressRow[cstrCityIDCol]));
        //            aAddress.pCountryIDInt = aAddressRow[cstrCountryIDCol] == DBNull.Value ? 0 : int.Parse(Convert.ToString(aAddressRow[cstrCountryIDCol]));
        //            aAddress.pStateOrProvinceIDInt = aAddressRow[cstrStateOrProvinceIDCol] == DBNull.Value ? 0 : int.Parse(Convert.ToString(aAddressRow[cstrStateOrProvinceIDCol]));
        //            aAddress.pIsPrimaryBln = bool.Parse(Convert.ToString(aAddressRow[cstrIsPrimaryCol]));
        //            aAddress.pStateStr = Convert.ToString(aAddressRow[cstrStateOrProvinceCol]);
        //            aAddress.pAddressLine1Str = Convert.ToString(aAddressRow[cstrAddressLine1Col]);
        //            aAddress.pAddressLine2Str = Convert.ToString(aAddressRow[cstrAddressLine2Col]);
        //            aAddress.pAddressLine3Str = Convert.ToString(aAddressRow[cstrAddressLine3Col]);
        //            aAddress.pAddressLine4Str = Convert.ToString(aAddressRow[cstrAddressLine4Col]);
        //            aAddress.pPostCodeInt = Convert.ToInt32(aAddressRow[cstrPostCodeCol]);
        //            aAddress.pAreaInt = Convert.ToInt32(aAddressRow[cstrAreaCol]);

        //            //Get address points for the address
        //            aAddress.pAddressPoints = clsAddressPoint.GetAddressPoints(aAddress.pAddressIDInt, DSAddresses.Tables[1]);

        //            addresses.Add(aAddress);
        //        }
        //    }

        //    return addresses;
        //}

        //public static PublicAddressData PopulateListWithAddressPointsForPublicAddress(DataSet DSAddresses)
        //{
        //    PublicAddressData data = new PublicAddressData();
        //    List<clsAddress> addresses = new List<clsAddress>();
        //    data.PublicAddressesList = addresses;
        //    //If data set contains tables
        //    if (DSAddresses != null && DSAddresses.Tables.Count > 0)
        //    {
        //        if (DSAddresses.Tables[0].Rows.Count > 0)
        //            data.TotalRecords = Convert.ToInt32(DSAddresses.Tables[0].Rows[0]["TotalRecords"].ToString());

        //        foreach (DataRow aAddressRow in DSAddresses.Tables[0].Rows)
        //        {
        //            clsAddress aAddress = new clsAddress();
        //            aAddress.pIsPublicAddressBln = bool.Parse(Convert.ToString(aAddressRow[cstrIsPublicAddressCol]));
        //            aAddress.pCityStr = Convert.ToString(aAddressRow[cstrCityNameCol]);
        //            aAddress.pcountryNameStr = Convert.ToString(aAddressRow[cstrCountryCol]);
        //            aAddress.pLatitudeDbl = aAddressRow[cstrLatitudeCol] == DBNull.Value ? 0 : double.Parse(Convert.ToString(aAddressRow[cstrLatitudeCol]));
        //            aAddress.pLongitudeDbl = aAddressRow[cstrLongitudeCol] == DBNull.Value ? 0 : double.Parse(Convert.ToString(aAddressRow[cstrLongitudeCol]));
        //            aAddress.pAddressIDInt = aAddressRow[cstrAddressIDCol] == DBNull.Value ? 0 : int.Parse(Convert.ToString(aAddressRow[cstrAddressIDCol]));
        //            aAddress.pCityIDInt = aAddressRow[cstrCityIDCol] == DBNull.Value ? 0 : int.Parse(Convert.ToString(aAddressRow[cstrCityIDCol]));
        //            aAddress.pCountryIDInt = aAddressRow[cstrCountryIDCol] == DBNull.Value ? 0 : int.Parse(Convert.ToString(aAddressRow[cstrCountryIDCol]));
        //            aAddress.pStateOrProvinceIDInt = aAddressRow[cstrStateOrProvinceIDCol] == DBNull.Value ? 0 : int.Parse(Convert.ToString(aAddressRow[cstrStateOrProvinceIDCol]));
        //            aAddress.pStateStr = Convert.ToString(aAddressRow[cstrStateOrProvinceCol]);
        //            aAddress.pAddressLine1Str = Convert.ToString(aAddressRow[cstrAddressLine1Col]);
        //            aAddress.pAddressLine2Str = Convert.ToString(aAddressRow[cstrAddressLine2Col]);
        //            aAddress.pAddressLine3Str = Convert.ToString(aAddressRow[cstrAddressLine3Col]);
        //            aAddress.pAddressLine4Str = Convert.ToString(aAddressRow[cstrAddressLine4Col]);
        //            aAddress.pPostCodeInt = Convert.ToInt32(aAddressRow[cstrPostCodeCol]);
        //            aAddress.pAreaInt = Convert.ToInt32(aAddressRow[cstrAreaCol]);
        //            aAddress.publicAddressTitle = Convert.ToString(aAddressRow["PublicAddressTitle"]);
        //            aAddress.publicAddressCode = Convert.ToString(aAddressRow["PublicAddressCode"]);
        //            aAddress.publicAddressTypeId = Convert.ToInt32(aAddressRow["PublicAddressTypeID"]);
        //            aAddress.publicAddressType = Convert.ToString(aAddressRow["PublicAddressType"]);

        //            aAddress.ZoneID = aAddressRow["ZoneID"] == DBNull.Value ? 0 : Convert.ToInt32(aAddressRow["ZoneID"]);

        //            //Get address points for the address
        //            aAddress.pAddressPoints = clsAddressPoint.GetAddressPoints(aAddress.pAddressIDInt, DSAddresses.Tables[1]);

        //            addresses.Add(aAddress);
        //        }
        //    }

        //    return data;
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj != null)
        //    {
        //        bool result = false;
        //        clsAddress add = (clsAddress)obj;

        //        if (this.pAddressLine1Str == null)
        //            this.pAddressLine1Str = "";
        //        if (this.pAddressLine2Str == null)
        //            this.pAddressLine2Str = "";
        //        if (this.pAddressLine3Str == null)
        //            this.pAddressLine3Str = "";
        //        if (this.pAddressLine4Str == null)
        //            this.pAddressLine4Str = "";

        //        if (add.pAddressLine1Str.Trim().Length > 0)
        //        {
        //            if (this.pAddressLine1Str.ToLower().Contains(add.pAddressLine1Str.Trim().ToLower()))
        //            {
        //                result = true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        if (add.pAddressLine2Str.Trim().Length > 0)
        //        {
        //            if (this.pAddressLine2Str.ToLower().Contains(add.pAddressLine2Str.Trim().ToLower()))
        //            {
        //                result = true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        if (add.pAddressLine3Str.Trim().Length > 0)
        //        {
        //            if (this.pAddressLine3Str.ToLower().Contains(add.pAddressLine3Str.Trim().ToLower()))
        //            {
        //                result = true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        if (add.pAddressLine4Str.Trim().Length > 0)
        //        {
        //            if (this.pAddressLine4Str.ToLower().Contains(add.pAddressLine4Str.Trim().ToLower()))
        //            {
        //                result = true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        if (add.pCountryIDInt > 0)
        //        {
        //            if (this.pCountryIDInt != add.pCountryIDInt)
        //            {
        //                return false; //result = false;
        //            }
        //            else
        //            {
        //                result = true;
        //            }
        //        }

        //        if (add.pStateOrProvinceIDInt > 0)
        //        {
        //            if (this.pStateOrProvinceIDInt != add.pStateOrProvinceIDInt)
        //            {
        //                return false; //result = false;
        //            }
        //            else
        //            {
        //                result = true;
        //            }
        //        }

        //        if (add.pCityIDInt > 0)
        //        {
        //            if (this.pCityIDInt != add.pCityIDInt)
        //            {
        //                return false; //result = false;
        //            }
        //            else
        //            {
        //                result = true;
        //            }
        //        }

        //        if (add.pPostCodeInt > 0)
        //        {
        //            if (this.pPostCodeInt == add.pPostCodeInt)
        //            {
        //                result = true;
        //            }
        //            else
        //            {
        //                return false; //result = false;
        //            }
        //        }

        //        if (add.pAreaInt > 0)
        //        {
        //            if (this.pAreaInt == add.pAreaInt)
        //            {
        //                result = true;
        //            }
        //            else
        //            {
        //                return false; //result = false;
        //            }
        //        }

        //        if (add.pIsPublicAddressBln != this.pIsPublicAddressBln)
        //        {
        //            return false;
        //        }

        //        if ((this.pIsPublicAddressBln && add.pIsPublicAddressBln) && this.pPublicAddress != null)
        //        {
        //            if (add.PublicAddressTitle.Trim().Length > 0)
        //            {
        //                if (this.pPublicAddress.pPublicAddressTitleStr.ToLower().Contains(add.PublicAddressTitle.Trim().ToLower()))
        //                {
        //                    result = true;
        //                }
        //                else
        //                {
        //                    return false; //result = false;
        //                }
        //            }
        //            if (add.publicAddressTypeId > 0)
        //            {
        //                if (this.pPublicAddress.pPublicAddressTypeIDInt != add.publicAddressTypeId)
        //                {
        //                    return false; //result = false;
        //                }
        //                else
        //                {
        //                    result = true;
        //                }
        //            }
        //            if (add.PublicAddressCode.Trim().Length > 0)
        //            {
        //                if (this.pPublicAddress.pPublicAddressCodeStr.ToLower().Contains(add.PublicAddressCode.Trim().ToLower()))
        //                {
        //                    result = true;
        //                }
        //                else
        //                {
        //                    return false; //result = false;
        //                }
        //            }
        //        }

        //        return result;
        //    }
        //    else
        //    {
        //        return base.Equals(obj);
        //    }
        //}

        //#endregion

        //public bool IsSameAddress(object obj)
        //{

        //    bool result = false;
        //    clsAddress add = (clsAddress)obj;

        //    if (this.pAddressLine1Str == null)
        //        this.pAddressLine1Str = "";
        //    if (this.pAddressLine2Str == null)
        //        this.pAddressLine2Str = "";
        //    if (this.pAddressLine3Str == null)
        //        this.pAddressLine3Str = "";
        //    if (this.pAddressLine4Str == null)
        //        this.pAddressLine4Str = "";

        //    if (this.pAddressLine1Str.ToLower() == add.pAddressLine1Str.ToLower())
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if (this.pAddressLine2Str.ToLower() == add.pAddressLine2Str.ToLower())
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        return false;
        //    }


        //    if (this.pAddressLine3Str.ToLower() == add.pAddressLine3Str.ToLower())
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if (this.pAddressLine4Str.ToLower() == add.pAddressLine4Str.ToLower())
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if (this.pCountryIDInt != add.pCountryIDInt)
        //        result = false;
        //    else
        //    {
        //        result = true;
        //    }

        //    if (this.pStateOrProvinceIDInt != add.pStateOrProvinceIDInt)
        //        result = false;
        //    else
        //    {
        //        result = true;
        //    }


        //    if (this.pCityIDInt != add.pCityIDInt)
        //        result = false;
        //    else
        //    {
        //        result = true;
        //    }

        //    if (this.pPostCodeInt == add.pPostCodeInt)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }

        //    if (this.pAreaInt == add.pAreaInt)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }

        //    if (this.PublicAddressTitle.ToLower() == add.PublicAddressTitle.ToLower())
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }


        //    if (this.PublicAddressType.ToLower() == add.PublicAddressType.ToLower())
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }

        //    return result;

        //}

        //public wsCommon.dsAddress Save(wsCommon.dsAddress pDS, out String pstrErrorMsg, out int AuditTrailIdOut)
        //{
        //    if (clsGeneral.blnIsOfflineMode)
        //    {
        //        pstrErrorMsg = String.Empty;
        //        AuditTrailIdOut = 0;
        //        DataSet dsTemp = OfflineWorkManager.AddressManager.SaveAddress(pDS);
        //        dsAddress addressDataSet = new dsAddress();
        //        using (MemoryStream memStream = OfflineWorkManager.Utilities.GetDataSetInStream(dsTemp))
        //        {
        //            addressDataSet.ReadXml(memStream);
        //        }
        //        return addressDataSet;
        //    }
        //    else
        //    {
        //        return WSCommonObj.AddressSave(pDS, out pstrErrorMsg, out  AuditTrailIdOut);
        //    }
        //}

        //public dsAddress GetAddressFromPhoneNo(String phoneNo)
        //{
        //    if (clsGeneral.blnIsOfflineMode)
        //        return null;

        //    return WSCommonObj.GetAddressFromPhoneNo(phoneNo);
        //}

        //public void GetAddressFromPhoneNoAsync(String phoneNo)
        //{
        //    if (clsGeneral.blnIsOfflineMode)
        //        return;

        //     WSCommonObj.GetAddressFromPhoneNoAsync(phoneNo);
        //}

        //public DataSet SaveAddressInformation(Int32 countryId, String stateProvince, String city, String area, String postCode, String subPostCode, Int32 userId
        //    , Double dblLongitude, Double dblLatitude, Int32 iEasting, Int32 iNorthing, String strGridReference)
        //{
        //    return WSCommonObj.SaveAddressInformation(countryId, stateProvince, city, area, postCode, subPostCode, userId
        //          , dblLongitude, dblLatitude, iEasting, iNorthing, strGridReference);
        //}

        //public wsCommon.dsAddress GetTypedDSRecordOnPatientID(int pintPatientID)
        //{
        //    if (clsGeneral.blnIsOfflineMode)
        //        return null;
        //    return WSCommonObj.GetTypedDSRecordOnPatientID(pintPatientID);
        //}

        //public wsCommon.dsAddress GetTypedDSRecords(int pintAddressID)
        //{
        //    if (clsGeneral.blnIsOfflineMode)
        //    {
        //        wsCommon.dsAddress dsAddress = new dsAddress();
        //        DataSet untypedDS = new DataSet();
        //        untypedDS = OfflineWorkManager.AddressManager.FindAddressById(pintAddressID);
        //        if (untypedDS == null)
        //            return null;

        //        using (MemoryStream memStream = OfflineWorkManager.Utilities.GetDataSetInStream(untypedDS))
        //        {
        //            dsAddress.ReadXml(memStream);
        //            return dsAddress;
        //        }
        //    }

        //    return WSCommonObj.GetTypedDSRecords(pintAddressID);
        //}

        //public wsCommon.dsAddress GetAddressByGeoLocation(double p_dLongitude, double p_dLatitude)
        //{
        //    return WSCommonObj.GetAddressByGeoLocation(p_dLongitude, p_dLatitude);
        //}

        //public void DeleteAddress(string pstrAddressIDs)
        //{
        //    WSCommonObj.DeleteAddress(pstrAddressIDs);
        //}
        //public void DeleteClientAddress(string pstrAddressIDs, string pstrClientID)
        //{
        //    WSCommonObj.DeleteClientAddress(pstrAddressIDs, pstrClientID);
        //}

        //public Boolean UpdateLatLong(Double pLatitude, Double pLongitude, Int32 pSubPostCodeID)
        //{
        //    return WSCommonObj.UpdateLatLong(pLatitude, pLongitude, pSubPostCodeID);
        //}
        //public DataSet GetAddressByAddressIDForWeb(int pintAddressID)
        //{
        //    return WSAddressObj.loadAddressByAddressIDForWeb(pintAddressID);
        //}

        //public DataSet GetDispatchsWithoutLocationName()
        //{
        //    //initialize web service object
        //    BusinessRule.wsCommon.wsCommon common = new BusinessRule.wsCommon.wsCommon();

        //    if (WsBaseUrlStr != "")
        //        common.Url = WsBaseUrlStr + "/CADServices/wsCommon.asmx";
        //    else if (Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL != "")
        //        common.Url = Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL + "/CADServices/wsCommon.asmx";

        //    return common.GetDispatchsWithoutLocationName();
        //}

        //public void UpdateDispatchLocation(Int32 vehicleDispatchId, String locationName)
        //{
        //    //initialize web service object
        //    BusinessRule.wsCommon.wsCommon common = new BusinessRule.wsCommon.wsCommon();

        //    if (WsBaseUrlStr != "")
        //        common.Url = WsBaseUrlStr + "/CADServices/wsCommon.asmx";
        //    else if (Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL != "")
        //        common.Url = Common.dsSettings.Obj.ClientAppSettings[0].WSBaseURL + "/CADServices/wsCommon.asmx";

        //    common.UpdateDispatchLocation(vehicleDispatchId, locationName);
        //}

        //public Int32  GetAddresses(Int32? postCodeID, Int32? subPostCodeID,Int32? addressID)
        //{
        //    if (postCodeID.HasValue && postCodeID.Value > 0 && subPostCodeID.HasValue && subPostCodeID.Value > 0)
        //        return WSAddressObj.GetAddresses(postCodeID, subPostCodeID, addressID);
        //    else
        //        return 0; 
        //}

        //public Int32 GetAddressesForPostCode(String postCode, String subPostCode)
        //{
        //    return WSAddressObj.GetAddressesForPostCode(postCode, subPostCode);
        //}

        //public string GetFullAddress(dsAddress.AddressRow AddressRow)
        //{
        //    string FullAddress = string.Empty;

        //    //if (AddressRow.AddressTitle.Trim() != String.Empty)
        //    //    FullAddress = AddressRow.AddressLine1 + " ";

        //    if (AddressRow.AddressLine1.Trim() != String.Empty)
        //        FullAddress = AddressRow.AddressLine1 + " ";

        //    if (!AddressRow.IsAddressLine4Null() && AddressRow.AddressLine4.Trim() != String.Empty)
        //        FullAddress += AddressRow.AddressLine4;

        //    if (AddressRow.AddressLine2.Trim() != String.Empty)
        //        FullAddress += AddressRow.AddressLine2 + ",";

        //    if (AddressRow.AddressLine3.Trim() != String.Empty)
        //        FullAddress += AddressRow.AddressLine3 + ",";

        //    if (FullAddress != String.Empty) FullAddress = FullAddress.Substring(0, FullAddress.Length - 1);

        //    return FullAddress;
        //}
    }

    public struct PublicAddressData
    {
        public Int32 TotalRecords;
        public List<clsAddress> PublicAddressesList;
    }
}
