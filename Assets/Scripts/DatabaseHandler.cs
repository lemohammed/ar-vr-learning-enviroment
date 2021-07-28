// using UnityEngine;
// using Mono.Data.Sqlite;
// using System.Data;
// using System;

// public class DatabaseHandler : MonoBehaviour
// {
//     void Start()
//     {
//         Debug.Log("Test");
//     }

//     void createTables()
//     {
//         string conn = "Server=tcp:ar-vr-research-db.database.windows.net,1433;Initial Catalog=hololens-events;Persist Security Info=False;User ID=m.m2000;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
//         //Database URI
//         IDbConnection dbconn;
//         dbconn = (IDbConnection)new SqliteConnection(conn);
//         dbconn.Open(); //Open connection to the database.
//         IDbCommand dbcmd = dbconn.CreateCommand();
//         string sqlQuery = @"
//         select * from monitoredEvents
//                 "; // Add query here
//         dbcmd.CommandText = sqlQuery;
//         IDataReader reader = dbcmd.ExecuteReader();
//         reader.Close();
//         reader = null;
//         dbcmd.Dispose();
//         dbcmd = null;
//         dbconn.Close();
//         dbconn = null;
//     }
//     public void insertEvent(string message, string status, int timestamp)
//     {
//     }
// }