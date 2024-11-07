using UnityEngine;

namespace Game.Services.Impl
{
    public class LogServiceImpl : ILogService
    {
        #region Methods
        
        public void Log(string message)
        {
            Debug.Log($"[LogService] {message}");
        }
        
        public void LogWarning(string message)
        {
            Debug.LogWarning($"[LogService] {message}");
        }

        #endregion

    }
}