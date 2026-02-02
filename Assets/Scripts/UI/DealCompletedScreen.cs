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

        [SerializeField] private GameObject rejected = null; 
        [SerializeField] private GameObject accepted = null; 
        
        public void NextDealButtonClicked()
        {
            UIController.Instance.StartGame();
            gameObject.SetActive(false);
        }

        public void Init(Deal completedDeal)
        {
            // if the deal is null, the user ran out of time and the deal is rejected
            if (completedDeal == null)
            {
                // make sure the accepted container is turned off 
                if (accepted != null) accepted.gameObject.SetActive(false);
                if (rejected != null) rejected.gameObject.SetActive(true);
                return;
            }

            // make sure the rejected container is turned off 
            rejected.gameObject.SetActive(false);
            accepted.gameObject.SetActive(true);

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