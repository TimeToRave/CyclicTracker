using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace CyclicTracker
{
    public class DataBaseConnector
    {
        private string connectionString;

        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public DataBaseConnector() : this(@"Tasks.db") { } 

        public DataBaseConnector(string dataBaseFilePath)
        {
            ConnectionString = string.Format(@"URI=file:{0}", dataBaseFilePath);

            if(!File.Exists(dataBaseFilePath))
            {
                CreateDataBase();
            }
        }

        private void CreateDataBase ()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                ExecuteQuery(connection, "DROP TABLE IF EXISTS Task");
                ExecuteQuery(connection, "CREATE TABLE Task (Id INTEGER PRIMARY KEY, Task TEXT NOT NULL, StartDate TEXT, EndDate TEXT)");
            }
        }

        private void ExecuteQuery(SQLiteConnection connection, string query)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }

        public int InsertTask(Tasker task)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString)) {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Task(Task, StartDate, EndDate) VALUES(@task, @start, @end)";
                    command.Parameters.AddWithValue("@task", task.CurrentTask);
                    command.Parameters.AddWithValue("@start", task.CurrentTaskStart);
                    command.Parameters.AddWithValue("@end", DateTime.Now);
                    command.Prepare();

                    command.ExecuteNonQuery();
                }
            }
            return 0;
        }
    }
}
