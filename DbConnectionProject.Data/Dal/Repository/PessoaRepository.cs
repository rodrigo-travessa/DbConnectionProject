using DbConnectionProject.Controllers;
using DbConnectionProject.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnectionProject.Data.Dal.Repository
{
    public class PessoaRepository : IRepository<Pessoa>
    {
        private readonly string CS = "Server=localhost;Database=myDb;User Id=sa;Password=123456;TrustServerCertificate=True;";
        public void Create(Pessoa pessoa)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                var cmd = new SqlCommand("spAddNewPessoa", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@sobrenome", pessoa.Sobrenome);
                cmd.Parameters.AddWithValue("@idade", pessoa.Idade);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Pessoa pessoa)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                var cmd = new SqlCommand("spDeletePessoa", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Id", pessoa.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public Pessoa Read(int id)
        {
            Pessoa pessoa = new Pessoa();
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetPessoas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    pessoa.Id = Convert.ToInt32(rdr["Id"]);
                    pessoa.Nome = rdr["nome"].ToString();
                    pessoa.Sobrenome = rdr["sobrenome"].ToString();
                    pessoa.Idade = Convert.ToInt32(rdr["idade"]);
                }
                return pessoa;
            }
        }
        public List<Pessoa> ReadAll()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetPessoas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var pessoa = new Pessoa()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Nome = rdr["nome"].ToString(),
                        Sobrenome = rdr["sobrenome"].ToString(),
                        Idade = Convert.ToInt32(rdr["idade"]),
                    };
                    pessoas.Add(pessoa);
                }
                return (pessoas);
            }
        }
        public void Update(Pessoa pessoa)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                var cmd = new SqlCommand("spUpdatePessoas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Id", pessoa.Id);
                cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@sobrenome", pessoa.Sobrenome);
                cmd.Parameters.AddWithValue("@idade", pessoa.Idade);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
