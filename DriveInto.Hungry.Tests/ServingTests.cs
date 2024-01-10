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
            foreach (var file in Directory.GetFiles(sourcePath, "*.proto", SearchOption.TopDirectoryOnly))
            {
                var path = file.Replace(sourcePath, targetPath);
                File.Copy(file, path, true);
            }

            foreach (var directory in Directory.GetDirectories(sourcePath, "*", SearchOption.TopDirectoryOnly))
            {
                var path = directory.Replace(sourcePath, targetPath);
                Directory.CreateDirectory(path);

                DirectoryCopy(directory, path);
            }
        }

        [Test]
        public void GetProtos()
        {
            var targetPath = "D:\\Projects\\Hungry\\DriveInto.Hungry.Serving\\Protos";

            foreach (var sourcePath in new string[] { "D:\\Projects\\serving", "D:\\Projects\\tensorflow" })
                DirectoryCopy(sourcePath, targetPath);

            EmptyDirectory(targetPath);
        }

        [Test]
        public void SetProtos()
        {
            var targetPath = "D:\\Projects\\Hungry\\DriveInto.Hungry.Serving\\Protos";
            var format = "<!--<Protobuf Include=\"{0}\" GrpcServices=\"Client\" />-->";

            var files = Directory.GetFiles(targetPath, "*.proto", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var value = string.Format(format, file.Replace("D:\\Projects\\Hungry\\DriveInto.Hungry.Serving\\", string.Empty));
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
