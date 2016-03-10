using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessRules
{
    public class GT_OSGB
    {

        private double northings;
        private double eastings;
        private string status = "";


        public int Easting
        {
            get { return Convert.ToInt32(eastings); }
        }
        public int Northing
        {
            get { return Convert.ToInt32(northings); }
        }
        public string Error
        {
            get { return status; }
        }

        private List<string[]> prefixes = new List<string[]>();

        public GT_OSGB()
        {
            string[] str = { "SV", "SW", "SX", "SY", "SZ", "TV", "TW" };
            prefixes.Add(str);
            string[] str1 = { "SQ", "SR", "SS", "ST", "SU", "TQ", "TR" };
            prefixes.Add(str1);
            string[] str2 = { "SL", "SM", "SN", "SO", "SP", "TL", "TM" };
            prefixes.Add(str2);
            string[] str3 = { "SF", "SG", "SH", "SJ", "SK", "TF", "TG" };
            prefixes.Add(str3);
            string[] str4 = { "SA", "SB", "SC", "SD", "SE", "TA", "TB" };
            prefixes.Add(str4);
            string[] str5 = { "NV", "NW", "NX", "NY", "NZ", "OV", "OW" };
            prefixes.Add(str5);
            string[] str6 = { "NQ", "NR", "NS", "NT", "NU", "OQ", "OR" };
            prefixes.Add(str6);
            string[] str7 = { "NL", "NM", "NN", "NO", "NP", "OL", "OM" };
            prefixes.Add(str7);
            string[] str8 = { "NF", "NG", "NH", "NJ", "NK", "OF", "OG" };
            prefixes.Add(str8);
            string[] str9 = { "NA", "NB", "NC", "ND", "NE", "OA", "OB" };
            prefixes.Add(str9);
            string[] str10 = { "HV", "HW", "HX", "HY", "HZ", "JV", "JW" };
            prefixes.Add(str10);
            string[] str11 = { "HQ", "HR", "HS", "HT", "HU", "JQ", "JR" };
            prefixes.Add(str11);
            string[] str12 = { "HL", "HM", "HN", "HO", "HP", "JL", "JM" };
            prefixes.Add(str12);
        }

        public void setGridCoordinates(double eastings, double northings)
        {
            this.northings = northings;
            this.eastings = eastings;
            this.status = "OK";
        }

        public bool parseGridRef(string OSRefrence)
        {
            try
            {
                bool ok = false;


                this.northings = 0;
                this.eastings = 0;

                int precision = 0;

                for (precision = 5; precision >= 1; precision--)
                {
                    Regex RE = new Regex("^([A-Z]{2})\\s*(\\d{" + precision + "})\\s*(\\d{" + precision + "})$", RegexOptions.Multiline);
                    MatchCollection reff = RE.Matches(OSRefrence);


                    string[] gridRef = new string[0];
                    if (reff.Count > 0)
                        gridRef = OSRefrence.Split(' ');

                    if (gridRef.Length > 0)
                    {
                        string gridSheet = gridRef[0].ToString();
                        double gridEast = 0;
                        double gridNorth = 0;

                        //5x1 4x10 3x100 2x1000 1x10000 
                        if (precision > 0)
                        {
                            double mult = Math.Pow(10, 5 - precision);
                            gridEast = Convert.ToInt32(gridRef[1], 10) * mult;
                            gridNorth = Convert.ToInt32(gridRef[2], 10) * mult;
                        }

                        int x, y;
                        for (y = 0; y < prefixes.Count; y++)
                        {
                            string[] arr = prefixes[y];

                            for (x = 0; x < arr.Length; x++)
                            {
                                if (arr[x] == gridSheet)
                                {
                                    this.eastings = (x * 100000) + gridEast;
                                    this.northings = (y * 100000) + gridNorth;
                                    ok = true;
                                    break;
                                }
                            }
                            if (ok)
                                break;
                        }
                        if (ok)
                            break;
                    }
                }

                return ok;
            }
            catch (Exception exp)
            {
                status = exp.Message;
                return false;
            }
        }
        public void getWGS84(out double Latitude, out double Langitude)
        {
            try
            {
                int height = 0;

                double lat1 = E_N_to_Lat(this.eastings, this.northings, 6377563.396, 6356256.910, 400000, -100000, 0.999601272, 49.00000, -2.00000);
                double lon1 = E_N_to_Long(this.eastings, this.northings, 6377563.396, 6356256.910, 400000, -100000, 0.999601272, 49.00000, -2.00000);

                double x1 = Lat_Long_H_to_X(lat1, lon1, height, 6377563.396, 6356256.910);
                double y1 = Lat_Long_H_to_Y(lat1, lon1, height, 6377563.396, 6356256.910);
                double z1 = Lat_H_to_Z(lat1, height, 6377563.396, 6356256.910);

                double x2 = Helmert_X(x1, y1, z1, 446.448, 0.2470, 0.8421, -20.4894);
                double y2 = Helmert_Y(x1, y1, z1, -125.157, 0.1502, 0.8421, -20.4894);
                double z2 = Helmert_Z(x1, y1, z1, 542.060, 0.1502, 0.2470, -20.4894);

                double latitude = XYZ_to_Lat(x2, y2, z2, 6378137.000, 6356752.313);
                double longitude = XYZ_to_Long(x2, y2);

                var wgs84 = new GT_WGS84();
                Latitude = latitude;
                Langitude = longitude;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        public string getGridRef(int precision)
        {
            try
            {
                if (precision < 0)
                    precision = 0;
                if (precision > 5)
                    precision = 5;

                double e = 0;
                double n = 0;
                double y = 0;
                double x = 0;

                if (precision > 0)
                {
                    y = Math.Floor(this.northings / 100000);
                    x = Math.Floor(this.eastings / 100000);


                    e = Math.Round(this.eastings % 100000);
                    n = Math.Round(this.northings % 100000);


                    var div = (5 - precision);
                    e = Math.Round(e / Math.Pow(10, div));
                    n = Math.Round(n / Math.Pow(10, div));
                }
                int index = Convert.ToInt32(y);
                string[] pp = prefixes[index];

                string prefix = pp[Convert.ToInt32(x)];

                return prefix + " " + _zeropad(e, precision) + " " + this._zeropad(n, precision);

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }

        private void setError(string msg)
        {
            try
            {
                this.status = msg;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private string _zeropad(double num, int len)
        {
            try
            {
                var str = num.ToString();
                while (str.Length < len)
                {
                    str = '0' + str;
                }
                return str;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double E_N_to_Long(double East, double North, double a, double b, double e0, double n0, double f0, double PHI0, double LAM0)
        {
            try
            {
                //Un-project Transverse Mercator eastings and northings back to longitude.
                //Input: - _
                //eastings (East) and northings (North) in meters; _
                //ellipsoid axis dimensions (a & b) in meters; _
                //eastings (e0) and northings (n0) of false origin in meters; _
                //central meridian scale factor (f0) and _
                //latitude (PHI0) and longitude (LAM0) of false origin in decimal degrees.

                //REQUIRES THE "Marc" AND "InitialLat" FUNCTIONS

                //Convert angle measures to radians
                double Pi = 3.14159265358979;
                double RadPHI0 = PHI0 * (Pi / 180);
                double RadLAM0 = LAM0 * (Pi / 180);

                //Compute af0, bf0, e squared (e2), n and Et
                double af0 = a * f0;
                double bf0 = b * f0;
                double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
                double n = (af0 - bf0) / (af0 + bf0);
                double Et = East - e0;

                //Compute initial value for latitude (PHI) in radians
                double PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0);

                //Compute nu, rho and eta2 using value for PHId
                double nu = af0 / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(PHId), 2)))));
                double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(PHId), 2)));
                double eta2 = (nu / rho) - 1;

                //Compute Longitude
                double X = (Math.Pow(Math.Cos(PHId), -1)) / nu;
                double XI = ((Math.Pow(Math.Cos(PHId), -1)) / (6 * Math.Pow(nu, 3))) * ((nu / rho) + (2 * (Math.Pow(Math.Tan(PHId), 2))));
                double XII = ((Math.Pow(Math.Cos(PHId), -1)) / (120 * Math.Pow(nu, 5))) * (5 + (28 * (Math.Pow(Math.Tan(PHId), 2))) + (24 * (Math.Pow(Math.Tan(PHId), 4))));
                double XIIA = ((Math.Pow(Math.Cos(PHId), -1)) / (5040 * Math.Pow(nu, 7))) * (61 + (662 * (Math.Pow(Math.Tan(PHId), 2))) + (1320 * (Math.Pow(Math.Tan(PHId), 4))) + (720 * (Math.Pow(Math.Tan(PHId), 6))));

                double E_N_to_Long = (180 / Pi) * (RadLAM0 + (Et * X) - (Math.Pow(Et, 3) * XI) + (Math.Pow(Et, 5) * XII) - (Math.Pow(Et, 7) * XIIA));

                return E_N_to_Long;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double E_N_to_Lat(double East, double North, double a, double b, double e0, double n0, double f0, double PHI0, double LAM0)
        {
            try
            {
                //Un-project Transverse Mercator eastings and northings back to latitude.
                //Input: - _
                //eastings (East) and northings (North) in meters; _
                //ellipsoid axis dimensions (a & b) in meters; _
                //eastings (e0) and northings (n0) of false origin in meters; _
                //central meridian scale factor (f0) and _
                //latitude (PHI0) and longitude (LAM0) of false origin in decimal degrees.

                //'REQUIRES THE "Marc" AND "InitialLat" FUNCTIONS

                //Convert angle measures to radians
                double Pi = 3.14159265358979;
                double RadPHI0 = PHI0 * (Pi / 180);
                double RadLAM0 = LAM0 * (Pi / 180);

                //Compute af0, bf0, e squared (e2), n and Et
                double af0 = a * f0;
                double bf0 = b * f0;
                double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
                double n = (af0 - bf0) / (af0 + bf0);
                double Et = East - e0;

                //Compute initial value for latitude (PHI) in radians
                var PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0);

                //Compute nu, rho and eta2 using value for PHId
                var nu = af0 / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(PHId), 2)))));
                var rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(PHId), 2)));
                var eta2 = (nu / rho) - 1;

                //Compute Latitude
                var VII = (Math.Tan(PHId)) / (2 * rho * nu);
                var VIII = ((Math.Tan(PHId)) / (24 * rho * Math.Pow(nu, 3))) * (5 + (3 * (Math.Pow(Math.Tan(PHId), 2))) + eta2 - (9 * eta2 * (Math.Pow(Math.Tan(PHId), 2))));
                //var IX = ((Math.Tan(PHId)) / (720 * rho * Math.Pow(nu, 5))) * (61 + (90 * ((Math.Tan(PHId)) ^ 2)) + (45 * (Math.Pow(Math.Tan(PHId), 4))));
                var IX = ((Math.Tan(PHId)) / (720 * rho * Math.Pow(nu, 5))) * (61 + (90 * (Math.Pow((Math.Tan(PHId)), 2))) + (45 * (Math.Pow(Math.Tan(PHId), 4))));


                //var E_N_to_Lat = (180 / Pi) * (PHId - (Math.Pow(Et, 2) * VII) + (Math.Pow(Et, 4) * VIII) - ((Et ^ 6) * IX));
                var E_N_to_Lat = (180 / Pi) * (PHId - (Math.Pow(Et, 2) * VII) + (Math.Pow(Et, 4) * VIII) - ((Math.Pow(Et, 6)) * IX));

                return (E_N_to_Lat);

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double InitialLat(double North, double n0, double afo, double PHI0, double n, double bfo)
        {
            try
            {
                //Compute initial value for Latitude (PHI) IN RADIANS.
                //Input: - _
                //northing of point (North) and northing of false origin (n0) in meters; _
                //semi major axis multiplied by central meridian scale factor (af0) in meters; _
                //latitude of false origin (PHI0) IN RADIANS; _
                //n (computed from a, b and f0) and _
                //ellipsoid semi major axis multiplied by central meridian scale factor (bf0) in meters.

                //REQUIRES THE "Marc" FUNCTION
                //THIS FUNCTION IS CALLED BY THE "E_N_to_Lat", "E_N_to_Long" and "E_N_to_C" FUNCTIONS
                //THIS FUNCTION IS ALSO USED ON IT'S OWN IN THE  "Projection and Transformation Calculations.xls" SPREADSHEET

                //First PHI value (PHI1)
                var PHI1 = ((North - n0) / afo) + PHI0;

                //Calculate M
                var M = Marc(bfo, n, PHI0, PHI1);

                //Calculate new PHI value (PHI2)
                var PHI2 = ((North - n0 - M) / afo) + PHI1;

                //Iterate to get final value for InitialLat
                while (Math.Abs(North - n0 - M) > 0.00001)
                {
                    PHI2 = ((North - n0 - M) / afo) + PHI1;
                    M = Marc(bfo, n, PHI0, PHI2);
                    PHI1 = PHI2;
                }
                return PHI2;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Lat_Long_H_to_X(double PHI, double LAM, int H, double a, double b)
        {
            try
            {
                // Convert geodetic coords lat (PHI), long (LAM) and height (H) to cartesian X coordinate.
                // Input: - _
                //    Latitude (PHI)& Longitude (LAM) both in decimal degrees; _
                //  Ellipsoidal height (H) and ellipsoid axis dimensions (a & b) all in meters.

                // Convert angle measures to radians
                var Pi = 3.14159265358979;
                var RadPHI = PHI * (Pi / 180);
                var RadLAM = LAM * (Pi / 180);

                // Compute eccentricity squared and nu
                var e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
                var V = a / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(RadPHI), 2)))));

                // Compute X
                return (V + H) * (Math.Cos(RadPHI)) * (Math.Cos(RadLAM));

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Lat_Long_H_to_Y(double PHI, double LAM, int H, double a, double b)
        {
            try
            {
                // Convert geodetic coords lat (PHI), long (LAM) and height (H) to cartesian Y coordinate.
                // Input: - _
                // Latitude (PHI)& Longitude (LAM) both in decimal degrees; _
                // Ellipsoidal height (H) and ellipsoid axis dimensions (a & b) all in meters.

                // Convert angle measures to radians
                var Pi = 3.14159265358979;
                var RadPHI = PHI * (Pi / 180);
                var RadLAM = LAM * (Pi / 180);

                // Compute eccentricity squared and nu
                var e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
                var V = a / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(RadPHI), 2)))));

                // Compute Y
                return (V + H) * (Math.Cos(RadPHI)) * (Math.Sin(RadLAM));

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Lat_H_to_Z(double PHI, int H, double a, double b)
        {
            try
            {
                // Convert geodetic coord components latitude (PHI) and height (H) to cartesian Z coordinate.
                // Input: - _
                //    Latitude (PHI) decimal degrees; _
                // Ellipsoidal height (H) and ellipsoid axis dimensions (a & b) all in meters.

                // Convert angle measures to radians
                var Pi = 3.14159265358979;
                var RadPHI = PHI * (Pi / 180);

                // Compute eccentricity squared and nu
                var e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
                var V = a / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(RadPHI), 2)))));

                // Compute X
                return ((V * (1 - e2)) + H) * (Math.Sin(RadPHI));

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Helmert_X(double X, double Y, double Z, double DX, double Y_Rot, double Z_Rot, double s)
        {
            try
            {

                // (X, Y, Z, DX, Y_Rot, Z_Rot, s)
                // Computed Helmert transformed X coordinate.
                // Input: - _
                //    cartesian XYZ coords (X,Y,Z), X translation (DX) all in meters ; _
                // Y and Z rotations in seconds of arc (Y_Rot, Z_Rot) and scale in ppm (s).

                // Convert rotations to radians and ppm scale to a factor
                var Pi = 3.14159265358979;
                var sfactor = s * 0.000001;

                var RadY_Rot = (Y_Rot / 3600) * (Pi / 180);

                var RadZ_Rot = (Z_Rot / 3600) * (Pi / 180);

                //Compute transformed X coord
                return (X + (X * sfactor) - (Y * RadZ_Rot) + (Z * RadY_Rot) + DX);

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Helmert_Y(double X, double Y, double Z, double DY, double X_Rot, double Z_Rot, double s)
        {
            try
            {
                // (X, Y, Z, DY, X_Rot, Z_Rot, s)
                // Computed Helmert transformed Y coordinate.
                // Input: - _
                //    cartesian XYZ coords (X,Y,Z), Y translation (DY) all in meters ; _
                //  X and Z rotations in seconds of arc (X_Rot, Z_Rot) and scale in ppm (s).

                // Convert rotations to radians and ppm scale to a factor
                var Pi = 3.14159265358979;
                var sfactor = s * 0.000001;
                var RadX_Rot = (X_Rot / 3600) * (Pi / 180);
                var RadZ_Rot = (Z_Rot / 3600) * (Pi / 180);

                // Compute transformed Y coord
                return (X * RadZ_Rot) + Y + (Y * sfactor) - (Z * RadX_Rot) + DY;

            }
            catch (Exception exp)
            {
                throw (exp);
            }

        }
        private double Helmert_Z(double X, double Y, double Z, double DZ, double X_Rot, double Y_Rot, double s)
        {
            try
            {
                // (X, Y, Z, DZ, X_Rot, Y_Rot, s)
                // Computed Helmert transformed Z coordinate.
                // Input: - _
                //    cartesian XYZ coords (X,Y,Z), Z translation (DZ) all in meters ; _
                // X and Y rotations in seconds of arc (X_Rot, Y_Rot) and scale in ppm (s).
                // 
                // Convert rotations to radians and ppm scale to a factor
                var Pi = 3.14159265358979;
                var sfactor = s * 0.000001;
                var RadX_Rot = (X_Rot / 3600) * (Pi / 180);
                var RadY_Rot = (Y_Rot / 3600) * (Pi / 180);

                // Compute transformed Z coord
                return (-1 * X * RadY_Rot) + (Y * RadX_Rot) + Z + (Z * sfactor) + DZ;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double XYZ_to_Lat(double X, double Y, double Z, double a, double b)
        {
            try
            {
                // Convert XYZ to Latitude (PHI) in Dec Degrees.
                // Input: - _
                // XYZ cartesian coords (X,Y,Z) and ellipsoid axis dimensions (a & b), all in meters.

                // THIS FUNCTION REQUIRES THE "Iterate_XYZ_to_Lat" FUNCTION
                // THIS FUNCTION IS CALLED BY THE "XYZ_to_H" FUNCTION

                var RootXYSqr = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
                var e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
                var PHI1 = Math.Atan2(Z, (RootXYSqr * (1 - e2)));

                var PHI = Iterate_XYZ_to_Lat(a, e2, PHI1, Z, RootXYSqr);

                var Pi = 3.14159265358979;

                return PHI * (180 / Pi);

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double XYZ_to_Long(double X, double Y)
        {
            try
            {
                // Convert XYZ to Longitude (LAM) in Dec Degrees.
                // Input: - _
                // X and Y cartesian coords in meters.

                var Pi = 3.14159265358979;
                return Math.Atan2(Y, X) * (180 / Pi);

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Lat_Long_to_East(double PHI, double LAM, double a, double b, double e0, double f0, double PHI0, double LAM0)
        {
            try
            {
                //Project Latitude and longitude to Transverse Mercator eastings.
                //Input: - _
                //    Latitude (PHI) and Longitude (LAM) in decimal degrees; _
                //    ellipsoid axis dimensions (a & b) in meters; _
                //    eastings of false origin (e0) in meters; _
                //    central meridian scale factor (f0); _
                // latitude (PHI0) and longitude (LAM0) of false origin in decimal degrees.

                // Convert angle measures to radians
                var Pi = 3.14159265358979;
                var RadPHI = PHI * (Pi / 180);
                var RadLAM = LAM * (Pi / 180);
                var RadPHI0 = PHI0 * (Pi / 180);
                var RadLAM0 = LAM0 * (Pi / 180);

                var af0 = a * f0;
                var bf0 = b * f0;
                var e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
                var n = (af0 - bf0) / (af0 + bf0);
                var nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow(Math.Sin(RadPHI), 2))));
                var rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(RadPHI), 2)));
                var eta2 = (nu / rho) - 1;
                var p = RadLAM - RadLAM0;

                var IV = nu * (Math.Cos(RadPHI));
                var V = (nu / 6) * (Math.Pow(Math.Cos(RadPHI), 3)) * ((nu / rho) - (Math.Pow(Math.Tan(RadPHI), 2)));
                var VI = (nu / 120) * (Math.Pow(Math.Cos(RadPHI), 5)) * (5 - (18 * (Math.Pow(Math.Tan(RadPHI), 2))) + (Math.Pow(Math.Tan(RadPHI), 4)) + (14 * eta2) - (58 * (Math.Pow(Math.Tan(RadPHI), 2)) * eta2));

                return e0 + (p * IV) + (Math.Pow(p, 3) * V) + (Math.Pow(p, 5) * VI);

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Lat_Long_to_North(double PHI, double LAM, double a, double b, double e0, double n0, double f0, double PHI0, double LAM0)
        {
            try
            {
                // Project Latitude and longitude to Transverse Mercator northings
                // Input: - _
                // Latitude (PHI) and Longitude (LAM) in decimal degrees; _
                // ellipsoid axis dimensions (a & b) in meters; _
                // eastings (e0) and northings (n0) of false origin in meters; _
                // central meridian scale factor (f0); _
                // latitude (PHI0) and longitude (LAM0) of false origin in decimal degrees.

                // REQUIRES THE "Marc" FUNCTION

                // Convert angle measures to radians
                var Pi = 3.14159265358979;
                var RadPHI = PHI * (Pi / 180);
                var RadLAM = LAM * (Pi / 180);
                var RadPHI0 = PHI0 * (Pi / 180);
                var RadLAM0 = LAM0 * (Pi / 180);

                var af0 = a * f0;
                var bf0 = b * f0;
                var e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
                var n = (af0 - bf0) / (af0 + bf0);
                var nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow(Math.Sin(RadPHI), 2))));
                var rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(RadPHI), 2)));
                var eta2 = (nu / rho) - 1;
                var p = RadLAM - RadLAM0;
                var M = Marc(bf0, n, RadPHI0, RadPHI);

                var I = M + n0;
                var II = (nu / 2) * (Math.Sin(RadPHI)) * (Math.Cos(RadPHI));
                var III = ((nu / 24) * (Math.Sin(RadPHI)) * (Math.Pow(Math.Cos(RadPHI), 3))) * (5 - (Math.Pow(Math.Tan(RadPHI), 2)) + (9 * eta2));
                var IIIA = ((nu / 720) * (Math.Sin(RadPHI)) * (Math.Pow(Math.Cos(RadPHI), 5))) * (61 - (58 * (Math.Pow(Math.Tan(RadPHI), 2))) + (Math.Pow(Math.Tan(RadPHI), 4)));

                return I + (Math.Pow(p, 2) * II) + (Math.Pow(p, 4) * III) + (Math.Pow(p, 6) * IIIA);

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Iterate_XYZ_to_Lat(double a, double e2, double PHI1, double Z, double RootXYSqr)
        {
            try
            {
                // Iteratively computes Latitude (PHI).
                // Input: - _
                //    ellipsoid semi major axis (a) in meters; _
                //    eta squared (e2); _
                //    estimated value for latitude (PHI1) in radians; _
                //    cartesian Z coordinate (Z) in meters; _
                // RootXYSqr computed from X & Y in meters.

                // THIS FUNCTION IS CALLED BY THE "XYZ_to_PHI" FUNCTION
                // THIS FUNCTION IS ALSO USED ON IT'S OWN IN THE _
                // "Projection and Transformation Calculations.xls" SPREADSHEET


                var V = a / (Math.Sqrt(1 - (e2 * Math.Pow(Math.Sin(PHI1), 2))));
                var PHI2 = Math.Atan2((Z + (e2 * V * (Math.Sin(PHI1)))), RootXYSqr);

                while (Math.Abs(PHI1 - PHI2) > 0.000000001)
                {
                    PHI1 = PHI2;
                    V = a / (Math.Sqrt(1 - (e2 * Math.Pow(Math.Sin(PHI1), 2))));
                    PHI2 = Math.Atan2((Z + (e2 * V * (Math.Sin(PHI1)))), RootXYSqr);
                }

                return PHI2;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private double Marc(double bf0, double n, double PHI0, double PHI)
        {
            try
            {
                //Compute meridional arc.
                //Input: - _
                // ellipsoid semi major axis multiplied by central meridian scale factor (bf0) in meters; _
                // n (computed from a, b and f0); _
                // lat of false origin (PHI0) and initial or final latitude of point (PHI) IN RADIANS.

                //THIS FUNCTION IS CALLED BY THE - _
                // "Lat_Long_to_North" and "InitialLat" FUNCTIONS
                // THIS FUNCTION IS ALSO USED ON IT'S OWN IN THE "Projection and Transformation Calculations.xls" SPREADSHEET

                return bf0 * (((1 + n + ((5 / 4) * Math.Pow(n, 2)) + ((5 / 4) * Math.Pow(n, 3))) * (PHI - PHI0)) - (((3 * n) + (3 * Math.Pow(n, 2)) + ((21 / 8) * Math.Pow(n, 3))) * (Math.Sin(PHI - PHI0)) * (Math.Cos(PHI + PHI0))) + ((((15 / 8) * Math.Pow(n, 2)) + ((15 / 8) * Math.Pow(n, 3))) * (Math.Sin(2 * (PHI - PHI0))) * (Math.Cos(2 * (PHI + PHI0)))) - (((35 / 24) * Math.Pow(n, 3)) * (Math.Sin(3 * (PHI - PHI0))) * (Math.Cos(3 * (PHI + PHI0)))));

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
    }
}
