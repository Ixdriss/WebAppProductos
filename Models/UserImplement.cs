using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserImplement
    {
        private SqlConnection con;
        private void Conectar() {
            con = new SqlConnection(@"Server=BRUCE\INSTANCEONE;Database=TIStore; User id=sa; Password='1234'");
            }
        
        public int Create(User usr)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into StoreUser(id_User,name_User,password_User) values (@id_User,@name_User,@password_User)", con);
            comando.Parameters.Add("@id_User", SqlDbType.Int);
            comando.Parameters.Add("@name_User", SqlDbType.VarChar);
            comando.Parameters.Add("@password_User", SqlDbType.VarChar);
            comando.Parameters["@id_User"].Value = usr.id_User;
            comando.Parameters["@name_User"].Value = usr.name_User;
            comando.Parameters["@password_User"].Value = usr.password_User;
            con.Open();
          

            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public Boolean LogIn(User usr)
        {
            Conectar();

          
            SqlCommand comando = new SqlCommand("select * from StoreUser where name_User=@name_User and password_User=@password_User", con);
            comando.Parameters.Add("@name_User", SqlDbType.VarChar);
            comando.Parameters.Add("@password_User", SqlDbType.VarChar);
            comando.Parameters["@name_User"].Value = usr.name_User;
            comando.Parameters["@password_User"].Value = usr.password_User;
            con.Open();
            SqlDataReader sqlDataReader = comando.ExecuteReader();

            if(sqlDataReader.Read())
            {
                User us = new User()
                {
                    id_User = int.Parse(sqlDataReader["id_User"].ToString()),
                    name_User = sqlDataReader["name_User"].ToString(),
                    password_User = sqlDataReader["password_User"].ToString(),

                };
                con.Close();
                if(us.name_User != null)
                {
                    return true;
                }
            }
            con.Close();
            return false;
        }
    }
}