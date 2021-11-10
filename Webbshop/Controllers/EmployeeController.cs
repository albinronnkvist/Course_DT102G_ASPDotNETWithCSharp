using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Webbshop.Models;

namespace Webbshop.Controllers
{
    public class EmployeeController : Controller
    {
        /* Categories */
        // Get all Categories
        [HttpGet]
        [Authorize]
        public ActionResult Categories()
        {
            // Create new instance of:
            // CategoryMethods & CategoryDetail-list
            CategoryMethods cm = new CategoryMethods();
            List<CategoryDetail> CategoryList = new List<CategoryDetail>();

            // Get all categories and add to list
            CategoryList = cm.SelectAllCategories(out string error);

            // Send error message with ViewBag
            ViewBag.error = error;

            if((TempData["deleteError"] != null) || (TempData["affectedRows"] != null) || (TempData["categoryName"] != null))
            {
                ViewBag.deleteError = TempData["deleteError"].ToString();
                ViewBag.affectedRows = Convert.ToInt32(TempData["affectedRows"]);
                ViewBag.categoryName = TempData["categoryName"].ToString();
            }

            // Send list to view
            return View(CategoryList);
        }

        // Create new category
        [HttpGet]
        [Authorize]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateCategory(CategoryDetail cd)
        {
            // Create new instance of CategoryMethods
            CategoryMethods cm = new CategoryMethods();

            // Hold amount of affected rows
            int i = 0;

            // Create category
            i = cm.InsertCategory(cd, out string error);

            // Send error, affected rows & categoryName to view
            ViewBag.error = error;
            ViewBag.affectedRows = i;
            ViewBag.categoryName = cd.CategoryName;

            return View();
        }

