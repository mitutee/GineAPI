namespace VkDotNet.Core
open System.Collections.Generic

type ApiQuery = { MethodModule: string; MethodName: string; Params: Dictionary<string, string>; ApiVersion: float }