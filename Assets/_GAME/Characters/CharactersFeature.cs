using System.Collections.Generic;
using UnityEngine;

namespace _GAME.Characters
{
    public class CharactersFeature : MonoBehaviour
    {
        public PlayerRefs player;
        public List<EnemyRefs> enemies = new List<EnemyRefs>();

        public CharactersSettings settings;
    }
}