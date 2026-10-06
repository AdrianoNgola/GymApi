using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using School.API.Models;

namespace School.API.Extantions
{
    public static class HttpExtansions
    {
        public static void AddPaginationHeader(this HttpResponse response, PagenationHeader Header)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", System.Text.Json.JsonSerializer.Serialize(Header, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
        
    }
}