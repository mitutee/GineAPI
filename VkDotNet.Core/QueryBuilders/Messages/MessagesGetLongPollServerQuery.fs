namespace VkDotNet.Core.QueryBuilders

type MessagesGetLongPollServerQuery(q) =
    inherit QueryBuilderBase(q)
type GetLPQuery = MessagesGetLongPollServerQuery
type EnumE = | Banana | Beacon | Beef
open System.Collections.Generic
open VkDotNet.Core
open System.Runtime.CompilerServices
[<Extension>]
module GetLPQueryExtensions =
    [<Extension>]
    let Build (b: QueryBuilderBase) = b.Query
    [<Extension>]
    let WithAccessToken(b: QueryBuilderBase) token =
        MessagesGetLongPollServerQuery (AddParam b.Query "access_token" token )
    [<Extension>]
    let WithNeedPts(b: GetLPQuery) (needPts: bool) = 
        GetLPQuery (AddParam b.Query "need_pts" ((toIntFlag >> string) needPts))       
    [<Extension>]
    let WithLpVersion(b: GetLPQuery) (lpVersion:int) = 
        GetLPQuery (AddParam b.Query "lp_version" (string lpVersion) )
    [<Extension>]
    let Execute(_: GetLPQuery) = "Hellos"  