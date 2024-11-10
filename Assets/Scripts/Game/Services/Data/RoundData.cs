namespace Game.Services.Data
{
    public class OnRoundStartEvent {}

    public class OnRoundEndEvent
    {
        public int lastRound { get; set; }
        public int currentRound { get; set; }
    }
}