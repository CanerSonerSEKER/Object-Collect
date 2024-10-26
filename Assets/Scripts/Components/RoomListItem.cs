using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Components
{
    public class RoomListItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public RoomInfo _info;
    
        public void SetUp(RoomInfo info)
        {
            _info = info;
            _text.text = info.Name;
        }

        public void OnClick()
        {
            Launcher.Instance.JoinRoom(_info);
        }
    }
}
