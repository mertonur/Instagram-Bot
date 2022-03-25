using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace instagram_bot
{
    class mysqlconn
    {


        public static MySqlConnection Sunucu_MySql_Baglanti = null;

        public static MySqlConnection MySqlBaslat()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

                //SQL Bilgileriniz
                builder.UserID = "";
                builder.Password = "";
                builder.Database = "";
                builder.Server = "";
                builder.Pooling = true;
                builder.ConnectionLifeTime = 0;
                builder.ConnectionTimeout = 30;

                string connString = builder.ToString();

                MySqlConnection baglanti = new MySqlConnection(connString + ";charset=utf8mb4;SSL Mode=0");

                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                    Console.WriteLine("Mysql Connection Acildi");
                    return baglanti;
                }

                return null;

            }
            catch (Exception e)
            {
                Console.WriteLine("MySql Bağlantısı Hatası : " + e.Message);
                return null;

            }

        }


        public static bool kullaniciekle(string isim, string soyisim, string nick, string ay, string gun, string yil, string makineid, string sonulke, string sonipadresi)
        {
            string SqlCommand = "INSERT INTO `botkullanicilar`( `isim`, `soyisim`, `nick`, `ay`, `gun`, `yil`, `makine`, `sonulke`, `sonipadresi`) VALUES ('" + isim + "','" + soyisim + "','" + nick + "','" + ay + "','" + gun + "','" + yil + "','" + makineid + "','" + sonulke + "','" + sonipadresi + "')";
            MySqlCommand guncelle = new MySqlCommand(SqlCommand, Sunucu_MySql_Baglanti);

            if (guncelle != null)
            {

                try
                {
                    if (guncelle.ExecuteNonQuery() >= 0)
                    {
                        Console.WriteLine("Eklendi " + nick);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Eklenemedi 1");
                    }
                }
                catch (Exception ea)
                {
                    Console.WriteLine("Eklenemedi 2");

                }


            }
            return false;
        }




    }
}
