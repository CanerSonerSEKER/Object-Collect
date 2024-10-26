using System;
using System.IO;
using Photon.Pun;
using UnityEngine;

namespace Components
{
    public class PlayerManager : MonoBehaviour
    {
        private PhotonView _pv;

        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (_pv.IsMine)
            {
                CreateController();
            }
        }

        private void CreateController()
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
        }
    }
}
