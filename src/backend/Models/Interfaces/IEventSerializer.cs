using Models.Messaging;

namespace Models.Interfaces
{
    public interface IEventSerializer
    {
        string Serialize<TE>(TE @event) where TE : Event;
    }
}