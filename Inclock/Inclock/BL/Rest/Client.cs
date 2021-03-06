﻿
using Inclock.VO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Inclock.BL.Rest
{
    /// <summary>
    /// 
    /// </summary>
    public class Client : IDisposable
    {
        public bool Disposed { get; private set; } = false;
        public static string UrlImagens { get { return "http://inclock-web.gearhostpreview.com/upload/Avisos/"; } }
       private readonly Uri URI = new Uri("http://inclock.gearhostpreview.com/Service.svc/rest/");
      // private readonly Uri URI = new Uri(" https://a30551da.ngrok.io/Service.svc/rest/");
        public Client()
        {

        }
        public async Task<FeedBack> CheckPoint(Funcionario user, char type, string code)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string fun = "\"funcionario\": " + user.Id;
                    string tp = "\"type\" :\"" + type + "\"";
                    string qr = "\"code\":\" " + code + " \"";
                    string jsonData = string.Concat("{",fun, ",", tp, "," + qr,"}");
                    StringContent argumento = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    argumento.Headers.Add("integracao", CriarIntegracao(user.Roles.ToArray()));
                    HttpResponseMessage response = await client.PostAsync(URI + "/CheckPoint", argumento);
                    string json = await response.Content.ReadAsStringAsync();
                    var bt = JsonConvert.DeserializeObject<MapedFeedBack>(json);
                    return bt.CheckPointResult;
                }
                catch (Exception ex)
                {
                    return new FeedBack() { Status = false, Mensagem = "Erro ao se conectar ao servidor" };
                }

            }
        }


        public List<Expediente> GetExpediente(string semana, string funcionario_Id)
        {
            throw new NotImplementedException();
        }

        public async Task<FeedBack> EnviarSenhaEmail(string Email)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = await client.GetStringAsync(URI + "sendaccount/" + Email);
                return JsonConvert.DeserializeObject<FeedBack>(json);
            }
        }
        public async void EncerrarSessao(int func)
        {
            using (HttpClient client = new HttpClient())
            {//int func, string dispositivo
                StringContent content = new StringContent("{\"func\":"+func+",\"dispositivo\":\"mob\"}", Encoding.UTF8, "application/json");
                var json = await client.PutAsync(URI + "ApagarSessao",content);
            }
        }
        public async Task<Funcionario> LogarAsync(string login, string senha)
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(URI + "logar/" + senha + "/" + login + "/mob");
            return JsonConvert.DeserializeObject<Funcionario>(json);
        }
        public async Task<List<VO.Aviso>> GetAvisos(int qtde, int index)
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(URI + "getavisos/" + qtde + "/" + index);
            return JsonConvert.DeserializeObject<VO.Aviso.BindingAvisos>(json).GetAvisosResult;
        }
        public string CriarIntegracao(params string[] dados)
        {
            return Rijndael.Criptografar(dados).ToBase64();
        }
        public void Dispose()
        {
            if (!Disposed)
            {
                GC.SuppressFinalize(this);
                Disposed = true;
            }
        }

    }
}
