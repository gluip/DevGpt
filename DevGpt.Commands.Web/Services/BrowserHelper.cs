using DevGpt.Commands.Web.Google;
using HtmlAgilityPack;
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

        public static string StripInvisibleElements(string html)
        {
            
                // Replace "yourHtmlString" with the actual HTML string you want to parse

                // Load the HTML string into HtmlDocument
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                // Select all elements with the custom attribute 'data-invisible'
                var invisibleElements = htmlDocument.DocumentNode.SelectNodes("//*[@data-visible='false']");

                // Remove each invisible element
                if (invisibleElements != null)
                {
                    foreach (var invisibleElement in invisibleElements)
                    {
                        invisibleElement.Remove();
                    }
                }

                // Get the modified HTML string
                return htmlDocument.DocumentNode.OuterHtml;

                // Now 'modifiedHtmlString' contains the HTML with invisible elements removed
                // Use 'modifiedHtmlString' as needed
            
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
            // strip data-v attributes with values from html
            html = Regex.Replace(html, " data-.*?=\".*?\"", "", RegexOptions.Singleline);

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
