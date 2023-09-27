using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerBar playerBar;

    [Header("事件监听")]
    public CharacterEventSO healthEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playerBar.OnHealthChange(persentage);
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;

    }
}
