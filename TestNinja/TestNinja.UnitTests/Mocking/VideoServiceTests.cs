using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IVideoRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _videoService = new VideoService();
            _repository = new Mock<IVideoRepository>();
        }

        [Test]
        public void ReadVideoTitle_FileEmpty_ReturnError()
        {
            //SetUp or Arrange            
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            //Act
            var result = _videoService.ReadVideoTitle(fileReader.Object);

            //Assert
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_NoUnprocessedVideos_ReturnEmptyString()
        {
            //Arrange            
            _repository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>());

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv(_repository.Object);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_OnlyOneUnprocessedVideo_ReturnStringId()
        {
            //Arrange            
            _repository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>() { new Video() { Id = 1 } });

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv(_repository.Object);

            //Assert
            Assert.That(result, Is.EqualTo("1"));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_ManyUnprocessedVideos_ReturnIdsConcatenatedByComma()
        {
            //Arrange            
            _repository.Setup(vr => vr.GetUnprocessedVideos()).Returns(
                new List<Video>() {
                    new Video() { Id = 1 },
                    new Video() { Id = 2 },
                    new Video() { Id = 3 }
                });

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv(_repository.Object);

            //Assert
            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
