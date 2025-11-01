using System;
using TMPro;
using UnityEngine;

public class JumpCounterUI : MonoBehaviour
{
    [SerializeField] JumpCounter _jumpCounter;
    [SerializeField] TextMeshProUGUI _countText;

    void OnEnable()
    {
        if (_jumpCounter == null)
            _jumpCounter = GetComponentInParent<JumpCounter>();

        if (_jumpCounter != null)
            _jumpCounter.OnCountChange += UpdateLabel;
    }

    void OnDisable()
    {
        if (_jumpCounter != null)
            _jumpCounter.OnCountChange -= UpdateLabel;
    }

    void Start()
    {
        UpdateLabel(_jumpCounter != null ? _jumpCounter.Count : 0);
    }
    
    void UpdateLabel(int value)
    {
        if (_countText != null)
            _countText.text = $"Jumps: {value}";
    }
}
