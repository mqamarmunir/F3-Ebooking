using System;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using System.Xml;


namespace BusinessRules
{
    # region Code Revew
    //********************************************************************************
    //			(Please don't delete this stamp)               
    //					
    //				***	Code Review ***				
    //
    //			FOR Reviewer USE ONLY
    //			------------------------------------------
    //			Date:		30/JAN/2006
    //			By:			Unknows
    //			Priority:	Normal
    //			Comment:	Please provide the comments for method and prominent statments and following coding standers for statments
    //			
    //			FOR Fixing Personnel USE ONYLY
    //			------------------------------------------
    //			Date:		//
    //			By:			
    //			Status:		Not Fixed
    //			Comment:	No Comments
    //
    //	
    //********************************************************************************
    # endregion

    /// <summary>
    /// Summary description for Rijndael.
    /// </summary>
    public class SysCrypt
    {
        #region Unused Data Members
        //private SymmetricAlgorithm mobjCryptoService;
        #endregion 
        #region Fields

       
        private byte[] md5Key, md5IV;
        private byte[] signature;
        const int BIN_SIZE = 4096;

        #endregion

        #region Construction

        public SysCrypt()
        {
            //mobjCryptoService = new RijndaelManaged();
            GenerateSignature();
            GenerateKey();
            GenerateIV();
        }


        #endregion

        #region Helper Functions

        private void GenerateSignature()
        {
            try
            {
                signature = new byte[16] {
											 123,	078,	099,	166,
											 000,	043,	244,	008,
											 005,	089,	239,	255,
											 045,	188,	007,	033
										 };
            }
            catch
            {
                throw;
            }
        }

        private void GenerateKey()
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                StringBuilder hash = new StringBuilder(GetKey(1) + GetKey(2));

