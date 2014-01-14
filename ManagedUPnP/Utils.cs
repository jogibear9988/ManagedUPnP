//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Net;
using System.Xml;

namespace ManagedUPnP
{
    /// <summary>
    /// Provides all internal utility functions.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Gets the stream for a URL.
        /// </summary>
        /// <param name="url">The URL to get the stream.</param>
        /// <returns>The stream created.</returns>
        public static Stream GetURLStream(string url)
        {
            WebRequest lrRequest = WebRequest.Create(url);
            lrRequest.Timeout = Globals.RequestTimeoutMS;
            WebResponse lrResponse = lrRequest.GetResponse();
            return lrResponse.GetResponseStream();
        }

        /// <summary>
        /// Gets an XML Text reader for a URL.
        /// </summary>
        /// <param name="url">The URL to get the text reader for.</param>
        /// <returns>The XMLTextReader created.</returns>
        public static XmlTextReader GetXMLTextReader(string url)
        {
            return new XmlTextReader(GetURLStream(url));
        }

        /// <summary>
        /// Combines a two URLs.
        /// </summary>
        /// <param name="baseURL">The base URL.</param>
        /// <param name="relURL">The relative URL.</param>
        /// <returns>A string representing the new URL.</returns>
        public static string CombineURL(string baseURL, string relURL)
        {
            return (new Uri(new Uri(baseURL), relURL)).ToString();
        }
    }
}
