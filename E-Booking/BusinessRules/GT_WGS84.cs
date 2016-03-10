using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRules
{
   public class GT_WGS84
    {

        private double latitude = 0;
        private double longitude = 0;

        public GT_WGS84()
        {
        }
        public void setDegrees(double Lat, double Lng)
        {
            this.latitude = Lat;
            this.longitude = Lng;
        }

        public bool isGreatBritain()
        {
            try
            {
                return this.latitude > 49 &&
                    this.latitude < 62 &&
                    this.longitude > -9.5 &&
                    this.longitude < 2.3;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        public bool isIreland()
        {
            try
            {
                return this.latitude > 51.2 &&
                    this.latitude < 55.73 &&
                    this.longitude > -12.2 &&
                    this.longitude < -4.8;

            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        public void getOSGB(out double Easting, out double Northing)
        {
            try
            {
                Easting = 0;
                Northing = 0;
                //var osgb=new GT_OSGB();
                if (this.isGreatBritain())
                {
                    int height = 0;

                    double x1 = Lat_Long_H_to_X(this.latitude, this.longitude, height, 6378137.00, 6356752.313);
                    double y1 = Lat_Long_H_to_Y(this.latitude, this.longitude, height, 6378137.00, 6356752.313);
                    double z1 = Lat_H_to_Z(this.latitude, height, 6378137.00, 6356752.313);

                    double x2 = Helmert_X(x1, y1, z1, -446.448, -0.2470, -0.8421, 20.4894);
                    double y2 = Helmert_Y(x1, y1, z1, 125.157, -0.1502, -0.8421, 20.4894);
                    double z2 = Helmert_Z(x1, y1, z1, -542.060, -0.1502, -0.2470, 20.4894);

                    var latitude2 = XYZ_to_Lat(x2, y2, z2, 6377563.396, 6356256.910);
                    var longitude2 = XYZ_to_Long(x2, y2);

                    var e = Lat_Long_to_East(latitude2, longitude2, 6377563.396, 6356256.910, 400000, 0.999601272, 49.00000, -2.00000);
                    var n = Lat_Long_to_North(latitude2, longitude2, 6377563.396, 6356256.910, 400000, -100000, 0.999601272, 49.00000, -2.00000);

                    Easting = Math.Round(e);
                    Northing = Math.Round(n);
                }
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
        private static double Lat_Long_H_to_Y(double PHI, double LAM, int H, double a, double b)
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
        private static double Lat_H_to_Z(double PHI, int H, double a, double b)
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
        private static double Helmert_X(double X, double Y, double Z, double DX, double Y_Rot, double Z_Rot, double s)
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
        private static double Helmert_Y(double X, double Y, double Z, double DY, double X_Rot, double Z_Rot, double s)
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
        private static double XYZ_to_Long(double X, double Y)
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
        private static double Lat_Long_to_East(double PHI, double LAM, double a, double b, double e0, double f0, double PHI0, double LAM0)
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
                #region Unused Locals
                //var RadPHI0 = PHI0 * (Pi / 180);
                #endregion 

                var Pi = 3.14159265358979;
                var RadPHI = PHI * (Pi / 180);
                var RadLAM = LAM * (Pi / 180);
                
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
        private static double Marc(double bf0, double n, double PHI0, double PHI)
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
