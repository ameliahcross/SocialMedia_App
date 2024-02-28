using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SocialMedia_App.Core.Application.Helpers
{
    public static class SessionHelper
    {
        // guarda la información en la sesión
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // obtener lo que está guardado en el "key" de la sesión
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}

