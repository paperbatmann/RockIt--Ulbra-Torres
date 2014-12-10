using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using mysqlPDS.classes;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedeMusica.Models
{
    public class Equipamentos
    {
        public int id { get; set; }

        public string arquivoFoto { get; set; }
        public string thumbnail { get; set; }
        public string usuarioId { get; set; }

        [StringLength(55, ErrorMessage = "Máximo 55 caracteres")]
        public string descricao { get; set; }

        public void Adicionar(Equipamentos novo)
        {

            string sql = null;
            sql = "insert into Equipamentos(Usuarios_idUsuario,descricao,arquivoFoto,thumbnail) Values(@user,@desc,@arquivo,@thumbnail);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@user", novo.usuarioId);
            comand.Parameters.AddWithValue("@desc", novo.descricao);
            comand.Parameters.AddWithValue("@arquivo", novo.arquivoFoto);
            comand.Parameters.AddWithValue("@thumbnail", novo.thumbnail);
            MysqlPDS.ExecutarCommando(comand);
        }
        public List<Equipamentos> getUserEquips(int id)
        {
            List<Equipamentos> lista = new List<Equipamentos>();
            string sql = "SELECT * FROM equipamentos WHERE Usuarios_idUsuario=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Equipamentos atual = new Equipamentos();
                atual.id = dr.GetInt32(dr.GetOrdinal("idEquip"));
                atual.descricao = dr.GetString(dr.GetOrdinal("descricao"));
                atual.arquivoFoto = dr.GetString(dr.GetOrdinal("arquivoFoto"));
                atual.thumbnail = dr.GetString(dr.GetOrdinal("thumbnail"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;
        }

        public void Delete(int id)
        {
            string sql = "delete from equipamentos WHERE idEquip=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            MysqlPDS.ExecutarCommando(comand);
        }
    }
}