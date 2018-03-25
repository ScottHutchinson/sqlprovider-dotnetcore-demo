namespace sqlprovider_dotnetstandard_library

open FSharp.Data.Sql

module Db =

    [<Literal>]
    let ConnStr = "Data Source=localhost; Initial Catalog=AdventureWorks2017; Integrated Security=True"
    type HR = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnStr>

    let GetEmployeeJobTitle conn = 
        let ctx = HR.GetDataContext conn
        query {
            for emp in ctx.HumanResources.Employee do
            select emp.JobTitle
        } |> Seq.head
