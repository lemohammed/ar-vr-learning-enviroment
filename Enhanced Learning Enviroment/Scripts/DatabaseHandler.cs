using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseHandler : MonoBehaviour
{
    public LessonsList lessons;
    public UserInterface userInterface;

    void Start()
    {
        Debug.Log("Test");
        FetchLessons();
        userInterface.UpdateDisplay();
    }

    void createTables()
    {
        string conn = "URI=file:" + Application.dataPath + "/events.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = @"CREATE TABLE monitoredEvents(
                id INT NOT NULL AUTO_INCREMENT,
                personId INT NOT NULL,
                content VARCHAR(255) NOT NULL,
                status  VARCHAR(255) NOT NULL,
                timestamp INT NOT NULL UNIQUE,
                PRIMARY KEY (id)
                );
                "; // Add query here
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void insertEvent(string message, string status)
    {
        long unixTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        string conn = "URI=file:" + Application.dataPath + "/events.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = $@"
        INSERT INTO monitoredEvents (message, status, timestamp)
        VALUES ({message},{status},{timestamp})
        "; // Add query here
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    } 
}