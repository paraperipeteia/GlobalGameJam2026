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
            moneyGeneratedText.text = $"Money Generated: {d.money_generated:C}";
            facilitiesGeneratedText.text = $"Facilities Generated: {d.facilities_generated:N0}";
            personnelGeneratedText.text = $"Employees Generated: {d.employees_generated:N0}";
        }
    }
}