using RedeMusica.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedeMusica.Controllers
{
    public class MusicasController : Controller
    {
        // GET: Musicas
        Musicas atual = new Musicas();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteMusica(int idM)
        {
            atual.Delete(idM);
            return RedirectToAction("PerfilIni","Usuarios");
        }  
        
        public ActionResult MusicaUsuario()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MusicaUsuario(Musicas novo, HttpPostedFileBase fileImagem, HttpPostedFileBase fileMusica)
        {
            String caminhoF = null;
            String caminhoM = null;
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (fileImagem != null && fileImagem.ContentLength > 0)
                {
                     string ext = Path.GetExtension(fileImagem.FileName);
                     if (String.IsNullOrEmpty(ext) || !ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || fileImagem == null)
                     {
                         ViewBag.Imagem = "Somente imagens .jpg";
                         return View("MusicaUsuario");
                     }
                     else
                     {
                         var fileName = Path.GetFileName(fileImagem.FileName);
                         string novas = fileName.Replace(" ", "");
                         var path = Path.Combine(Server.MapPath("~/ImagemMusica"), novas);
                         fileImagem.SaveAs(path);
                         caminhoF = "~/ImagemMusica/" + novas.ToString();
                         novo.imageMusica = caminhoF;
                     }
                }

                else
                {
                    caminhoF = "~/Filtros/musica.png"; ;
                    novo.imageMusica = caminhoF;
                }
                if (fileMusica!= null && fileMusica.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fileMusica.FileName);
                    if (String.IsNullOrEmpty(ext) || !ext.Equals(".mp3", StringComparison.OrdinalIgnoreCase) || fileMusica==null)
                    {
                        ViewBag.Musica = "Somente arquivos mp3";
                        return View("MusicaUsuario");
                    }
                    else
                    {
                        
                        var fileName = Path.GetFileName(fileMusica.FileName);
                        string nova = fileName.Replace(" ", "");
                      
                        var path = Path.Combine(Server.MapPath("~/Musicas"), nova);
                        fileMusica.SaveAs(path);
                        caminhoM = "~/Musicas/" + nova.ToString();
                        novo.arquivoMusica = caminhoM;
                        novo.usuarioId = Convert.ToInt32(Session["idUsuario"]) ;
                        novo.usuarioNome = Session["NomeCompleto"].ToString();
                        atual.MusicaUsuario(novo);
                        return RedirectToAction("PerfilIni", "Usuarios");
                    }
                }
                ViewBag.Musica = "Somente arquivos mp3";
                return View("MusicaUsuario");
            }
        }
        public ActionResult MusicaBanda()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MusicaBanda(Musicas novo, HttpPostedFileBase fileImagem, HttpPostedFileBase fileMusica)
        {
            String caminhoF = null;
            String caminhoM = null;
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (fileImagem != null && fileImagem.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fileImagem.FileName);
                    if (String.IsNullOrEmpty(ext) || !ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || fileImagem == null)
                    {
                        ViewBag.Imagem = "Somente imagens .jpg";
                        return View("MusicaBanda");
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileImagem.FileName);
                        string novas = fileName.Replace(" ", "");
                        var path = Path.Combine(Server.MapPath("~/ImagemMusica"), novas);
                        fileImagem.SaveAs(path);
                        caminhoF = "~/ImagemMusica/" + novas.ToString();
                        novo.imageMusica = caminhoF;
                    }
                }

                else
                {
                    caminhoF = "~/Filtros/musica.png";
                    novo.imageMusica = caminhoF;
                }
                if (fileMusica != null && fileMusica.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fileMusica.FileName);
                    if (String.IsNullOrEmpty(ext) || !ext.Equals(".mp3", StringComparison.OrdinalIgnoreCase) || fileMusica == null)
                    {
                        ViewBag.Musica = "Somente músicas .mp3";
                        return View("MusicaBanda");
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileMusica.FileName);
                        string nova = fileName.Replace(" ", "");
                        var path = Path.Combine(Server.MapPath("~/Musicas"), nova);
                        fileMusica.SaveAs(path);
                        caminhoM = "~/Musicas/" + nova.ToString();
                        novo.arquivoMusica = caminhoM;
                        novo.usuarioId = Convert.ToInt32(Session["idBanda"]); ;
                        novo.usuarioNome = Session["nomeBanda"].ToString();
                        atual.MusicaBanda(novo);
                        return RedirectToAction("Perfil", "Bandas", new { id = novo.usuarioId });
                    }
                }
                ViewBag.Musica = "Somente arquivos mp3";
                return View("MusicaBanda");
            }
        }

       
       

        public ActionResult Proprias()
        {
            string id= Session["idUsuario"].ToString();
            List<Musicas> lista = new List<Musicas>();
            lista= atual.Proprias(id);
            return View(lista);
        }

        

        public ActionResult Todas()
        {
            string id = Session["idUsuario"].ToString();
            List<Musicas> lista = new List<Musicas>();
            lista = atual.Todas(id);
            return View(lista);
        }

        public ActionResult MusicasUsers()
        {
            string id = Session["idUsuarioAtual"].ToString();
            List<Musicas> lista = new List<Musicas>();
            lista = atual.Proprias(id);
            return View(lista);

        }
       
       

       

    }
}