namespace VkDotNet.Core.QueryBuilders

type WallGetQuery(q) =
    inherit QueryBuilderBase(q)
    
open System.Collections.Generic
open VkDotNet.Core
open System.Runtime.CompilerServices
[<Extension>]
type WallGetQueryExtensions =
    [<Extension>]
    static member Build (b: QueryBuilderBase) = b.Query
    [<Extension>]
    static member WithAccessToken(b: WallGetQuery) token =
        WallGetQuery (AddParam b.Query "access_token" token )
    [<Extension>]
    static member WithOwnerId(b: WallGetQuery) ownerId = 
        WallGetQuery (AddParam b.Query "owner_id" ownerId )        
    [<Extension>]
    static member WithDomain(b: WallGetQuery) domain = 
        WallGetQuery (AddParam b.Query "domain" domain )        
    [<Extension>]
    static member WithCount(b: WallGetQuery) (count: int) = 
        WallGetQuery (AddParam b.Query "count" (string count))        
    [<Extension>]
    static member WithFields(b: WallGetQuery) ownerId = 
        WallGetQuery (AddParam b.Query "fields" ownerId )
        