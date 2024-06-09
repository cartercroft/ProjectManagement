using Newtonsoft.Json;
using ProjectManagement.Classes;
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

        public virtual async Task<Response<TResponse>> PostAsync<TResponse>(string endpoint, object bodyContent, Dictionary<string, string>? headers = null)
        {
            HttpContent content = GetHttpContent(bodyContent, headers);
            var result = await _client.PostAsync(endpoint, content);
            Response<TResponse> response = new Response<TResponse>();
            if (!result.IsSuccessStatusCode)
            {
                response.ErrorMessage = GetRequestErrorMessage(result);
            }
            else
            {
                response.Result = await GetResponseObject<TResponse>(result);
            }
            return response;
        }
        public virtual async Task<Response> PostAsyncNoResponseObject(string endpoint, object bodyContent, Dictionary<string, string>? headers = null)
        {
            HttpContent content = GetHttpContent(bodyContent, headers);
            var result = await _client.PostAsync(endpoint, content);

            Response response = new Response();
            if (!result.IsSuccessStatusCode)
            {
                response.ErrorMessage = GetRequestErrorMessage(result);
            }
            return response;
        }
        public virtual async Task<Response<TResponse>> GetAsync<TResponse>(string endpoint, Dictionary<string, object>? pathParameters = null, Dictionary<string, string>? headers = null)
        {
            string requestUri = $"{_client.BaseAddress}{endpoint}{GetPathParametersAsString(pathParameters)}";
            HttpRequestMessage getRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);

            getRequest = AddHttpRequestMessageHeaders(getRequest, headers);
            var result = await _client.SendAsync(getRequest);

            Response<TResponse> response = new Response<TResponse>();
            if (!result.IsSuccessStatusCode)
            {
                response.ErrorMessage = GetRequestErrorMessage(result);
            }
            else
            {
                response.Result = await GetResponseObject<TResponse>(result);
            }
            return response;
        }
        private async Task<TResponse?> GetResponseObject<TResponse>(HttpResponseMessage responseMessage)
        {
            Stream receiveStream = await responseMessage.Content.ReadAsStreamAsync();
            using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string responseBody = await readStream.ReadToEndAsync();
            readStream.BaseStream.Seek(0, SeekOrigin.Begin);
            TResponse? response = JsonConvert.DeserializeObject<TResponse>(responseBody);
            return response;
        }
        private string GetPathParametersAsString(Dictionary<string, object>? pathParameters)
        {
            if(pathParameters == null)
            {
                return "";
            }

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
        private static HttpContent GetHttpContentBody(object bodyContent)
        {
            string body = JsonConvert.SerializeObject(bodyContent);
            HttpContent content = new StringContent(body);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            return content;
        }
        private static HttpContent GetHttpContent(object bodyContent, Dictionary<string, string>? headers)
        {
            var content = GetHttpContentBody(bodyContent);
            if(headers != null)
            {
                foreach(var entry in headers)
                {
                    content.Headers.Add(entry.Key, entry.Value);
                }
            }
            return content;
        }
        private string GetRequestErrorMessage(HttpResponseMessage httpResponseMessage)
        {
            return $"Error calling {httpResponseMessage.RequestMessage?.RequestUri}\n\nError: {httpResponseMessage.ReasonPhrase}";
        }
        private HttpRequestMessage AddHttpRequestMessageHeaders(HttpRequestMessage httpRequestMessage, Dictionary<string, string>? headers = null) 
        {
            if(headers != null)
            {
                foreach(var entry in headers)
                {
                    httpRequestMessage.Headers.Add(entry.Key, entry.Value);
                }
            }
            return httpRequestMessage;
        }
    }
}
