using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbshop.Models;

namespace Webbshop.Controllers
{
    public class HomeController : Controller
    {
        // GET products & categories
        [HttpGet]
        public ActionResult Index()
        {
            // Create new instances of:
            // ProductMethods & CategoryMethods
            ProductMethods pm = new ProductMethods();
            CategoryMethods cm = new CategoryMethods();

            // Store error-message
            string error = "";

            // Create new instance of ViewModelPC
            ViewModelPCC modelPCC = new ViewModelPCC()
            {
                // Create a product-list and category-list
                ProductList = pm.SelectAllProducts(out string error2),
                CategoryList = cm.SelectAllCategories(out error2)
            };

            // Pass error message with ViewBag
            ViewBag.error = error;

            // Pass lists to view
            return View(modelPCC);
        }

        // POST filters
        [HttpPost]
        public ActionResult Index(string categoryId, string productName)
        {
            // Get categoryId and convert to int
            int categoryid = Convert.ToInt32(categoryId);

            // Create new instances of:
            // ProductMethods & CategoryMethods
            ProductMethods pm = new ProductMethods();
            CategoryMethods cm = new CategoryMethods();

            // Create new instance of ViewModelPC
            ViewModelPCC modelPCC = new ViewModelPCC()
            {
                // Create a category-list
                CategoryList = cm.SelectAllCategories(out string error)
            };

            // If no filter was choosen and no search was made:
            // get all products
            if ((categoryid == 0) && (productName == null))
            {
                modelPCC.ProductList = pm.SelectAllProducts(out string error2);
            }
            // Else if some filtering was made
            {
                // If a search was made
                if (productName != null)
                {
                    modelPCC.ProductList = pm.SelectProductsByName(productName, out string error3);
                    ViewBag.productName = productName;

                    // If search didn't give a result: get all products
                    if (modelPCC.ProductList == null)
                    {
                        modelPCC.ProductList = pm.SelectAllProducts(out string error4);
                        ViewBag.searchError = "Din sökning gav inga resultat.";
                    }
                }
                // If no search was made: check if user filtered by category
                else
                {
                    modelPCC.ProductList = pm.SelectProductsByCategory(categoryid, out string error5);
                    modelPCC.SingleCategory = cm.SelectSingleCategory(categoryid, out string error6);

                    // If filtering didn't give a result: get all products
                    if (modelPCC.ProductList == null)
                    {
                        modelPCC.ProductList = pm.SelectAllProducts(out string error7);
                        ViewBag.searcherror = "Din filtrering gav inga resultat.";
                    }
                }
            }

            // Pass categoryid to view
            ViewBag.categoryid = categoryid;

            // Pass lists to view
            return View(modelPCC);
        }

        // GET specific product
        [HttpGet]
        public ActionResult SpecificProduct(int productId)
        {
            // Create new instance of:
            // CategoryMethods & CategoryDetail
            ProductMethods pm = new ProductMethods();
            ProductDetail product = new ProductDetail();

            // Get single category
            product = pm.SelectSingleProduct(productId, out string error);

            return View(product);
        }



        // GET Shopping-cart
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            return View();
        }

        // POST Shopping-cart
        [HttpPost]
        public ActionResult ShoppingCart(ProductDetail pd)
        {
            if(Session["ShoppingList"] == null)
            {
                List<ProductDetail> productList = new List<ProductDetail>
                {
                    new ProductDetail { Id = pd.Id, ProductName = pd.ProductName, ProductPrice = pd.ProductPrice }
                };

                Session["ShoppingList"] = productList;
            }
            else
            {
                var productList = (List<ProductDetail>)Session["ShoppingList"];
                productList.Add(new ProductDetail { Id = pd.Id, ProductName = pd.ProductName, ProductPrice = pd.ProductPrice });
                Session["ShoppingList"] = productList;
            }

            return View();
        }

        // Clear items from shopping-cart
        public ActionResult ClearShoppingCart()
        {
            // Remove session
            Session.Remove("ShoppingList");

            // Redirect to ShoppingCart
            return RedirectToAction("ShoppingCart", "Home");
        }

    }
}