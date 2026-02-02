using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Clock : MonoBehaviour
    {
        public static event Action OnCountdownComplete;
       
        private const int CountdownDuration = 60;
        
        private float _duration;
        private float _currentTime;
        private bool _countingDown = false;
        private Action _onCountDownComplete;

        [SerializeField] private RectTransform minuteHand;

        [SerializeField] private Image fillIndicator;
    
        private void Awake()
        {
            Init(() =>
            {
                ResetClock();
                OnCountdownComplete?.Invoke();
            });
        }

        /// <summary>
        /// Test to verify the countdown clock functions as intended and fires callback on completion
        /// </summary>
        private void ClockTest()
        {
            Init( () =>
            {
                //Debug.Log("Countdown has completed!");
                ResetClock(); 
            });
            StartCountdown();
        }
    
        /// <summary>
        /// Initialize the clock with a specified duration. Optional param for callback Action 
        /// </summary>
        /// <param name="onCountDownComplete">A callback invoked upon countdown complete</param>
        public void Init(Action onCountDownComplete = null)
        {
            _duration = CountdownDuration;
            _currentTime = _duration;
            _onCountDownComplete = onCountDownComplete;
        }

        /// <summary>
        /// Resets internal time to the duration passed in Init and halts countdown behaviour 
        /// </summary>
        public void ResetClock()
        {
            _currentTime = CountdownDuration;
            _countingDown = false;
        }

        /// <summary>
        /// Starts the countdown clock
        /// </summary>
        public void StartCountdown()
        {
            _countingDown = true;
        }

        /// <summary>
        /// Stops the countdown clock
        /// </summary>
        public void StopCountdown()
        {
            _countingDown = false;
        }

        private void Update()
        {
            if (!_countingDown) return;
        
            _currentTime -= Time.deltaTime;

            float currentRoation = -_currentTime * 6.0f;

            if (minuteHand != null)
            { 
                minuteHand.rotation = Quaternion.Euler(0, 0, currentRoation);
            }

            if (fillIndicator != null)
            {
                fillIndicator.fillAmount = _currentTime / 60.0f;
            }
        
            if (_currentTime < 0)
            {
                _duration = 0;
                _countingDown = false;
                _onCountDownComplete?.Invoke();
            }
        }
    }
}