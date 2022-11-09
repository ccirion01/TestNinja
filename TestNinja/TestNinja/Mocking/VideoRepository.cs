using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }

    public class VideoRepository : IVideoRepository
    {
        private VideoContext _videoContext;

        public VideoRepository(VideoContext videoContext)
        {
            _videoContext = videoContext;
        }

        public IEnumerable<Video> GetUnprocessedVideos()
        {
            return
                    (from video in _videoContext.Videos
                     where !video.IsProcessed
                     select video).ToList();
        }
    }
}
