using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace App
{
    internal static class Program
    {
        private static void Main() => File.WriteAllLines(
            "certs.csv",
            Get().Select(x => $"{x.location}; {x.name}; {x.cert}")
        );

        private static IEnumerable<(StoreLocation location, StoreName name, string cert)> Get()
        {
            foreach (var location in Enum.GetValues(typeof(StoreLocation)).Cast<StoreLocation>())
            foreach (var name in Enum.GetValues(typeof(StoreName)).Cast<StoreName>())
            {
                var store = new X509Store(name, location);
                store.Open(OpenFlags.ReadOnly);

                foreach (var certificate in store.Certificates)
                {
                    yield return (location, name, certificate.Thumbprint);
                }

                store.Close();
            }
        }
    }
}
