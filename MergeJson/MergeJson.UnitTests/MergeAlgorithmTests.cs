using MergeJson.Algorithm;
using NUnit.Framework;

namespace MergeJson.UnitTests
{
    public class MergeAlgorithmTests
    {
        [Test]
        public void MergeJson_WithDiffConditions_ResultIsCorrect()
        {
            // Arrange
            var json =
                "{\"ranked\":[{\"priority\":2,\"vals\":{\"timeout\":\"3s\",\"num_threads\":500,\"buffer_size\":4000,\"use_sleep\":true}}," +
                "{\"priority\":1,\"vals\":{\"timeout\":\"2s\",\"startup_delay\":\"2m\",\"skip_percent_active\":0.2}}," +
                "{\"priority\":0,\"vals\":{\"num_threads\":300,\"buffer_size\":3000,\"label\":\"testing\"}}]}";
            var sut = new MergeAlgorithm();

            // Act
            var result = sut.Merge(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Exactly(6).Items, "Result of MergeJSON function is not as expected");
                Assert.That(result, Does.ContainKey("timeout").WithValue("2s"), "Result of MergeJSON function is not as expected");
                Assert.That(result, Does.ContainKey("num_threads").WithValue(300), "Result of MergeJSON function is not as expected");
                Assert.That(result, Does.ContainKey("buffer_size").WithValue(3000), "Result of MergeJSON function is not as expected");
                Assert.That(result, Does.ContainKey("use_sleep").WithValue(true), "Result of MergeJSON function is not as expected");
                Assert.That(result, Does.ContainKey("startup_delay").WithValue("2m"), "Result of MergeJSON function is not as expected");
                Assert.That(result, Does.ContainKey("label").WithValue("testing"), "Result of MergeJSON function is not as expected");
            });
        }
    }
}