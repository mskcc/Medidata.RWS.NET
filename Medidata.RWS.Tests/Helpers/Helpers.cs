using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Medidata.RWS.Tests.Helpers
{
    public static class Helpers
    {

        public static string env(string key, string _default = null)
        {

            Dictionary<string, string> variables =  DotEnvFile.DotEnvFile.LoadFile("/.env");

            return "";

            /*

            $value = getenv($key);

            if ($value === false) {
                return value($default);
            }

            switch (strtolower($value))
            {
                case 'true':
                case '(true)':
                    return true;
                case 'false':
                case '(false)':
                    return false;
                case 'empty':
                case '(empty)':
                    return '';
                case 'null':
                case '(null)':
                    return;
            }

            if (strlen($value) > 1 && Str::startsWith($value, '"') && Str::endsWith($value, '"'))
            {
                return substr($value, 1, -1);
            }

            return $value;
            */
        }
    }
}
