using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;

using HuecasAppUsers.Conexiones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HuecasAppUsers.Servicios
{
    public class MiYouTubeService
    {
        private const string ApiKey = Constantes.YoutubeKey;

        public async Task UploadVideoAsync(string videoPath, string title, string description)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,
                ApplicationName = "MyApp"
            });

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = title;
            video.Snippet.Description = description;
            video.Snippet.Tags = new string[] { "tag1", "tag2" };
            video.Snippet.CategoryId = "22";
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = "unlisted";

            using (var fileStream = new FileStream(videoPath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += VideosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += VideosInsertRequest_ResponseReceived;

                await videosInsertRequest.UploadAsync();
            }
        }

        private void VideosInsertRequest_ProgressChanged(IUploadProgress progress)
        {
            // Handle progress changes
        }

        private void VideosInsertRequest_ResponseReceived(Video video)
        {
            // Handle response
        }
    }
}
