namespace VkDotNet.Core
open VkDotNet.Core.QueryBuilders

module Vk = 
    let Messages = MessagesQuery { BlankQuery with MethodModule = "messages" }
    let LongPoll = LongPollServerQuery BlankQuery