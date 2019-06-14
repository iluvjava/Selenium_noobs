using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WithThePython
{

    /// <summary>
    /// In this class, we investiate the process of making get or/and POST request in the 
    /// c# programming language. 
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Main enter point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            {
                Console.WriteLine("---");
                string res = makeGETRequest();
                IDictionary<string, object> j = JsonDecode(res);
                Console.WriteLine(res);
                Console.WriteLine("The URL is: \n");
                Console.WriteLine(j["url"]);
            }
            Console.WriteLine("Testing The Pokemon POST Api:\n");
            {
                string pokemonurlget =
                    "https://courses.cs.washington.edu/courses/cse154/webservices/pokedex/game.php";
                // Read text file. 

                Console.WriteLine(MakePostRequest(pokemonurlget).Result);

                // string Post_Data = System.IO.File.ReadAllText("Formdata.txt");
                // Console.WriteLine(Post_Data);
                // Task<string> response = PostAsync
                //     (
                //         pokemonurlget,
                //         Post_Data,
                //         "multipart/form-data; boundary=----WebKitFormBoundaryPAFE87rrx5myY4GX"
                //     );
                // string responsetext = response.Result;
                // Console.WriteLine(responsetext);
               
            }
            Console.WriteLine("Make another get request using the new method: ");
            {
                string theurl = @"https://backend.deviantart.com/oembed?url=http%3A%2F%2Ffav.me%2Fd2enxz7";
                Console.WriteLine(MakeGetRequest(theurl).Result);
            }
            Thread.Sleep(-1);

        }

        /// <summary>
        /// stuff
        /// </summary>
        /// <returns>
        /// true
        /// </returns>
        public static bool TestMethod()
        {
            return true;
        }

        /// <summary>
        /// 
        /// Make a GET request to an url. 
        /// </summary>
        public static string makeGETRequest()
        {
            string theurl = @"https://backend.deviantart.com/oembed?url=http%3A%2F%2Ffav.me%2Fd2enxz7";
            Task<string> t = GetAsync(theurl);
            return t.Result;
        }

        /// <summary>
        /// A async method that returns the content of the response from uri 
        /// as a string. 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (System.IO.Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// This method make a POST request or more. It return content as string. 
        /// 
        /// </summary>
        /// <param name="uri">
        /// string for the uri
        /// </param>
        /// <param name="data">
        /// The raw data you want to post to the uri
        /// It has to be url encoded. 
        /// </param>
        /// <param name="contentType">
        ///    http://www.iana.org/assignments/media-types/media-types.xhtml
        ///    The content type seems also to be the header of the request. 
        /// </param>
        /// <param name="method">
        /// POST OR GET.
        /// </param>
        /// <param name= "cookies">
        /// An instance of a cookie container
        /// </param>
        /// <param name="formdata">
        /// An instance of the MultipartFormDataContent for the POST request. 
        /// </param>
        /// <returns>
        /// Response as a string. 
        /// </returns>
        public static async Task<string> PostAsync
        (
            string uri,
            string data, 
            string contentType,
            string method = "POST",
            CookieContainer cookies = null
        )
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.ContentLength = dataBytes.Length;
                request.ContentType = contentType;
                request.Method = method;
                if (cookies != null) request.CookieContainer = cookies;
                //request.UserAgent =
                //"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.90 Safari/537.36";

                using (Stream requestBody = request.GetRequestStream())
                {
                    await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("Error Occurred when POST request is processed.\n");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.TargetSite);
                Console.WriteLine(e.Data);
                return null;
            }

        }

        /// <summary>
        /// Convert a string represeting JSON object to a map object in c#
        /// </summary>
        /// <returns>
        /// A map that represents a recursive JSON object. 
        /// </returns>
        public static IDictionary<string, object> JsonDecode(string j)
        {
            IDictionary<string, object> values 
                = JsonConvert.DeserializeObject<Dictionary<string, object>>(j);
            return values;
        }


        public static async Task<string> MakePostRequest(string url)
        {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
               { "startgame", "true" },
               { "mypokemon", "detective-pikachu" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<string> MakeGetRequest
            (
                string url,
                HttpRequestHeaders header = null
            )
        {
            HttpClient client = new HttpClient();
            if (header != null) client.DefaultRequestHeaders = header;
            var response = await client.GetAsync(url);
            string responsestring = await response.Content.ReadAsStringAsync();
            return responsestring;
        }
    }
}