                // Manipulate the hash string - not strictly necessary.
                for (int i = 1; i < hash.Length; i += 2)
                {
                    char c = hash[i - 1];
                    hash[i - 1] = hash[i];
                    hash[i] = c;
                }
                // Convert the string into a byte array.
                Encoding unicode = Encoding.Unicode;
                byte[] unicodeBytes = unicode.GetBytes(hash.ToString());
                // Compute the key from the byte array
                md5Key = md5.ComputeHash(unicodeBytes);
            }
            catch
            {
                throw;
            }
        }
        private void GenerateIV()
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                string hash = GetKey(1) + GetKey(2);

                // Convert the string into a byte array.
                Encoding unicode = Encoding.Unicode;
                byte[] unicodeBytes = unicode.GetBytes(hash);

                // Compute the IV from the byte array
                md5IV = md5.ComputeHash(unicodeBytes);
            }
            catch
            {
                throw;
            }
        }
        private static string GetKey(int Sequence)
        {
            try
            {
                if (Sequence == 1)
                {
                    string[] K1 = { "OA", "PY", "0N", "E9" };
                    return K1[0] + K1[1] + K1[2] + K1[3];
                }
                else if (Sequence == 2)
                {
                    string[] K2 = { "T0", "EX", "L1", "VZ" };
                    return K2[0] + K2[1] + K2[2] + K2[3];
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Methods to write and verify the signature

        private void WriteSignature(FileStream fOut)
        {
            try
            {
                fOut.Position = 0;
                fOut.Write(signature, 0, 16);
            }
            catch
            {
                throw;
            }
        }
        private bool VerifySignature(FileStream fIn)
        {
            try
            {
                byte[] bin = new byte[16];
                fIn.Read(bin, 0, 16);
                for (int i = 0; i < 16; i++)
                {
                    if (bin[i] != signature[i])
                    {
                        return false;
                    }
                }
                // Reset file pointer.
                fIn.Position = 0;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void WriteSignature(MemoryStream fOut)
        {
            try
            {
                fOut.Position = 0;
                fOut.Write(signature, 0, 16);
            }
            catch
            {
                throw;
            }
        }
        private bool VerifySignature(MemoryStream fIn)
        {
            try
            {
                byte[] bin = new byte[16];
                fIn.Read(bin, 0, 16);
                for (int i = 0; i < 16; i++)
                {
                    if (bin[i] != signature[i])
                    {
                        return false;
                    }
                }
                // Reset file pointer.
                fIn.Position = 0;
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Encrypting/Decrypting strings

        public string EncryptingString(string Source)
        {
            try
            {
                return Encrypting(Source);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string Encrypting(string Source)
        {
            try
            {
                byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);

                // create a MemoryStream so that the process can be done without I/O files
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                WriteSignature(ms);
                // create an Encryptor
                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform encrypto = rijn.CreateEncryptor(md5Key, md5IV);

                // create Crypto Stream that transforms a stream using the encryption
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

                // write out encrypted content into MemoryStream
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();


                // get the output and trim the '\0' bytes
                byte[] bout = ms.ToArray();
                //				byte[] bytOut = ms.GetBuffer();
                //				int i = 0;
                //				for (i = 0; i < bytOut.Length; i++)
                //					if (bytOut[i] == 0 && i>bytIn.Length)
                //						break;
                // convert into Base64 so that the result can be used in xml
                return System.Convert.ToBase64String(bout, 0, bout.Length);
            }
            catch (Exception ex)
            {
                throw ex;
                //return string.Empty;
            }
        }


        public string DecryptingString(string Source)
        {
            try
            {
                return Decrypting(Source);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string Decrypting(string Source)
        {

            try
            {
                // create a MemoryStream with the input
                byte[] bytIn = System.Convert.FromBase64String(Source);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);
                if (!VerifySignature(ms))
                {
                    throw new Exception("Invalid signature");
                }

                byte[] encryptedXmlData = new byte[(int)ms.Length - signature.Length];
                ms.Position = signature.Length;
                ms.Read(encryptedXmlData, 0, encryptedXmlData.Length);

                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform decrypto = rijn.CreateDecryptor(md5Key, md5IV);

                MemoryStream msdec = new MemoryStream(encryptedXmlData, 0, encryptedXmlData.Length);

                //ms.Write(encryptedXmlData,0,encryptedXmlData.Length ) ;
                // create Crypto Stream that transforms a stream using the decryption
                CryptoStream cs = new CryptoStream(msdec, decrypto, CryptoStreamMode.Read);

                // read out the result from the Crypto Stream			
                System.IO.StreamReader sr = new System.IO.StreamReader(cs);
                string str = sr.ReadToEnd();
                return str;
            }
            catch (Exception ex)
            {
                throw ex;//return string.Empty;
            }
        }




        public StreamReader retSRDecrypt(string Source)
        {
            try
            {
                byte[] bytIn = System.Convert.FromBase64String(Source);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);
                if (!VerifySignature(ms))
                {
                    throw new Exception("Invalid Signature");
                }

                byte[] encryptedXmlData = new byte[(int)ms.Length - signature.Length];
                ms.Position = signature.Length;
                ms.Read(encryptedXmlData, 0, encryptedXmlData.Length);

                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform decrypto = rijn.CreateDecryptor(md5Key, md5IV);

                MemoryStream msdec = new MemoryStream(encryptedXmlData, 0, encryptedXmlData.Length);

                //ms.Write(encryptedXmlData,0,encryptedXmlData.Length ) ;
                // create Crypto Stream that transforms a stream using the decryption
                CryptoStream cs = new CryptoStream(msdec, decrypto, CryptoStreamMode.Read);

                // read out the result from the Crypto Stream			
                System.IO.StreamReader sr = new System.IO.StreamReader(cs);
                //string str = sr.ReadToEnd() ;
                return sr;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Encrypting/Decrypting streams
        public MemoryStream EncryptingStream(MemoryStream mstrm)
        {
            try
            {
                //byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);			
                byte[] bytIn = mstrm.ToArray();//System.Text.ASCIIEncoding.ASCII.GetBytes(Source);			

                // create a MemoryStream so that the process can be done without I/O files
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                WriteSignature(ms);
                // create an Encryptor
                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform encrypto = rijn.CreateEncryptor(md5Key, md5IV);

                // create Crypto Stream that transforms a stream using the encryption
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

                // write out encrypted content into MemoryStream
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();


                // get the output and trim the '\0' bytes
                //				byte[] bout = ms.ToArray() ;
                //				//				byte[] bytOut = ms.GetBuffer();
                //				//				int i = 0;
                //				//				for (i = 0; i < bytOut.Length; i++)
                //				//					if (bytOut[i] == 0 && i>bytIn.Length)
                //				//						break;
                //				// convert into Base64 so that the result can be used in xml
                //				return System.Convert.ToBase64String( bout,0,bout.Length );						
                return ms;
            }
            catch (Exception ex)
            {
                throw ex;
                //return string.Empty;
            }
        }

        public MemoryStream DecryptingStream(MemoryStream mstrm)
        {

            try
            {
                // create a MemoryStream with the input
                //byte[] bytIn = System.Convert.FromBase64String(Source);

                byte[] bytIn = mstrm.ToArray();
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);
                if (!VerifySignature(ms))
                {
                    throw new Exception("Invalid Signature");
                }

                byte[] encryptedXmlData = new byte[(int)ms.Length - signature.Length];
                ms.Position = signature.Length;
                ms.Read(encryptedXmlData, 0, encryptedXmlData.Length);

                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform decrypto = rijn.CreateDecryptor(md5Key, md5IV);

                MemoryStream msdec = new MemoryStream(encryptedXmlData, 0, encryptedXmlData.Length);

                //ms.Write(encryptedXmlData,0,encryptedXmlData.Length ) ;
                // create Crypto Stream that transforms a stream using the decryption
                CryptoStream cs = new CryptoStream(msdec, decrypto, CryptoStreamMode.Read);

                // read out the result from the Crypto Stream			

                byte[] PlainText = new byte[bytIn.Length];


                // Start decrypting.

                int DecryptedCount = cs.Read(PlainText, 0, PlainText.Length);

                MemoryStream decMS = new MemoryStream();
                decMS.Write(PlainText, 0, DecryptedCount);

                //				decMS.Write();
                //				System.IO.StreamReader sr = new System.IO.StreamReader( cs );			
                //				string str = sr.ReadToEnd() ;
                //return str;
                //decMS.Position = 0;
                return decMS;
            }
            catch (Exception ex)
            {
                throw ex;//return string.Empty;
            }
        }

        #endregion

        #region Encrypting/Decrypting xml files

        public DataSet ReadEncryptedXML(string fileName)
        {
            try
            {
                if (!(File.Exists(fileName)))
                {
                    //if xml encrypted file not exist at the specififed location then this error occur
                    FileNotFoundException ex = new FileNotFoundException("File not exists", fileName);
                    throw ex;
                }

                FileInfo fi = new FileInfo(fileName);
                FileStream inFile;

                #region Check for possible errors (includes verification of the signature).

                if (!fi.Exists)
                {
                    throw new FileNotFoundException("File not exists", fileName);
                }
                if (fi.Length > Int32.MaxValue)
                {
                    throw new Exception("File length out of range.");
                }

                try
                {
                    inFile = new FileStream(fileName, FileMode.Open);
                    //inFile =  new FileStream(fi.Name , FileMode.Open);
                }
                catch (Exception exc)
                {
                    Trace.WriteLine(exc.Message + "Cannot perform decryption");
                    //return null;

                    //close stream of file is empty
                    //encryptedXmlStream.Flush();
                    //encryptedXmlStream.Close();
                    //	inFile.Close();


                    Exception ex = new Exception("File is empty" + fileName);
                    throw ex;

                }
                if (!VerifySignature(inFile))
                {
                    throw new Exception("Invalid signature");
                }
                #endregion

                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform decryptor = rijn.CreateDecryptor(md5Key, md5IV);
                // Allocate byte array buffer to read only the xml part of the file (ie everything following the signature).
                byte[] encryptedXmlData = new byte[(int)fi.Length - signature.Length];
                inFile.Position = signature.Length;
                inFile.Read(encryptedXmlData, 0, encryptedXmlData.Length);

                // Convert the byte array to a MemoryStream object so that it can be passed on to the CryptoStream
                MemoryStream encryptedXmlStream = new MemoryStream(encryptedXmlData);
                // Create a CryptoStream, bound to the MemoryStream containing the encrypted xml data
                CryptoStream csDecrypt = new CryptoStream(encryptedXmlStream, decryptor, CryptoStreamMode.Read);

                // Read in the DataSet from the CryptoStream
                DataSet data = new DataSet();
                try
                {
                    data.ReadXml(csDecrypt, XmlReadMode.Auto);
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.ToString());
                }

                // flush & close files.
                encryptedXmlStream.Flush();
                encryptedXmlStream.Close();
                inFile.Close();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable tableReadEncryptedXML(string fileName)
        {
            try
            {
                if (!(File.Exists(fileName)))
                {
                    //if xml encrypted file not exist at the specififed location then this error occur
                    FileNotFoundException ex = new FileNotFoundException("File not found", fileName);
                    throw ex;
                }

                FileInfo fi = new FileInfo(fileName);
                FileStream inFile;

                #region Check for possible errors (includes verification of the signature).

                if (!fi.Exists)
                {
                    throw new Exception("File not found");
                }
                if (fi.Length > Int32.MaxValue)
                {
                    throw new Exception("File length out of range.");
                }

                try
                {
                    inFile = new FileStream(fileName, FileMode.Open);
                    //inFile =  new FileStream(fi.Name , FileMode.Open);
                }
                catch (Exception exc)
                {
                    Trace.WriteLine(exc.Message + "Cannot perform decryption");
                    //return null;

                    //close stream of file is empty
                    //encryptedXmlStream.Flush();
                    //encryptedXmlStream.Close();
                    //	inFile.Close();


                    Exception ex = new Exception("File is empty " + fileName);
                    throw ex;

                }
                if (!VerifySignature(inFile))
                {
                    throw new Exception("Invalid signature");
                }
                #endregion

                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform decryptor = rijn.CreateDecryptor(md5Key, md5IV);
                // Allocate byte array buffer to read only the xml part of the file (ie everything following the signature).
                byte[] encryptedXmlData = new byte[(int)fi.Length - signature.Length];
                inFile.Position = signature.Length;
                inFile.Read(encryptedXmlData, 0, encryptedXmlData.Length);

                // Convert the byte array to a MemoryStream object so that it can be passed on to the CryptoStream
                MemoryStream encryptedXmlStream = new MemoryStream(encryptedXmlData);
                // Create a CryptoStream, bound to the MemoryStream containing the encrypted xml data
                CryptoStream csDecrypt = new CryptoStream(encryptedXmlStream, decryptor, CryptoStreamMode.Read);

                // Read in the DataSet from the CryptoStream
                DataSet data = new DataSet();
                try
                {
                    data.ReadXml(csDecrypt, XmlReadMode.Auto);
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.ToString());
                }

                // flush & close files.
                encryptedXmlStream.Flush();
                encryptedXmlStream.Close();
                inFile.Close();
                //DataTable dtMyTable = data.Tables[0].Copy();
                //dtMyTable.ParentRelations.Clear();
                //return dtMyTable;
                return data.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public XmlTextReader xmlReadEncryptedXML(string fileName)
        {
            try
            {
                if (!(File.Exists(fileName)))
                {
                    //if xml encrypted file not exist at the specififed location then this error occur
                    FileNotFoundException ex = new FileNotFoundException("File not exists", fileName);
                    throw ex;
                }

                FileInfo fi = new FileInfo(fileName);
                FileStream inFile;

                #region Check for possible errors (includes verification of the signature).

                if (!fi.Exists)
                {
                    throw new Exception("File not exists");
                }
                if (fi.Length > Int32.MaxValue)
                {
                    throw new Exception("File size out of range");
                }

                try
                {
                    inFile = new FileStream(fi.Name, FileMode.Open);
                }
                catch (Exception exc)
                {
                    Trace.WriteLine(exc.Message + "Cannot perform decryption");
                    return null;
                }
                if (!VerifySignature(inFile))
                {
                    throw new Exception("Invalid signature");
                }
                #endregion

                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform decryptor = rijn.CreateDecryptor(md5Key, md5IV);
                // Allocate byte array buffer to read only the xml part of the file (ie everything following the signature).
                byte[] encryptedXmlData = new byte[(int)fi.Length - signature.Length];
                inFile.Position = signature.Length;
                inFile.Read(encryptedXmlData, 0, encryptedXmlData.Length);

                // Convert the byte array to a MemoryStream object so that it can be passed on to the CryptoStream
                MemoryStream encryptedXmlStream = new MemoryStream(encryptedXmlData);
                
                // Create a CryptoStream, bound to the MemoryStream containing the encrypted xml data
                //    CryptoStream csDecrypt = new CryptoStream(encryptedXmlStream, decryptor, CryptoStreamMode.Read);

                XmlTextReader reader = null;
                // Read in the DataSet from the CryptoStream
                try
                {
                    encryptedXmlStream.Position = 0;
                    reader = new XmlTextReader(encryptedXmlStream);
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.ToString());
                }
                // flush & close files.
                encryptedXmlStream.Flush();
                encryptedXmlStream.Close();
                inFile.Close();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MemoryStream msReadEncryptedXML(string fileName)
        {
            try
            {
                if (!(File.Exists(fileName)))
                {
                    //if xml encrypted file not exist at the specififed location then this error occur
                    FileNotFoundException ex = new FileNotFoundException("File not exists", fileName);
                    throw ex;
                }

                FileInfo fi = new FileInfo(fileName);
                FileStream inFile;

                #region Check for possible errors (includes verification of the signature).

                if (!fi.Exists)
                {
                    throw new Exception("File not exists");
                }
                if (fi.Length > Int32.MaxValue)
                {
                    throw new Exception("File length out of range");
                }

                try
                {
                    inFile = new FileStream(fileName, FileMode.Open);
                }
                catch (Exception exc)
                {
                    Trace.WriteLine(exc.Message + "Cannot perform decryption");
                    return null;
                }
                if (!VerifySignature(inFile))
                {
                    throw new Exception("Invalid signature.");
                }
                #endregion

                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform decryptor = rijn.CreateDecryptor(md5Key, md5IV);
                // Allocate byte array buffer to read only the xml part of the file (ie everything following the signature).
                byte[] encryptedXmlData = new byte[(int)fi.Length - signature.Length];
                inFile.Position = signature.Length;
                inFile.Read(encryptedXmlData, 0, encryptedXmlData.Length);

                // Convert the byte array to a MemoryStream object so that it can be passed on to the CryptoStream
                MemoryStream encryptedXmlStream = new MemoryStream(encryptedXmlData);
                // Create a CryptoStream, bound to the MemoryStream containing the encrypted xml data
                CryptoStream csDecrypt = new CryptoStream(encryptedXmlStream, decryptor, CryptoStreamMode.Read);

                MemoryStream ms = new MemoryStream(); ;
                DataSet data = new DataSet();
                try
                {
                    data.ReadXml(csDecrypt, XmlReadMode.Auto);
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.ToString());
                }

                // flush & close files.
                encryptedXmlStream.Flush();
                encryptedXmlStream.Close();
                inFile.Close();
                data.WriteXml(ms);
                ms.Position = 0;
                return ms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void WriteEncryptedXML(MemoryStream xmlStream, string encFileName)
        {
            try
            {
                FileStream fOut;
                // Reset the pointer of the MemoryStream (which is at the EOF after the WriteXML function).
                xmlStream.Position = 0;

                // Create a write FileStream and write the signature to it (unencrypted).
                fOut = new FileStream(encFileName, FileMode.Create);
                WriteSignature(fOut);

                #region Encryption objects
                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform encryptor = rijn.CreateEncryptor(md5Key, md5IV);
                CryptoStream csEncrypt = new CryptoStream(fOut, encryptor, CryptoStreamMode.Write);
                #endregion
                //Create variables to help with read and write.
                byte[] bin = new byte[BIN_SIZE];			// Intermediate storage for the encryption.
                int rdlen = 0;									// The total number of bytes written.
                int totlen = (int)xmlStream.Length;	// The total length of the input stream.
                int len;											// The number of bytes to be written at a time.

                //Read from the input file, then encrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = xmlStream.Read(bin, 0, bin.Length);
                    if (len == 0 && rdlen == 0)
                    {
                        Trace.WriteLine("No read");
                        break;
                    }
                    csEncrypt.Write(bin, 0, len);
                    rdlen += len;
                }
                csEncrypt.FlushFinalBlock();
                csEncrypt.Close();
                fOut.Close();
                xmlStream.Close();
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void WriteEncryptedXML(DataSet ds, string encFileName)
        {
            try
            {
                FileStream fOut;
                // Reset the pointer of the MemoryStream (which is at the EOF after the WriteXML function).
                MemoryStream xmlStream = new MemoryStream();
                ds.WriteXml(xmlStream);
                xmlStream.Position = 0;

                // Create a write FileStream and write the signature to it (unencrypted).
                fOut = new FileStream(encFileName, FileMode.Create);
                WriteSignature(fOut);

                #region Encryption objects
                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Padding = PaddingMode.Zeros;
                ICryptoTransform encryptor = rijn.CreateEncryptor(md5Key, md5IV);
                CryptoStream csEncrypt = new CryptoStream(fOut, encryptor, CryptoStreamMode.Write);
                #endregion
                //Create variables to help with read and write.
                byte[] bin = new byte[BIN_SIZE];			// Intermediate storage for the encryption.
                int rdlen = 0;									// The total number of bytes written.
                int totlen = (int)xmlStream.Length;	// The total length of the input stream.
                int len;											// The number of bytes to be written at a time.

                //Read from the input file, then encrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = xmlStream.Read(bin, 0, bin.Length);
                    if (len == 0 && rdlen == 0)
                    {
                        Trace.WriteLine("No read");
                        break;
                    }
                    csEncrypt.Write(bin, 0, len);
                    rdlen += len;
                }
                csEncrypt.FlushFinalBlock();
                csEncrypt.Close();
                fOut.Close();
                xmlStream.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteEncryptedXML(DataTable dt, string encFileName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt.Copy());
                ds.AcceptChanges();
                WriteEncryptedXML(ds, encFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
        
        #endregion

    }
}
