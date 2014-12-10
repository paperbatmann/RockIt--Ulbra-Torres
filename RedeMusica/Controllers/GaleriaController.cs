using System;
using ImageResizer;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedeMusica.Models;
using System.IO;

namespace RedeMusica.Controllers
{
    public class GaleriaController : Controller
    {
        // GET: Galeria
        Galeria foto = new Galeria();
        public ActionResult AdicinarFoto()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult AdicinarFotoBanda()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AdicinarFotoBanda(Galeria novo, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (novo.descricao == null)
                    novo.descricao = "";
                if (file != null && file.ContentLength > 0)
                {

                    string ext = Path.GetExtension(file.FileName);
                    if (String.IsNullOrEmpty(ext) || !ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || file == null)
                    {
                        ViewBag.ImagemF = "Somente imagens .jpg";
                        return View("AdicinarFotoBanda");
                    }
                    var fileName = Path.GetFileName(file.FileName);
                    string nova = fileName.Replace(" ", "");
                    var path = Path.Combine(Server.MapPath("~/ImagemGaleria"), nova);
                    file.SaveAs(path);
                    String caminho = "~/ImagemGaleria/" + nova.ToString();
                    novo.arquivoFoto = caminho;
                    novo.usuarioId = Convert.ToString(Session["idBanda"]);
                    var thumbnail = new ImageJob(file, "~/Thumbnails/<guid>", new Instructions("mode=max;format=jpg;width=300;height=300;"));
                    thumbnail.CreateParentDirectory = true;
                    thumbnail.AddFileExtension = true;
                    thumbnail.Build();
                    fileName = Path.GetFileName(thumbnail.FinalPath);
                    novo.thumbnail = Url.Content("~/Thumbnails/" + fileName);
                    foto.AdicionarFotoBanda(novo);
                }
                else
                {
                    ViewBag.ImagemF = "Somente imagens .jpg";
                    return View("AdicinarFotoBanda");
                }
                int numero = Convert.ToInt32(Session["idBanda"]);
                return RedirectToAction("Perfil", "Bandas", new { id = numero });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AdicinarFoto(Galeria novo, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (novo.descricao == null)
                    novo.descricao = "Sem legenda";
                if (file != null && file.ContentLength > 0)
                {

                    string ext = Path.GetExtension(file.FileName);
                    if (String.IsNullOrEmpty(ext) || !ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || file== null)
                    {
                        ViewBag.ImagemF = "Somente imagens .jpg";
                        return View("AdicinarFoto");
                    }
                    var fileName = Path.GetFileName(file.FileName);
                    string nova = fileName.Replace(" ", "");
                    var path = Path.Combine(Server.MapPath("~/ImagemGaleria"), nova);
                    file.SaveAs(path);
                    String caminho = "~/ImagemGaleria/" + nova.ToString();
                    novo.arquivoFoto = caminho;
                    novo.usuarioId = Convert.ToString(Session["idUsuario"]) ;
                    var thumbnail = new ImageJob(file, "~/Thumbnails/<guid>", new Instructions("mode=max;format=jpg;width=300;height=300;"));
                    thumbnail.CreateParentDirectory = true;
                    thumbnail.AddFileExtension = true;
                    thumbnail.Build();
                    fileName = Path.GetFileName(thumbnail.FinalPath);
                    novo.thumbnail = Url.Content("~/Thumbnails/" + fileName);
                    foto.Adicionar(novo);
                }
                else
                {
                    ViewBag.ImagemF = "Somente imagens .jpg";
                    return View("AdicinarFoto");
                }
                return RedirectToAction("PerfilIni", "Usuarios");
            }

        }
        public ActionResult DeleteFoto()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<RedeMusica.Models.Galeria> listaFotos = new List<RedeMusica.Models.Galeria>();
            listaFotos = foto.getUserFotos(Convert.ToInt32(Session["idUsuario"]));
            return View(listaFotos);

        }
        public ActionResult DeleteFotoBanda()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<RedeMusica.Models.Galeria> listaFotos = new List<RedeMusica.Models.Galeria>();
            listaFotos = foto.getBandaFotos(Convert.ToInt32(Session["idBanda"]));
            return View(listaFotos);

        }
        public ActionResult DeleteFotosB(int id)
        {
            foto.Delete(id);
            return RedirectToAction("DeleteFotoBanda");
        }


        public ActionResult DeleteFotos(int id)
        {
            foto.Delete(id);
            return RedirectToAction("DeleteFoto");
        }


    }
}