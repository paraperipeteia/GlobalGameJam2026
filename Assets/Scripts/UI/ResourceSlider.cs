using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceSlider : MonoBehaviour 
    {
        public static event Action<ResourceType, float> OnSliderValueChanged;

        [SerializeField] private Slider slider;

        private ResourceType _t;

        [SerializeField] private TextMeshProUGUI resourceName;

        [SerializeField] private TextMeshProUGUI currentValue;

        [SerializeField] private Image sliderFill;

        [SerializeField] private Image handle; 
        
        private string GetStringFormat()
        {
            return _t switch
            {
                ResourceType.Money => "C",
                ResourceType.Personnel or ResourceType.Facilities => "N0",
                _ => ""
            };
        }

        private Color GetColor()
        {
            Debug.Log($"GetColor for type: {_t}");
            return _t switch
            {
                ResourceType.Money => new Color(62.0f/255.0f, 145.0f/255.0f, 32.0f/255.0f, 255.0f/255.0f),
                ResourceType.Personnel => new Color(156/255.0f, 40/255.0f, 14/255.0f, 255.0f/255.0f),
                ResourceType.Facilities => new Color(12/255.0f, 50/255.0f, 148/255.0f, 255.0f),
                _ => Color.black
            };
        }

        public ResourceType GetResourceType()
        {
            return _t;
        }

        public float GetValue()
        {
            return slider.value;
        }

        public void Refresh(float maxValue)
        {
            slider.maxValue = maxValue;
            slider.normalizedValue = 0.5f; // let's start off in the middle of the range
            currentValue.text = slider.value.ToString(GetStringFormat());
            
            var c = GetColor();
            if (sliderFill != null)
            {
                sliderFill.color = c;
            }

            if (handle != null)
            {
                handle.color = c;
            }
            
        }
        
        public void Init(ResourceType t)
        {
            _t = t; 
        
            if (slider != null)
            {
                // start amount is the max value
                slider.maxValue = 1;
                slider.minValue = 0;
                slider.value = 0;
            }

            if (resourceName != null)
            {
                resourceName.text = t.ToString();
            }

            var c = GetColor();
            if (sliderFill != null)
            {
                sliderFill.color = c;
                //sliderFill.CrossFadeAlpha(0.99f, 0.01f, false);
            }

            if (handle != null)
            {
                handle.color = c;
                //handle.CrossFadeAlpha(0.99f, 0.01f, false);
            }

            slider.onValueChanged.AddListener(TriggerChangeEvent);
        }

        private void TriggerChangeEvent(float change)
        {
            OnSliderValueChanged?.Invoke(_t, change);
        }
        
        private void Update()
        {
            currentValue.text = slider.value.ToString(GetStringFormat());
        }
       
    }
}