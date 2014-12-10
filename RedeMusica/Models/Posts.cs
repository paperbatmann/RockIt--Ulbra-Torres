using System;
using MySql.Data.MySqlClient;
using mysqlPDS.classes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;
using System.Text;

namespace RedeMusica.Models
{
    public class Posts
    {
        public Int64 usuarioId { get; set; }
        public int idPost { get; set; }
        [Required(ErrorMessage = "Obrigatorio")]
        public string texto { get; set; }

        public string imagem { get; set; }

        public string nome{get;set;}
        public string sobrenome { get; set; }
       
        public void CriarNovo(Posts novo)
        {
            string sql = null;
            sql = "insert into postagens(idUser,texto,arquivoFoto,dataPost) Values(@idUser,@texto,@arqs,@data);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@idUser", novo.usuarioId);
            comand.Parameters.AddWithValue("@texto", novo.texto);
            comand.Parameters.AddWithValue("@arqs", "o");
            comand.Parameters.AddWithValue("@data", DateTime.Now);
            MysqlPDS.ExecutarCommando(comand);
        }

        public void CriarNovoBanda(Posts novo)
        {
            string sql = null;
            sql = "insert into postagens(idBanda,texto,arquivoFoto,dataPost) Values(@idUser,@texto,@arqs,@data);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@idUser", novo.usuarioId);
            comand.Parameters.AddWithValue("@texto", novo.texto);
            comand.Parameters.AddWithValue("@arqs", "o");
            comand.Parameters.AddWithValue("@data", DateTime.Now);
            MysqlPDS.ExecutarCommando(comand);
        }

        public void Delete(int id)
        {
            string sql = null;
            sql = "Delete from postagens where id=@id;";
                MySqlCommand  comand = new MySqlCommand();
            comand.CommandText=sql;
            comand.Parameters.AddWithValue("@id",id);
            MysqlPDS.ExecutarCommando(comand);
        }
        public List<Posts> getPosts(int id)
        {
            string sql = null;
            sql = "select * from postagens Inner Join Usuarios On postagens.idUser=Usuarios.idUsuario where postagens.idUser=@id order by postagens.id desc;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            List<Posts> lista = new List<Posts>();
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Posts atual = new Posts();
                
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("idUser"));
                atual.idPost = dr.GetInt32(dr.GetOrdinal("id"));
                atual.texto = dr.GetString(dr.GetOrdinal("texto"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome"))+ " " + dr.GetString(dr.GetOrdinal("sobrenome"));
          
                atual.imagem = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                 lista.Add(atual);
             }
            dr.Dispose();
            return lista;

        }

        public List<Posts> getPostsBanda(int id)
        {
            string sql = null;
            sql = "select * from postagens Inner Join Bandas On postagens.idBanda=Bandas.idBanda where postagens.idBanda=@id order by postagens.id desc;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            List<Posts> lista = new List<Posts>();
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Posts atual = new Posts();

                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("idBanda"));
                atual.idPost = dr.GetInt32(dr.GetOrdinal("id"));
                atual.texto = dr.GetString(dr.GetOrdinal("texto"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeBanda"));
                atual.imagem = dr.GetString(dr.GetOrdinal("foto"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;

        }
        public List<Posts> PostagensFollowing(int id)
        {
            List<Posts> lista = new List<Posts>();
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "select * from Usuarios inner join seguindo_seguidores On @id=Seguindo_seguidores.idUser1 inner join postagens on postagens.idUser=seguindo_seguidores.idUser2 where Usuarios.idUsuario=Seguindo_seguidores.idUser2 order by postagens.id Desc;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Posts atual = new Posts();
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.idPost = dr.GetInt32(dr.GetOrdinal("id"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome")) + " " + dr.GetString(dr.GetOrdinal("sobreNome"));
              
                atual.imagem = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                atual.texto = dr.GetString(dr.GetOrdinal("texto"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public List<Posts> PostagensFollowingBandas(int id)
        {
            List<Posts> lista = new List<Posts>();
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "select * from Bandas inner join seguindo_seguidores On @id=Seguindo_seguidores.idUser1 inner join postagens on postagens.idBanda=seguindo_seguidores.idBanda where Bandas.idBanda=Seguindo_seguidores.idBanda order by postagens.id Desc;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Posts atual = new Posts();
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("idBanda"));
                atual.idPost = dr.GetInt32(dr.GetOrdinal("id"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeBanda"));
                atual.imagem = dr.GetString(dr.GetOrdinal("foto"));
                atual.texto = dr.GetString(dr.GetOrdinal("texto"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }
    }
}