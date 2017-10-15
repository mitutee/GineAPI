namespace VkDotNet.Core

open System.Collections.Generic
type AbstractQuery = { MethodModule: string; MethodName: string; Params: IDictionary<string, string>; ApiVersion: float }

module I =
    let BlankQuery = { MethodModule = ""; MethodName = ""; Params = BlankDictionary; ApiVersion = 5.68 }

    let AddParam q key value = {q with Params = AddValue key value q.Params }
open I
type QueryBuilderBase(query) =
     member this.query: AbstractQuery = query

type WallQueryBuilder(q) =
    inherit QueryBuilderBase(q)


open System.Runtime.CompilerServices
[<Extension>]
type QueryBuilderExtensions =
    [<Extension>]
    static member Build (b: QueryBuilderBase) = b.query
    [<Extension>]
    static member WithAccessToken(b: WallQueryBuilder) token =
        WallQueryBuilder (AddParam b.query "access_token" token )
    [<Extension>]
    static member WithOwnerId(b: WallQueryBuilder) ownerId = 
        WallQueryBuilder (AddParam b.query "owner_id" ownerId )        
    [<Extension>]
    static member WithDomain(b: WallQueryBuilder) domain = 
        WallQueryBuilder (AddParam b.query "domain" domain )        
    [<Extension>]
    static member WithCount(b: WallQueryBuilder) (count: int) = 
        WallQueryBuilder (AddParam b.query "count" (string count))        
    [<Extension>]
    static member WithFields(b: WallQueryBuilder) ownerId = 
        WallQueryBuilder (AddParam b.query "fields" ownerId )
        