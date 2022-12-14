using System.Threading.Tasks;

namespace NoThanks.Runtime.Application
{
    public abstract class PlayStrategy
    {
        public abstract Task<bool> ListenIfTakeCard();
    }
}