namespace VkDotNet.Core
open System.Collections.Generic

type ApiQuery = { MethodModule: string; MethodName: string; Params: IDictionary<string, string>; ApiVersion: float }