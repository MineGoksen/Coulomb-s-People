using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace YoutubePlayer
{
    /// <summary>
    /// Downloads and plays Youtube videos a VideoPlayer component
    /// </summary>
    [RequireComponent(typeof(VideoPlayer))]
    public class YoutubePlayer : MonoBehaviour
    {
        public enum Cli
        {
            YoutubeDl,
            YtDlp,
        }

        // The needed fields to play a video
        static readonly string[] k_PlayFields = { "url" };

        // The needed fields to download a video
        static readonly string[] k_DownloadFields = { "title", "_filename" , "ext", "url" };

        /// <summary>
        /// Youtube url (e.g. https://www.youtube.com/watch?v=VIDEO_ID)
        /// </summary>
        private string youtubeUrl;
        void Update()
        {
            System.Random rnd = new System.Random();
            string[] videoClips = { "https://www.youtube.com/watch?v=nbD8XYTw23Y", "https://youtu.be/Npcv4vnD1LU?t=270", "https://www.youtube.com/watch?v=U1zEx7QISnQ", "https://youtu.be/rUokxZu2ltA?t=42", "https://www.youtube.com/watch?v=O5wn8Adxr_8","https://www.youtube.com/watch?v=X68teeQN34M","https://www.youtube.com/watch?v=E7_cwXaLU94&ab_channel=KEN","https://www.youtube.com/watch?v=5-tW-wjW1YA" };
            int randomIndex = rnd.Next(0, videoClips.Length);
            youtubeUrl = videoClips[randomIndex];
        }
        /// <summary>
        /// Specify whether to use 360 configuration
        /// </summary>
        public bool is360Video;

        /// <summary>
        /// The cli tool to be used (youtube-dl | yt-dlp)
        /// </summary>
        public Cli cli = Cli.YtDlp;

        /// <summary>
        /// VideoPlayer component associated with the current YoutubePlayer instance
        /// </summary>
        public VideoPlayer VideoPlayer { get; private set; }

        void Awake()
        {
            VideoPlayer = GetComponent<VideoPlayer>();
        }

        async void OnEnable()
        {
            if (VideoPlayer.playOnAwake)
                await PlayVideoAsync();
        }

        /// <summary>
        /// Triggers a request to youtube-dl to parse the webpage for the raw video url
        /// </summary>
        /// <param name="videoUrl">Youtube url (e.g. https://www.youtube.com/watch?v=VIDEO_ID)</param>
        /// <param name="options">Options for downloading the raw video</param>
        /// <param name="cancellationToken">A CancellationToken used to cancel the current async task</param>
        /// <returns>A Task to await</returns>
        public static async Task<string> GetRawVideoUrlAsync(string videoUrl, YoutubeDlOptions options = null, CancellationToken cancellationToken = default)
        {
            options = options ?? YoutubeDlOptions.Default;
            var metaData = await YoutubeDl.GetVideoMetaDataAsync<YoutubeVideoMetaData>(videoUrl, options, k_PlayFields, cancellationToken);
            return metaData.Url;
        }

        /// <summary>
        /// Triggers a request to youtube-dl to parse the webpage for the raw video url
        /// </summary>
        /// <param name="videoUrl">Youtube url (e.g. https://www.youtube.com/watch?v=VIDEO_ID)</param>
        /// <param name="cli">The cli tool that will be used to download the video or parse its raw url</param>
        /// <param name="options">Options for downloading the raw video</param>
        /// <param name="cancellationToken">A CancellationToken used to cancel the current async task</param>
        /// <returns>A Task to await</returns>
        public static async Task<string> GetRawVideoUrlAsync(string videoUrl, YoutubeDlCli cli, YoutubeDlOptions options = null, CancellationToken cancellationToken = default)
        {
            options = options ?? YoutubeDlOptions.Default;
            var metaData = await YoutubeDl.GetVideoMetaDataAsync<YoutubeVideoMetaData>(videoUrl, options, k_PlayFields, cli, cancellationToken);
            return metaData.Url;
        }

        /// <summary>
        /// Prepare the video for playing. This includes a web request to youtube-dl, as well as preparing/warming up
        /// the VideoPlayer.
        /// </summary>
        /// <param name="videoUrl">Youtube url (e.g. https://www.youtube.com/watch?v=VIDEO_ID)</param>
        /// <param name="options">Options for downloading the raw video</param>
        /// <param name="cancellationToken">A CancellationToken used to cancel the current async task</param>
        /// <returns>A Task to await</returns>
        public async Task PrepareVideoAsync(string videoUrl = null, YoutubeDlOptions options = null, CancellationToken cancellationToken = default)
        {
            videoUrl = videoUrl ?? youtubeUrl;
            options = options ?? (is360Video ? YoutubeDlOptions.Three60 : YoutubeDlOptions.Default);
            var downloader = GetCli(cli);
            var rawUrl = await GetRawVideoUrlAsync(videoUrl, downloader, options, cancellationToken);

            VideoPlayer.source = VideoSource.Url;

            //Resetting the same url restarts the video...
            if (VideoPlayer.url != rawUrl)
                VideoPlayer.url = rawUrl;

            youtubeUrl = videoUrl;

            await VideoPlayer.PrepareAsync(cancellationToken);
        }

        /// <summary>
        /// Play the youtube video in the attached Video Player component.
        /// </summary>
        /// <param name="videoUrl">Youtube url (e.g. https://www.youtube.com/watch?v=VIDEO_ID)</param>
        /// <param name="options">Options for downloading the raw video</param>
        /// <param name="cancellationToken">A CancellationToken used to cancel the current async task</param>
        /// <returns>A Task to await</returns>
        public async Task PlayVideoAsync(string videoUrl = null, YoutubeDlOptions options = null, CancellationToken cancellationToken = default)
        {
            options = options ?? (is360Video ? YoutubeDlOptions.Three60 : YoutubeDlOptions.Default);
            await PrepareVideoAsync(videoUrl, options, cancellationToken);
            VideoPlayer.Play();
        }

        /// <summary>
        /// Download a youtube video to a destination folder
        /// </summary>
        /// <param name="destinationFolder">A folder to create the file in</param>
        /// <param name="videoUrl">Youtube url (e.g. https://www.youtube.com/watch?v=VIDEO_ID)</param>
        /// <param name="cancellationToken">A CancellationToken used to cancel the current async task</param>
        /// <returns>Returns the path to the file where the video was downloaded</returns>
        public async Task<string> DownloadVideoAsync(string destinationFolder = null, string videoUrl = null, CancellationToken cancellationToken = default)
        {
            videoUrl = videoUrl ?? youtubeUrl;
            var downloader = GetCli(cli);
            var video = await YoutubeDl.GetVideoMetaDataAsync<YoutubeVideoMetaData>(videoUrl, YoutubeDlOptions.Default,
                k_DownloadFields, downloader, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            var fileName = GetVideoFileName(video);

            var filePath = fileName;
            if (!string.IsNullOrEmpty(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
                filePath = Path.Combine(destinationFolder, fileName);
            }

            await YoutubeDownloader.DownloadAsync(video, filePath, cancellationToken);
            return filePath;
        }

        static string GetVideoFileName(YoutubeVideoMetaData video)
        {
            if (!string.IsNullOrWhiteSpace(video.FileName))
            {
                return video.FileName;
            }

            var fileName = $"{video.Title}.{video.Extension}";

            var invalidChars = Path.GetInvalidFileNameChars();
            foreach (var invalidChar in invalidChars)
            {
                fileName = fileName.Replace(invalidChar.ToString(), "_");
            }

            return fileName;
        }

        static YoutubeDlCli GetCli(Cli cli)
        {
            switch (cli)
            {
                case Cli.YoutubeDl:
                    return YoutubeDlCli.YoutubeDl;
                case Cli.YtDlp:
                    return YoutubeDlCli.YtDlp;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cli), cli, "value of cli not supported");
            }
        }
    }
}