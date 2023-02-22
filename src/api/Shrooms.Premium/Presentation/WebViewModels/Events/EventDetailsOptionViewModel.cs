using System.Collections.Generic;

namespace Shrooms.Premium.Presentation.WebViewModels.Events
{
    public class EventDetailsOptionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<EventParticipantViewModel> Participants { get; set; }
    }
}
