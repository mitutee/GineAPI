namespace VkDotNet.Core.QueryBuilder

open System.Collections.Generic
open System.Runtime.CompilerServices


module Infrastructure = 
    let BlankDictionary = new Dictionary<string, string>()
    let AddValue key value (dict: IDictionary<string, string>)  =
        dict.Add(key, value)
        dict

module MyModule =
    type FooRecord = { Id: string; Name: string }
    type MessageFooRecord = FooRecord

open Infrastructure
type AbstractQuery = { MethodModule: string; MethodName: string; QueryParams: IDictionary<string, string>; ApiVersion: float }

type WallQuery = { Query: AbstractQuery }
type MessageQuery = { Query: AbstractQuery }
type AccountQuery = { Query: AbstractQuery }
type GroupQuery = { Query: AbstractQuery }


[<AutoOpen>]
module QueryBuilderInfrastructure = 
    let BlankQuery: AbstractQuery = { MethodModule = ""; MethodName = ""; QueryParams = BlankDictionary; ApiVersion = 5.68}

[<Extension>]        
type WallQueryBuilder =

    static member Wall: WallQuery = { Query = {BlankQuery with MethodModule = "wall"} }
    [<Extension>] 
    static member Get(q: WallQuery): WallQuery = { Query = { q.Query with MethodName = "get" } }
    [<Extension>]
    static member OwnerId(q: WallQuery) (ownerId): WallQuery = WallQueryBuilder.AddParameter q "ownerId" ownerId
    [<Extension>]
    static member Token(q: WallQuery) (token): WallQuery = WallQueryBuilder.AddParameter q "access_token" token
    [<Extension>]
    static member AddParameter(q: WallQuery) key value: WallQuery = { Query = { q.Query with QueryParams = q.Query.QueryParams |> AddValue key value } }


    
