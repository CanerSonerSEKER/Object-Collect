using DG.Tweening;
using Events;
using Extensions.Plugins.Demigiant;
using TMPro;
using UnityEngine;

namespace Components
{
    public class PlayerSoreTMP : EventListenerMono, ITweenContainerBind
    {
        [SerializeField] private TextMeshProUGUI _playerScoreTMP;
        public ITweenContainer TweenContainer { get; set; }

        private Tween _counterTween;
        private int _currCounterVal;
        private int _playerScore;


        private void Awake()
        {
            TweenContainer = TweenContain.Install(this);
        }

        protected override void RegisterEvents()
        {
            CollisionEvents.Score += OnScore;
        }

        protected override void UnRegisterEvents()
        {
            CollisionEvents.Score += OnScore;
        }

        private void OnScore(int val)
        {
            _playerScore += val;

            if (_counterTween.IsActive())
            {
                _counterTween.Kill();
            }

            _counterTween = DOVirtual.Int
            (
                _currCounterVal,
                _playerScore,
                1f,
                OnCounterUpdate
            );

        }

        private void OnCounterUpdate(int value)
        {
            _currCounterVal = value;
            _playerScoreTMP.text = $"Score: {_currCounterVal}";
        }
    }
    
}