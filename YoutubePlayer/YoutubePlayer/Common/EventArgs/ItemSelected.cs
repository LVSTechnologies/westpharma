using System;
namespace YoutubePlayer.Common.EventArgs
{
    public class ItemSelected
    {
        public string SourceUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
