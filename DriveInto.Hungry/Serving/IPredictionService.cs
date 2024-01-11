using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveInto.Hungry.Serving
{
    public interface IPredictionService
    {
        Task ClassifyAsync();

        Task RegressAsync();

        Task PredictAsync();

        Task GetModelMetadataAsync(string modelName);
    }
}
