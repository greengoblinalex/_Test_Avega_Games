using System.Collections.Generic;
using _GAME.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GAME.Characters
{
    public class PlayerRefs : MonoBehaviour
    {
        public float hp;
        public CollisionCatcher collisionCatcher;

        public int redCubesCounter;
        public int yellowCubesCounter;
        public int greenCubesCounter;
        
        public BulletRefs bullet;
        public Renderer bulletRenderer;
        public Transform bulletSpawnTransform;
        public List<BulletRefs> bullets = new List<BulletRefs>();
    }
}