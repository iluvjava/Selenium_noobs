using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WithThePython
{
    /// <summary>
    /// Creates a class to handle all the things needed for a http request
    /// </summary>
    public class MyLittleRequest
    {
        // URL upon first request. 
        public string base_url;
        public IDictionary<string, string> cookie_jar;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">
        /// The extra parameters that should be appeneded 
        /// at the end of the url.
        /// </param>
        public MyLittleRequest(string url)
        {
            this.base_url = url;
        }

        /// <summary>
        /// Give params appeneded to url, it will make a get request. 
        /// </summary>
        /// <param name="Url_Params">
        /// String, representing the part at the end of the URL. 
        /// </param>
        /// <returns>
        /// A Http ResponseMessage for you to process. 
        /// </returns>
        public async Task<HttpResponseMessage> MakeGetRequest
        (
            string Url_Params = ""
        )
        {
            Uri uri = new Uri(this.base_url + Url_Params);
            HttpClientHandler handler = new HttpClientHandler();
            CookieContainer c = new CookieContainer();
            //prepare cookie
            if(this.cookie_jar!=null)
            foreach (KeyValuePair<string, string> kvp in this.cookie_jar)
            {
                c.Add(uri, new Cookie(kvp.Key, kvp.Value));
            }
            handler.CookieContainer = c;
            //prepare http client
            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = await client.GetAsync(uri);

            return response;
        }

        /// <summary>
        /// A method that makes a mpost request to the base url in the class. 
        /// </summary>
        /// <param name="body">
        /// 
        /// </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> MakePostRequest
        (IDictionary<string, string> formdata)
        {
            if (formdata == null)
            {
                formdata = new Dictionary<string, string>();
                formdata[""] = "";
            }
            string url = this.base_url;
            HttpClient client = new HttpClient();
            FormUrlEncodedContent content = new FormUrlEncodedContent(formdata);
            HttpResponseMessage response = await client.PostAsync(url, content);
            return response;
        }

    }
}
