using System.Collections.Generic;
using System.Linq;
using Game.Services.Data;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Game.Services.Impl
{
    public class StorageServiceImpl : IStorageService
    {
        #region Variables
        
        private List<ScoringData> _scoringData;
        
        private const string STORAGE_DATA_KEY = "StorageData";
        
        #endregion

        #region Lifecycle
        
        public void Start()
        {
            Load();
        }

        #endregion
        
        #region Public Methods
        
        public void AddData(int score, int turns, int combo, int round)
        {
            _scoringData.Add(new ScoringData
            {
                Score = score,
                Turns = turns,
                Combo = combo,
                Round = round
            });
            Save();
        }
        
        public void ClearData()
        {
            _scoringData.Clear();
            Save();
        }
        
        public int GetLastRound()
        {
            return _scoringData.Count > 0 ? _scoringData.Max(it => it.Round) + 1 : 0;
        }
        
        public int GetHighestScore()
        {
            return _scoringData.Count > 0 ? _scoringData.Max(it => it.Score) : 0;
        }

        #endregion

        #region Private Methods

        private void Load()
        {
            var rawData = PlayerPrefs.GetString(STORAGE_DATA_KEY);
            _scoringData = string.IsNullOrEmpty(rawData) ? new List<ScoringData>() : JsonConvert.DeserializeObject<List<ScoringData>>(rawData);
        }

        private void Save()
        {
            var rawData = JsonConvert.SerializeObject(_scoringData);
            PlayerPrefs.SetString(STORAGE_DATA_KEY, rawData);
            PlayerPrefs.Save();
        }

        #endregion

    }
}