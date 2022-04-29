using System;
using _GAME.Common;
using UnityEngine;

namespace _GAME.Characters
{
    public class EnemyRefs : MonoBehaviour
    {
        public bool isCollisionActivated;
        public CollisionCatcher collisionCatcher;
        public int hitCounter;
        public float attackTimer;
        public CubeRefs cube;
        public Transform cubeSpawnTransform;
    }
}