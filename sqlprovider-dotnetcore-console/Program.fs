// Learn more about F# at http://fsharp.org

open System
open System.IO
open Microsoft.Extensions.Configuration
open sqlprovider_dotnetstandard_library

let builder = 
    let ret = new ConfigurationBuilder()
    FileConfigurationExtensions.SetBasePath(ret, Directory.GetCurrentDirectory()) |> ignore
    JsonConfigurationExtensions.AddJsonFile(ret, "appSettings.json")

let config = builder.Build ()

[<EntryPoint>]
let main argv =
    let runtimeConnectionString = config.Item("App:Connection:Value")
    let employeeJobTitle = Db.GetEmployeeJobTitle runtimeConnectionString
    printfn "Hello %s!" employeeJobTitle
    System.Threading.Thread.Sleep 2000
    0 // return an integer exit code
