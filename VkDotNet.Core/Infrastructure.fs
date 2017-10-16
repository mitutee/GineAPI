namespace VkDotNet.Core

open System.Collections.Generic
open System.Runtime.CompilerServices

[<AutoOpen>]
module Infrastructure =
    let toIntFlag (b: bool) = if b then 1 else 0
    let BlankDictionary() = new Dictionary<string, string>()
    let AddValue (dict: IDictionary<string, string>) key value =
        let newDict = Dictionary<string, string> dict
        newDict.Add(key, value)
        newDict

    
    let toQueryString (dict: IDictionary<string, string>) =
        let getValue (kvp: KeyValuePair<string, string>) = kvp.Key + "=" + kvp.Value
        let queries = dict |> Seq.toArray |> Array.map getValue
        "?" + String.concat "&" queries

    let BaseUri = "https://api.vk.com/method/";
    let GetQueryUrl q = BaseUri + q.MethodModule + "." + q.MethodName + (toQueryString q.Params) + "&v=" + (string q.ApiVersion)

    let BlankQuery = { MethodModule = ""; MethodName = ""; Params = BlankDictionary(); ApiVersion = 5.68 }

    open System.Net
    open System
    open System.IO
    let FetchUrl url = 
        let req = WebRequest.Create(Uri(url))
        use resp = req.GetResponse()
        use stream = resp.GetResponseStream()
        use reader = new IO.StreamReader(stream)
        let text = reader.ReadToEnd()
        text


    let ExecuteQuery (q: ApiQuery) =
        let uri = GetQueryUrl q
        FetchUrl uri
