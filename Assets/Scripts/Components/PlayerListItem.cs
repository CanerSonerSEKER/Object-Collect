using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Components
{
    public class PlayerListItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _text;
        private Photon.Realtime.Player _player;

        public void SetUp(Photon.Realtime.Player player)
        {
            _player = player;

            _text.text = _player.NickName; 
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            if (_player == otherPlayer)
            {
                Destroy(gameObject);
            }
        }

        public override void OnLeftRoom()
        {
            Destroy(gameObject);
        }
    }
}
