namespace VkDotNet.Core.QueryBuilders

type MessagesGetLongPollServerQuery(q) =
    inherit QueryBuilderBase(q)
type GetLPQuery = MessagesGetLongPollServerQuery
    
open System.Collections.Generic
open VkDotNet.Core
open System.Runtime.CompilerServices
[<Extension>]
type GetLPQueryExtensions =
    [<Extension>]
    static member Build (b: QueryBuilderBase) = b.Query
    [<Extension>]
    static member WithAccessToken(b: QueryBuilderBase) token =
        MessagesGetLongPollServerQuery (AddParam b.Query "access_token" token )
    [<Extension>]
    static member WithNeedPts(b: GetLPQuery) (needPts: bool) = 
        GetLPQuery (AddParam b.Query "need_pts" ((toIntFlag >> string) needPts))       
    [<Extension>]
    static member WithLpVersion(b: GetLPQuery) (lpVersion:int) = 
        GetLPQuery (AddParam b.Query "lp_version" (string lpVersion) )        
        