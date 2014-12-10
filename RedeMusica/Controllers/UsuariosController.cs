using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedeMusica.Models;
using System.IO;

namespace RedeMusica.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        Usuarios user = new Usuarios();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Cadastro(Usuarios novo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                string email_=user.GetEmail(novo.email);
                if (email_ == "existe")
                {
                    ViewBag.Email = "Este email ja esta cadastrado.";
                    return View("Cadastro");
                }
                user.criar(novo);
                return RedirectToAction("Index","Home");        
            }
            
        }
       
        public ActionResult Perfil(int id)
        {
            
            if(id==Convert.ToInt32(Session["idUsuario"]))
            {
                return RedirectToAction("PerfilIni");
            }
            ViewBag.SeguindoU = false;
            Session["idUsuarioAtual"] = id; 
            ViewBag.SeguindoU = RedeMusica.Models.Usuarios.VerificaUsereguir(Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(Session["idUsuarioAtual"]));
            string ids;
            ids = Convert.ToString(id);
            Usuarios atual = user.PegaUser(ids);
            return View(atual);
        }
        public ActionResult unfollowUser(int idUserUnf)
        {
            Usuarios user = new Usuarios();
            user.UnfollowUsuario(Convert.ToInt32(Session["idUsuario"]), idUserUnf);
            return RedirectToAction("PerfilIni", "Usuarios");
        }

        public ActionResult followUser(int idUserf)
        {
            Usuarios user = new Usuarios();
            user.SeguirUsuario(Convert.ToInt32(Session["idUsuario"]), idUserf);
            return RedirectToAction("PerfilIni", "Usuarios");
        }
        public ActionResult PerfilIni()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Usuarios atual = user.PegaUser(Session["idUsuario"].ToString());
            Session["NomeCompleto"] = atual.nome + " "+ atual.sobreNome;
            return View(atual);
        }

        public ActionResult Config()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Usuarios atual = user.PegaUser(Session["idUsuario"].ToString());
            atual.senha = "";
            return View(atual);
        }

        public ActionResult AtualizarFoto()
        {
            return View();
        }

        public ActionResult ListaUsers()
        {
            List<Usuarios> lista = new List<Usuarios>();
            lista=user.ListaUser(Session["idUsuario"].ToString());
            return View(lista);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Config(Usuarios atual, HttpPostedFileBase fileImagem)
        {
            String caminhoF = null;

            if (atual.nome==null || atual.sobreNome==null || atual.senha==null)
            {
                return View("Config");
            }
            else
            {
                if (fileImagem != null && fileImagem.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fileImagem.FileName);
                    if (!ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewBag.Imagem = "Somente imagens .jpg";
                        return View("Config");
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileImagem.FileName);
                        string novas = fileName.Replace(" ", "");
                        var path = Path.Combine(Server.MapPath("~/ImagemPerfil"), novas);
                        fileImagem.SaveAs(path);
                        caminhoF = "~/ImagemPerfil/" + novas.ToString();
                        atual.fotoPerfil = caminhoF;
                        user.AtualizaFoto(atual);
                        user.editar(atual);
                        return RedirectToAction("PerfilIni","Usuarios");
                    }
                }

                else
                {
                    user.editar(atual);
                    return RedirectToAction("PerfilIni","Usuarios");
                }
            }
        }

    [HttpPost]
 public JsonResult VerificaEmail(string email)
    {
        string email_ = null;
        email_ = user.GetEmail(email);
        if (email_ == null)
        {
            string passou = null;
            return Json(passou);
        }
        return Json(email_);
    }
        public ActionResult Sair()
    {
        Session["idUsuario"] = null;
        Session["idUsuarioAtual"] = null;
        Session["buscando"] = null;
        return RedirectToAction("Index","Home");
    }
    
    }


}