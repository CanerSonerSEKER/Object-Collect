using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Components
{
    public class Countdown : EventListenerMono
    {
        [SerializeField] private TMP_Text _countDownTMP;
        [SerializeField] private TMP_Text _gameTimerTMP;

        private float _countDownTimer = 3.0f;
        private bool _count;
        private int _gameTime;
        private Hashtable setTime = new();

        
        private void Start()
        {
            TimeEvents.CountdownTimer?.Invoke();

            _count = true;
            
        }

        private void Update()
        {
            _gameTime = (int)PhotonNetwork.CurrentRoom.CustomProperties["Time"];
            float minutes = Mathf.FloorToInt((int)PhotonNetwork.CurrentRoom.CustomProperties["Time"] / 60);
            float seconds = Mathf.FloorToInt((int)PhotonNetwork.CurrentRoom.CustomProperties["Time"] % 60);

            _gameTimerTMP.text = $"{minutes:00} : {seconds:00}";
            
            if (PhotonNetwork.IsMasterClient)
            {
                if (_count)
                {
                    _count = false;
                    StartCoroutine(GameTimer());
                }
            }
            
        }
        
        protected override void RegisterEvents()
        {
            TimeEvents.CountdownTimer += OnCountdownTimer;
        }

        private void OnCountdownTimer()
        {
            StartCoroutine(CountdownToStart());
        }

        private IEnumerator CountdownToStart()
        {
            Time.timeScale = 0f;
            
            while (_countDownTimer > 0)
            {
                _countDownTMP.text = _countDownTimer.ToString();

                yield return new WaitForSecondsRealtime(1f);

                _countDownTimer--;
            }

            _countDownTMP.text = "GO!";
            
            yield return new WaitForSecondsRealtime(1f);

            Time.timeScale = 1f;
            
            _countDownTMP.gameObject.SetActive(false);
            
        }
        
        private IEnumerator GameTimer()
        {
            yield return new WaitForSeconds(1f);
            int nextTime = _gameTime -= 1;
            if (nextTime >= 0)
            {
                setTime["Time"] = nextTime;
                PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
                _count = true;   
            }
            else
            {
                Time.timeScale = 0f; // scoreboard 
            }
        }

        protected override void UnRegisterEvents()
        {
            TimeEvents.CountdownTimer -= OnCountdownTimer;
        }
    }
}