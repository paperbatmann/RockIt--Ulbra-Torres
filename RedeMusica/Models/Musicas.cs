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
    public class Musicas
    {
        #region atributos       
        public int id{get;set;}

        public string usuarioNome{get;set;}
        public int usuarioId{get;set;}
        public int bandaId { get; set; }
        public string estiloNome {get;set;}
        //public Bandas banda;

        [Required(ErrorMessage = "Obrigatório informar o nome")]
        [StringLength(55, ErrorMessage = "O nome deve possuir no máximo 55 caracteres")]
        public string nome{get;set;}
        public string imageMusica{get;set;}

       
        public string arquivoMusica{get;set;}

        public DateTime dataPost{get;set;}
        public int nOuvida{get;set;}
      

        #endregion 

        public void MusicaUsuario(Musicas novo)
        {
            string sql = null;
            sql = "insert into Musicas(nomeMusica,Usuarios_idUsuario,usuario_nome,imagemMusica,arquivoMusica,dataPost,nOuvida,estilos_nome) Values(@nome,@usuario,@usuarioNome,@imagem,@musica,@data,@nOuvida,@estilos);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@nome", novo.nome);
            comand.Parameters.AddWithValue("@usuario", novo.usuarioId);
            comand.Parameters.AddWithValue("@usuarioNome",novo.usuarioNome);
            comand.Parameters.AddWithValue("@imagem", novo.imageMusica);
            comand.Parameters.AddWithValue("@musica", novo.arquivoMusica);
            comand.Parameters.AddWithValue("@data", DateTime.Today);
            comand.Parameters.AddWithValue("@nOuvida", 0);
            comand.Parameters.AddWithValue("@estilos",novo.estiloNome); 
            MysqlPDS.ExecutarCommando(comand);
        }

        public void MusicaBanda(Musicas novo)
        {
            string sql = null;
            sql = "insert into Musicas(nomeMusica,Bandas_idBanda,usuario_nome,imagemMusica,arquivoMusica,dataPost,nOuvida,estilos_nome) Values(@nome,@usuario,@usuarioNome,@imagem,@musica,@data,@nOuvida,@estilos);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@nome", novo.nome);
            comand.Parameters.AddWithValue("@usuario", novo.usuarioId);
            comand.Parameters.AddWithValue("@usuarioNome", novo.usuarioNome);
            comand.Parameters.AddWithValue("@imagem", novo.imageMusica);
            comand.Parameters.AddWithValue("@musica", novo.arquivoMusica);
            comand.Parameters.AddWithValue("@data", DateTime.Today);
            comand.Parameters.AddWithValue("@nOuvida", 0);
            comand.Parameters.AddWithValue("@estilos", novo.estiloNome);
            MysqlPDS.ExecutarCommando(comand);
        }

        public List<Musicas> Proprias(string id)
        {
            List<Musicas> lista = new List<Musicas>();
            string sql = "SELECT * FROM musicas WHERE Usuarios_idUsuario=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            Convert.ToInt32(id);
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Musicas atual = new Musicas();
                atual.id = dr.GetInt32(dr.GetOrdinal("idMusica"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeMusica"));
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("Usuarios_idUsuario"));
                atual.usuarioNome = dr.GetString(dr.GetOrdinal("usuario_nome"));
                atual.imageMusica = dr.GetString(dr.GetOrdinal("imagemMusica"));
                atual.arquivoMusica = dr.GetString(dr.GetOrdinal("arquivoMusica"));
                atual.dataPost = dr.GetDateTime(dr.GetOrdinal("dataPost"));
                atual.nOuvida = dr.GetInt32(dr.GetOrdinal("nOuvida"));
                atual.estiloNome = dr.GetString(dr.GetOrdinal("Estilos_nome"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public List<Musicas> PropriasBanda(int id)
        {
            List<Musicas> lista = new List<Musicas>();
            string sql = "SELECT * FROM musicas WHERE Bandas_idBanda=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Musicas atual = new Musicas();
                atual.id = dr.GetInt32(dr.GetOrdinal("idMusica"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeMusica"));
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("Bandas_idBanda"));
                atual.usuarioNome = dr.GetString(dr.GetOrdinal("usuario_nome"));
                atual.imageMusica = dr.GetString(dr.GetOrdinal("imagemMusica"));
                atual.arquivoMusica = dr.GetString(dr.GetOrdinal("arquivoMusica"));
                atual.dataPost = dr.GetDateTime(dr.GetOrdinal("dataPost"));
                atual.estiloNome = dr.GetString(dr.GetOrdinal("Estilos_nome"));

                atual.nOuvida = dr.GetInt32(dr.GetOrdinal("nOuvida"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public static List<Musicas> estilo()
        {
            List<Musicas> listaEstilo = new List<Musicas>();
            string sql = "SELECT * FROM estilos;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            var dr = MysqlPDS.getLista(comand);
            while(dr.Read())
            {
                Musicas atual = new Musicas();
                atual.estiloNome=dr.GetString(dr.GetOrdinal("nome"));
                listaEstilo.Add(atual);
            }
            dr.Close();
            return listaEstilo;
        }

        public List<Musicas> Todas(string id)
        {
            List<Musicas> lista = new List<Musicas>();
            string sql = "SELECT * FROM musicas WHERE Usuarios_idUsuario<>@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            Convert.ToInt32(id);
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Musicas atual = new Musicas();
                atual.id = dr.GetInt32(dr.GetOrdinal("idMusica"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeMusica"));
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("Usuarios_idUsuario"));
                atual.usuarioNome = dr.GetString(dr.GetOrdinal("usuario_nome"));
                atual.imageMusica = dr.GetString(dr.GetOrdinal("imagemMusica"));
                atual.arquivoMusica = dr.GetString(dr.GetOrdinal("arquivoMusica"));
                atual.dataPost = dr.GetDateTime(dr.GetOrdinal("dataPost"));
                atual.estiloNome = dr.GetString(dr.GetOrdinal("Estilos_nome"));
                atual.nOuvida = dr.GetInt32(dr.GetOrdinal("nOuvida"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }
        public void Delete(int id)
        {
            string sql = "delete from musicas WHERE idMusica=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            MysqlPDS.ExecutarCommando(comand);
        }

        public List<Musicas> Busca(string buscando)
        {
           
         
            List<Musicas> lista = new List<Musicas>();
            if (buscando == "")
            {
                return lista;
            }
            string sql = "SELECT * FROM musicas WHERE Usuarios_idUsuario Is not null and nomeMusica  LIKE  @buscando  or estilos_nome Like @buscando and Usuarios_idUsuario Is not null;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@buscando", "%" + buscando + "%");
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Musicas atual = new Musicas();
                atual.id = dr.GetInt32(dr.GetOrdinal("idMusica"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeMusica"));
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("Usuarios_idUsuario"));
                atual.usuarioNome = dr.GetString(dr.GetOrdinal("usuario_nome"));
                atual.imageMusica = dr.GetString(dr.GetOrdinal("imagemMusica"));
                atual.arquivoMusica = dr.GetString(dr.GetOrdinal("arquivoMusica"));
                atual.dataPost = dr.GetDateTime(dr.GetOrdinal("dataPost"));
                atual.estiloNome = dr.GetString(dr.GetOrdinal("Estilos_nome"));
                atual.nOuvida = dr.GetInt32(dr.GetOrdinal("nOuvida"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public List<Musicas> BuscaMusicaBanda(string buscando)
        {
            
            List<Musicas> lista = new List<Musicas>();
            if (buscando == "")
            {
                return lista;
            }
            string sql = "SELECT * FROM musicas WHERE Bandas_idBanda Is not null and nomeMusica  LIKE  @buscando  or estilos_nome Like @buscando and Bandas_idBanda Is not null;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@buscando", "%" + buscando + "%");
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Musicas atual = new Musicas();
                atual.id = dr.GetInt32(dr.GetOrdinal("idMusica"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeMusica"));
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("Bandas_idBanda"));
                atual.usuarioNome = dr.GetString(dr.GetOrdinal("usuario_nome"));
                atual.imageMusica = dr.GetString(dr.GetOrdinal("imagemMusica"));
                atual.arquivoMusica = dr.GetString(dr.GetOrdinal("arquivoMusica"));
                atual.dataPost = dr.GetDateTime(dr.GetOrdinal("dataPost"));
                atual.estiloNome = dr.GetString(dr.GetOrdinal("Estilos_nome"));
                atual.nOuvida = dr.GetInt32(dr.GetOrdinal("nOuvida"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public List<Musicas> MusicasFollowing(int id)
        {
            List<Musicas> lista = new List<Musicas>();
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "select * from Usuarios inner join seguindo_seguidores On @id=Seguindo_seguidores.idUser1 inner join musicas on musicas.Usuarios_idUsuario=seguindo_seguidores.idUser2 where Usuarios.idUsuario=Seguindo_seguidores.idUser2 order by musicas.idMusica Desc;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Musicas atual = new Musicas();
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.usuarioNome = dr.GetString(dr.GetOrdinal("nome"))+" " + dr.GetString(dr.GetOrdinal("sobreNome"));
                atual.id = dr.GetInt32(dr.GetOrdinal("idMusica"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeMusica"));
                atual.estiloNome = dr.GetString(dr.GetOrdinal("Estilos_nome"));
                atual.imageMusica = dr.GetString(dr.GetOrdinal("imagemMusica"));
                atual.arquivoMusica = dr.GetString(dr.GetOrdinal("arquivoMusica"));
                atual.dataPost = dr.GetDateTime(dr.GetOrdinal("dataPost"));
                atual.nOuvida = dr.GetInt32(dr.GetOrdinal("nOuvida"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }
        public List<Musicas> MusicasFollowingBanda(int id)
        {
            List<Musicas> lista = new List<Musicas>();
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "select * from Bandas inner join seguindo_seguidores On @id=Seguindo_seguidores.idUser1 inner join musicas on musicas.Bandas_idBanda=seguindo_seguidores.idBanda where Bandas.idBanda=Seguindo_seguidores.idBanda order by musicas.idMusica Desc;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Musicas atual = new Musicas();
                atual.usuarioId = dr.GetInt32(dr.GetOrdinal("idBanda"));
                atual.usuarioNome = dr.GetString(dr.GetOrdinal("nomeBanda")); 
                atual.id = dr.GetInt32(dr.GetOrdinal("idMusica"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeMusica"));
                atual.estiloNome = dr.GetString(dr.GetOrdinal("Estilos_nome"));
                atual.imageMusica = dr.GetString(dr.GetOrdinal("imagemMusica"));
                atual.arquivoMusica = dr.GetString(dr.GetOrdinal("arquivoMusica"));
                atual.dataPost = dr.GetDateTime(dr.GetOrdinal("dataPost"));
                atual.nOuvida = dr.GetInt32(dr.GetOrdinal("nOuvida"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public List<Musicas> retornaMusica(int id)
        {
//para o delete(?)
            List<Musicas> lista = new List<Musicas>();
            
            string sql = "SELECT * FROM musicas WHERE idMusica=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id",id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Musicas atual = new Musicas();
                atual.id = dr.GetInt32(dr.GetOrdinal("idMusica"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }
       

    }
}