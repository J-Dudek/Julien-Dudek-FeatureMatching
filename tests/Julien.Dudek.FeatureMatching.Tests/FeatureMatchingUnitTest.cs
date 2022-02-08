using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Julien.Dudek.FeatureMatching.Tests;

public class FeatureMatchingUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in
                 Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        var objectImageData = await File.ReadAllBytesAsync(Path.Combine(executingPath, "DUDEK-JULIEN-object.jpg"));
        var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData);
        Assert.Equal("[{\"X\":1929,\"Y\":2114},{\"X\":421,\"Y\":2482},{\"X\":692,\"Y\":3759},{\"X\":2266,\"Y\":3279}]",JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));
        Assert.Equal("[{\"X\":2275,\"Y\":1662},{\"X\":1165,\"Y\":1185},{\"X\":807,\"Y\":2033},{\"X\":1916,\"Y\":2484}]",JsonSerializer.Serialize(detectObjectInScenesResults[1].Points));
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}