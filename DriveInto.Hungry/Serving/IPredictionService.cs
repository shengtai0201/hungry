using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveInto.Hungry.Serving
{
    public interface IPredictionService
    {
        Task ClassifyAsync(string modelName);

        Task RegressAsync(string modelName);

        Task PredictAsync(string modelName);

        Task GetModelMetadataAsync(string modelName);
    }
}
