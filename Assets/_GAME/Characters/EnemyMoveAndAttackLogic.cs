using System;
using _GAME.Levels;
using DG.Tweening;
using UnityEngine;

namespace _GAME.Characters
{
    public class EnemyMoveAndAttackLogic : MonoBehaviour
    {
        private LevelsFeature _levelsFeature;
        private CharactersFeature _charactersFeature;

        private void Awake()
        {
            _levelsFeature = FindObjectOfType<LevelsFeature>();
            _charactersFeature = FindObjectOfType<CharactersFeature>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            foreach (var enemy in _charactersFeature.enemies)
            {
                if (Vector3.Distance(_charactersFeature.player.transform.position, enemy.transform.position) 
                    < _charactersFeature.settings.enemyDistanceToAttack)
                {
                    Attack(enemy);
                    return;
                }
                
                var moveDir = (_charactersFeature.player.transform.position - enemy.transform.position).normalized;
                enemy.transform.position += moveDir * _charactersFeature.settings.enemySpeed * Time.deltaTime;
                enemy.attackTimer = 0;
            }
        }

        private void Attack(EnemyRefs enemy)
        {
            enemy.attackTimer += Time.deltaTime;
            
            if (enemy.attackTimer < _charactersFeature.settings.enemyAttackTimer)
                return;

            enemy.attackTimer = 0;
            _charactersFeature.player.hp -= _charactersFeature.settings.enemyAttackDamage;
            _charactersFeature.player.transform.DOPunchPosition(
                (enemy.transform.position - _charactersFeature.player.transform.position).normalized 
                * _charactersFeature.settings.enemyAttackForce, 0.1f);
        }
    }
}