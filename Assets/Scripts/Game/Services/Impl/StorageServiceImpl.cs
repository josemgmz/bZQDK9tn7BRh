﻿using System.Collections.Generic;
using Game.Services.Data;
using Newtonsoft.Json;
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
            if(score == 0 && turns == 0 && combo == 0 && round == 0) return;
            Save();
        }
        
        public void ClearData()
        {
            _scoringData.Clear();
            Save();
        }
        
        public List<ScoringData> GetData()
        {
            return _scoringData;
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