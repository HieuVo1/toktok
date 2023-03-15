using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace toktok_api.DTOs.Commons.Responses
{
    public class APIResponse<T>
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public object Metadata { get; set; }
        public override string ToString()
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            return JsonSerializer.Serialize(this, jsonSerializerOptions);
        }
    }
}