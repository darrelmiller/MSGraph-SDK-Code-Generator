﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typewriter
{
    using NLog;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;

        internal class MetadataResolver
        {
            internal static Logger Logger => LogManager.GetLogger("MetadataResolver");

            public static string GetMetadata(string metadataPath)
            {
                string csdlContents = "";

                if (Uri.IsWellFormedUriString(metadataPath, UriKind.Absolute))
                {
                    Logger.Info("Downloading metadata from {0}.", metadataPath);

                    csdlContents = LoadEdmxFromWeb(metadataPath).Result;
                }
                else
                {
                    Logger.Info("Loading metadata from {0}.", metadataPath);

                    csdlContents = LoadEdmxFromFile(metadataPath);
                }
                return csdlContents;
            }

            private static string LoadEdmxFromFile(string filepath)
            {
                return File.ReadAllText(filepath);
            }

            private static async Task<string> LoadEdmxFromWeb(string uri)
            {
                var httpClient = new HttpClient();
             
                var result = await httpClient.GetStringAsync(new Uri(uri));

                return result;
            }
        }
}
