namespace VkDotNet.Core.QueryBuilder

open System.Collections.Generic
open System.Runtime.CompilerServices


module Infrastructure = 
    let BlankDictionary = new Dictionary<string, string>()
    let AddValue key value (dict: IDictionary<string, string>)  =
        dict.Add(key, value)
        dict




open Infrastructure
type AbstractQuery = { MethodModule: string; MethodName: string; QueryParams: IDictionary<string, string>; ApiVersion: float }


[<Extension>]
type QueryBuilderInfrastructure = 
    static member BlankQuery: AbstractQuery = { MethodModule = ""; MethodName = ""; QueryParams = BlankDictionary; ApiVersion = 5.68 }
    [<Extension>]
    static member WithMethodModule(q: AbstractQuery) methodModule = { q with MethodModule = methodModule }
    [<Extension>]
    static member WithMethodName(q: AbstractQuery) methodModule = { q with MethodModule = methodModule }
    [<Extension>]
    static member WithParameter(q: AbstractQuery) methodModule = { q with MethodModule = methodModule }

type Query =
    | Account of AbstractQuery
    | Database of AbstractQuery
    | Docs of AbstractQuery
    | Fave of AbstractQuery
    | Friends of AbstractQuery
    | Groups of AbstractQuery
    | Likes of AbstractQuery
    | Photos of AbstractQuery
    | WallQuery of AbstractQuery
    | MessageQuery of AbstractQuery
    | AccountQuery of AbstractQuery



    
