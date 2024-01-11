using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveInto.Hungry.Serving.Services
{
    internal class PredictionService : IPredictionService
    {
        public Task ClassifyAsync()
        {
            throw new NotImplementedException();
        }

        public async Task GetModelMetadataAsync(string modelName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7042");
            var client = new Tensorflow.Serving.PredictionService.PredictionServiceClient(channel);
            var reply = await client.GetModelMetadataAsync(new Tensorflow.Serving.GetModelMetadataRequest
            {
                ModelSpec = new Tensorflow.Serving.ModelSpec
                {
                    Name = modelName
                }
            });

            foreach (var data in reply.Metadata)
            {
                // todo                
            }
        }

        public Task PredictAsync()
        {
            throw new NotImplementedException();
        }

        public Task RegressAsync()
        {
            throw new NotImplementedException();
        }
    }
}
