using System;
using UnityEngine;
using UnityEngine.UI;

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

        private float currentTime = 0.0f;
        private float totalTime = 4.0f;

        private Vector2 usedLeftExtremity; 
        private Vector2 usedRightExtremity;
        
        private bool targetLeft = true;

        private Func<float,float> easeFunc; 
        
        public void Init(BoardMemberData data,  HappinessLevel status = HappinessLevel.Happy)
        {
            UpdateMemberType(data, status);

       
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
                currentMask.sprite = _d.maskSprites[(int) l]; // default to OK 
            }

            if (avatar != null)
            {
                avatar.sprite = UIController.Instance.GetRandomBoardMemberSprite();
            }      
            
            var extremityL = UnityEngine.Random.Range(5, leftExtremity.x);
            var extremityR = UnityEngine.Random.Range(5, rightExtremity.x);
            usedLeftExtremity = new Vector2(extremityL, leftExtremity.y);
            usedRightExtremity = new Vector2(extremityR, rightExtremity.y);
            totalTime = UnityEngine.Random.Range(0.5f, 2.0f);
            easeFunc = Utils.PickRandomEase();
        }

        private void Update()
        {
            if (currentTime >= totalTime || currentTime <= 0)
            {
                currentTime = currentTime <= 0 ? 0 : totalTime;
                currentMask.transform.localPosition = new Vector2(targetLeft? usedLeftExtremity.x : usedRightExtremity.x, currentMask.transform.localPosition.y);
                targetLeft = !targetLeft;
            }
            
            var timeMod = targetLeft ? 1 : -1;
            currentTime += Time.deltaTime * timeMod;
            
            var amount = easeFunc(currentTime / totalTime);
            float totalDistX = usedRightExtremity.x - usedLeftExtremity.x;
            float totalDistY = usedRightExtremity.y - usedLeftExtremity.y;
            float positionX = usedRightExtremity.x - (totalDistX * amount); 
            float positionY = usedRightExtremity.y - (totalDistY * amount);
            currentMask.transform.localPosition = new Vector2(positionX, positionY);
        }


        public void SetMask(int index)
        {
            currentMask.sprite = _d.maskSprites[index];
        }
    }
}