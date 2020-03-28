using RestSharp;

namespace CryptoTraderService.Services
{
public class Request
{
  public string SendRequest(string host, string token, string json, Method method)
  {
      var client = new RestClient(host);

      var request = new RestRequest(method);
      request.AddHeader("x-api-key", token);
      request.AddHeader("Accept", "application/json");
      request.AddHeader("Cache-Control", "no-cache");
      request.AddHeader("Content-Type", "application/json");
      request.AddParameter("application/json", json, ParameterType.RequestBody);

      IRestResponse response = client.Execute(request);

      return response.Content;
  }


  public string SendRequestMarket(string host, string token, string json, Method method)
  {

      var client = new RestClient(host);
      var request = new RestRequest(method);

      request.AddHeader("Authorization", $"ApiToken {token}");
      request.AddHeader("Accept", "application/json");
      request.AddHeader("Cache-Control", "no-cache");
      request.AddHeader("Content-Type", "application/json");
      request.AddParameter("application/json", json, ParameterType.RequestBody);

      IRestResponse response = client.Execute(request);

      return response.Content;
  }

}
}