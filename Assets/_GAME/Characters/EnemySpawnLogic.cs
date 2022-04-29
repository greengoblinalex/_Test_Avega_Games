using System;
using _GAME.Levels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GAME.Characters
{
    public class EnemySpawnLogic : MonoBehaviour
    {
        private CharactersFeature _charactersFeature;
        private LevelsFeature _levelsFeature;

        private Camera _mainCamera;
        private float _timer;
        
        private void Awake()
        {
            _charactersFeature = FindObjectOfType<CharactersFeature>();
            _levelsFeature = FindObjectOfType<LevelsFeature>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            SpawnTimer();
        }

        private void SpawnTimer()
        {
            _timer += Time.deltaTime;

            if (_timer < _charactersFeature.settings.enemySpawnTimer)
                return;

            _timer = 0;
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var spawnPos = _levelsFeature.currentLevel.spawnerCenter.position + new Vector3(
                Random.Range(
                    -_levelsFeature.currentLevel.spawnerSize.x / 2,
                    _levelsFeature.currentLevel.spawnerSize.x / 2),
                _charactersFeature.settings.enemy.transform.position.y
                -_levelsFeature.currentLevel.spawnerCenter.position.y,
                Random.Range(
                    -_levelsFeature.currentLevel.spawnerSize.z / 2,
                    _levelsFeature.currentLevel.spawnerSize.z / 2));

            var screenPoint = _mainCamera.WorldToViewportPoint(spawnPos);
            
            if (screenPoint.z > 0 && screenPoint.x > -0.25f && screenPoint.x < 1.25f)
            {
                SpawnEnemy();
                return;
            }
            
            var newEnemy = Instantiate(_charactersFeature.settings.enemy, spawnPos, Quaternion.identity);
            _charactersFeature.enemies.Add(newEnemy);
        }
    }
}