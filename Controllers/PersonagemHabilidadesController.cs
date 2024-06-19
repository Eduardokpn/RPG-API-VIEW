using Microsoft.AspNetCore.Mvc;
using RPG_API_VIEW.Models;

namespace RPG_API_VIEW.Controllers
{
    public class PersonagemHabilidadesController : Controller
    {
        public string uriBase = "http://luizsouza.somee.com/RpgApi/PersonagemHabilidades/";

        [HttpGet("PersonagemHabilidades/{id}")]

        public async Task<ActionResult> IndexAsync(int id) {
            try {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK) {
                    List<PersonagemHabilidadesViewModel> lista = await Task.Run(() =>
                        //TODO: CONTINUAR DO SLIDE AULA 17, PAGINA 5, LINHA JsonConvert
                }
            }
        }
    }
}