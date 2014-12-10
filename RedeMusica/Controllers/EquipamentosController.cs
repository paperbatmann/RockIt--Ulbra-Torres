using System;
using ImageResizer;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedeMusica.Models;

namespace RedeMusica.Controllers
{
    public class EquipamentosController : Controller
    {
        // GET: Equipamentos

        Equipamentos equip = new Equipamentos();
        public ActionResult AdicinarEquip()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AdicinarEquip(Equipamentos novo, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View("AdicionarEquip");
            }
            else
            {
                if (novo.descricao == null)
                    novo.descricao = "Sem legenda";
                if (file != null && file.ContentLength > 0)
                {

                    string ext = Path.GetExtension(file.FileName);
                    if (String.IsNullOrEmpty(ext) || !ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || file == null)
                    {
                        ViewBag.ImagemE = "Somente imagens .jpg";
                        return View("AdicinarEquip");
                    }
                    var fileName = Path.GetFileName(file.FileName);
                    string nova = fileName.Replace(" ", "");
                    var path = Path.Combine(Server.MapPath("~/ImagemEquipamento"), nova);
                    file.SaveAs(path);
                    String caminho = "~/ImagemEquipamento/" + nova.ToString();
                    novo.arquivoFoto = caminho;
                    novo.usuarioId = Convert.ToString(Session["idUsuario"]);
                    var thumbnail = new ImageJob(file, "~/Thumbnails/<guid>", new Instructions("mode=max;format=jpg;width=300;height=300;"));
                    thumbnail.CreateParentDirectory = true;
                    thumbnail.AddFileExtension = true;
                    thumbnail.Build();
                    fileName = Path.GetFileName(thumbnail.FinalPath);
                    novo.thumbnail = Url.Content("~/Thumbnails/" + fileName);
                    equip.Adicionar(novo);
                }
                else
                {
                    ViewBag.ImagemE = "Somente imagens .jpg";
                    return View("AdicinarEquip");
                }

                return RedirectToAction("PerfilIni", "Usuarios");
            }
            }
        public ActionResult DeleteEquip()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
             List<RedeMusica.Models.Equipamentos> listaEquips = new List<RedeMusica.Models.Equipamentos>();
             listaEquips = equip.getUserEquips(Convert.ToInt32(Session["idUsuario"]));
            return View(listaEquips);
            
          }
  
        
        public ActionResult DeleteEquips(int id)
        {
            equip.Delete(id);
            return RedirectToAction("DeleteEquip");
        }

    }
}