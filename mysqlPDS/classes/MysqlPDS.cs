using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;
namespace mysqlPDS.classes
{
    public class MysqlPDS
    {
      private static MySqlConnection conn = new MySqlConnection();
      private static MySqlCommand cmm;
      private static MySqlDataReader dr;
      private static MySqlDataReader dr2;
      private static string _nomeServidor = "localhost";
      private static string _nomeBanco = "rededb";
      public  static string vida
      {
          get
          {
              return "Server= " + _nomeServidor +
                  "; Port=3306" + ";Database=" + _nomeBanco +
                  ";Uid=root" +
                  ";Allow User Variables=True"+
                  ";Pwd=admin" +
                   ";default command timeout=512";
          }
      }
      private static void Conectar()
      {
         
          if(conn.State != ConnectionState.Open)
          {
              conn.ConnectionString = vida;
              conn.Open();
          }
      }

      private static void Desconectar()
      {
          if(conn.State==ConnectionState.Open)
          {
              conn.Close();
          }
      }
      public static void ExecutarCommando(MySqlCommand comando)
      {
          Conectar();
          comando.Connection = conn;
          comando.ExecuteNonQuery();
          Desconectar();
      }
        public static int ExecutarCommandoInt(string sql)
        {
            Conectar();
            cmm = new MySqlCommand();
            cmm.Connection = conn;
            cmm.CommandText = sql;
            int id = (int)cmm.ExecuteScalar();
            Desconectar();
            return id;
        }
      public static MySqlDataReader getLista(MySqlCommand comando)
      {
          Conectar();
          comando.Connection = conn;
          dr = comando.ExecuteReader();
          return dr;
      }
      public static MySqlDataReader getLista2(MySqlCommand comando2)
      {
          Conectar();
          comando2.Connection = conn;
          dr2 = comando2.ExecuteReader();
          return dr;
      }

    }
    
}