        // Update category
        [HttpGet]
        [Authorize]
        public ActionResult EditCategory(int id)
        {
            // Create new instance of:
            // CategoryMethods & CategoryDetail
            CategoryMethods cm = new CategoryMethods();
            CategoryDetail Category = new CategoryDetail();

            // Get single category
            Category = cm.SelectSingleCategory(id, out string error);

            return View(Category);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditCategory(CategoryDetail cd)
        {
            // Create new instance of CategoryMethods
            CategoryMethods cm = new CategoryMethods();

            // Hold amount of affected rows
            int i = 0;

            // Update category
            i = cm.UpdateCategory(cd, out string error);

            // Send error & affected rows to view
            ViewBag.error = error;
            ViewBag.affectedRows = i;

            // Refresh page
            // Response.Redirect(Request.RawUrl);

            return View();
        }

        // Delete category
        [Authorize]
        public ActionResult DeleteCategory(int id, string categoryName)
        {
            // Create new instance of CategoryMethods
            CategoryMethods cm = new CategoryMethods();

            // Hold amount of affected rows
            int i = 0;

            // Update category
            i = cm.DeleteCategory(id, out string error);

            // Send error, affected rows & categoryName to view
            TempData["deleteError"] = error;
            TempData["affectedRows"] = i;
            TempData["categoryName"] = categoryName;

            return RedirectToAction("Categories", "Employee");
        }



        /* Products */
        // Get all Products
        [HttpGet]
        [Authorize]
        public ActionResult Products()
        {
            // Create new instance of:
            // CategoryMethods & CategoryDetail-list
            ProductMethods pm = new ProductMethods();
            List<ProductDetail> ProductList = new List<ProductDetail>();

            // Get all categories and add to list
            ProductList = pm.SelectAllProducts(out string error);

            // Send error message with ViewBag
            ViewBag.error = error;

            if ((TempData["deleteError"] != null) || (TempData["affectedRows"] != null) || (TempData["productName"] != null))
            {
                ViewBag.deleteError = TempData["deleteError"].ToString();
                ViewBag.affectedRows = Convert.ToInt32(TempData["affectedRows"]);
                ViewBag.productName = TempData["productName"].ToString();
            }


            // Send list to view
            return View(ProductList);
        }

        // Create new product
        [HttpGet]
        [Authorize]
        public ActionResult CreateProduct()
        {
            // Create new instances
            CategoryMethods cm = new CategoryMethods();
            CreateProductDetail product = new CreateProductDetail();

            // Get categories
            product.CategoryList = cm.SelectAllCategories(out string error);

            // Send error message with ViewBag
            ViewBag.error = error;

            // return list to view
            return View(product);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateProduct(CreateProductDetail cpd, HttpPostedFileBase file, int categoryId)
        {

            // Create new instance of AdministrativeMethods
            ProductMethods pm = new ProductMethods();
            int i = 0;

            // Get category
            cpd.Product.CategoryId = categoryId;

            // Get image
            if (file != null)
            {
                // Create new memory-stream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Write image-stream to memory-stream
                    file.InputStream.CopyTo(ms);

                    // Store memory-stream as a byte-array in ProductImage-property in ProductDetail-object
                    cpd.Product.ProductImage = ms.GetBuffer();
                }
            }

            // Save object to database
            i = pm.InsertProduct(cpd.Product, out string error);

            // Create new instances
            CategoryMethods cm = new CategoryMethods();
            CreateProductDetail product = new CreateProductDetail();

            // Get all products
            product.CategoryList = cm.SelectAllCategories(out string error2);

            // Send error, affected rows & productName to view
            ViewBag.error = error;
            ViewBag.affectedRows = i;
            ViewBag.productName = cpd.Product.ProductName;

            return View(product);
        }

        // Edit product
        [HttpGet]
        [Authorize]
        public ActionResult EditProduct(int id)
        {
            CategoryMethods cm = new CategoryMethods();
            ProductMethods pm = new ProductMethods();
            CreateProductDetail product = new CreateProductDetail();

            // Get single product
            product.Product = pm.SelectSingleProduct(id, out string error);

            // Get categories
            product.CategoryList = cm.SelectAllCategories(out string error2);

            // Send error messages with ViewBag
            ViewBag.error = error;
            ViewBag.error2 = error2;
            ViewBag.selectedCategory = product.Product.CategoryId;

            return View(product);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProduct(CreateProductDetail cpd, HttpPostedFileBase file, int categoryId)
        {

            // Create new instance of AdministrativeMethods
            ProductMethods pm = new ProductMethods();
            int i = 0;

            // Get category
            cpd.Product.CategoryId = categoryId;

            // Get image
            if (file != null)
            {
                // Create new memory-stream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Write image-stream to memory-stream
                    file.InputStream.CopyTo(ms);

                    // Store memory-stream as a byte-array in ProductImage-property in ProductDetail-object
                    cpd.Product.ProductImage = ms.GetBuffer();
                }
            }

            // Update object in database
            i = pm.UpdateProduct(cpd.Product, out string error);

            CategoryMethods cm = new CategoryMethods();
            CreateProductDetail p = new CreateProductDetail();

            // Get single product
            p.Product = pm.SelectSingleProduct(cpd.Product.Id, out string error3);

            // Get categories
            p.CategoryList = cm.SelectAllCategories(out string error2);

            // Send messages with ViewBag
            ViewBag.error = error;
            ViewBag.error2 = error2;
            ViewBag.affectedRows = i;
            ViewBag.selectedCategory = p.Product.CategoryId;

            return View(p);
        }

        // Delete product
        [Authorize]
        public ActionResult DeleteProduct(int id, string productName)
        {
            ProductMethods pm = new ProductMethods();

            // Hold amount of affected rows
            int i = 0;

            // Update product
            i = pm.DeleteProduct(id, out string error);

            // Send error, affected rows & productName to view
            TempData["deleteError"] = error;
            TempData["affectedRows"] = i;
            TempData["productName"] = productName;

            return RedirectToAction("Products", "Employee");
        }
    }
}