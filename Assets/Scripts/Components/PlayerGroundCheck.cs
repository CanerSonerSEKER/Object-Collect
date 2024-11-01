using System;
using UnityEngine;

namespace Components
{
    public class PlayerGroundCheck : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerController.gameObject)
            {
                return;
            }
            
            _playerController.SetGroundedState(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _playerController.gameObject)
            {
                return;
            }
            _playerController.SetGroundedState(false);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject == _playerController.gameObject)
            {
                return;
            }
            
            _playerController.SetGroundedState(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == _playerController.gameObject)
            {
                return;
            }
            
            _playerController.SetGroundedState(true);
        }
        

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject == _playerController.gameObject)
            {
                return;
            }
            
            _playerController.SetGroundedState(false);
        }
        

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject == _playerController.gameObject)
            {
                return;
            }
            
            _playerController.SetGroundedState(true);
        }
    }
}
