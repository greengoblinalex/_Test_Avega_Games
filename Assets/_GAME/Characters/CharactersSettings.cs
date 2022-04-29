using UnityEngine;
using UnityEngine.Serialization;

namespace _GAME.Characters
{
    [CreateAssetMenu(fileName = "CharactersSettings", menuName = "GAME Settings/CharactersSettings", order = 0)]
    public class CharactersSettings : ScriptableObject
    {
        [Header("Shoot")]
        public bool isInfiniteAmmo;
        public Vector3 rayAim;
        public float bulletSpeed;
        public float shootDelay = 0.15F;
        public float reloadTime = 1.0F;
        public int ammoCount = 15;

        [Header("Enemy")] 
        public EnemyRefs enemy;
        public float enemySpawnTimer;
        public int enemyMaxHit;
        public float enemySpeed;
        public float enemyDistanceToAttack;
        public float enemyAttackTimer;
        public float enemyAttackDamage;
        public float enemyAttackForce;
        public Color[] cubeColors;
    }
}