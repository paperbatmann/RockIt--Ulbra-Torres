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
    public class Usuarios
    {
        
        public Int64 id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Obrigatório informar o nome")]
        [StringLength(55, ErrorMessage = "O nome deve possuir no máximo 55 caracteres")]
        public string nome { get; set; }
        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Obrigatório informar o sobrenome")]
        [StringLength(55, ErrorMessage = "O sobrenome deve possuir no máximo 55 caracteres")]
        public string sobreNome { get; set; }
       
        public string tipo { get; set; }
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Obrigatório informar a senha")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "A senha deve possuir no máximo 50 caracteres")]
        public string senha { get; set; }
        
        [Display(Name = "Foto")]
        public string fotoPerfil { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o e-mail")]
        [StringLength(100, ErrorMessage = "O e-mail deve possuir no máximo 100 caracteres")]
        [Display(Name="Email")]
        public string email { get; set; }
        

        public void criar(Usuarios novo)
        {
            var senhaEn=encripta(novo.senha);
            string sql = null;
            sql="insert into usuarios(nome,sobrenome,Senha,Email,fotoPerfil) Values(@nome,@sobreNome,@Senha,@Email,@fotoPerfil);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@nome", novo.nome);
            comand.Parameters.AddWithValue("@sobreNome", novo.sobreNome);
            comand.Parameters.AddWithValue("@Senha", senhaEn);
            comand.Parameters.AddWithValue("@Email", novo.email);
            comand.Parameters.AddWithValue("@fotoPerfil", "~/Filtros/avatar.png");  
            MysqlPDS.ExecutarCommando(comand);
        }

        public Usuarios loginUser(string email, string senha)
        {
            var senhaEn = encripta(senha);
            Usuarios logado = new Usuarios();
            string sql = "SELECT * FROM usuarios WHERE email=@email && senha=@senha;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@email", email);
            comand.Parameters.AddWithValue("@senha", senhaEn);
            var dr=MysqlPDS.getLista(comand);
            if (dr.Read())
                {
               
                    logado.id = dr.GetInt32("idUsuario");
                    dr.Dispose();
                    return logado;
                }
            else
                {
                    dr.Dispose();
                    return null;
                }
        }

        public Usuarios PegaUser(string id)
        {
            Usuarios atual = new Usuarios();
            string sql = "SELECT * FROM usuarios WHERE idUsuario=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            Convert.ToInt32(id);
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            if (dr.Read())
            {

                atual.id = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.nome=dr.GetString(dr.GetOrdinal("nome"));
                atual.senha = dr.GetString(dr.GetOrdinal("senha"));
                atual.sobreNome = dr.GetString(dr.GetOrdinal("sobrenome"));
                atual.email = dr.GetString(dr.GetOrdinal("email"));
                atual.fotoPerfil = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                dr.Dispose();
                return atual;
            }
            else
            {
                dr.Dispose();
                return null;
            }
            
        }

        public  static string encripta(string _senha)
        {
            if(_senha==null)
            {
                return null;
            }
            StringBuilder senha = new StringBuilder();

            MD5 md5 = MD5.Create();
            byte[] entrada = Encoding.ASCII.GetBytes(_senha);
            byte[] hash = md5.ComputeHash(entrada);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                senha.Append(hash[i].ToString("X2"));
            }
            return senha.ToString();
        }

        public void AtualizaFoto(Usuarios atual)
        {
            MySqlCommand cmm = new MySqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE usuarios set fotoPerfil=@foto where idUsuario=@id");
            cmm.Parameters.AddWithValue("@id", atual.id);
            cmm.Parameters.AddWithValue("@foto",atual.fotoPerfil);
            cmm.CommandText = sql.ToString();
            MysqlPDS.ExecutarCommando(cmm);
        }
        public void editar(Usuarios atual)
        {
            var senhaEn = encripta(atual.senha);
            MySqlCommand cmm = new MySqlCommand();
            string sql;
           
            sql="UPDATE usuarios set nome=@nome, senha=@senha, sobrenome=@sobrenome where idUsuario=@id";
            cmm.Parameters.AddWithValue("@id", atual.id);
            cmm.Parameters.AddWithValue("@nome", atual.nome);
            cmm.Parameters.AddWithValue("@senha", senhaEn);
            cmm.Parameters.AddWithValue("@sobrenome", atual.sobreNome);
            cmm.CommandText = sql.ToString();
            MysqlPDS.ExecutarCommando(cmm);
            sql = "Update Musicas set usuario_nome=@nomeCompleto where Usuarios_idUsuario=@id";
            cmm.Parameters.AddWithValue("@nomeCompleto", atual.nome + " " + atual.sobreNome);
            cmm.CommandText = sql.ToString();
            MysqlPDS.ExecutarCommando(cmm);
        }

        public List<Usuarios> ListaUser(string id)
        {
            List<Usuarios> lista= new List<Usuarios>();
            string sql = "SELECT * FROM usuarios WHERE idUsuario<>@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            Convert.ToInt32(id);
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Usuarios atual = new Usuarios();
                atual.id = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome"));
                atual.sobreNome = dr.GetString(dr.GetOrdinal("sobrenome"));
                atual.email = dr.GetString(dr.GetOrdinal("email"));
                atual.fotoPerfil = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                //atual.tipo = dr.GetString(dr.GetOrdinal("tipo"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public string GetEmail(string email)
        {
            string sql = "SELECT * FROM usuarios WHERE email=@email;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@email", email);
            var dr = MysqlPDS.getLista(comand);
            if (dr.Read())
            {
                dr.Dispose();
                return "existe";
            }
            else
            {
                dr.Dispose();
                return null;
            }
        }
        public static List<Usuarios> BuscaUser(string buscando)
        {
            List<Usuarios> lista = new List<Usuarios>();
            string sql = "SELECT * FROM Usuarios WHERE nome LIKE  @buscando  ;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@buscando", "%" + buscando + "%");
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Usuarios atual = new Usuarios();
                atual.id = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome")) + " " + dr.GetString(dr.GetOrdinal("sobreNome"));
                atual.fotoPerfil = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public void SeguirUsuario(int idSeguidor, int idSeguindo)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "insert into seguindo_seguidores(idUser1,idUser2) Values(@idSeguidor,@idSeguindo);";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@idSeguidor", idSeguidor);
            comand.Parameters.AddWithValue("@idSeguindo", idSeguindo);
            MysqlPDS.ExecutarCommando(comand);
        }
        public void UnfollowUsuario(int idSeguidor, int idSeguindo)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "delete from seguindo_seguidores where idUser1=@idSeguidor and idUser2=@idSeguindo;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@idSeguidor", idSeguidor);
            comand.Parameters.AddWithValue("@idSeguindo", idSeguindo);
            MysqlPDS.ExecutarCommando(comand);
        }
        public void UnfollowBanda(int idSeguidor, int idBanda)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "delete from seguindo_seguidores where idUser1=@idSeguidor and idBanda=@idBanda;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@idSeguidor", idSeguidor);
            comand.Parameters.AddWithValue("@idBanda", idBanda);
            MysqlPDS.ExecutarCommando(comand);
        }
        public void SeguirBanda(int idSeguidor, int idBanda)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "insert into seguindo_seguidores (idUser1,idBanda) values(@idSeguidor, @idBanda);";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@idSeguidor", idSeguidor);

            comand.Parameters.AddWithValue("@idBanda", idBanda);
            MysqlPDS.ExecutarCommando(comand);

        }
        public static bool VerificaUsereguir(int idSeguidor, int idSeguiriei)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = " Select * from seguindo_seguidores where iduser1=@idSeguidor and idUser2=@idSeguiriei;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@idSeguidor", idSeguidor);
            comand.Parameters.AddWithValue("@idSeguiriei", idSeguiriei);
            var dr = MysqlPDS.getLista(comand);
            if (dr.Read())
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


        public static bool VerificaBandaSeguir(int idSeguidor, int idBanda)
        {
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = " Select * from seguindo_seguidores where iduser1=@idSeguidor and idBanda=@idbanda;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@idSeguidor", idSeguidor);
            comand.Parameters.AddWithValue("@idBanda", idBanda);
            var dr = MysqlPDS.getLista(comand);
            if(dr.Read())
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

        public List<Usuarios> PegaSeguindos(int id)
        {
            List<Usuarios> lista = new List<Usuarios>();
             
            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "select * from Usuarios inner join seguindo_seguidores On @id=Seguindo_seguidores.idUser1 where Usuarios.idUsuario=Seguindo_seguidores.idUser2 ;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Usuarios atual = new Usuarios();
                atual.id = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome"));
                atual.sobreNome = dr.GetString(dr.GetOrdinal("sobreNome"));
                atual.fotoPerfil = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public List<Usuarios> PegaSeguidores(int id)
        {
            List<Usuarios> lista = new List<Usuarios>();

            string sql = null;
            MySqlCommand comand = new MySqlCommand();
            sql = "select * from Usuarios inner join seguindo_seguidores On @id=Seguindo_seguidores.idUser2 where Usuarios.idUsuario=Seguindo_seguidores.idUser1 ;";
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Usuarios atual = new Usuarios();
                atual.id = dr.GetInt32(dr.GetOrdinal("idUsuario"));
                atual.nome = dr.GetString(dr.GetOrdinal("nome")) + " " + dr.GetString(dr.GetOrdinal("sobreNome"));
                atual.fotoPerfil = dr.GetString(dr.GetOrdinal("fotoPerfil"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }


    }
}