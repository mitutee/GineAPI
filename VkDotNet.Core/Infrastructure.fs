namespace VkDotNet.Core

open System.Collections.Generic
open System.Runtime.CompilerServices

[<AutoOpen>]
module Infrastructure =
    let toIntFlag (b: bool) = if b then 1 else 0
    let BlankDictionary = new Dictionary<string, string>()
    let AddValue (dict: IDictionary<string, string>) key value =
        dict.Add(key, value)
        dict

    let BlankQuery = { MethodModule = ""; MethodName = ""; Params = BlankDictionary; ApiVersion = 5.68 }

    let AddParam q key value = { q with Params = AddValue q.Params key value }