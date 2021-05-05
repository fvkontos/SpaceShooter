using System;
using UnityEngine;

namespace UnityStandardAssets.Water
{
    internal class WaterBase
    {
        public GameObject gameObject { get; internal set; }
        public Material sharedMaterial { get; internal set; }

        public static explicit operator WaterBase(UnityEngine.Object v)
        {
            throw new NotImplementedException();
        }
    }
}