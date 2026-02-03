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

        [SerializeField] private RectTransform earningsContainerParent;

        [SerializeField] private float containerHeight = 350.0f; 
        
        [SerializeField] private ScrollRect earningsScrollRect;
        
        private void Awake()
        {
            if (nextButton != null)
            {
                nextButton.onClick.AddListener(() => UIController.Instance.CloseQuarterlyEarningsScreen());
            }
        }

        private void OnEnable()
        {
            // for each deal we've completed in the playerVC, we need to generate an earnings summary prefab and populate it 

            // hardcoding this because of time constraints - not a good idea, but too bad!!
            if (earningsContainerParent != null)
            {
                // set the height 
                earningsContainerParent.sizeDelta = new Vector2(earningsContainerParent.rect.width, containerHeight * GameController.Instance.playerVC.GetActiveDeals().Count);
            }
            
            // clean the parent of any garbage placeholder stuff 
            if (dealContainer != null)
            {
                for (int i = 0; i < dealContainer.transform.childCount; i++)
                {
                    Destroy(dealContainer.transform.GetChild(i).gameObject);
                }
            }
            
            foreach (var activeDeal in GameController.Instance.playerVC.GetActiveDeals())
            {
                var instance = Instantiate(earningsPrefab, dealContainer);
                instance.Init(activeDeal);
            }

            if (earningsScrollRect != null)
            {
                earningsScrollRect.content.position = new Vector2(earningsScrollRect.content.position.x, 0f);
            }
        }
    }
}