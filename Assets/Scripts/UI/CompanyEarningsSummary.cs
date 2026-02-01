using TMPro;
using UnityEngine;

namespace UI
{
    public class CompanyEarningsSummary : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI companyNameText;

        [SerializeField] private TextMeshProUGUI moneyGeneratedText;

        [SerializeField] private TextMeshProUGUI personnelGeneratedText;

        [SerializeField] private TextMeshProUGUI facilitiesGeneratedText;

        public void Init(Deal d)
        {
            companyNameText.text = d.companyName;
            moneyGeneratedText.text = $"$ Generated: {d.money_generated}";
            facilitiesGeneratedText.text = $"Facilities Generated: {d.facilities_generated}";
            personnelGeneratedText.text = $"$ Employees Generated: {d.employees_generated}";
        }
    }
}