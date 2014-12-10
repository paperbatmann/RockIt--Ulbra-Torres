using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedeMusica.Models;
using System.Web.Mvc;

namespace RedeMusica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuarios login)
        {
            Usuarios conectado = login.loginUser(login.email, login.senha);
            if (conectado != null)
            {
                Session["idUsuario"] = conectado.id;
                return RedirectToAction("PerfilIni", "Usuarios");
            }
            else
            {
               
                return RedirectToAction("PerfilIni","Usuarios");
            }
        }
        public ActionResult Acesso()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Buscar(string buscando)
        {
            if(Session["idUsuario"]==null)
            {

                ViewBag.ErroLogin = "Você não está logado.";
                return RedirectToAction("Index", "Home");
            }
            Usuarios user = new Usuarios();
            Usuarios atual = user.PegaUser(Session["idUsuario"].ToString());
            Session["NomeCompleto"] = atual.nome + " " + atual.sobreNome;
            Session["buscando"] = buscando;
            return View(atual);
        }
      
    }
}