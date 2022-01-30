using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Processor
    {
        string URL = "http://localhost:59345/api/processor/ProcessString";
        public CalResult ProcessString(string input)
        {
            Uri u = new Uri(URL);
          
            AppRequest<string> requestData = new AppRequest<string>();
            requestData.Content = input;
            var payload = JsonConvert.SerializeObject(requestData);

            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => PostURI(u, c));
            t.Wait();
            return t.Result;
        }

        //public void GetResult(string input)
        //{
        //    Uri u = new Uri("http://localhost:59345/api/processor/ProcessString");
        //    var t = Task.Run(() => GetURI(u, input));
        //    t.Wait();

        //    Console.WriteLine(t.Result);
        //    Console.ReadLine();
        //}

        private static async Task<CalResult> PostURI(Uri u, HttpContent c)
        {
            CalResult response = null;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                {
                    var responseContent = result.Content.ReadAsStringAsync().Result;
                    AppResponse<CalResult> data = JsonConvert.DeserializeObject<AppResponse<CalResult>>(responseContent);
                    response = data.Content;
                }
            }
            return response;
        }

        //static async Task<CalResult> GetURI(Uri u, string content)
        //{
        //    HttpClient client = new HttpClient();
        //    CalResult response = null;
        //    string uri = "http://localhost:59345/api/processor/ProcessString?inputString="+ content;
        //    HttpResponseMessage result = await client.GetAsync(uri);
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var responseContent = result.Content.ReadAsStringAsync().Result;
        //        AppResponse<CalResult> data = JsonConvert.DeserializeObject<AppResponse<CalResult>>(responseContent);
        //        response = data.Content;
        //    }
        //    return response;
        //}


    }


}
