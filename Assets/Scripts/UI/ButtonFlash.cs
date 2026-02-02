using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonFlash : MonoBehaviour
    {
        [SerializeField] private Color defaultBtnColor;

        [SerializeField] private Color targetFlashColor;

        [SerializeField] private Image image; 
        
        private float fadeDuration = 0.3f;
        private float fadeTime = 0;
        
        private bool fading = false;
        private bool reversing = false;
        
        private void Update()
        {
            if (image == null) return;
            
            if (!fading && !reversing)
            {
                image.CrossFadeColor(targetFlashColor, fadeDuration, true, false);
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
                    image.CrossFadeColor(defaultBtnColor, fadeDuration, true, false);
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
            }
        }
    }
}