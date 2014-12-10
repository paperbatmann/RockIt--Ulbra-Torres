using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedeMusica.Models;
using System.IO;

namespace RedeMusica.Controllers
{
    public class BandasController : Controller
    {
        Bandas atual = new Bandas();
        // GET: Bandas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Criar()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Criar(Bandas novo, HttpPostedFileBase fileImagem)
        {
            String caminhoF = null;

            if (!ModelState.IsValid)
            {
                return View("Criar");
            }
            else
            {
                if (fileImagem != null && fileImagem.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fileImagem.FileName);
                    if (String.IsNullOrEmpty(ext) || !ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || fileImagem == null)
                    {
                        ViewBag.Musica = "Somente arquivos .jpg";
                        return View("MusicaUsuario");
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileImagem.FileName);
                        string novas = fileName.Replace(" ", "");
                        var path = Path.Combine(Server.MapPath("~/ImagemBanda"), novas);
                        fileImagem.SaveAs(path);
                        caminhoF = "~/ImagemBanda/" + novas.ToString();
                        novo.foto = caminhoF;
                        novo.idUsuario = Convert.ToInt32(Session["idUsuario"]);
                        atual.Criar(novo);
                        return RedirectToAction("PerfilIni", "Usuarios");
                    }
                }

                else
                {
                    caminhoF = "~/Filtros/banda.png";
                    novo.foto = caminhoF;
                    novo.idUsuario = Convert.ToInt32(Session["idUsuario"]);
                    atual.Criar(novo);
                    return RedirectToAction("PerfilIni", "Usuarios");
                }

            }
        }
        [HttpPost]
        public ActionResult BuscarUsers(string buscando)
        {
            Usuarios user = new Usuarios();
            Bandas atuali = atual.PegaBanda(Session["idBanda"].ToString());
            Session["buscando"] = buscando;
            return View(atuali);
        }



        public ActionResult AdicionaIntegrante(int id)
        {
            atual.AdicionarIntegrante(id, Convert.ToInt32(Session["idBanda"]));
            return RedirectToAction("PerfilIni", "Usuarios");
        }

        public ActionResult removeUser(int idR)
        {
            atual.DeletarIntegrante(idR, Convert.ToInt32(Session["idBanda"]));
            return RedirectToAction("PerfilIni", "Usuarios");
        }

        public ActionResult Perfil(int id)
        {
            bool lider = false;
            string ids;
            ids = Convert.ToString(id);
            atual = atual.PegaBanda(ids);
            lider = atual.verificaUser(Convert.ToInt32(Session["idUsuario"]), id);
            if (lider == true)
            {
                Session["idBanda"] = id;
                Session["nomeBanda"] = atual.nome;
                return View("PerfilLiders", atual);
            }
            else
            {
                ViewBag.Seguindo = false;
                Session["idBanda"] = id;
                ViewBag.Seguindo = RedeMusica.Models.Usuarios.VerificaBandaSeguir(Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(Session["idBanda"]));
                return View(atual);
            }

        }

        public ActionResult unfollowBanda(int idBandaUnf)
        {
            Usuarios user = new Usuarios();
            user.UnfollowBanda(Convert.ToInt32(Session["idUsuario"]), idBandaUnf);
            return RedirectToAction("PerfilIni", "Usuarios");
        }

        public ActionResult followBanda(int idBandaf)
        {
            Usuarios user = new Usuarios();
            user.SeguirBanda(Convert.ToInt32(Session["idUsuario"]), idBandaf);
            return RedirectToAction("PerfilIni", "Usuarios");
        }
        public ActionResult BuscaUserBanda(string busca)
        {
            Bandas banda = new Bandas();
            Bandas atual = banda.PegaBanda(Session["idBanda"].ToString());
            Session["buscando"] = busca;
            return View(atual);
        }

        public ActionResult ConfigBanda()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Bandas deAgora = atual.PegaBanda(Session["idBanda"].ToString());
            return View(deAgora);
        }

        public ActionResult DeleteBanda()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            atual.DeletarBanda(Convert.ToInt32(Session["idBanda"]));
  
            return RedirectToAction("PerfilIni", "Usuarios");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ConfigBanda(Bandas atual, HttpPostedFileBase fileImagem)
        {
            String caminhoF = null;
            Bandas qse = new Bandas();
            if (atual.nome == null || atual.cidadeNatal == null)
            {
                return View("ConfigBanda");
            }
            else
            {
                if (fileImagem != null && fileImagem.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fileImagem.FileName);
                    if (!ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewBag.Imagem = "Somente imagens .jpg";
                        return View("ConfigBanda");
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileImagem.FileName);
                        string novas = fileName.Replace(" ", "");
                        var path = Path.Combine(Server.MapPath("~/ImagemBanda"), novas);
                        fileImagem.SaveAs(path);
                        caminhoF = "~/ImagemBanda/" + novas.ToString();
                        atual.foto = caminhoF;
                        qse.AtualizaFoto(atual);
                        qse.editar(atual);
                        return RedirectToAction("PerfilIni", "Usuarios");
                    }
                }

                else
                {
                    qse.editar(atual);
                    return RedirectToAction("PerfilIni", "Usuarios");
                }
            }

        }
    }
}