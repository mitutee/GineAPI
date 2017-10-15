namespace VkDotNet.Core.QueryBuilders
open System.Collections.Generic
open VkDotNet.Core

type QueryBuilderBase(query) =
     member internal this.Query: ApiQuery = query