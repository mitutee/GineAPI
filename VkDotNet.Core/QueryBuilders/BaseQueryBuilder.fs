namespace VkDotNet.Core.QueryBuilders
open System.Collections.Generic
open VkDotNet.Core
open System.Runtime.CompilerServices

type AbstractQueryBuilder(q) = 
    member internal this.Query: ApiQuery = q
   
[<AutoOpen>]
module Builder = 
    let AddParam q key value = { q with Params = AddValue q.Params key value }

[<Extension>]
module BaseQueryBuilder = 
    [<Extension>]
    let WithAccessToken q token = AddParam q "access_token" token