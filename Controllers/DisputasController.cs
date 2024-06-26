using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
<<<<<<< HEAD
using RPG_API_VIEW.Models;
=======
using RpgMv.Models;
>>>>>>> ab6cf85b7cbfd30d5c50ee4890a5e7787c613b04
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

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PersonagemViewModel> listaPersonagem = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                    ViewBag.ListaAtacantes = listaPersonagem;
                    ViewBag.ListaOponentes = listaPersonagem;
                    return View();

                }
                else
                    throw new System.Exception(serialized);

            }
            catch (System.Exception ex)
            {
                TempData["MenssagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
<<<<<<< HEAD
=======
        
    
>>>>>>> ab6cf85b7cbfd30d5c50ee4890a5e7787c613b04


<<<<<<< HEAD
        [HttpPost]
        public async Task<ActionResult> IndexAsync(DisputaViewModel disputa)
        {
            try
=======
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
>>>>>>> ab6cf85b7cbfd30d5c50ee4890a5e7787c613b04
            {
                HttpClient httpClient = new HttpClient();
                string uriComplementar = "Arma";

                var content = new StringContent(JsonConvert.SerializeObject(disputa));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    disputa = await Task.Run(() => JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                    TempData["Message"] = disputa.Narracao;
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

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PersonagemViewModel> listaPersonagem = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                    ViewBag.ListaAtacantes = listaPersonagem;
                    ViewBag.ListaOponentes = listaPersonagem;

                }
                else
                    throw new System.Exception(serialized);

                string uriBuscaHabilidades = "http://luizsouza.somee.com/RpgApi/PersonagemHabilidades/GetHabilidades";
                response = await httpClient.GetAsync(uriBuscaHabilidades);
                serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<HabilidadeViewModel> listaHabilidades = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<HabilidadeViewModel>>(serialized));
                    ViewBag.ListaHabilidades = listaPersonagem;
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


        [HttpGet]
        public async Task<ActionResult> IndexDisputasAsync()
        {
            try
            {
                string uriComplementar = "Listar";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase +
                uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<DisputaViewModel> lista = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<DisputaViewModel>>(serialized));
                    return View(lista);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<ActionResult> ApagarDisputasAsync()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", token);
                string uriComplementar = "ApagarDisputas";
                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase +
                uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    TempData["Mensagem"] = "Disputas apagadas com sucessso.";
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }
            return RedirectToAction("IndexDisputas", "Disputas");
        }

    }


}
}
