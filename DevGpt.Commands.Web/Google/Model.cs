using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DevGpt.Commands.Web.Google
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Context
    {
        [JsonPropertyName("title")]
        public string title { get; set; }
    }

    public class CseImage
    {
        [JsonPropertyName("src")]
        public string src { get; set; }
    }

    public class CseThumbnail
    {
        [JsonPropertyName("src")]
        public string src { get; set; }

        [JsonPropertyName("width")]
        public string width { get; set; }

        [JsonPropertyName("height")]
        public string height { get; set; }
    }

    

    public class Item
    {
        //[JsonPropertyName("kind")]
        //public string kind { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        //[JsonPropertyName("htmlTitle")]
        //public string htmlTitle { get; set; }

        [JsonPropertyName("link")]
        public string link { get; set; }

        //[JsonPropertyName("displayLink")]
        //public string displayLink { get; set; }

        //[JsonPropertyName("snippet")]
        //public string snippet { get; set; }

        //[JsonPropertyName("htmlSnippet")]
        //public string htmlSnippet { get; set; }

        //[JsonPropertyName("cacheId")]
        //public string cacheId { get; set; }

        //[JsonPropertyName("formattedUrl")]
        //public string formattedUrl { get; set; }

        //[JsonPropertyName("htmlFormattedUrl")]
        //public string htmlFormattedUrl { get; set; }

       
    }

    public class Metatag
    {
        [JsonPropertyName("viewport")]
        public string viewport { get; set; }

        [JsonPropertyName("og:title")]
        public string ogtitle { get; set; }

        [JsonPropertyName("og:description")]
        public string ogdescription { get; set; }

        [JsonPropertyName("referrer")]
        public string referrer { get; set; }

        [JsonPropertyName("og:image")]
        public string ogimage { get; set; }

        [JsonPropertyName("theme-color")]
        public string themecolor { get; set; }

        [JsonPropertyName("og:image:width")]
        public string ogimagewidth { get; set; }

        [JsonPropertyName("og:type")]
        public string ogtype { get; set; }

        [JsonPropertyName("og:image:height")]
        public string ogimageheight { get; set; }

        [JsonPropertyName("format-detection")]
        public string formatdetection { get; set; }

        [JsonPropertyName("al:ios:app_name")]
        public string aliosapp_name { get; set; }

        [JsonPropertyName("al:android:package")]
        public string alandroidpackage { get; set; }

        [JsonPropertyName("al:ios:url")]
        public string aliosurl { get; set; }

        [JsonPropertyName("color-scheme")]
        public string colorscheme { get; set; }

        [JsonPropertyName("al:ios:app_store_id")]
        public string aliosapp_store_id { get; set; }

        [JsonPropertyName("al:android:url")]
        public string alandroidurl { get; set; }

        [JsonPropertyName("apple-mobile-web-app-status-bar-style")]
        public string applemobilewebappstatusbarstyle { get; set; }

        [JsonPropertyName("mobile-web-app-capable")]
        public string mobilewebappcapable { get; set; }

        [JsonPropertyName("og:url")]
        public string ogurl { get; set; }

        [JsonPropertyName("al:android:app_name")]
        public string alandroidapp_name { get; set; }

        [JsonPropertyName("comment_count")]
        public string comment_count { get; set; }

        [JsonPropertyName("msapplication-config")]
        public string msapplicationconfig { get; set; }

        [JsonPropertyName("article:published_time")]
        public DateTime? articlepublished_time { get; set; }

        [JsonPropertyName("twitter:card")]
        public string twittercard { get; set; }

        [JsonPropertyName("og:site_name")]
        public string ogsite_name { get; set; }

        [JsonPropertyName("msapplication-tileimage")]
        public string msapplicationtileimage { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("body")]
        public string body { get; set; }

        [JsonPropertyName("twitter:image")]
        public string twitterimage { get; set; }

        [JsonPropertyName("twitter:site")]
        public string twittersite { get; set; }

        [JsonPropertyName("content_type")]
        public string content_type { get; set; }

        [JsonPropertyName("article:modified_time")]
        public DateTime? articlemodified_time { get; set; }

        [JsonPropertyName("published_at")]
        public string published_at { get; set; }

        [JsonPropertyName("msapplication-tilecolor")]
        public string msapplicationtilecolor { get; set; }

        [JsonPropertyName("image")]
        public string image { get; set; }

        [JsonPropertyName("og:image:alt")]
        public string ogimagealt { get; set; }

        [JsonPropertyName("twitter:title")]
        public string twittertitle { get; set; }

        [JsonPropertyName("author")]
        public string author { get; set; }

        [JsonPropertyName("topics")]
        public string topics { get; set; }

        [JsonPropertyName("tags")]
        public string tags { get; set; }

        [JsonPropertyName("fragment")]
        public string fragment { get; set; }

        [JsonPropertyName("post_id")]
        public string post_id { get; set; }

        [JsonPropertyName("twitter:description")]
        public string twitterdescription { get; set; }

        [JsonPropertyName("og:locale")]
        public string oglocale { get; set; }

        [JsonPropertyName("nyt_uri")]
        public string nyt_uri { get; set; }

        [JsonPropertyName("twitter:app:id:googleplay")]
        public string twitterappidgoogleplay { get; set; }

        [JsonPropertyName("pt")]
        public string pt { get; set; }

        [JsonPropertyName("twitter:url")]
        public string twitterurl { get; set; }

        [JsonPropertyName("pdate")]
        public string pdate { get; set; }

        [JsonPropertyName("articleid")]
        public string articleid { get; set; }

        [JsonPropertyName("al:ipad:app_store_id")]
        public string alipadapp_store_id { get; set; }

        [JsonPropertyName("twitter:app:name:googleplay")]
        public string twitterappnamegoogleplay { get; set; }

        [JsonPropertyName("pst")]
        public string pst { get; set; }

        [JsonPropertyName("twitter:image:alt")]
        public string twitterimagealt { get; set; }

        [JsonPropertyName("al:iphone:app_name")]
        public string aliphoneapp_name { get; set; }

        [JsonPropertyName("news_keywords")]
        public string news_keywords { get; set; }

        [JsonPropertyName("article:content_tier")]
        public string articlecontent_tier { get; set; }

        [JsonPropertyName("msapplication-starturl")]
        public string msapplicationstarturl { get; set; }

        [JsonPropertyName("article:section")]
        public string articlesection { get; set; }

        [JsonPropertyName("cg")]
        public string cg { get; set; }

        [JsonPropertyName("pubp_event_id")]
        public string pubp_event_id { get; set; }

        [JsonPropertyName("slack-app-id")]
        public string slackappid { get; set; }

        [JsonPropertyName("article:author")]
        public string articleauthor { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }

        [JsonPropertyName("article:tag")]
        public string articletag { get; set; }

        [JsonPropertyName("al:iphone:url")]
        public string aliphoneurl { get; set; }

        [JsonPropertyName("twitter:app:url:googleplay")]
        public string twitterappurlgoogleplay { get; set; }

        [JsonPropertyName("fb:app_id")]
        public string fbapp_id { get; set; }

        [JsonPropertyName("al:ipad:url")]
        public string alipadurl { get; set; }

        [JsonPropertyName("byl")]
        public string byl { get; set; }

        [JsonPropertyName("al:iphone:app_store_id")]
        public string aliphoneapp_store_id { get; set; }

        [JsonPropertyName("al:ipad:app_name")]
        public string alipadapp_name { get; set; }

        [JsonPropertyName("article:opinion")]
        public string articleopinion { get; set; }

        [JsonPropertyName("application-name")]
        public string applicationname { get; set; }

        [JsonPropertyName("google")]
        public string google { get; set; }

        [JsonPropertyName("twitter:text:description")]
        public string twittertextdescription { get; set; }

        [JsonPropertyName("publication_date")]
        public string publication_date { get; set; }

        [JsonPropertyName("accessibility_version")]
        public string accessibility_version { get; set; }

        [JsonPropertyName("apple-itunes-app")]
        public string appleitunesapp { get; set; }

        [JsonPropertyName("fb:pages")]
        public string fbpages { get; set; }

        [JsonPropertyName("og:nationality")]
        public string ognationality { get; set; }

        [JsonPropertyName("fb:admins")]
        public string fbadmins { get; set; }

        [JsonPropertyName("og:birthyear")]
        public string ogbirthyear { get; set; }

        [JsonPropertyName("scg")]
        public string scg { get; set; }

        [JsonPropertyName("og:image:url")]
        public string ogimageurl { get; set; }

        [JsonPropertyName("ga-datalayer")]
        public string gadatalayer { get; set; }

        [JsonPropertyName("og:image:type")]
        public string ogimagetype { get; set; }

        [JsonPropertyName("brightspot.contentid")]
        public string brightspotcontentid { get; set; }

        [JsonPropertyName("gtm-datalayer")]
        public string gtmdatalayer { get; set; }
    }

    public class NextPage
    {
        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("totalResults")]
        public string totalResults { get; set; }

        [JsonPropertyName("searchTerms")]
        public string searchTerms { get; set; }

        [JsonPropertyName("count")]
        public int count { get; set; }

        [JsonPropertyName("startIndex")]
        public int startIndex { get; set; }

        [JsonPropertyName("inputEncoding")]
        public string inputEncoding { get; set; }

        [JsonPropertyName("outputEncoding")]
        public string outputEncoding { get; set; }

        [JsonPropertyName("safe")]
        public string safe { get; set; }

        [JsonPropertyName("cx")]
        public string cx { get; set; }
    }

    public class Pagemap
    {
        [JsonPropertyName("cse_thumbnail")]
        public List<CseThumbnail> cse_thumbnail { get; set; }

        [JsonPropertyName("metatags")]
        public List<Metatag> metatags { get; set; }

        [JsonPropertyName("cse_image")]
        public List<CseImage> cse_image { get; set; }

        //[JsonPropertyName("hcard")]
        //public List<Hcard> hcard { get; set; }

        [JsonPropertyName("person")]
        public List<Person> person { get; set; }

        [JsonPropertyName("xfn")]
        public List<Xfn> xfn { get; set; }
    }

    public class Person
    {
        [JsonPropertyName("role")]
        public string role { get; set; }
    }

    public class Queries
    {
        [JsonPropertyName("request")]
        public List<Request> request { get; set; }

        [JsonPropertyName("nextPage")]
        public List<NextPage> nextPage { get; set; }
    }

    public class Request
    {
        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("totalResults")]
        public string totalResults { get; set; }

        [JsonPropertyName("searchTerms")]
        public string searchTerms { get; set; }

        [JsonPropertyName("count")]
        public int count { get; set; }

        [JsonPropertyName("startIndex")]
        public int startIndex { get; set; }

        [JsonPropertyName("inputEncoding")]
        public string inputEncoding { get; set; }

        [JsonPropertyName("outputEncoding")]
        public string outputEncoding { get; set; }

        [JsonPropertyName("safe")]
        public string safe { get; set; }

        [JsonPropertyName("cx")]
        public string cx { get; set; }
    }

    public class GoogleJsonRoot
    {
        //[JsonPropertyName("kind")]
        //public string kind { get; set; }

        //[JsonPropertyName("url")]
        //public Url url { get; set; }

        //[JsonPropertyName("queries")]
        //public Queries queries { get; set; }

        //[JsonPropertyName("context")]
        //public Context context { get; set; }

        //[JsonPropertyName("searchInformation")]
        //public SearchInformation searchInformation { get; set; }

        [JsonPropertyName("items")]
        public List<Item> items { get; set; }
    }

    public class SearchInformation
    {
        [JsonPropertyName("searchTime")]
        public double searchTime { get; set; }

        [JsonPropertyName("formattedSearchTime")]
        public string formattedSearchTime { get; set; }

        [JsonPropertyName("totalResults")]
        public string totalResults { get; set; }

        [JsonPropertyName("formattedTotalResults")]
        public string formattedTotalResults { get; set; }
    }

    public class Url
    {
        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("template")]
        public string template { get; set; }
    }

    public class Xfn
    {
    }




}
