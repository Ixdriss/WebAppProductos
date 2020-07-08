using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        public ActionResult ShowProducts()
        {
            ProductImplement cs = new ProductImplement();
            return View(cs.ReadAll());
        }
        public ActionResult CreateProduct()
        {
            return View();
        }
        public ActionResult DeleteProduct(int cod)
        {
            ProductImplement imp = new ProductImplement();
            imp.Delete(cod);
            return RedirectToAction("ShowProducts");
        }
        public ActionResult UpdateProduct(int cod)
        {
            ProductImplement imp = new ProductImplement();
            Product pr = new Product();
            pr = imp.Read(cod);
            return View(pr);
        }
        public ActionResult SearchProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(FormCollection collection)
        {
            ProductImplement imp = new ProductImplement();
            Product pr = new Product();
            
                pr.id_Product = imp.ReadAll().Count + 1;
                pr.name_Product = collection["nameProduct"];
                pr.price_Product = Double.Parse(collection["priceProduct"]);
                pr.categorie_Product = collection["categorieList"].ToString();
           
             

            imp.Create(pr);
            return RedirectToAction("ShowProducts");
             }
        
        [HttpPost]
        public ActionResult DeleteProduct(FormCollection collection, int cod)
        {
            ProductImplement imp = new ProductImplement();
            
            
            int i=imp.Delete(cod);
            return RedirectToAction("ShowProducts");
        }
        [HttpPost]
        public ActionResult UpdateProduct(FormCollection collection)
        {
            ProductImplement imp = new ProductImplement();
            Product pr = new Product();

            pr.id_Product = int.Parse(collection["idProduct"].ToString());
            pr.name_Product = collection["nameProduct"].ToString();
            pr.price_Product = Double.Parse(collection["priceProduct"]);
            pr.categorie_Product =collection["categorieList"].ToString();
            
            imp.Update(pr);
            return RedirectToAction("ShowProducts");
        }

       
        public ActionResult SearchCat(int cod)
        {
            ProductImplement imp = new ProductImplement();
            Product pr = new Product();
            String cat;

            if (cod == 1)
            {
                cat = ("Fruit").ToString();
            }
            else
            {
                if (cod == 2)
                {
                    cat = ("Vegetable").ToString();
                }
                else
                {
                    cat = ("Meat").ToString();
                }
            }

            List<Product> productos = new List<Product>();
            productos = imp.ReadAll();
            List<Product> productoscat = new List<Product>();

            //linq lambda...

            productoscat = productos.Where(o => o.categorie_Product == cat).ToList();

            //Con el codigo que sigue se puede reemplazar el if de arriba lol

           /* foreach (var Product in productos)
            {
                if (Product.categorie_Product.Equals(cat))
                {
                    productoscat.Add(Product);
                }
            }*/
            return View(productoscat);

        }
    }
}