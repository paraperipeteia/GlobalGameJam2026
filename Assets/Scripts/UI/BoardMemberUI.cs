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
        }
        
        
        
        public void SetMask(int index)
        {
            currentMask.sprite = _d.maskSprites[index];
        }
    }
}