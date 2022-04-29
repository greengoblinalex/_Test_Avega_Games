using _GAME.Common;
using UnityEngine;

namespace _GAME.Levels
{
    public class LevelRefs : MonoBehaviour
    {
        public CollisionCatcher[] levelObjects;
        public Transform spawnerCenter;
        public Vector3 spawnerSize;

        [SerializeField] private bool _isDrawGizmos;
        
        private void OnDrawGizmos()
        {
            if (!_isDrawGizmos)
                return;
            
            Gizmos.color = new Color(1,0,0,0.5f);
            Gizmos.DrawCube(spawnerCenter.position, spawnerSize);
        }
    }
}