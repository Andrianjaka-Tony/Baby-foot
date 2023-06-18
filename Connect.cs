using System.Data.SqlClient;

public class Connect {

  public string connectionString() {
    string connectionString = "Data Source=localhost;Initial Catalog=babyFoot;User Id=sa;Password=root;";
    return connectionString;
  }

  public SqlConnection getConnection() {
    SqlConnection connection = new SqlConnection(connectionString());
    connection.Open();
    return connection;
  }

  public static int nextValue(string sequence) {
    
    int reponse = 0;
    string query = "SELECT NEXT VALUE FOR " + sequence + " AS value";

    SqlConnection connection = new Connect().getConnection();
    SqlCommand command = new SqlCommand(query, connection);
    SqlDataReader reader = command.ExecuteReader();

    int value = reader.GetOrdinal("value");
    while (reader.Read()) {
      reponse = Convert.ToInt32(reader.GetInt64(value));
    }
    reader.Close();
    connection.Close();

    return reponse;
  }

}