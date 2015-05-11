

namespace WebApp2.Core.Controllers

open System
open System.Web.Mvc
open System.Reflection
open WebApp2.Core.DataAccess

[<HandleError>]
type HomeController() =
  inherit Controller()

  member x.Index() =
    //let mvcName = typeof(Controller).Assembly.GetName ()
    let h = typedefof<Controller>
    let mvcName = h.Assembly.GetName ()
    let isMono = Type.GetType ("Mono.Runtime") <> null
    let version = typeof<int * int>.Assembly.GetName().Version
    let msg = sprintf "This web page is running using F# version %s." (version.ToString(4))
    x.ViewData.["Message"] <- msg   
    x.ViewData.["Version"] <- sprintf "%A.%A" mvcName.Version.Major mvcName.Version.Minor
    x.ViewData.["Runtime"] <- match isMono with 
                                | true -> "Mono"
                                | false -> ".NET"
    let dbVersion = checkConnection()
    let first = Seq.head dbVersion
    x.ViewData.["DbVersion"] <-  first.version

    x.View()

  member x.About() =
    x.View() 


