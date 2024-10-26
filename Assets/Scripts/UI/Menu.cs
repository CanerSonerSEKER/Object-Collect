using UnityEngine;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public string _menuName;
        public bool _open;
            
        public void Open()
        {
            _open = true;
            gameObject.SetActive(true);
        }

        public void Close()
        {
            _open = false;
            gameObject.SetActive(false);
        }
    }
}
