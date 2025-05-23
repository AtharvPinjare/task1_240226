using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health PlayerHealth;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;

    private void Start()
    {
        totalhealthbar.fillAmount = PlayerHealth.currenthealth / 10;//Since from the start of the game, the plaeyr would have the maximum health.
    }
    private void Update()
    {
        currenthealthbar.fillAmount = PlayerHealth.currenthealth / 10;//Amount of hearts/10 
    }
}
