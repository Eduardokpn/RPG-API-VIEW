using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RpgMvc.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using RpgMvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Data.Common;



namespace RpgMvc.Controllers
{
    public class PersonagensController : Controller
    {
        public string uriBase = "http://luizsouza.somee.com/RpgApi/Persongens/";

        [HttpGet]

        public async Task<ActionResult> IndexAsyc()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));
                    return View(listaPersonagens);

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

        [HttpPost]

        public async Task<ActionResult> CreateAsync(PersonagemViewModel p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application//json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();
            
                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Perspnagem {0}, Id {!} salvo com sucesso!", p.Nome, serialized);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);


            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");


            }
            
            
            }

            [HttpGet]

            public Action Create()
            {
                return View();
            }

            [HttpGet]
            public async Task<ActionResult> DetailsAsync(int? id)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    string token = HttpContext.Session.GetString("SessionTokenUsuarios");

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString);

                    string serialized = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        PersonagemViewModel p = await Task.Run(() =>
                        JsonConvert.DeserializeObject<PersonagemViewModel>(serialized));
                        return View(p);
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
            public async Task<ActionResult> EditAsync(int? id)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    string token = HttpContext.Session.GetString("SessionTokenUsuarios");

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString);
                    
                    string serialized = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        PersonagemViewModel p = await Task.Run(() =>
                        JsonConvert.DeserializeObject<PersonagemViewModel>(serialized));
                        return View(p);
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

            [HttpPost]
            public async Task<ActionResult> EditAsync(PersonagemViewModel p)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    string token = HttpContext.Session.GetString("SessionTokenUsuarios");

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var content = new StringContent(JsonConvert.SerializeObject(p));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    
                    HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString);
                    string serialized = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["Mensagem"] = 
                            string.Format("Personagem {0}, class {1} atualizado com sucesso!", p.Nome, p.Classe);
                        return RedirectToAction("Index");
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

            [HttpGet']
            public async Task<ActionResult> DeleteAsync(int id)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    string token = HttpContext.Session.GetString("SessionTokenUsuarios");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                                      
                    HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString);
                    string serialized = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["Mensagem"] = string.Format("Personagem {0}, class {1} atualizado com sucesso!", id);
                        return RedirectToAction("Index");
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






        }
    }