using AniversarioApplication.Entidade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AniversarioApplication.Database {
    public class AniversarioDatabase {

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";

        public List<Amigo> ObterTodos() {
            List<Amigo> result = new List<Amigo>();

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                var sql = "SELECT * FROM AMIGOS ORDER BY Aniversario DESC";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var data = command.ExecuteReader();

                if(data.HasRows) {
                    while(data.Read()) {
                        Amigo amigo = new Amigo();
                        amigo.Id = data["Id"].ToString();
                        amigo.Nome = data["Nome"].ToString();
                        amigo.Sobrenome = data["Sobrenome"].ToString();
                        amigo.Aniversario = DateTime.Parse(data["Aniversario"].ToString());

                        result.Add(amigo);
                    }
                }
                connection.Close();

                return result;
            }
        }

        public Amigo ObterPorId(int id) {
            Amigo result = null;

            using (SqlConnection connection = new SqlConnection(connectionString)) {

                var sql = $"SELECT * FROM AMIGOS WHERE Id = @Param1;";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var param = new SqlParameter("@Param1", SqlDbType.Int);
                param.Value = id;

                command.Parameters.Add(param);

                var data = command.ExecuteReader();

                if (data.HasRows) {
                    if (data.Read()) {
                        result = new Amigo();

                        result.Id = data["Id"].ToString();
                        result.Nome = data["Nome"].ToString();
                        result.Sobrenome = data["Sobrenome"].ToString();
                        result.Aniversario = DateTime.Parse(data["Aniversario"].ToString());
                    }
                }

                connection.Close();

                return result;
            }
        }


        public void Salvar(Amigo amigo) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                var sql = @$"INSERT INTO AMIGOS(Nome, Sobrenome, Aniversario)
                             VALUES(@P1, @P2, @P3);   
                            ";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var p1 = new SqlParameter("@P1", SqlDbType.VarChar);
                p1.Value = amigo.Nome;
                command.Parameters.Add(p1);

                var p2 = new SqlParameter("@P2", SqlDbType.VarChar);
                p2.Value = amigo.Sobrenome;
                command.Parameters.Add(p2);

                var p3 = new SqlParameter("@P3", SqlDbType.Date);
                p3.Value = amigo.Aniversario;
                command.Parameters.Add(p3);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void SalvarEdit(Amigo amigo) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                var sql = @$"
                             UPDATE AMIGOS 
                             SET Nome = @P1, Sobrenome = @P2, Aniversario = @P3  
                             WHERE Id = @Pi   
                           ";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var pi = new SqlParameter("@Pi", SqlDbType.Int);
                pi.Value = amigo.Id;
                command.Parameters.Add(pi);

                var p1 = new SqlParameter("@P1", SqlDbType.VarChar);
                p1.Value = amigo.Nome;
                command.Parameters.Add(p1);

                var p2 = new SqlParameter("@P2", SqlDbType.VarChar);
                p2.Value = amigo.Sobrenome;
                command.Parameters.Add(p2);

                var p3 = new SqlParameter("@P3", SqlDbType.Date);
                p3.Value = amigo.Aniversario;
                command.Parameters.Add(p3);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void Excluir(int amigoId) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                var sql = $"DELETE FROM AMIGOS WHERE ID = @P1";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var p1 = new SqlParameter("@P1", SqlDbType.Int);
                p1.Value = amigoId;
                command.Parameters.Add(p1);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
