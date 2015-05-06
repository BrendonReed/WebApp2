

module WebApp2.Core.DataAccess
open System.Data
open Npgsql
open System

type Version = { version: string }    

let checkConnection() = seq {
    let conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=brendon;Password=;Database=traininglog;")
    conn.Open();
    let query2 = "select version()"
    use cmd = new NpgsqlCommand(query2, conn)
    cmd.CommandType <- CommandType.Text
    use reader = cmd.ExecuteReader()
    while reader.Read() do
        yield { version = unbox (reader.["version"]) } }
