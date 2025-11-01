using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour , IHealth
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _maxHealth = 100f;
    
    private float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        
        _healthSlider.minValue = 0;
        _healthSlider.maxValue = 1;
        _healthSlider.value = 1;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, _maxHealth);
        UpdateUI();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateUI()
    {
        _healthSlider.value = _currentHealth / _maxHealth;
    }

    private void Die()
    {
        Debug.Log("Player died.");
        gameObject.SetActive(false);
    }
}