using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleTriggerApp
{
    internal class Program
    {
        //private static string _con = "data source=DESKTOP-485P455; initial catalog=Gestione_Pratiche; integrated security=True";
        private static string _con = "Data Source=DESKTOP-485P455;Initial Catalog=Gestione_Pratiche; Trusted_Connection=True; Persist Security Info=False; User ID=sa;Password=;Connect Timeout=10";

        static void Main(string[] args)
        {
            Initialization();

            GetDataWithSqlDependency();

            Console.WriteLine("Waiting for data changes");
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();

            Termination();
        }

        private static void Initialization()
        {
            // Create a dependency connection.
            //SqlDependency.Start(_con, queueName);
            SqlDependency.Start(_con);
        }

        private static void Termination()
        {
            // Release the dependency.
            //SqlDependency.Stop(_con, queueName);
            SqlDependency.Stop(_con);
        }

        private static DataTable GetDataWithSqlDependency()
        {
            using (var connection = new SqlConnection(_con))
            {
                using (var cmd = new SqlCommand("SELECT Id FROM dbo.Gestione_Pratiche;", connection))
                {
                    var dt = new DataTable();

                    // Create dependency for this command and add event handler
                    var dependency = new SqlDependency(cmd);
                    dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);

                    // execute command to get data
                    connection.Open();
                    dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));

                    return dt;
                }
            }
        }

        // Handler method
        private static void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            Console.WriteLine($"OnChange Event fired. SqlNotificationEventArgs: Info={e.Info}, Source={e.Source}, Type={e.Type}.");

            if ((e.Info != SqlNotificationInfo.Invalid) && (e.Type != SqlNotificationType.Subscribe))
            {
                Console.WriteLine("Notification Info: " + e.Info);
                Console.WriteLine("Notification source: " + e.Source);
                Console.WriteLine("Notification type: " + e.Type);

                // resubscribe
                var dt = GetDataWithSqlDependency();

                Console.WriteLine($"Data changed. {dt.Rows.Count} rows returned.");
            }
            else
            {
                Console.WriteLine("SqlDependency not restarted");
            }
        }
    }
}
