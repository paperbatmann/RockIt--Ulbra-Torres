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
    public class Bandas
    {
        public Int64 id { get; set; }
       public Int64 idUsuario { get; set; }

       [Display(Name = "Nome da Banda")]
        [Required(ErrorMessage = "Obrigatorio informar nome")]
        [StringLength(55, ErrorMessage = "O nome deve possuir no máximo 55 caracteres")]
        public string nome { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage="Obrigatorio informar cidade")]
        [StringLength(99,ErrorMessage="Cidade natal deve possuir no máximo 99 caracteres")]
        public string cidadeNatal { get; set; }

        public string foto { get; set; }


        public void Criar(Bandas novo)
        {
            int id = 0;
            string sql = null;
            sql = "insert into Bandas(nomeBanda,cidadeNatal,foto) Values(@nome,@cidadeNatal,@foto);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@nome", novo.nome);
            comand.Parameters.AddWithValue("@cidadeNatal", novo.cidadeNatal);
            comand.Parameters.AddWithValue("@foto", novo.foto);
            MysqlPDS.ExecutarCommando(comand);
            comand.CommandText = "SELECT LAST_INSERT_ID()";
            var dr = MysqlPDS.getLista(comand);
            if (dr != null && dr.Read())
            {
                 id = dr.GetInt32(0);
            }
            dr.Dispose();
            sql = "insert into bandas_usuarios(idUsuario,idBanda,lider) Values(@usuario,@idBanda,@lider);";
            comand.CommandText=sql;
            comand.Parameters.AddWithValue("@usuario",novo.idUsuario);
            comand.Parameters.AddWithValue("@lider",1);
            comand.Parameters.AddWithValue("@idBanda",id);
            MysqlPDS.ExecutarCommando(comand);
        }
        public Bandas PegaBanda(string id)
        {
            Bandas atual = new Bandas();
            string sql = "SELECT * FROM bandas WHERE idBanda=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            Convert.ToInt32(id);
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            if (dr.Read())
            {

                atual.id = dr.GetInt32(dr.GetOrdinal("idBanda"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeBanda"));
                atual.cidadeNatal = dr.GetString(dr.GetOrdinal("cidadeNatal"));
                atual.foto = dr.GetString(dr.GetOrdinal("foto"));
                dr.Dispose();
                return atual;
            }
            else
            {
                dr.Dispose();
                return null;
            }
        }
       public List<Bandas> BandasUser(int id)
            {
           List< Bandas> lista = new List<Bandas>();
           string sql = "SELECT * FROM bandas inner join bandas_usuarios on bandas.idBanda=bandas_usuarios.idBanda where bandas_usuarios.idUsuario=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            Convert.ToInt32(id);
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Bandas atual = new Bandas();
                atual.id = dr.GetInt32(dr.GetOrdinal("idBanda"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeBanda"));
                atual.cidadeNatal = dr.GetString(dr.GetOrdinal("cidadeNatal"));
                atual.foto = dr.GetString(dr.GetOrdinal("foto"));
                lista.Add(atual);
                
            }
            dr.Dispose();
            return lista;

        }
        public bool verificaUser(int idUser, int idBanda)
        {
            
            string sql = "SELECT * FROM Bandas_usuarios WHERE idUsuario=@idUser AND idBanda=@idBanda;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            Convert.ToInt32(id);
            comand.Parameters.AddWithValue("@idUser", idUser);
            comand.Parameters.AddWithValue("@idBanda",idBanda);
            var dr = MysqlPDS.getLista(comand);
            if (dr.Read())
            {
                var lider = dr.GetInt32(dr.GetOrdinal("lider"));
                if (lider == 1)
                {
                    dr.Dispose();
                    return true;
                }
                else
                {
                    dr.Dispose();
                    return false;
                }
            }
            else
            {
                dr.Dispose();
                return false;
            }
        }

        public List<Usuarios> getIntegrantes(int idBanda)
        {
            string sql = null;
            sql = "select * from Usuarios Inner Join Bandas_usuarios On Usuarios.idUsuario=Bandas_usuarios.idUsuario where bandas_usuarios.idBanda=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            List<Usuarios> lista = new List<Usuarios>();
            comand.Parameters.AddWithValue("@id", idBanda);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Usuarios atual = new Usuarios();
                atual.id = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.fotoPerfil = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome")) + " " + dr.GetString(dr.GetOrdinal("sobrenome"));
                lista.Add(atual);
             }
            dr.Dispose();
            return lista;
        }
        public List<Usuarios> getIntegrantesLider(int idBanda,int idLider)
        {
            string sql = null;
            sql = "select * from Usuarios Inner Join Bandas_usuarios On Usuarios.idUsuario=Bandas_usuarios.idUsuario where bandas_usuarios.idBanda=@id and Usuarios.idUsuario<>@idLider ;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            List<Usuarios> lista = new List<Usuarios>();
            comand.Parameters.AddWithValue("@id", idBanda);
            comand.Parameters.AddWithValue("@idLider", idLider);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Usuarios atual = new Usuarios();
                atual.id = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.fotoPerfil = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome")) + " " + dr.GetString(dr.GetOrdinal("sobrenome"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }
        public void AdicionarIntegrante(int idUser, int idBanda)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "insert into bandas_usuarios(idUsuario,idBanda,lider) Values(@usuario,@idBanda,@lider);";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@usuario", idUser);
            comand.Parameters.AddWithValue("@lider", 0);
            comand.Parameters.AddWithValue("@idBanda", idBanda);
            MysqlPDS.ExecutarCommando(comand);
        }
        public void DeletarIntegrante(int idUser, int idBanda)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "delete from bandas_usuarios where idUsuario=@id and idBanda=@banda";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", idUser);
            comand.Parameters.AddWithValue("@Banda", idBanda);
            MysqlPDS.ExecutarCommando(comand);
        }


        public void DeletarBanda(int banda)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();

           
            sql = "Delete from musicas where bandas_idBanda=@id";
            comand.Parameters.AddWithValue("@id", banda);
            comand.CommandText = sql;
            MysqlPDS.ExecutarCommando(comand);
            sql = "Delete from seguindo_seguidores where idBanda=@id";
            comand.CommandText = sql;
            MysqlPDS.ExecutarCommando(comand);
            sql = "Delete from bandas_usuarios where idBanda=@id";
            comand.CommandText = sql;
            MysqlPDS.ExecutarCommando(comand);
            sql = "Delete from galeria where idBanda=@id ;";
            comand.CommandText = sql;
            MysqlPDS.ExecutarCommando(comand);
            sql = "Delete from postagens where idBanda=@id ;";
            comand.CommandText = sql;
            MysqlPDS.ExecutarCommando(comand);
            sql = "Delete from bandas where idBanda=@id ;";
            comand.CommandText = sql;
            MysqlPDS.ExecutarCommando(comand);
        }

        public List<Bandas> Busca(string buscando)
        {
            List<Bandas> lista = new List<Bandas>();
            string sql = "SELECT * FROM Bandas WHERE nomeBanda LIKE  @buscando  OR cidadeNatal Like @buscando;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@buscando", "%" + buscando + "%");
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Bandas atual = new Bandas();
                atual.id = dr.GetInt32(dr.GetOrdinal("idBanda"));
                atual.nome = dr.GetString(dr.GetOrdinal("nomeBanda"));
                atual.cidadeNatal = dr.GetString(dr.GetOrdinal("cidadeNatal"));
                atual.foto = dr.GetString(dr.GetOrdinal("foto"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public List<Usuarios> buscaUser(int idBanda, string buscando,int lider)
        {
            string sql = null;
            sql = "select * from Usuarios Inner Join  Bandas_usuarios On Usuarios.idUsuario<>Bandas_usuarios.idUsuario  where bandas_usuarios.idBanda=@id and Usuarios.nome like @buscando and Usuarios.idUsuario<>@lider ;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            List<Usuarios> lista = new List<Usuarios>();
            comand.Parameters.AddWithValue("@id", idBanda);
            comand.Parameters.AddWithValue("@lider", lider);
            comand.Parameters.AddWithValue("@buscando", "%" + buscando + "%");
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Usuarios atual = new Usuarios();
                atual.id = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.fotoPerfil = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome")) + " " + dr.GetString(dr.GetOrdinal("sobrenome"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public void AtualizaFoto(Bandas atual)
        {
            MySqlCommand cmm = new MySqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE bandas  set foto=@foto where idBanda=@id");
            cmm.Parameters.AddWithValue("@id", atual.id);
            cmm.Parameters.AddWithValue("@foto", atual.foto);
            cmm.CommandText = sql.ToString();
            MysqlPDS.ExecutarCommando(cmm);
        }
        public void editar(Bandas atual)
        {
           
            MySqlCommand cmm = new MySqlCommand();
            string sql;

            sql = "UPDATE Bandas set nomeBanda=@nome, cidadeNatal=@cidade where idBanda=@id";
            cmm.Parameters.AddWithValue("@id", atual.id);
            cmm.Parameters.AddWithValue("@nome", atual.nome);
            cmm.Parameters.AddWithValue("@cidade", atual.cidadeNatal);
            cmm.CommandText = sql.ToString();
            MysqlPDS.ExecutarCommando(cmm);
            sql = "Update Musicas set usuario_nome=@nomeCompleto where Bandas_idBanda=@id";
            cmm.Parameters.AddWithValue("@nomeCompleto", atual.nome );
            cmm.CommandText = sql.ToString();
            MysqlPDS.ExecutarCommando(cmm);
        }
    }
}