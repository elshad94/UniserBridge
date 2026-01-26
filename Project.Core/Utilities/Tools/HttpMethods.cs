using Project.Core.Entities.Dtos.CommonDtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Project.Core.Utilities.Tools
{
    public class HttpMethods
    {
        public async Task<List<TEntity>> GetAsync<TEntity>(string url, List<RequestHeader> headers = null, bool basicAuthorization = true, BasicAuthDto basicAuth = null)
        {
            List<TEntity> entities = new List<TEntity>();
            string apiResponse = string.Empty;
            HttpClient httpClient = new();

            GetClient(ref httpClient, headers: headers, basicAuthorization, basicAuth: basicAuth);

            using (var response = await httpClient.GetAsync(url))
            {
                apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TEntity>>(apiResponse);
            }

            return entities;
        }


        public async Task<string> GetAsync(string url, List<RequestHeader> headers = null, bool basicAuthorization = true, BasicAuthDto basicAuth = null)
        {
            string apiResponse = string.Empty;
            HttpClient httpClient = new();

            GetClient(ref httpClient, headers: headers, basicAuthorization, basicAuth: basicAuth);

            using (var response = await httpClient.GetAsync(url))
            {
                apiResponse = await response.Content.ReadAsStringAsync();
            }

            return apiResponse;
        }


        //public async Task<TEntity> GetByIdAsync<TEntity>(string url, List<RequestHeader> headers = null)
        //{
        //    TEntity entity;
        //    using (var httpClient = new HttpClient())
        //    {
        //        if (headers != null)
        //        {
        //            foreach (var header in headers)
        //            {
        //                httpClient.DefaultRequestHeaders.Add(header.Name, header.Value);
        //            }
        //        }

        //        using (var response = await httpClient.GetAsync(url))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);
        //        }
        //    }
        //    return entity;
        //}

        public async Task<string> PostAsync(string url, object content, List<RequestHeader> headers = null, bool basicAuthorization = true, BasicAuthDto basicAuth = null)
        {
            string apiResponse = string.Empty;

            HttpClient httpClient = new();

            GetClient(ref httpClient, headers: headers, basicAuthorization, basicAuth: basicAuth);

            var requestContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(url, requestContent))
            {
                apiResponse = await response.Content.ReadAsStringAsync();
            }

            return apiResponse;
        }

        public async Task<string> PutAsync(string url, object content, List<RequestHeader> headers = null, bool basicAuthorization = true, BasicAuthDto basicAuth = null)
        {
            string apiResponse = string.Empty;

            HttpClient httpClient = new();

            GetClient(ref httpClient, headers: headers, basicAuthorization, basicAuth: basicAuth);

            var requestContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync(url, requestContent))
            {
                apiResponse = await response.Content.ReadAsStringAsync();
            }

            return apiResponse;
        }

        private void GetClient(ref HttpClient httpClient, List<RequestHeader> headers = null, bool basicAuthorization = true, BasicAuthDto basicAuth = null)
        {

            if (basicAuthorization)
            {
                var byteArray = Encoding.ASCII.GetBytes($"{basicAuth.Username}:{basicAuth.Password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Name, header.Value);
                }
            }
        }
    }
}
