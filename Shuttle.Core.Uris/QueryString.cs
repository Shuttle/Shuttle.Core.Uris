using System;
using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Uris
{
    public class QueryString : Dictionary<string, string>
    {
        public QueryString()
        {
        }

        public QueryString(Uri uri) : this(uri, true)
        {
        }

        public QueryString(Uri uri, bool escape)
        {
            Guard.AgainstNull(uri, nameof(uri));

            var value = (uri.Query??string.Empty).Replace("?", "");

            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            var result = value.Split('&').Select(q => q.Split('='))
                .ToDictionary(k => k[0], v => v[1]);

            foreach (var pair in result)
            {
                if (escape)
                {
                    Add(pair.Key, pair.Value);
                }
                else
                {
                    AddUnescaped(pair.Key, pair.Value);
                }
            }
        }

        public new void Add(string key, string value)
        {
            AddUnescaped(Uri.UnescapeDataString(key), Uri.UnescapeDataString(value));
        }

        public void AddUnescaped(string key, string value)
        {
            base.Add(key, value);
        }
    }
}