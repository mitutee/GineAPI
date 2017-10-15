namespace VkDotNet.Core
open VkDotNet.Core.QueryBuilders

module Vk = 
    let Messages = MessagesGetLongPollServerQuery { BlankQuery with MethodModule = "messages"; MethodName = "getLongPollServer"}