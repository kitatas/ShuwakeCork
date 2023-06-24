using Furu.InGame.Data.DataStore;
using UniEx;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace Furu.InGame.Domain.Repository
{
    public sealed class LiquidRepository
    {
        private LiquidTable _liquidTable;

        public LiquidRepository()
        {
            Addressables.LoadAssetAsync<TextAsset>(GetKey("liquid")).Completed += x =>
            {
                _liquidTable = JsonUtility.FromJson<LiquidTable>(x.Result.text);
            };
        }

        private static string GetKey(string jsonName)
        {
            return $"{ResourceConfig.JSON_PATH}{jsonName}.json";
        }

        public Data.Entity.LiquidEntity Lot(int playCount)
        {
            // 全種類1回は抽選されるようにする
            if (_liquidTable.data_list.TryGetValue(playCount, out var liquid))
            {
                return liquid;
            }
            else
            {
                return _liquidTable.data_list.GetRandom();
            }
        }
    }
}