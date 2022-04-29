using System;
using _GAME.LevelView;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GAME.Characters
{
    public class PlayerHealthLogic : MonoBehaviour
    {
        private CharactersFeature _charactersFeature;
        private LevelViewFeature _levelViewFeature;

        private float _playerCurrentHp;
        
        private void Awake()
        {
            _charactersFeature = FindObjectOfType<CharactersFeature>();
            _levelViewFeature = FindObjectOfType<LevelViewFeature>();
        }

        private void Update()
        {
            HealthController();
        }

        private void HealthController()
        {
            if (_charactersFeature.player.hp == _playerCurrentHp)
                return;

            _playerCurrentHp = _charactersFeature.player.hp;
            _levelViewFeature.playerHpText.text = "Health: " + _charactersFeature.player.hp;
            
            if (_charactersFeature.player.hp <= 0)
                RestartScene();
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}