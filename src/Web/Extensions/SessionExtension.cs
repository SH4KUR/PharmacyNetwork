using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PharmacyNetwork.Web.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value) =>
            session.SetString(key, JsonSerializer.Serialize(value));

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
