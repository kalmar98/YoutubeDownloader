using Launcher.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace Launcher.Services
{
    public class HomeService : IHomeService
    {
        public IList<string> ExportDirectories { get; private set; }

        public HomeService()
        {
            Initialize();
        }

        public async Task Download(string url, string destination)
        {
            var client = new YoutubeClient();

            string id = YoutubeClient.ParseVideoId(url);

            var meta = await client.GetVideoAsync(id);

            var mediaStreamInfo = await client.GetVideoMediaStreamInfosAsync(id);

            var audioStreamInfo = mediaStreamInfo.Audio[0];

            string fullPath = $"{destination}\\{Regex.Replace(meta.Title, @"[\/\\]+", "&")}.mp3";
            
            await client.DownloadMediaStreamAsync(audioStreamInfo, fullPath);

            
        }

        private void Initialize()
        {
            ExportDirectories = new List<string>();

            LoadSubdirectories(Constants.PrefixDirectory);

        }

        private void LoadSubdirectories(string path)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(path);

            foreach (string subdirectory in subdirectoryEntries)
            {
                ExportDirectories.Add(subdirectory);
                LoadSubdirectories(subdirectory);
            }

        }
    }
}
