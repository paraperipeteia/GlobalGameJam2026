using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // This class manages updates to BoardMemberUI instances, company name text, clock, deal button, sliders, etc. 
    public class UIController : MonoBehaviour
    {
        public static UIController Instance { get; private set; }
        
        [SerializeField] private List<BoardMemberData> _memberData;
        [SerializeField] private List<Sprite> _boardMemberSprites;
        
        [SerializeField] private TextMeshProUGUI companyName;

        [SerializeField] private List<BoardMemberUI> boardMembers;

        [SerializeField] private Clock clock;

        [SerializeField] private Button dealBtn;

        [SerializeField] private SliderGroup sliders;

        [SerializeField] private GameObject startScreen = null;
        [SerializeField] private DealCompletedScreen completeScreen = null;
        [SerializeField] private GameObject quarterlyEarningsScreen = null;
        
        [SerializeField] private GameObject menuScreen = null; 
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            Init(); 
        }

        private void OnEnable()
        {
            GameController.OnDealUpdated += HandleProposalUpdates;
            GameController.OnDealSuccess += OnDealSuccess; 
            GameController.OnNewDealStarted += OnNewDealStarted;
            GameController.QuarterCompleted += OnQuarterCompleted;
            Clock.OnCountdownComplete += OnCountdownComplete;
        }

        private void OnDisable()
        {
            GameController.OnDealUpdated -= HandleProposalUpdates;
            GameController.OnNewDealStarted -= OnNewDealStarted;
            GameController.OnDealSuccess -= OnDealSuccess;
            GameController.QuarterCompleted -= OnQuarterCompleted;
            Clock.OnCountdownComplete -= OnCountdownComplete;
        }

        private void OnCountdownComplete()
        {
            if (completeScreen != null)
            {
                completeScreen.gameObject.SetActive(true);
                completeScreen.Init(null);
            }
        }
        
        private void OnQuarterCompleted()
        {
            if (quarterlyEarningsScreen != null)
            {
                quarterlyEarningsScreen.gameObject.SetActive(true);
            } 
        }
        
        /// <summary>
        /// TODO - build this out to do proper updates - just testing scriptable objects here - David M.
        /// </summary>
        public void Init()
        {
            var boardMemberCopies = new List<BoardMemberData>();
            
            foreach (var memberData in _memberData)
            {
                boardMemberCopies.Add(Instantiate(memberData));    
            }
            
            Utils.Shuffle(boardMemberCopies);
            
            for (var i = 0; i < boardMembers.Count; i++)
            {
               boardMembers[i].Init(boardMemberCopies[i]); 
            }
        }

        public List<Tuple<ResourceType,float>> GetCurrentSliderValues()
        {
            List<Tuple<ResourceType, float>> values = new List<Tuple<ResourceType, float>>();
            sliders.GetSliders().ForEach(s => values.Add(new Tuple<ResourceType, float>(s.GetResourceType(), s.GetValue())));
            return values;
        }

        private void HandleProposalUpdates()
        {
            for (var index = 0; index < GameController.Instance.currentDeal.boardMembers.Count; index++)
            {
                var happiness = GameController.Instance.currentDeal.boardMembers[index].happinessLevel;
                boardMembers[index].SetMask((int) happiness);
            }
        }

        private void OnNewDealStarted()
        {
            companyName.text = GameController.Instance.currentDeal.companyName;

            for (var index = 0; index < boardMembers.Count; index++)
            {
                var boardType = GameController.Instance.currentDeal.boardMembers[index].bmtype;
                var happiness = GameController.Instance.currentDeal.boardMembers[index].happinessLevel;
                var boardMemberUI = boardMembers[index];
                var memberData = _memberData.Find(d => d.boardMemberType == boardType);
                boardMemberUI.UpdateMemberType(memberData, happiness);
            }
        }

        public Sprite GetRandomBoardMemberSprite()
        {
            return _boardMemberSprites[UnityEngine.Random.Range(0, _boardMemberSprites.Count)];
        }

        public void StartGame()
        {
            GameController.Instance.StartGame();
            
            if (startScreen != null)
            {
                startScreen.gameObject.SetActive(false);    
            }
            clock.ResetClock();
            clock.StartCountdown();
        }

        private void OnDealSuccess(Deal d)
        {
            if (completeScreen != null)
            {
                completeScreen.gameObject.SetActive(true);
                completeScreen.Init(d);
            }
        }
        
        public void StartNextDeal()
        {
            StartGame();
        }

        public void CloseQuarterlyEarningsScreen()
        {
            if (quarterlyEarningsScreen != null)
            {
                quarterlyEarningsScreen.gameObject.SetActive(false);
            }
        }

        public void ToggleMenuScreen()
        {
            if (menuScreen != null)
            {
                menuScreen.SetActive(!menuScreen.activeSelf);
            }
        }
    }
}