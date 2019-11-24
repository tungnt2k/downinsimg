using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DownInsImg.Models;

namespace DownInsImg.Process
{
    public class GetLink
    {
        private readonly WebClient _webClient = new WebClient()
        {
            BaseAddress = "https://www.instagram.com"
        };

        private string GetSource(string url)
        {
            string source = "";

            source = _webClient.DownloadString(url);

            return source;
        }

        public string GetLinkFromImgUrl(string imgUrl)
        {
            string source = GetSource(imgUrl);

            var link = Regex.Match(source, "property=\"og:image\" content=\"(?<link>.*)\"");

            return link.Groups["link"].Value;
        }
        public string GetLinkFromImgUrls(string imgUrl)
        {
            string source = GetSource("p/" + imgUrl);

            var link = Regex.Match(source, "property=\"og:image\" content=\"(?<link>.*)\"");

            return link.Groups["link"].Value;
        }
        public List<string> GetLinkFromUserUrl(string userUrl)
        {
            string source = GetSource(userUrl);
            List<string> links = new List<string>();
            var matches = Regex.Matches(source, "shortcode\":\"(?<shortCode>.*?)\"");

            foreach (Match match in matches)
            {
                var link = GetLinkFromImgUrls(match.Groups["shortCode"].Value);
                links.Add(link);
            }

            return links;
        }
        
    }
}
