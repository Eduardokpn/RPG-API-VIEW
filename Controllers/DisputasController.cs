using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RpgMv.Models;
using RpgMvc.Models;


namespace RpgMvc.Controllers
{
    public class DisputasController : Controller
    {
        public string uriBase = "http://luizsouza.somee.com/RpgApi/Disputas/";
        //Substituir pelo no do site da api

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try 
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string uriBuscaPersonagens = "http://luizsouza.somee.com/RpgApi/Personagens/GetAll";
                HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);
                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK) {
                    List<PersonagemViewModel> listaPersonagem = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                    ViewBag.ListaAtacantes = listaPersonagem;
                    ViewBag.ListaOponentes = listaPersonagem;
                    return View();

                } else
                    throw new System.Exception(serialized);

            }
            catch (System.Exception ex)
            {
                TempData["MenssagemErro"] =  ex.Message;
                return RedirectToAction("Index");
            }
        }
        
    

    [HttpPost]
    public async Task<ActionResult> IndexAsync(DisputaViewModel disputa)
    {
        try {
            HttpClient httpClient = new HttpClient();
            string uriComplementar = "Arma";

            var content = new StringContent(JsonConvert.SerializeObject(disputa));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);
            string serialized = await response.Content.ReadAsStringAsync();

            if( response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                disputa = await Task.Run(() => JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                TempData["Mensagem"] = disputa.Narracao;
                return RedirectToAction("Index", "Personagens");
            }
            else 
            {
                throw new System.Exception(serialized);
            }
        }
        catch (System.Exception ex)
        {
            TempData["MenssageErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpGet] 
    public async Task<ActionResult> IndexHabilidadeAsync() 
    {
        try
        {
             HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string uriBuscaPersonagens = "http://luizsouza.somee.com/RpgApi/Personagens/GetAll";
                HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);
                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK) {
                    List<PersonagemViewModel> listaPersonagem = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                    ViewBag.ListaAtacantes = listaPersonagem;
                    ViewBag.ListaOponentes = listaPersonagem;

                } else
                    throw new System.Exception(serialized);

                string uriBuscaHabilidades = "http://luizsouza.somee.com/RpgApi/PersonagemHabilidades/GetHabilidades";
                response = await httpClient.GetAsync(uriBuscaHabilidades);
                serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK) 
                {
                    List<HabilidadeViewModel> listaHabilidades = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<HabilidadeViewModel>>(serialized));
                    ViewBag.ListaHabilidades = listaHabilidades;
                }
                else
                {
                    throw new System.Exception(serialized);
                }
                return View("IndexHabilidades");
        }
        catch (System.Exception ex) 
        {
            TempData["MenssageErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}
}
