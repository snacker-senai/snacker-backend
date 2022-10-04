using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Theme : BaseEntity
    {
        [Required]
        public string Color { get; set; }
        [Required]
        public string SecondaryColor { get; set; }
        [Required]
        public string FontColor { get; set; }
        [Required]
        public string SecondaryFontColor { get; set; }
        public string TertiaryFontColor { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        public long RestaurantId { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
    }
}
