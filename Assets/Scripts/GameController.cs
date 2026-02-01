using System;
using UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static event Action OnDealUpdated;
    public static event Action<Deal> OnDealSuccess;
    public static event Action OnNewDealStarted;
    
    public static GameController Instance;

    public VC playerVC;

    public Deal currentDeal;

    private bool _canCloseDeal;
    public bool CanCloseDeal => _canCloseDeal; 
    
    public void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("GameController instance already exists! - destroying it");
            Destroy(gameObject);
        }

        playerVC = new VC();
        Instance = this;
    }

    private void Start()
    {
        //StartNewDeal();
    }

    public void StartGame()
    {
        StartNewDeal();
    }
    
    private void OnEnable()
    {
       ResourceSlider.OnSliderValueChanged += OnSliderChanged; 
    }

    private void OnDisable()
    {
        ResourceSlider.OnSliderValueChanged -= OnSliderChanged;
    }

    private void StartNewDeal()
    {
        // TODO - do whatever clean up we need - David M. 
        currentDeal = new Deal();
        OnNewDealStarted?.Invoke();
    }
    
    private void OnSliderChanged(ResourceType t, float amount)
    {
        var sliderValues = UIController.Instance.GetCurrentSliderValues();
        var offeredMoney = sliderValues.Find(v => v.Item1 == ResourceType.Money).Item2;
        var offeredFacilities = sliderValues.Find(v => v.Item1 == ResourceType.Facilities).Item2;
        var offeredPersonnel =  sliderValues.Find(v => v.Item1 == ResourceType.Personnel).Item2;
        
        // Make sure the current offer isn't null before trying to make a new proposal 

        if (currentDeal != null)
        {
            _canCloseDeal = currentDeal.MakeProposal(offeredMoney, offeredFacilities, offeredPersonnel);
            Debug.Log("Proposal has changed...");
            OnDealUpdated?.Invoke();
        }
    }

    public void TryCloseDeal()
    {
        if (_canCloseDeal)
        {
            Debug.Log("Closed Deal!");
            playerVC.AddDeal(currentDeal);
            OnDealSuccess?.Invoke(currentDeal);
        }
    }
}