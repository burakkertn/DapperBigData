using DapperBigData.DAL;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperBigData.Controllers
{
    public class SearchController : Controller
    {
        private readonly string _connectionString = "Server = BURAK\\SQLEXPRESS; initial catalog = CARPLATES; integrated security = true";

        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var query = "SELECT TOP 100 * FROM PLATES"; 

          
                if (!string.IsNullOrEmpty(searchString))
                {
            
                    query = "SELECT * FROM PLATES WHERE BRAND LIKE @SearchString";
              
                    searchString = $"%{searchString}%";
                }

                var values = await connection.QueryAsync<Plates>(query, new { SearchString = searchString });
                return View(values);
            }
            catch (Exception ex)
            {
        
                return View("Error", ex.Message);
            }
        }



    }
}
