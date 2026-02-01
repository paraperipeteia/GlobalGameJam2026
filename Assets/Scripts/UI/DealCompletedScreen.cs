using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DealCompletedScreen : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> resultText;
        [SerializeField] private Button nextDealButton = null;

        private void Awake()
        {
            if (nextDealButton != null)
            {
                nextDealButton.onClick.AddListener(() =>
                {
                    UIController.Instance.StartNextDeal();
                    gameObject.SetActive(false);
                });
            } 
        }

        public void Init(Deal completedDeal)
        {
            if (GameController.Instance != null)
            {
                var emp = (int) completedDeal._employees_offered;
                var money = (int) completedDeal._money_offered;
                var fac = (int) completedDeal._facilities_offered;

                resultText[0].text = $"Money offered: {money}";
                resultText[1].text = $"Personnel offered: {emp}";
                resultText[2].text = $"Facilities offered: {fac}";
            }
        }
    }
}