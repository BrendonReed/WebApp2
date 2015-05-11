

module WebApp2.Core.DataAccess
open System.Data
open Npgsql
open System
open System.Configuration

type Version = { version: string }    

let checkConnection() = seq {
    let connString = ConfigurationManager.AppSettings.Item("dbConn")
    let conn = new NpgsqlConnection(connString)
    conn.Open();
    let query2 = "select version()"
    use cmd = new NpgsqlCommand(query2, conn)
    cmd.CommandType <- CommandType.Text
    use reader = cmd.ExecuteReader()
    while reader.Read() do
        yield { version = unbox (reader.["version"]) } }
