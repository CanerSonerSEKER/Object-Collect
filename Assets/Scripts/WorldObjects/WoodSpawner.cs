using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldObjects
{
    public class WoodSpawner : MonoBehaviour
    {
        [SerializeField] private Vector2Int _terrainSize;
        [SerializeField] private List<Wood> _woods;
        [SerializeField] private int _initWoodCount = 50;
        [SerializeField] private float _woodSpawnFreq = 1f;
        [SerializeField] private Coroutine _coroutine;
        [SerializeField] private GameObject _woodPrefab;
        [SerializeField] private float _spawnOffSetY = 0.1f;
        
        private void Awake()
        {
            SpawnAllWoods();
            
            _coroutine = StartCoroutine(SpawnRoutine());
        }

        private void SpawnAllWoods()
        {
            _woods = new List<Wood>();
            
            for (int i = 0; i < _initWoodCount; i++)
            {
                SpawnRandWood();
            }
        }

        private void SpawnRandWood()
        {
            float randX = Random.Range(0f, _terrainSize.x);
            float randZ = Random.Range(0, _terrainSize.y);

            Vector3 newWoodPos = new Vector3(randX, _spawnOffSetY, randZ);

            GameObject newWoodGo = Instantiate(_woodPrefab, transform);

            newWoodGo.transform.localPosition = newWoodPos;
            Wood newWood = newWoodGo.GetComponent<Wood>();
            
            _woods.Add(newWood);

            newWood.Pickable += OnPickable;
        }

        private void OnPickable(Wood pickedWood)
        {
            Debug.LogWarning($"SpawnerPreRemove: {pickedWood}");
            pickedWood.Pickable -= OnPickable;
            _woods.Remove(pickedWood);
            Destroy(pickedWood.gameObject);
            

            // Transform olmalı bu noktada çünkü yok oluyor üstüne sırt bölgesine ışınlanması toplanması lazım . Ve oluşturulan listenin yalnız bir elemanı kullanılabilir kalmalı.
            //pickedWood.transform.DOMove(_backTransform.transform.position, 0.5f).SetEase(Ease.InOutBounce);
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                if (_woods.Count < _initWoodCount)
                {
                    SpawnRandWood();                    
                }
                
                yield return new WaitForSeconds(_woodSpawnFreq);
            }
        }
    }
}
