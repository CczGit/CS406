using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MLOBuddy.Services
{
    class RestServices
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        private string RequestUrl = "https://api.primerhogarpr.com:8443";
        public async Task<PreQual> GetPreQual(string id) 
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions 
            { 
                PropertyNamingPolicy  = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            HttpResponseMessage res = await _client.GetAsync(RequestUrl+"/record/"+id);

        if (res.IsSuccessStatusCode)
            {
                var resCont = await res.Content.ReadAsStringAsync();
                PreQual result = JsonSerializer.Deserialize<PreQual>(resCont,_serializerOptions);
                return result;
            }
        else
            {
                throw new Exception("Unable to Authenticate. Sorting Functionality Offline");
            }
        }
        public async Task<PreQual[]> GetOriginator(string originator)
        {
            _client = new();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            HttpResponseMessage res = await _client.GetAsync(RequestUrl + "/get/originator/records/" + originator);

            if (res.IsSuccessStatusCode)
            {
                var resCont = await res.Content.ReadAsStringAsync();
                PreQual[] result = JsonSerializer.Deserialize<PreQual[]>(resCont, _serializerOptions);
                return result;
            }
            else
            {
                throw new Exception("Unable to Authenticate. Sorting Functionality Offline");
            }
        }

        public async void PostCase(PreQual Case, bool NewCase) 
        {
            _client = new();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            string JSON = JsonSerializer.Serialize(Case, _serializerOptions);
            StringContent content = new (JSON, Encoding.UTF8, "application/json");
            HttpResponseMessage res;
            if (NewCase) { res = await _client.PostAsync(RequestUrl + "/add", content); }
            else { res = await _client.PostAsync(RequestUrl + "/record/update/update", content); }
            if (!res.IsSuccessStatusCode) { Debug.WriteLine("Post failed"); } // write to the debugger if the post fails
        }



    } // class end
}
