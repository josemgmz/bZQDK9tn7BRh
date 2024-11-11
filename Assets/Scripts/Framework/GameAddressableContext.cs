﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Framework
{
    /// <summary>
    /// Manages the loading and releasing of addressable assets in the game.
    /// </summary>
    public class GameAddressableContext
    {
        #region Variables

        /// <summary>
        /// List of async operation handles for tracking asset requests.
        /// </summary>
        private List<AsyncOperationHandle> _requestHandles = new ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Asynchronously loads a list of assets by their keys.
        /// </summary>
        /// <typeparam name="TObject">The type of the assets to load.</typeparam>
        /// <param name="keys">The keys of the assets to load.</param>
        /// <param name="instantiate">Whether to instantiate the loaded assets.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of loaded assets.</returns>
        public async Task<List<TObject>> LoadAssetsAsync<TObject>(List<string> keys, bool instantiate = true) where TObject : Object
        {
            var request = _LoadAssetsAsync<TObject>(keys);
            _requestHandles.Add(request);
            var result = await request.Task;
            return instantiate ? result.Select(Object.Instantiate).ToList() : result.ToList();
        }

        /// <summary>
        /// Asynchronously loads a single asset by its key.
        /// </summary>
        /// <typeparam name="TObject">The type of the asset to load.</typeparam>
        /// <param name="key">The key of the asset to load.</param>
        /// <param name="instantiate">Whether to instantiate the loaded asset.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the loaded asset.</returns>
        public async Task<TObject> LoadAssetAsync<TObject>(string key, bool instantiate = true) where TObject : Object
        {
            var result = await LoadAssetsAsync<TObject>(new List<string> { key }, instantiate);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Synchronously loads a list of assets by their keys.
        /// </summary>
        /// <typeparam name="TObject">The type of the assets to load.</typeparam>
        /// <param name="keys">The keys of the assets to load.</param>
        /// <param name="instantiate">Whether to instantiate the loaded assets.</param>
        /// <returns>The list of loaded assets.</returns>
        public List<TObject> LoadAssets<TObject>(List<string> keys, bool instantiate = true) where TObject : Object
        {
            var request = _LoadAssetsAsync<TObject>(keys);
            _requestHandles.Add(request);
            var result = request.WaitForCompletion();
            return instantiate ? result.Select(Object.Instantiate).ToList() : result.ToList();
        }

        /// <summary>
        /// Synchronously loads a single asset by its key.
        /// </summary>
        /// <typeparam name="TObject">The type of the asset to load.</typeparam>
        /// <param name="key">The key of the asset to load.</param>
        /// <param name="instantiate">Whether to instantiate the loaded asset.</param>
        /// <returns>The loaded asset.</returns>
        public TObject LoadAsset<TObject>(string key, bool instantiate = true) where TObject : Object
        {
            return LoadAssets<TObject>(new List<string> { key }, instantiate).First();
        }

        /// <summary>
        /// Releases all tracked asset requests.
        /// </summary>
        public void Release()
        {
            _requestHandles.Clear();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates an async operation handle for loading a list of assets by their keys.
        /// </summary>
        /// <typeparam name="TObject">The type of the assets to load.</typeparam>
        /// <param name="keys">The keys of the assets to load.</param>
        /// <returns>The async operation handle for the asset loading request.</returns>
        private AsyncOperationHandle<IList<TObject>> _LoadAssetsAsync<TObject>(List<string> keys) where TObject : Object
        {
            return Addressables.LoadAssetsAsync<TObject>(keys, null, Addressables.MergeMode.Union, false);
        }

        #endregion
    }
}