﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using vk_dotnet.Objects;

namespace vk_dotnet.Methods
{
    public class Photos_Methods : Method
    {
        public Photos_Methods(string token) : base(token)
        {}

        public List<Photo> GetAll(string OwnerId = "0", int offset = 0, int count = 20)
        {
            string request = GetMethodUri("photos.getAll",
                $"owner_id={OwnerId}",
                $"offset={offset}",
                $"count={count}",
                $"photo_sizes=1",
                $"access_token={_token}"
            );
            var result =  CallApiAsync(request).Result;
            return JsonConvert.DeserializeObject<List<Photo>>(JToken.Parse(result)["items"].ToString());
        }
    }
}
