using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDataBase : ScriptableObject
{
    [Serializable]
    struct Paramator
    {
        int hitCounter;
        int mass;

        public int Mass { get => mass; }
    }
}
