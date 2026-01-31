using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BoardMember : MonoBehaviour
    {
        [SerializeField] 
        private Image frame;

        [SerializeField] 
        private Image avatar;

        [SerializeField] 
        private Image currentMask;

        private BoardMemberData _d;
        
        public void Init(BoardMemberData data,  BoardMemberStatus status = BoardMemberStatus.Ok)
        {
            _d = data;

            if (frame != null)
            {
                frame.color = _d.frameColor;
            }

            if (avatar != null)
            {
                avatar.sprite = _d.avatar;
            }

            if (currentMask != null)
            {
                var defaultMask = (int) BoardMemberStatus.Ok;
                currentMask.sprite = _d.maskSprites[defaultMask]; // default to OK 
            }
        }
        
    }
}