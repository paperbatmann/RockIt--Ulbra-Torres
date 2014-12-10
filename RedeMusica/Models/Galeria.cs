using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using mysqlPDS.classes;

namespace RedeMusica.Models
{
    public class Galeria
    {
        public int id { get; set; }

        public string arquivoFoto { get; set; }
        public string usuarioId { get; set; }
        public string thumbnail { get; set; }
       
        [StringLength(55, ErrorMessage = "Máximo 55 caracteres")]
        public string descricao { get; set; }

        public void Adicionar(Galeria novo)
        {
            
            string sql = null;
            sql = "insert into Galeria(idUser,descricao,arquivoFoto,thumbnail) Values(@user,@desc,@arquivo,@thumbnail);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@user", novo.usuarioId);
            comand.Parameters.AddWithValue("@desc",novo.descricao);
            comand.Parameters.AddWithValue("@arquivo", novo.arquivoFoto);
            comand.Parameters.AddWithValue("@thumbnail", novo.thumbnail);
            MysqlPDS.ExecutarCommando(comand);
        }

        public void AdicionarFotoBanda(Galeria novo)
        {

            string sql = null;
            sql = "insert into Galeria(idBanda,descricao,arquivoFoto,thumbnail) Values(@user,@desc,@arquivo,@thumbnail);";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@user", novo.usuarioId);
            comand.Parameters.AddWithValue("@desc", novo.descricao);
            comand.Parameters.AddWithValue("@arquivo", novo.arquivoFoto);
            comand.Parameters.AddWithValue("@thumbnail", novo.thumbnail);
            MysqlPDS.ExecutarCommando(comand);
        }

        public void Delete(int id)
        {
            
                string sql = "delete from galeria WHERE idFoto=@id;";
                MySqlCommand comand = new MySqlCommand();
                comand.CommandText = sql;
                comand.Parameters.AddWithValue("@id", id);
                MysqlPDS.ExecutarCommando(comand);
            
        }
        public List<Galeria> getUserFotos(int id)
        {
            List<Galeria> lista = new List<Galeria>();
            string sql = "SELECT * FROM galeria WHERE idUser=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Galeria atual = new Galeria();
                atual.id = dr.GetInt32(dr.GetOrdinal("idFoto"));
                atual.descricao = dr.GetString(dr.GetOrdinal("descricao"));
                atual.arquivoFoto = dr.GetString(dr.GetOrdinal("arquivoFoto"));
                atual.thumbnail = dr.GetString(dr.GetOrdinal("thumbnail"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;

        }
        public List<Galeria> getBandaFotos(int id)
        {
            List<Galeria> lista = new List<Galeria>();
            string sql = "SELECT * FROM galeria WHERE idBanda=@id;";
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@id", id);
            var dr = MysqlPDS.getLista(comand);
            while (dr.Read())
            {
                Galeria atual = new Galeria();
                atual.id = dr.GetInt32(dr.GetOrdinal("idFoto"));
                atual.descricao = dr.GetString(dr.GetOrdinal("descricao"));
                atual.arquivoFoto = dr.GetString(dr.GetOrdinal("arquivoFoto"));
                atual.thumbnail = dr.GetString(dr.GetOrdinal("thumbnail"));
                lista.Add(atual);
            }
            dr.Dispose();
            return lista;

        }

    }
}