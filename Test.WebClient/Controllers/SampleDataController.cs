using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Test.WebClient.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        static HttpClient client = new HttpClient();

        [HttpGet("[action]")]
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            IEnumerable<Usuario> usuarios = null;
            HttpResponseMessage response = await client.GetAsync("https://localhost:44316/api/usuarios");
            if (response.IsSuccessStatusCode)
            {
                var usuariosResponse =  response.Content.ReadAsStringAsync().Result;
                usuarios = JsonConvert.DeserializeObject<List<Usuario>>(usuariosResponse);
            }
            return usuarios;
        }
        [HttpPost("[action]")]
        public async Task<string> PostUsuario([FromBody]Usuario usuario)
        {
            var json = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:44316/api/usuarios",data);
            if (response.IsSuccessStatusCode)
            {
                var usuariosResponse = response.Content.ReadAsStringAsync().Result;
                return usuariosResponse;
            }
            return "Error";
        }
        public class Usuario
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string email { get; set; }
        }
    }
}
