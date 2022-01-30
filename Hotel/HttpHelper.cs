using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class HttpHelper
    {
        static string URL = "http://localhost:59345/api/hotel/{0}";

        private static async Task<T> PostURI<T, R>(string api, R requestData)
        {
            T response = default(T);
            AppRequest<R> request = new AppRequest<R>();
            request.Content = requestData;
            Uri u = new Uri(string.Format(URL, api));
            using (var client = new HttpClient())
            {
                var payload = JsonConvert.SerializeObject(request);
                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage result = await client.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                {
                    var responseContent = result.Content.ReadAsStringAsync().Result;
                    AppResponse<T> data = JsonConvert.DeserializeObject<AppResponse<T>>(responseContent);
                    response = data.Content;
                }
            }
            return response;
        }

        public IList<ComboItem> GetAvailableRooms()
        {
            var t = Task.Run(() => PostURI<IList<ComboItem>, string>("GetAvailableRooms", "empty"));
            t.Wait();
            return t.Result;
        }

        public IList<ComboItem> GetOccupiedRooms()
        {
            var t = Task.Run(() => PostURI<IList<ComboItem>, string>("GetOccupiedRooms", "empty"));
            t.Wait();
            return t.Result;
        }

        public string AssignRoom() 
        {
            var t = Task.Run(() => PostURI<string, string>("AssignRoom", "empty"));
            t.Wait();
            return t.Result;
        }

        public bool CheckoutRoom(string roomNumber)
        {
            var t = Task.Run(() => PostURI<bool, string>("CheckoutRoom", roomNumber));
            t.Wait();
            return t.Result;
        }
        //GetVacantRooms
        public IList<ComboItem> GetVacantRooms()
        {
            var t = Task.Run(() => PostURI<IList<ComboItem>, string>("GetVacantRooms", "empty"));
            t.Wait();
            return t.Result;
        }
        public bool MarkRoomCleaned(string roomNumber)
        {
            var t = Task.Run(() => PostURI<bool, string>("MarkRoomCleaned", roomNumber));
            t.Wait();
            return t.Result;
        }

        public bool MarkRoomForRepair(string roomNumber)
        {
            var t = Task.Run(() => PostURI<bool, string>("MarkRoomForRepair", roomNumber));
            t.Wait();
            return t.Result;
        }
        public bool MarkRepairCompleted(string roomNumber)
        {
            var t = Task.Run(() => PostURI<bool, string>("MarkRepairCompleted", roomNumber));
            t.Wait();
            return t.Result;
        }

        public IList<ComboItem> GetRepairingRooms()
        {
            var t = Task.Run(() => PostURI<IList<ComboItem>, string>("GetRepairingRooms", "empty"));
            t.Wait();
            return t.Result;
        }

        public bool RestAllRoomStatuses()
        {
            var t = Task.Run(() => PostURI<bool, string>("RestAllRoomStatuses", "empty"));
            t.Wait();
            return t.Result;
        }
        //RestAllRoomStatuses
    }
}
