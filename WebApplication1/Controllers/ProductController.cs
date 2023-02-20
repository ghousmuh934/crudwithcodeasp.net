using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source=(local);Initial Catalog=MvcCrudDb;User ID=sa;Password=123";
        // GET: Product
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product", sqlCon);
                sqlDa.Fill(dtblProduct);
                sqlCon.Close();

            }
            return View(dtblProduct);
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Insert into Product values(@ProductName, @Price, @Count)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlCmd.Parameters.AddWithValue("@Count", productModel.Count);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dtble = new DataTable();
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from Product where ProductId = @ProductId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductId", id);
                sqlDa.Fill(dtble);
            }
            if(dtble.Rows.Count > 0)
            {
                productModel.ProductId = Convert.ToInt32(dtble.Rows[0][0]);
                productModel.ProductName = dtble.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtble.Rows[0][2]);
                productModel.Count = Convert.ToInt32(dtble.Rows[0][3]);
                return View(productModel);
            }
            else
                return RedirectToAction("Index");

        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Product SET ProductName = @ProductName, Price = @Price, Count = @Count WHERE ProductId = @ProductId; ";
                SqlCommand sqlCmd = new SqlCommand(query,sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductId", productModel.ProductId);
                sqlCmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlCmd.Parameters.AddWithValue("@Count", productModel.Count);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "delete from Product where ProductId = @ProductId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductId", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}
