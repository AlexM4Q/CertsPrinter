using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace App
{
    internal static class Program
    {
        private static void Main()
        {
            foreach (var value in Enum.GetValues(typeof(StoreName)).Cast<StoreName>())
            {
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                foreach (var certificate in store.Certificates)
                {
                    Console.WriteLine($"{value} {certificate.Thumbprint}");
                }

                store.Close();
            }
        }
    }
}
