namespace VkDotNet.Core.QueryBuilders

open System.Runtime.CompilerServices
open VkDotNet.Core
open FSharp.Data
open FSharp.Data.JsonExtensions
open System.Collections.Generic
type GetLongPollServerJson = JsonProvider<""" { "response": { "key": "80d9de42848e8298c5934ecefe3d71a2018ed132", "server": "imv4.vk.com/im2852", "ts": 1757519170 } }""">
type LongPollJson = JsonProvider<""" { "ts": 1820350874, "updates": [4,2105994,561,123456,1496404246,"hello",{"attach1_type":"photo","attach1":"123456_417336473","attach2_type":"audio","attach2":"123456_456239018","title":" ... "}] }""">

type LongPollEvent = JsonProvider<""" [4,2105994,561,123456,1496404246,"hello",{"attach1_type":"photo","attach1":"123456_417336473","attach2_type":"audio","attach2":"123456_456239018","title":" ... "}]  """>


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
                
    let tryParse i = System.Int32.TryParse i
    type MaybeBuilder() =
        member this.Bind(m, f) = Option.bind f m
        member this.Return(x) = Some x

    
    let maybe = new MaybeBuilder()

    let isIncoming i = i &&& 2 = 0
    let isNew i = i = 4


    let mapLongPollEvent (event:JsonValue) : Option<Message> = 
        let array = event.AsArray()
        
        
        match array.[0].AsInteger() with 
        | i when isNew i && (isIncoming (array.[2].AsInteger())) -> Some {AuthorId = array.[3].AsString(); Body = array.[5].AsString() }
        | _ -> None

                
    [<Extension>]
    let Execute (b: LongPollServerQuery) =
        let uri = LongPollResponse.GetLongPollUri b.Query
        let answer = LongPollJson.Load(uri)
        let json = answer.Updates.JsonValue
        let messageUpdates = json.AsArray() |> Array.choose mapLongPollEvent

        {Ts = string answer.Ts; MessageUpdates = messageUpdates}


           

