using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductImplement
    {
        private SqlConnection con;

        private void Conectar()
        {
            con = new SqlConnection(@"Server=BRUCE\INSTANCEONE;Database=TIStore; User id=sa; Password='1234'");

        }
        public int Create(Product prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into Product(id_Product,name_Product,price_Product,categorie_Product) values (@id_Product,@name_Product,@price_Product,@categorie_Product)", con);
            comando.Parameters.Add("@id_Product", SqlDbType.Int);
            comando.Parameters.Add("@name_Product", SqlDbType.VarChar);
            comando.Parameters.Add("@price_Product", SqlDbType.Money);
            comando.Parameters.Add("@categorie_Product", SqlDbType.VarChar);
          //  comando.Parameters.Add("@image_Product", SqlDbType.Image);
            comando.Parameters["@id_Product"].Value = prod.id_Product;
            comando.Parameters["@name_Product"].Value = prod.name_Product;
            comando.Parameters["@price_Product"].Value = prod.price_Product;
            comando.Parameters["@categorie_Product"].Value = prod.categorie_Product;
           // comando.Parameters["@image_Product"].Value = prod.Image_Product;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Product> ReadAll()
        {
            Conectar();
            List<Product> productos = new List<Product>();

            SqlCommand com = new SqlCommand("select id_Product,name_Product,price_Product,categorie_Product from Product", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Product prod = new Product
                {
                    id_Product = int.Parse(registros["id_Product"].ToString()),
                    name_Product = registros["name_Product"].ToString(),
                    price_Product = double.Parse(registros["price_Product"].ToString()),
                    categorie_Product = registros["categorie_Product"].ToString()
                };
                productos.Add(prod);
            }
            con.Close();
            return productos;
        }
        public Product Read(int code)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("select id_Product,name_Product,price_Product,categorie_Product from Product where id_Product=@code", con);
            comando.Parameters.Add("@code", SqlDbType.Int);
            comando.Parameters["@code"].Value = code;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Product producto = new Product();
            if (registros.Read())
            {
                producto.id_Product = int.Parse(registros["id_Product"].ToString());
                producto.name_Product = registros["name_Product"].ToString();
                producto.price_Product = double.Parse(registros["price_Product"].ToString());
                producto.categorie_Product = registros["categorie_Product"].ToString();
            }
            con.Close();
            return producto;
        }
        public int Update(Product prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("update Product set name_Product=@name_Product,price_Product=@price_Product,categorie_Product=@categorie_Product where id_Product=@id_Product", con);
            comando.Parameters.Add("@name_Product", SqlDbType.VarChar);
            comando.Parameters["@name_Product"].Value = prod.name_Product;
            comando.Parameters.Add("@price_Product",SqlDbType.Float);
            comando.Parameters["@price_Product"].Value = prod.price_Product;
            comando.Parameters.Add("@categorie_Product", SqlDbType.VarChar);
            comando.Parameters["@categorie_Product"].Value = prod.categorie_Product;
            comando.Parameters.Add("@id_Product", SqlDbType.Int);
            comando.Parameters["@id_Product"].Value = prod.id_Product;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Delete(int code)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from Product where id_Product=@code", con);
            comando.Parameters.Add("@code", SqlDbType.Int);
            comando.Parameters["@code"].Value = code;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}