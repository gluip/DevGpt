using DevGpt.Commands.Web.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DevGpt.Commands.Web.Services
{
    public static class BrowserHelper
    {
        public static IList<string> GetBlockedUrls()
        {
            return new List<string>()
            {
                "https://googleads.",
                "https://www.googleads.",
                "https://pagead2.",
                "https://tpc.googlesyndication.com",
                "https://www.googletagmanager.com",
                "https://www.google-analytics.com",
                "https://www.google.com/pagead",
                "https://tags.tiqcdn.com",
                "https://dev.visualwebsiteoptimizer.com",
                "https://asr.mopinion.com"
            };
        }

        public static string CleanHtml(string html)
        {
            // strip comments from html
            html = Regex.Replace(html, "<!--.*?-->", "", RegexOptions.Singleline);
            // strip script tags from html
            html = Regex.Replace(html, "<script.*?</script>", "", RegexOptions.Singleline);
            // strip style tags from html
            html = Regex.Replace(html, "<style.*?</style>", "", RegexOptions.Singleline);
            // strip noscript tags from html
            html = Regex.Replace(html, "<noscript.*?</noscript>", "", RegexOptions.Singleline);
            // strip svg tags from html
            html = Regex.Replace(html, "<svg.*?</svg>", "", RegexOptions.Singleline);
            // strip style attributes from html
            html = Regex.Replace(html, " style=\".*?\"", "", RegexOptions.Singleline);
            // strip class attributes from html
            html = Regex.Replace(html, " class=\".*?\"", "", RegexOptions.Singleline);
            // string whitespace from html
            html = Regex.Replace(html, @"\s+", " ", RegexOptions.Singleline);
            // string base64 src attributes from html
            html = Regex.Replace(html, " src=\"data:image.*?\"", "", RegexOptions.Singleline);
            // remove head elements
            html = Regex.Replace(html, "<head.*?</head>", "", RegexOptions.Singleline);
            // remove header element
            html = Regex.Replace(html, "<header.*?</header>", "", RegexOptions.Singleline);
            // remove footer element
            html = Regex.Replace(html, "<footer.*?</footer>", "", RegexOptions.Singleline);
            return html;
        }
    }
}
