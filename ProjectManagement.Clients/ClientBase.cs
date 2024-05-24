using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace ProjectManagement.Clients
{
    public class ClientBase
    {
        private readonly HttpClient _client;
        public ClientBase(HttpClient client)
        {
            _client = client;
        }

        public async Task<TResponse> PostAsync<TResponse>(string endpoint, object bodyContent)
        {
            string body = JsonConvert.SerializeObject(bodyContent);
            HttpContent content = new StringContent(body);
            var result = await _client.PostAsync(endpoint, content);
            if (!result.IsSuccessStatusCode)
            {
                HandleFailedRequest(result);
            }
            return await GetResponseObject<TResponse>(result);
        }

        public async Task PostAsync(string endpoint, object bodyContent)
        {
            string body = JsonConvert.SerializeObject(bodyContent);
            HttpContent content = new StringContent(body);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            var result = await _client.PostAsync(endpoint, content);
            if (!result.IsSuccessStatusCode)
            {
                HandleFailedRequest(result);
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(string endpoint, Dictionary<string, object> pathParameters)
        {
            var result = await _client.GetAsync($"{endpoint}{GetPathParametersAsString(pathParameters)}");
            if (!result.IsSuccessStatusCode)
            {
                HandleFailedRequest(result);
            }
            return await GetResponseObject<TResponse>(result);
        }
        private async Task<TResponse> GetResponseObject<TResponse>(HttpResponseMessage? responseMessage)
        {
            Stream receiveStream = await responseMessage.Content.ReadAsStreamAsync();
            using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string responseBody = await readStream.ReadToEndAsync();
            readStream.BaseStream.Seek(0, SeekOrigin.Begin);
            TResponse response = JsonConvert.DeserializeObject<TResponse>(responseBody);
            return response;
        }
        private string GetPathParametersAsString(Dictionary<string, object> pathParameters)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < pathParameters.Count; i++) 
            {
                var entry = pathParameters.ElementAt(i);
                builder.Append(entry.Key);
                builder.Append("=");
                builder.Append(entry.Value);
                if(i < pathParameters.Count - 1)
                {
                    builder.Append("&");
                }
            }
            return builder.ToString();
        }
        private void HandleFailedRequest(HttpResponseMessage httpResponseMessage)
        {
            throw new HttpRequestException($"Error calling {httpResponseMessage.RequestMessage.RequestUri}");
        }
    }
}
