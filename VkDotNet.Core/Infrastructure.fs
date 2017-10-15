namespace VkDotNet.Core

open System.Collections.Generic
open System.Runtime.CompilerServices

[<AutoOpen>]
module Infrastructure =
    let BlankDictionary = new Dictionary<string, string>()
    let AddValue key value (dict: IDictionary<string, string>)  =
        dict.Add(key, value)
        dict
