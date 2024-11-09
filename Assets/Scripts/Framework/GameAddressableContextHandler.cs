using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Framework
{
    public class GameAddressableContext
    {
        #region Variables

        private List<AsyncOperationHandle> _requestHandles = new ();

        #endregion

        #region Public Methods

        public async Task<List<TObject>> LoadAssetsAsync<TObject>(List<string> keys) where TObject : Object
        {
            var request = _LoadAssetsAsync<TObject>(keys);
            _requestHandles.Add(request);
            var result = await request.Task;
            var instances = result.Select(Object.Instantiate).ToList();
            return instances;
        }
        
        public async Task<TObject> LoadAssetAsync<TObject>(string key) where TObject : Object
        {
            var result = await LoadAssetsAsync<TObject>(new List<string> { key });
            return result.FirstOrDefault();
        }
        
        public List<TObject> LoadAssets<TObject>(List<string> keys) where TObject : Object
        {
            var request = _LoadAssetsAsync<TObject>(keys);
            _requestHandles.Add(request);
            var result = request.WaitForCompletion();
            return result.Select(Object.Instantiate).ToList();
        }
        
        public TObject LoadAsset<TObject>(string key) where TObject : Object
        {
            return LoadAssets<TObject>(new List<string> { key }).First();
        }
        
        public void Release()
        {
            _requestHandles.Clear();
        }

        #endregion

        #region Private Methods

        private AsyncOperationHandle<IList<TObject>> _LoadAssetsAsync<TObject>(List<string> keys) where TObject : Object
        {
            return Addressables.LoadAssetsAsync<TObject>(keys, null, Addressables.MergeMode.Union, false);
        }

        #endregion
    }
}