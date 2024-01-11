using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveInto.Hungry.Serving.Services
{
    internal class ModelService : IModelService
    {
        public async Task GetModelStatusAsync(string modelName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7042");
            var client = new Tensorflow.Serving.ModelService.ModelServiceClient(channel);
            var reply = await client.GetModelStatusAsync(new Tensorflow.Serving.GetModelStatusRequest
            {
                ModelSpec = new Tensorflow.Serving.ModelSpec
                {
                    Name = modelName
                }
            });

            foreach(var status in reply.ModelVersionStatus)
            {
                // todo
            }
        }
    }
}
