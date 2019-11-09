using System;
using System.Collections.Generic;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace CandyMarket.Api.Repositories
{
    public class CandyRepository : ICandyRepository
    {
        string _connectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";

        public IEnumerable<Candy> GetAllCandy()
        {
            string sql = "SELECT * FROM Candy";

            using (var db = new SqlConnection(_connectionString))
            {
                var AllCandy = db.Query<Candy>(sql);

                return AllCandy.AsList<Candy>();
            }           
        }

        public bool AddCandy(AddCandyDto newCandy)
        {
            string sql = "INSERT INTO [Candy] (Candy) Values (@newCandy);";

            using (var db = new SqlConnection(_connectionString))
            {
                var CandyAdded = db.Execute(sql, new {newCandy});

                return CandyAdded == 1;
            }
        }

        public bool EatCandy(Guid candyIdToDelete)
        {
            string sql = "delete from [Candy] where candyIdToDelete = @candyIdToDelete";

            using (var db = new SqlConnection(_connectionString))
            {
                var CandyDeleted = db.Execute(sql, new { candyIdToDelete });

                return CandyDeleted == 1;
            }
        }
    }
}