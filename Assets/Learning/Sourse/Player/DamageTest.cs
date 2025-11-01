using UnityEngine;
using UnityEngine.InputSystem;

public class DamageTest : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;

    private void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            _playerHealth.TakeDamage(10f);
        }
        
    }
}