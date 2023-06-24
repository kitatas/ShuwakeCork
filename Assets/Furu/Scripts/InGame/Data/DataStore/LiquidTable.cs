using System;
using Furu.InGame.Data.Entity;

namespace Furu.InGame.Data.DataStore
{
    [Serializable]
    public sealed class LiquidTable
    {
        public LiquidEntity[] data_list;
    }
}