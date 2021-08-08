using MergeJson.Algorithm;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MergeJson.UnitTests
{
    public class MergeAlgorithmTests
    {
        private MergeAlgorithm _sut;

        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<MergeAlgorithm>>();
            _sut = new MergeAlgorithm(loggerMock.Object);
        }

        [Test]
        public void MergeJson_WithDiffConditions_ResultIsCorrect()
        {
            // Arrange
            var json =
                "{\"ranked\":[{\"priority\":2,\"vals\":{\"timeout\":\"3s\",\"num_threads\":500,\"buffer_size\":4000,\"use_sleep\":true}}," +
                "{\"priority\":1,\"vals\":{\"timeout\":\"2s\",\"startup_delay\":\"2m\",\"skip_percent_active\":0.2}}," +
                "{\"priority\":0,\"vals\":{\"num_threads\":300,\"buffer_size\":3000,\"label\":\"testing\"}}]}";
            
            // Act
            var result = _sut.Merge(json);

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

        [Test]
        public void MergeJson_ValsByPriority_LowerPriorityIsReceived()
        {
            // Arrange
            var json =
                "{\"ranked\":[" +
                "{\"priority\":1,\"vals\":{\"test\":1}}," +
                "{\"priority\":0,\"vals\":{\"test\":2}}" +
                "]}";
            // Act
            var result = _sut.Merge(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Exactly(1).Items);
                Assert.That(result, Does.ContainKey("test").WithValue(2));
            });
        }

        [Test]
        public void MergeJson_WithValuePresentInLowerPriorityOnly_LowerValsAreReceived()
        {
            // Arrange
            var json =
                "{\"ranked\":[" +
                "{\"priority\":1,\"vals\":{\"test\":1}}," +
                "{\"priority\":0,\"vals\":{\"test\":2,\"some_unique_property\":\"should_be_copied\"}}" +
                "]}";

            // Act
            var result = _sut.Merge(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Exactly(2).Items);
                Assert.That(result, Does.ContainKey("test").WithValue(2));
                Assert.That(result, Does.ContainKey("some_unique_property").WithValue("should_be_copied"));
            });
        }

        [Test]
        public void MergeJson_WithSkipInTheKeyName_KeyIsSkipped()
        {
            // Arrange
            var json =
                "{\"ranked\":[" +
                "{\"priority\":1,\"vals\":{\"test\":1}}," +
                "{\"priority\":0,\"vals\":{\"skip_test\":2}}" +
                "]}";

            // Act
            var result = _sut.Merge(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Exactly(1).Items);
                Assert.That(result, Does.ContainKey("test").WithValue(1));
            });
        }

        [Test]
        public void MergeJson_EmptyJsonReceived_NoExceptionIsThrown()
        {
            // Arrange
            var json = "";

            // Act / Assert
            Assert.That(() => _sut.Merge(json), Throws.Nothing);
        }
    }
}