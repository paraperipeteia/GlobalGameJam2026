using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DealButton : MonoBehaviour
    {
        [SerializeField] private Image btnImage;
        
        // sprite displayed by default when no offer is acceptable
        [SerializeField] private Color defaultBtnColor;

        [SerializeField] private Color targetFlashColor;

        private float fadeDuration = 0.3f;
        private float fadeTime = 0;
        
        private bool fading = false;
        private bool reversing = false;
        
        private void Update()
        {
            if (GameController.Instance.CanCloseDeal && !fading && !reversing)
            {
                btnImage.CrossFadeColor(targetFlashColor, fadeDuration, true, false);
                fading = true;
                return;
            }

            if (fading)
            {
                fadeTime += Time.deltaTime;

                if (fadeTime >= fadeDuration)
                {
                    fading = false;
                    reversing = true;
                    fadeTime = 0f;
                    btnImage.CrossFadeColor(defaultBtnColor, fadeDuration, true, false);
                }

                return;
            }

            if (reversing)
            {
                fadeTime += Time.deltaTime;

                if (fadeTime >= fadeDuration)
                {
                    reversing = false;
                    fadeTime = 0f;
                }

                return;
            }
            
            if (btnImage != null)
            {
                btnImage.color = defaultBtnColor;
            }
        }
    }
}