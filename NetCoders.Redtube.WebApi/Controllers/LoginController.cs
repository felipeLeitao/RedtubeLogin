using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NetCoders.Redtube.WebApi.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> Login(string usuario, string senha)
        {
            String url = "http://www.redtube.com";

            var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            var request = new HttpRequestMessage(HttpMethod.Post, "/logout");

            var resposta = await client.SendAsync(request);

            var teste = await resposta.Content.ReadAsStringAsync();

            request = new HttpRequestMessage(HttpMethod.Post, "/htmllogin");

            var keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("sPassword", senha));
            keyValues.Add(new KeyValuePair<string, string>("sUsername", usuario));
            keyValues.Add(new KeyValuePair<string, string>("do", "Login"));

            request.Content = new FormUrlEncodedContent(keyValues);

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var contains = content.Contains("Username or password incorrect. Please try again.");


            if (contains)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Falha ao realizar login.");
            }

            else
            {
                var objeto = content.Split('(',')');
                var json = JObject.Parse(objeto[1]);

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }

        }
    }
}
