using System;
using NUnit.Framework;
using UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static event Action OnDealUpdated;
    public static event Action<Deal> OnDealSuccess;
    public static event Action OnNewDealStarted;
    public static event Action QuarterCompleted; 
    
    public static GameController Instance;

    public VC playerVC;

    public Deal currentDeal;

    [SerializeField, Tooltip("Threshold to reach before a report is generated")]
    private int reportThreshold = 5;

    public int ReportThreshold => reportThreshold;
    
    private int _currentQuarterDeals = 0; 
    
    private bool _canCloseDeal;
    public bool CanCloseDeal => _canCloseDeal; 
    
    public void Awake()
    {
        if (Instance != null)
        {
            //Debug.Log("GameController instance already exists! - destroying it");
            Destroy(gameObject);
        }

        //nameGenerator = new NameGen();
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
            OnDealUpdated?.Invoke();
        }
    }

    public void TryCloseDeal()
    {
        if (_canCloseDeal)
        {
            playerVC.AddDeal(currentDeal);
            OnDealSuccess?.Invoke(currentDeal);
            _currentQuarterDeals++;
            if (_currentQuarterDeals >= reportThreshold)
            {
                QuarterCompleted?.Invoke();
                _currentQuarterDeals = 0;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        } 
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}