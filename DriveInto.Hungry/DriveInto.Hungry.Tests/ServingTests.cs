using Grpc.Net.Client;
using NUnit.Framework;

namespace DriveInto.Hungry.Tests
{
    [TestFixture]
    public class ServingTests
    {
        [Test]
        public async Task Test()
        {

        }

        [Test]
        public void GetModelStatus()
        {
            using var channel = GrpcChannel.ForAddress("127.0.0.1");
            //var client = new GrpcModelServiceClient.
        }
    }
}
