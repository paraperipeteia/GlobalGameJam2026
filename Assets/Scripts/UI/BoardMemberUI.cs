using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace UI
{
    public class BoardMemberUI : MonoBehaviour
    {
        [SerializeField] 
        private Image frame;

        [SerializeField] 
        private Image avatar;

        [SerializeField] 
        private Image currentMask;

        [SerializeField] private Image background;
        
        private BoardMemberData _d;
        
        [SerializeField]
        private Vector2 leftExtremity;
        [SerializeField]
        private Vector2 rightExtremity;

        private float _currentTime = 0.0f;
        private float _totalTime = 4.0f;

        private Vector2 _usedLeftExtremity; 
        private Vector2 _usedRightExtremity;

        [SerializeField] private float _maxTravelDist = 15.0f; 
        
        private Vector2 _startPosition; 
        
        private int _directionMod = 1;
        
        private bool _targetLeft = true;

        private Func<float,float> _easeFunc;

        private enum EaseType
        {
            Linear = 0, 
            Circular = 1
        }

        private EaseType _currentEase = EaseType.Circular;
        private float _currentTravelDist; 
        
        public void Init(BoardMemberData data,  HappinessLevel status = HappinessLevel.Happy)
        {
            UpdateMemberType(data, status);
            _startPosition = currentMask.transform.localPosition;
        }

        public void ChooseNewAvatar()
        {
            avatar.sprite = UIController.Instance.GetRandomBoardMemberSprite();
        }

        public void UpdateMemberType(BoardMemberData data, HappinessLevel l)
        {
            _d = data;

            if (frame != null)
            {
                frame.color = _d.frameColor;
            }

            if (background != null)
            {
                background.color = _d.backgroundColor;
            }
            
            if (avatar != null)
            {
                avatar.sprite = _d.avatar;
            }

            if (currentMask != null)
            {
                currentMask.sprite = _d.maskSprites[(int) l]; 
            }

            if (avatar != null)
            {
                avatar.sprite = UIController.Instance.GetRandomBoardMemberSprite();
            }      
            
            var extremityL = UnityEngine.Random.Range(leftExtremity.x/2, leftExtremity.x);
            var extremityR = UnityEngine.Random.Range(rightExtremity.x/2, rightExtremity.x);
            _usedLeftExtremity = new Vector2(extremityL, leftExtremity.y);
            _usedRightExtremity = new Vector2(extremityR, rightExtremity.y);
            _totalTime = UnityEngine.Random.Range(0.5f, 4.0f);
            _easeFunc = Utils.PickRandomEase();
            _directionMod = UnityEngine.Random.Range(0, 100) > 50 ? 1 : -1;
            _currentTravelDist = UnityEngine.Random.Range(_maxTravelDist/2, _maxTravelDist);
            _currentEase = (EaseType) UnityEngine.Random.Range(0, Enum.GetValues(typeof(EaseType)).Length);
        }

        private void Update()
        {
            if (_currentEase == EaseType.Linear)
            {
                LinearUpdate();
            }

            if (_currentEase == EaseType.Circular)
            {
                CircularUpdate();
            }
        }

        private void CircularUpdate()
        {
            if (_currentTime >= _totalTime)
            {
                _currentTime = 0; 
            }

            _currentTime += Time.deltaTime;
            
            var amount = _easeFunc(_currentTime / _totalTime);
            var angle = (2 * Mathf.PI) * amount;
            float x = Mathf.Sin(angle) * _maxTravelDist;
            float y = Mathf.Cos(angle) * _maxTravelDist;
            
            currentMask.transform.localPosition = new Vector2(_startPosition.x + x, _startPosition.y + y);
        }
        
        private void LinearUpdate()
        {
            if (_currentTime >= _totalTime || _currentTime <= 0)
            {
                _currentTime = _currentTime <= 0 ? 0 : _totalTime;
                currentMask.transform.localPosition = new Vector2(_startPosition.x + (_targetLeft? _usedLeftExtremity.x : _usedRightExtremity.x), currentMask.transform.localPosition.y);
                _targetLeft = !_targetLeft;
            }
            
            var timeMod = _targetLeft ? 1 : -1;
            _currentTime += Time.deltaTime * timeMod;
            
            var amount = _easeFunc(_currentTime / _totalTime);
            var totalDistX = _usedRightExtremity.x - _usedLeftExtremity.x;
            var totalDistY = _usedRightExtremity.y - _usedLeftExtremity.y;
            var positionX = _startPosition.x + (_usedRightExtremity.x - (totalDistX * amount * _directionMod)); 
            var positionY = _startPosition.y + (_usedRightExtremity.y - (totalDistY * amount));
            currentMask.transform.localPosition = new Vector2(positionX, positionY);
        }
        

        public void SetMask(int index)
        {
            currentMask.sprite = _d.maskSprites[index];
        }
    }
}