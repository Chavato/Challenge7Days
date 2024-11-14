using RestSharp;

namespace Challenge7Days.Services
{
    public class HttpService
    {
        public readonly string ApiUrl;
        private readonly RestClient Client;

        public HttpService(string apiUrl)
        {
            ApiUrl = apiUrl;
            Client = new RestClient(ApiUrl);
        }

        public string Get(string route = "")
        {
            RestRequest request = new RestRequest(route, Method.Get);
            RestResponse response = Client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Error, status code is not as expected.");

            if (response.Content == null)
                throw new Exception("response content is missing.");

            return response.Content;
        }
    }
}