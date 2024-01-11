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
        public async Task ClassifyAsync(string modelName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7042");
            var client = new Tensorflow.Serving.PredictionService.PredictionServiceClient(channel);
            var reply = await client.ClassifyAsync(new Tensorflow.Serving.ClassificationRequest
            {
                ModelSpec = new Tensorflow.Serving.ModelSpec
                {
                    Name = modelName
                }
            });

            // todo
            var result = reply.Result;
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

        public async Task PredictAsync(string modelName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7042");
            var client = new Tensorflow.Serving.PredictionService.PredictionServiceClient(channel);
            var reply = await client.PredictAsync(new Tensorflow.Serving.PredictRequest
            {
                ModelSpec = new Tensorflow.Serving.ModelSpec
                {
                    Name = modelName
                }
            });

            foreach (var data in reply.Outputs)
            {
                // todo                
            }
        }

        public async Task RegressAsync(string modelName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7042");
            var client = new Tensorflow.Serving.PredictionService.PredictionServiceClient(channel);
            var reply = await client.RegressAsync(new Tensorflow.Serving.RegressionRequest
            {
                ModelSpec = new Tensorflow.Serving.ModelSpec
                {
                    Name = modelName
                }
            });

            // todo
            var result = reply.Result;
        }
    }
}
