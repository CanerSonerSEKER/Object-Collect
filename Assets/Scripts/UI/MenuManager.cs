using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        [SerializeField] private Menu[] _menus;

        private void Awake()
        {
            Instance = this;
        }

        public void OpenMenu(string menuName)
        {
            for (int i = 0; i < _menus.Length; i++)
            {
                if (_menus[i]._menuName == menuName)
                {
                    _menus[i].Open();
                }
                else if (_menus[i]._open)
                {
                    CloseMenu(_menus[i]);
                }
            }
        }

        public void OpenMenu(Menu menu)
        {
            for (int i = 0; i < _menus.Length; i++)
            {   
                if (_menus[i]._open)
                {
                    CloseMenu(_menus[i]);
                }
            }
            
            menu.Open();
        }
        
        public void CloseMenu(Menu menu)
        {
            menu.Close();
        }

    }
}
