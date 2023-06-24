using Furu.InGame.Data.DataStore;
using UniEx;
using UnityEngine;

namespace Furu.InGame.Domain.Repository
{
    public sealed class LiquidRepository
    {
        private readonly LiquidTable _liquidTable;

        public LiquidRepository(TextAsset liquidMaster)
        {
            _liquidTable = JsonUtility.FromJson<LiquidTable>(liquidMaster.text);
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