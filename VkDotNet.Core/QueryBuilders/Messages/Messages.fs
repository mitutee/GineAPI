namespace VkDotNet.Core.QueryBuilders

open System.Collections.Generic
open VkDotNet.Core
open System.Runtime.CompilerServices

type MessagesQuery(q) = inherit AbstractQueryBuilder(q)

type MessagesGetLongPollServerQuery(q) = inherit MessagesQuery(q)

type LongPollServerQuery(q) = inherit AbstractQueryBuilder(q)

type MessagesSendQuery(q) = inherit AbstractQueryBuilder(q)

[<Extension>]
module Messages =
    let SetMethodName q name = { q with MethodName = name }
    [<Extension>]
    let GetLongPollServer (b: MessagesQuery) =
        MessagesGetLongPollServerQuery (SetMethodName b.Query "getLongPollServer")
    [<Extension>]
    let Send (b: MessagesQuery) = 
        MessagesSendQuery (SetMethodName b.Query "send")

[<Extension>]
module LongPollServer =
    [<Extension>]
    let WithKey (b: LongPollServerQuery) key =
        LongPollServerQuery ( AddParam b.Query "key" key)
    [<Extension>]
    let WhithServer (b: LongPollServerQuery) server =
        LongPollServerQuery ( AddParam b.Query "server" server)
    [<Extension>]
    let WithTs (b: LongPollServerQuery) ts =
        LongPollServerQuery ( AddParam b.Query "ts" ts)    
    [<Extension>]
    let WithWait (b: LongPollServerQuery) (wait: int) =
        LongPollServerQuery ( AddParam b.Query "wait" (string wait))

       

[<Extension>]
module MessagesSend =
    [<Extension>]
    let WithUserId (b: MessagesSendQuery) userId = MessagesSendQuery ( AddParam b.Query "user_id" userId)
    [<Extension>]
    let WithMessage (b: MessagesSendQuery) message = MessagesSendQuery ( AddParam b.Query "message" message)
    [<Extension>]
    let Execute (b: MessagesSendQuery) = ExecuteQuery b.Query
    [<Extension>]
    let WithAccessToken (b: MessagesSendQuery) token = MessagesSendQuery ( AddParam b.Query "access_token" token)

  