using DbConnectionProject.Controllers;
using DbConnectionProject.Models.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnectionProject.Data.Dal.Repository
{
    public class CarroRepository : IRepository<Carro>
    {
        private readonly string CS = "Server=localhost;Database=myDb;User Id=sa;Password=123456;TrustServerCertificate=True;";



        public void Create(Carro entity)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                var cmd = new SqlCommand();
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Carros (marca, modelo, donoid) VALUES (@marca, @modelo, @donoid)";
                cmd.Parameters.AddWithValue("@marca", entity.Marca);
                cmd.Parameters.AddWithValue("@modelo", entity.Modelo);
                cmd.Parameters.AddWithValue("@donoid", entity.DonoId);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Carro entity)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                var cmd = new SqlCommand();
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Carros WHERE id = @Id";
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
        }

        public Carro Read(int id)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Carros WHERE id = @id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                var carro = new Carro()
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Marca = rdr["marca"].ToString(),
                    Modelo = rdr["modelo"].ToString(),
                    DonoId = Convert.ToInt32(rdr["donoid"]),
                };
                return (carro);

            }
        }

        public List<Carro> ReadAll()
        {
            List<Carro> carros = new List<Carro>();
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Carros";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var carro = new Carro()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Marca = rdr["marca"].ToString(),
                        Modelo = rdr["modelo"].ToString(),
                        DonoId = Convert.ToInt32(rdr["donoid"]),
                    };
                    carros.Add(carro);
                }
                return (carros);
            }

        }

        public void Update(Carro entity)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                var cmd = new SqlCommand();
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Carros SET marca = @marca, modelo = @modelo, donoid = @donoid WHERE id = @Id";
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@marca", entity.Marca);
                cmd.Parameters.AddWithValue("@modelo", entity.Modelo);
                cmd.Parameters.AddWithValue("@donoid", entity.DonoId);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
