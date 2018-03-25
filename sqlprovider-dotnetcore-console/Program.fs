// Learn more about F# at http://fsharp.org

open System
open System.IO
open Microsoft.Extensions.Configuration
open FSharp.Data.Sql

let builder = 
    let ret = new ConfigurationBuilder()
    FileConfigurationExtensions.SetBasePath(ret, Directory.GetCurrentDirectory()) |> ignore
    JsonConfigurationExtensions.AddJsonFile(ret, "appSettings.json")

let config = builder.Build ()
[<Literal>]
let ConnStr = "Data Source=localhost; Initial Catalog=AdventureWorks2017; Integrated Security=True"
type HR = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnStr>

[<EntryPoint>]
let main argv =
    let runtimeConnectionString = config.Item("App:Connection:Value")
    let ctx = HR.GetDataContext runtimeConnectionString
    let employeeJobTitle = 
        query {
            for emp in ctx.HumanResources.Employee do
            select emp.JobTitle
        } |> Seq.head

    printfn "Hello %s!" employeeJobTitle
    System.Threading.Thread.Sleep 2000
    0 // return an integer exit code
