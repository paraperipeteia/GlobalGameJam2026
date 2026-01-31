using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // This class will manage update to BoardMember instances, company name text, clock, deal button, sliders, etc. 
    public class UIController : MonoBehaviour 
    {
        [SerializeField] private List<BoardMemberData> _memberData;
        
        [SerializeField] private TextMeshProUGUI companyName;

        [SerializeField] private List<BoardMember> boardMembers;

        [SerializeField] private Clock clock;

        [SerializeField] private Button dealBtn;

        [SerializeField] private SliderGroup sliders;

        private void Awake()
        {
           Init(); 
        }

        /// <summary>
        /// TODO - build this out to do proper updates - just testing scriptable objects here - David M.
        /// </summary>
        public void Init()
        {
            // copy scriptable objects 
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
    }
}