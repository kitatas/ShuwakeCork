using System;
using UnityEngine;

namespace Furu.InGame.Data.Entity
{
    [Serializable]
    public sealed class LiquidEntity
    {
        public int id;
        public string name;
        public float r;
        public float g;
        public float b;

        public Color color => new Color(r, g, b, 1.0f);
    }
}