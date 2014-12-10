using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedeMusica.Models;
using System.Web.Mvc;

namespace RedeMusica.Controllers
{
    public class PostsController : Controller
    {

        Posts atual = new Posts();
        // GET: Posts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CriarPost()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult CriarPostBanda()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CriarPostBanda(Posts novo)
        {
            if (!ModelState.IsValid)
            {
                return View("CriarPostBanda");
            }
            novo.usuarioId = Convert.ToInt64(Session["idBanda"]);
            atual.CriarNovoBanda(novo);
            return RedirectToAction("Perfil", "Bandas", new { id = novo.usuarioId });
        }

       [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult CriarPost(Posts novo)
        {
            if (!ModelState.IsValid)
            {
                return View("CriarPost");
            }
            novo.usuarioId = Convert.ToInt64(Session["idUsuario"]);
            atual.CriarNovo(novo);
            return RedirectToAction("PerfilIni","Usuarios");    
        }

        public ActionResult ListaPropriosPosts()
        {
            List<Posts> Lista = new List<Posts>();
           Lista= atual.getPosts(Convert.ToInt32(Session["idUsuario"]));
            return View(Lista);

        }
        
        public ActionResult Delete(int idPosts)
        {
            atual.Delete(idPosts);
            return RedirectToAction("PerfilIni", "Usuarios");
        }
    }
}