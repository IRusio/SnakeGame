using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SnakeGame.Models
{
    public partial class PixabayRequest
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("totalHits")]
        public long TotalHits { get; set; }

        [JsonProperty("hits")]
        public List<Hit> Hits { get; set; }
    }

    public partial class Hit
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("pageURL")]
        public Uri PageUrl { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("previewURL")]
        public Uri PreviewUrl { get; set; }

        [JsonProperty("previewWidth")]
        public long PreviewWidth { get; set; }

        [JsonProperty("previewHeight")]
        public long PreviewHeight { get; set; }

        [JsonProperty("webformatURL")]
        public Uri WebformatUrl { get; set; }

        [JsonProperty("webformatWidth")]
        public long WebformatWidth { get; set; }

        [JsonProperty("webformatHeight")]
        public long WebformatHeight { get; set; }

        [JsonProperty("largeImageURL")]
        public Uri LargeImageUrl { get; set; }

        [JsonProperty("imageWidth")]
        public long ImageWidth { get; set; }

        [JsonProperty("imageHeight")]
        public long ImageHeight { get; set; }

        [JsonProperty("imageSize")]
        public long ImageSize { get; set; }

        [JsonProperty("views")]
        public long Views { get; set; }

        [JsonProperty("downloads")]
        public long Downloads { get; set; }

        [JsonProperty("favorites")]
        public long Favorites { get; set; }

        [JsonProperty("likes")]
        public long Likes { get; set; }

        [JsonProperty("comments")]
        public long Comments { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("userImageURL")]
        public string UserImageUrl { get; set; }
    }

}
