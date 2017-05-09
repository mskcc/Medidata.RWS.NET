using System;
using System.Collections.Generic;

namespace Medidata.RWS.Tests
{
    /// <summary>
    /// Static class for .env file operations
    /// </summary>
    public static class Env
    {

        /// <summary>
        /// Return the environment value for the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="_default"></param>
        /// <returns></returns>
        public static string Get(string key, string _default = null)
        {
            Dictionary<string, string> variables;
            try
            {
                variables = DotEnvFile.DotEnvFile.LoadFile(
                    $"{AppDomain.CurrentDomain.BaseDirectory}\\.env");

            }
            catch (Exception e)
            {
                throw new Exception(".env file not found.", innerException: e);
            }

            string value;
            if (!variables.TryGetValue(key, out value))
            {
                return (_default);
            }

            if (value.Length > 1 && value.StartsWith("\"") && value.EndsWith("\""))
            {
                return value.Substring(1, -1);
            }

            return value;

        }
    }
}
