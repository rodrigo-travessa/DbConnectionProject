using Microsoft.Data.SqlClient;

namespace DbConnectionProject.Dal
{
    public class SqlServerDal
    {
        SqlConnection connection = new SqlConnection();
        //connection.ConnectionString = "Server=localhost;Database=myDb;Trusted_Connection=True;TrustServerCertificate=True";
        
        public SqlServerDal()
        {
            connection.ConnectionString = "Server=localhost;Database=myDb;User Id=sa;Password=123456;TrustServerCertificate=True;";
            connection.Open();

        }

        public void TesteConectar()
        {
            
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO myTable (nome) VALUES ('Vivian')";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();


            Console.WriteLine("Teste");
            connection.Close();
            
        }
    }
}
