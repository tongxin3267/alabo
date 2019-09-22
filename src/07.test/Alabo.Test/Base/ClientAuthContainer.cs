using System.Net.Http;

namespace Alabo.Test.Base
{
    public static class ClientAuthContainer
    {
        public static HttpClientHandler CurrentHandler { get; set; }
    }
}