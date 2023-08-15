using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
namespace JobPortal_ManagementSystem.Error
{
   
        public class ErrHandler
        {
            public static void WriteError(string errorMessage)
            {
                try
                {
                    string path = "~/Error/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
                    if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
                    {
                        File.Create(HttpContext.Current.Server.MapPath(path)).Close();
                    }

                    using (StreamWriter w = File.AppendText(HttpContext.Current.Server.MapPath(path)))
                    {
                        w.WriteLine("\r\nLog Entry : ");
                        w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        string err = "Error in: " + HttpContext.Current.Request.Url.ToString() +
                                      ". Error Message: " + errorMessage;
                        w.WriteLine(err);
                        w.WriteLine("__________");
                        w.Flush();
                        w.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Handle potential errors during error logging
                    WriteError(ex.Message);
                }
            }
        
    }
}