namespace VkDotNet.Core.QueryBuilders

open System.Runtime.CompilerServices
open VkDotNet.Core
open FSharp.Data
open System.Collections.Generic
type GetLongPollServerJson = JsonProvider<""" { "response": { "key": "80d9de42848e8298c5934ecefe3d71a2018ed132", "server": "imv4.vk.com/im2852", "ts": 1757519170 } }""">
type LongPollJson = JsonProvider<""" { "ts": 1820350874, "updates": [ [4, 1619489, 561, 123456, 1464958914, "hello", { "attach1_type": "photo", "attach1": "123456_414233177", "attach2_type": "audio", "attach2": "123456_456239018", "title": " ... " }] ] }""">


type GetLongPollServerResponse = { Key: string; Server: string; Ts: string }

type Message = { AuthorId: string; Body: string }
type LongPollResponse = {Ts: string; MessageUpdates: array<Message>}

[<Extension>]
module LongPollResponse =
    [<Extension>]
    let Execute (b: MessagesGetLongPollServerQuery) = 
        let response = (ExecuteQuery b.Query) |> GetLongPollServerJson.Parse
        { Key = response.Response.Key; Server = response.Response.Server; Ts = string response.Response.Ts }

    let GetValue (d: Dictionary<string, string>) key =
        match d.TryGetValue key with   
        | (true, v) -> v
        | (false, _) -> ""


    let GetLongPollUri (q: ApiQuery) =
        sprintf "https://%s?act=a_check&key=%s&ts=%s&wait=25&mode=2&version=2" (GetValue q.Params "server") (GetValue q.Params "key") (GetValue q.Params "ts")


[<Extension>]
module LongPoll =
    [<Extension>]
    let Execute (b: LongPollServerQuery) =
        let uri = LongPollResponse.GetLongPollUri b.Query
        LongPollJson.Load(uri)


           

