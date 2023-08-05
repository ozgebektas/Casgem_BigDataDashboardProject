using Casgem_BigDataDashboardProject.Dtos.Dto;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Casgem_BigDataDashboardProject.Controllers
{
    public class DefaultController : Controller
    {

        private readonly string _connectionString = "Server=LAPTOP-8JIDE4EC\\SQLEXPRESS;initial Catalog=CARPLATES;integrated security=true";
        public async Task<IActionResult> Index()
        
        {
            await using var connection = new SqlConnection(_connectionString);
            var brandMax = await connection.QueryFirstAsync<Brand>("SELECT TOP 1 BRAND, COUNT(*) AS count FROM PLATES GROUP BY BRAND ORDER BY count DESC");

            ViewData["brandMax"] = brandMax.BRAND;

            return View();
        }
        public async Task<IActionResult> Search(string plaka)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlQuery = "SELECT Brand, CityNr FROM Plates WHERE Plate = @Plaka";
                var platesViewModel = await connection.QueryFirstOrDefaultAsync<Plates>(sqlQuery, new { Plates = plaka });


                return Json(platesViewModel);
            }
        }
    }
}
