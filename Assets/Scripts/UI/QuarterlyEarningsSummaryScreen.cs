using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuarterlyEarningsSummaryScreen : MonoBehaviour
    {
        [SerializeField] 
        private CompanyEarningsSummary earningsPrefab;

        [SerializeField] private RectTransform dealContainer; 
        
        [SerializeField] private Button nextButton;


        private void Awake()
        {
            if (nextButton != null)
            {
                nextButton.onClick.AddListener(() => UIController.Instance.CloseQuarterlyEarningsScreen());
            }

            // clean the parent of any garbage placeholder stuff 
            if (dealContainer != null)
            {
                for (int i = 0; i < dealContainer.transform.childCount; i++)
                {
                    Destroy(dealContainer.transform.GetChild(i).gameObject);
                }
            }
        }

        private void Start()
        {
            // for each deal we've completed in the playerVC, we need to generate an earnings summary prefab and populate it 

            foreach (var activeDeal in GameController.Instance.playerVC.GetActiveDeals())
            {
                var instance = Instantiate(earningsPrefab, dealContainer);
                instance.Init(activeDeal);
            }
        }
    }
}