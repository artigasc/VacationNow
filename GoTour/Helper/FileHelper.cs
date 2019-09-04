using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GoTour.Helper {
        public static class FileHelper {
            public static string ReadFile(string valNameFile) {
                string vResult = string.Empty;
                try {
                    string vAppPath = System.IO.Path.GetDirectoryName(HttpContext.Current.Server.MapPath("~/"));
                    string vFullPath = string.Empty;
                    vFullPath = vAppPath + valNameFile;
                    if (!string.IsNullOrEmpty(vFullPath)) {
                        vResult = System.IO.File.ReadAllText(vFullPath, Encoding.Default);
                        return vResult;
                    }
                } catch (Exception) {
                    return vResult;

                }
                return vResult;
            }
        }
    }
