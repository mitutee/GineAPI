namespace VkDotNet.Core.QueryBuilders

type WallGetQuery(q) =
    inherit QueryBuilderBase(q)
    
open System.Collections.Generic
open VkDotNet.Core
open System.Runtime.CompilerServices
[<Extension>]
module WallGetQueryExtensions =
    [<Extension>]
    let WithAccessToken(b: WallGetQuery) token =
        WallGetQuery (AddParam b.Query "access_token" token )
    [<Extension>]
    let WithOwnerId(b: WallGetQuery) ownerId = 
        WallGetQuery (AddParam b.Query "owner_id" ownerId )        
    [<Extension>]
    let WithDomain(b: WallGetQuery) domain = 
        WallGetQuery (AddParam b.Query "domain" domain )        
    [<Extension>]
    let WithCount(b: WallGetQuery) (count: int) = 
        WallGetQuery (AddParam b.Query "count" (string count))        
    [<Extension>]
    let WithFields(b: WallGetQuery) ownerId = 
        WallGetQuery (AddParam b.Query "fields" ownerId )
        