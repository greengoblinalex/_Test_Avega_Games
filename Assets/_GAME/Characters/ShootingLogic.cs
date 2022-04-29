using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.Audio;
using UnityEngine;

namespace _GAME.Characters
{
    public class ShootingLogic : MonoBehaviour
    {
        private CharactersFeature _charactersFeature;
        private AudioFeature _audioFeature;
        private FP_Input _playerInput;

        private Camera _mainCamera;
        
        private int _ammo;
        private int _ammoCount;
        private float _shootDelay;
        private bool _isReloading;
        
        private void Awake()
        {
            _playerInput = FindObjectOfType<FP_Input>();
            _charactersFeature = FindObjectOfType<CharactersFeature>();
            _audioFeature = FindObjectOfType<AudioFeature>();
            _mainCamera = Camera.main;
        }

        private void Start ()
        {
            _ammoCount = _charactersFeature.settings.ammoCount;
            _ammo = _ammoCount;
        }
	
        private void Update () 
        {
            if(_playerInput.Shoot())
                if(Time.time > _shootDelay)
                    Shoot();
            
            if (_playerInput.Reload())
                if (!_isReloading && _ammoCount < _ammo)
                    StartCoroutine("Reload");

            BulletMove();
        }

        private void Shoot()
        {
            if (_ammoCount > 0 || _charactersFeature.settings.isInfiniteAmmo)
            {
                Debug.Log("Shoot");
                _audioFeature.shot.Play();
                BulletSpawn();
                _ammoCount--;
            }
            else
                Debug.Log("Empty");
            
            _shootDelay = Time.time + _charactersFeature.settings.shootDelay;
        }

        private void BulletSpawn()
        {
            Ray ray = _mainCamera.ViewportPointToRay(_charactersFeature.settings.rayAim);
            RaycastHit hit;
            Vector3 hitPoint;

            if (Physics.Raycast(ray, out hit))
                hitPoint = hit.point;
            else
                hitPoint = ray.GetPoint(100);

            var newBullet = Instantiate(_charactersFeature.player.bullet);
            newBullet.gameObject.SetActive(true);
            newBullet.transform.position = _charactersFeature.player.bulletSpawnTransform.position;
            newBullet.moveDir = hitPoint - newBullet.transform.position;
            // newBullet.rb.AddForce(newBullet.moveDir.normalized * _charactersFeature.settings.bulletForce, ForceMode.Impulse);
            _charactersFeature.player.bullets.Add(newBullet);
        }

        private void BulletMove()
        {
            foreach (var bullet in _charactersFeature.player.bullets)
            {
                bullet.transform.position += bullet.moveDir.normalized * _charactersFeature.settings.bulletSpeed * Time.deltaTime;
            }
        }

        private IEnumerator Reload()
        {
            _isReloading = true;
            Debug.Log("Reloading");
            yield return new WaitForSeconds(_charactersFeature.settings.reloadTime);
            _ammoCount = _ammo;
            Debug.Log("Reloading Complete");
            _isReloading = false;
        }
    }
}