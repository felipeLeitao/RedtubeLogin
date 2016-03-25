using NetCoders.Redtube.WebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NetCoders.Redtube.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> Login(string usuario, string senha)
        {
            //link pro redtube
            String url = "http://www.redtube.com";

            //criando o client pra realizar a requisição http
            var client = new HttpClient() 
            {
                //Aqui eu seto o endereço base(o que vier depois é complemento)
                BaseAddress =  new Uri(url)
            };


            await Logout(client);

            var request = new HttpRequestMessage(HttpMethod.Post, "/htmllogin");

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
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Oops, Usuário e/ou senha incorretos.");
            }

            else
            {
                var objeto = content.Split('(',')');
                var json = JObject.Parse(objeto[1]);

                var retorno = new User() 
                { 
                    Codigo = Convert.ToInt32(json["iUserID"]),
                    isAtivo = Convert.ToBoolean(json["isConfirmed"]),
                    UrlAvatar = json["sAvatar"].ToString(),
                    Usuario = json["sUsername"].ToString()
                };

                return Request.CreateResponse(HttpStatusCode.OK, retorno);
            }

        }

        private async Task Logout(HttpClient client_)
        {
            //Aqui eu escolho o verbo que vou utilizar e a página, no caso, vou fazer logout, pra quando eu tentar fazer o login
            //não correr o risco de já estar logado e ele dar um falso positivo
            var request = new HttpRequestMessage(HttpMethod.Post, "/logout");

            await client_.SendAsync(request);
        }
    }
}
