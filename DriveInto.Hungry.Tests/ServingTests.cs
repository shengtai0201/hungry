using Grpc.Net.Client;
using NUnit.Framework;

namespace DriveInto.Hungry.Tests
{
    [TestFixture]
    public class ServingTests
    {
        private void EmptyDirectory(string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
            {
                EmptyDirectory(directory);

                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                    Directory.Delete(directory, false);
            }
        }

        private void DirectoryCopy(string sourcePath, string targetPath)
        {
            foreach (var directory in Directory.GetDirectories(sourcePath, "*", SearchOption.TopDirectoryOnly))
            {
                var path = directory.Replace(sourcePath, targetPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                DirectoryCopy(directory, path);
            }
        }

        private async Task<IDictionary<string, IList<string>>> GetProtoValuesAsync(params string[] paths)
        {
            var result = new Dictionary<string, IList<string>>();

            foreach (var path in paths)
            {
                foreach (var file in Directory.GetFiles(path, "*.proto", SearchOption.AllDirectories))
                {
                    var values = new List<string>();

                    var lines = await File.ReadAllLinesAsync(file);
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("import"))
                        {
                            var item = line.Split(new char[] { '\"', ';' }, StringSplitOptions.RemoveEmptyEntries).Last();
                            values.Add(item);
                        }
                    }

                    result.Add(file, values);
                }
            }

            return result;
        }

        private class Proto
        {
            public KeyValuePair<string, IList<string>> Current { get; set; }

            public ICollection<Proto> Children { get; set; } = new List<Proto>();
        }

        private void SetProtoValues(Proto proto, IDictionary<string, IList<string>> values)
        {
            foreach (var child in proto.Current.Value)
            {
                if (child.StartsWith("google/protobuf"))
                    continue;

                try
                {
                    var value = values.Single(x => x.Key.Replace("D:\\Projects\\serving\\", string.Empty).Replace("D:\\Projects\\tensorflow\\", string.Empty).Replace("\\", "/").EndsWith(child));
                    var item = new Proto { Current = value };
                    proto.Children.Add(item);

                    SetProtoValues(item, values);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void FileCopy(Proto proto, string targetPath)
        {
            var destFileName = proto.Current.Key.Replace("D:\\Projects\\serving", targetPath).Replace("D:\\Projects\\tensorflow", targetPath);
            if (!File.Exists(destFileName))
                File.Copy(proto.Current.Key, destFileName);

            foreach (var child in proto.Children)
                FileCopy(child, targetPath);
        }

        [Test]
        public async Task GetProtos()
        {
            var targetPath = "D:\\Projects\\Hungry\\DriveInto.Hungry.Serving\\Protos";
            DirectoryCopy("D:\\Projects\\serving", targetPath);
            DirectoryCopy("D:\\Projects\\tensorflow", targetPath);

            var values = await GetProtoValuesAsync("D:\\Projects\\serving", "D:\\Projects\\tensorflow");

            var modelServiceProto = new Proto { Current = values.Single(x => x.Key.EndsWith("tensorflow_serving\\apis\\model_service.proto")) };
            SetProtoValues(modelServiceProto, values);
            FileCopy(modelServiceProto, targetPath);

            var predictionServiceProto = new Proto { Current = values.Single(x => x.Key.EndsWith("tensorflow_serving\\apis\\prediction_service.proto")) };
            SetProtoValues(predictionServiceProto, values);
            FileCopy(predictionServiceProto, targetPath);

            EmptyDirectory(targetPath);
        }

        private void SetProtoList(Proto proto, IList<string> paths)
        {
            foreach(var child in proto.Children)
                SetProtoList(child, paths);

            if (!paths.Contains(proto.Current.Key))
                paths.Add(proto.Current.Key);
        }

        [Test]
        public async Task SetProtos()
        {
            var paths = new List<string>();
            var values = await GetProtoValuesAsync("D:\\Projects\\serving", "D:\\Projects\\tensorflow");

            var modelServiceProto = new Proto { Current = values.Single(x => x.Key.EndsWith("tensorflow_serving\\apis\\model_service.proto")) };
            SetProtoValues(modelServiceProto, values);
            SetProtoList(modelServiceProto, paths);

            var predictionServiceProto = new Proto { Current = values.Single(x => x.Key.EndsWith("tensorflow_serving\\apis\\prediction_service.proto")) };
            SetProtoValues(predictionServiceProto, values);
            SetProtoList(predictionServiceProto, paths);

            var format = "<Protobuf Include=\"{0}\" GrpcServices=\"Client\" />";
            foreach (var path in paths)
            {
                var value = string.Format(format, path.Replace("D:\\Projects\\serving\\", string.Empty).Replace("D:\\Projects\\tensorflow\\", string.Empty));
                Console.WriteLine(value);
            }
        }

        [Test]
        public void GetModelStatus()
        {
            using var channel = GrpcChannel.ForAddress("127.0.0.1");
            
        }
    }
}
