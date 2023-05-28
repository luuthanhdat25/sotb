using System;
using UnityEngine;

namespace DefaultNamespace.Resources.Item
{
    [Serializable]
    public class DropRate
    {
        public Transform itemPrefab;
        public int dropRate;
        public int maxDrop;
        public int minDrop;
    }
}