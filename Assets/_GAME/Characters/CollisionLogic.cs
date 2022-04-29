using System;
using System.Security.Cryptography;
using _GAME.Audio;
using _GAME.Levels;
using _GAME.LevelView;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GAME.Characters
{
    public class CollisionLogic : MonoBehaviour
    {
        private LevelsFeature _levelsFeature;
        private CharactersFeature _charactersFeature;
        private LevelViewFeature _levelViewFeature;
        private AudioFeature _audioFeature;

        private void Awake()
        {
            _levelsFeature = FindObjectOfType<LevelsFeature>();
            _charactersFeature = FindObjectOfType<CharactersFeature>();
            _levelViewFeature = FindObjectOfType<LevelViewFeature>();
            _audioFeature = FindObjectOfType<AudioFeature>();
        }

        private void OnEnable()
        {
            _charactersFeature.player.collisionCatcher.OnTriggerEnterEvent += PlayerCollider;
            
            foreach (var levelObject in _levelsFeature.currentLevel.levelObjects)
            {
                levelObject.OnCollisionEnterEvent += LevelObjectCollision;
            }
        }

        private void Update()
        {
            EnemiesCollisionActivate();
        }

        private void EnemiesCollisionActivate()
        {
            foreach (var enemy in _charactersFeature.enemies)
            {
                if (enemy.isCollisionActivated)
                    continue;

                enemy.collisionCatcher.OnCollisionEnterEvent += EnemyCollision;
            }
        }

        private void PlayerCollider(Collider collider)
        {
            if (!collider.GetComponentInParent<CubeRefs>())
                return;

            var cube = collider.GetComponentInParent<CubeRefs>();
            _charactersFeature.player.bulletRenderer.material.color = cube.renderer.material.color;
            _audioFeature.getCube.Play();
            
            if (cube.renderer.material.color == _charactersFeature.settings.cubeColors[0])
            {
                _charactersFeature.player.redCubesCounter += 1;
                _levelViewFeature.collectedRedCubesText.text =
                    "Red Cubes: " + _charactersFeature.player.redCubesCounter;
            }
            
            if (cube.renderer.material.color == _charactersFeature.settings.cubeColors[1])
            {
                _charactersFeature.player.yellowCubesCounter += 1;
                _levelViewFeature.collectedYellowCubesText.text =
                    "Yellow Cubes: " + _charactersFeature.player.yellowCubesCounter;
            }
            
            if (cube.renderer.material.color == _charactersFeature.settings.cubeColors[2])
            {
                _charactersFeature.player.greenCubesCounter += 1;
                _levelViewFeature.collectedGreenCubesText.text =
                    "Green Cubes: " + _charactersFeature.player.greenCubesCounter;
            }
            
            Destroy(cube.gameObject);
        }

        private void EnemyCollision(Collision collision)
        {
            if (!collision.contacts[0].otherCollider.GetComponentInParent<BulletRefs>())
                return;

            var enemy = collision.contacts[0].thisCollider.GetComponentInParent<EnemyRefs>();
            var bullet = collision.contacts[0].otherCollider.GetComponentInParent<BulletRefs>();
            _charactersFeature.player.bullets.Remove(bullet);
            bullet.gameObject.SetActive(false);
            Destroy(bullet.gameObject);

            enemy.hitCounter += 1;

            if (enemy.hitCounter < _charactersFeature.settings.enemyMaxHit)
                return;

            var newCube = Instantiate(enemy.cube, enemy.cubeSpawnTransform.position, Quaternion.identity);
            newCube.renderer.material.color =
                _charactersFeature.settings.cubeColors[Random.Range(0, _charactersFeature.settings.cubeColors.Length)];
            _charactersFeature.enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }

        private void LevelObjectCollision(Collision collision)
        {
            if (!collision.contacts[0].otherCollider.GetComponentInParent<BulletRefs>())
                return;

            var bullet = collision.contacts[0].otherCollider.GetComponentInParent<BulletRefs>();
            _charactersFeature.player.bullets.Remove(bullet);
            Destroy(bullet.gameObject);
        }
    }
}