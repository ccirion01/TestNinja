using System.Net;

namespace TestNinja.Mocking
{
    public class Program
    {
        public static void Main()
        {
            var service = new VideoService();
            service.ReadVideoTitle(new FileReader());
            var installerHelper = new InstallerHelper(new FileDownloader());
        }
    }
}
