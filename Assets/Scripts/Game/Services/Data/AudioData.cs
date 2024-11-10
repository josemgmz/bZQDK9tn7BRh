using System;

namespace Game.Services.Data
{
    public static class AudioData
    {
        public enum Music
        {
            Background,
        }
        
        public enum Sfx
        {
            ErrorMatching,
            SuccessMatching,
            SuccessRound,
            CardFlip
        }
        
        public enum Voice
        {
        }
        
        public static string GetString(this Music value)
        {
            return value switch
            {
                Music.Background => "backgroundMusic.mp3",
                _ => throw new Exception("Invalid music")
            };
        }
        public static string GetString(this Sfx value)
        {
            return value switch
            {
                Sfx.ErrorMatching => "errorMatching.mp3",
                Sfx.SuccessMatching => "successMatching.mp3",
                Sfx.SuccessRound => "successRound.mp3",
                Sfx.CardFlip => "cardFlip.mp3",
                _ => throw new Exception("Invalid sfx")
            };
        }
        public static string GetString(this Voice value)
        {
            return value switch
            {
                _ => throw new Exception("Invalid voice")
            };
        }
    }
}