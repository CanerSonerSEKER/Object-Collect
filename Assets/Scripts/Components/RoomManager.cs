using System;
using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        public static RoomManager Instance;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public override void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex == 1)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            }
        }

        public override void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
