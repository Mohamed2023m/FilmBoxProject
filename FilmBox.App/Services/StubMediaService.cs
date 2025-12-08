using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.App.Services
{
    public class StubMediaService
    {
        // Returnerer kun stub for MediaId = 1
        public MediaModel? GetStubMedia(int mediaId)
        {
            if (mediaId == 1)
            {
                return new MediaModel
                {
                    MediaId = 1,
                    Title = "Breaking Bad",
                    Genre = "Drama",
                    Description = "A chemistry teacher turns to crime to secure his family's future.",
                    ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fstatic1.colliderimages.com%2Fwordpress%2Fwp-content%2Fuploads%2F2023%2F02%2Fbreaking-bad-tv-poster.jpg&f=1&nofb=1&ipt=550d78f6a0bc8ed05449f959cb8079bdcfeb1bc8271c704fc305208473ed47f6",
                    PublishDate = new DateTime(2008, 1, 20)
                };
            }

            // Ingen fallback — returner null hvis mediaId != 1
            return null;
        }
    }
}