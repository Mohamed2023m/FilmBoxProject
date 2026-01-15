using Microsoft.AspNetCore.Components.Forms;

namespace Filmbox.Admin.Models
{
    public class MediaCreateFormModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string MediaType { get; set; }
        public DateTime? PublishDate { get; set; }
        public IBrowserFile? ImageUrl { get; set; }
    }
}
