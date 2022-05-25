using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerActionsView : BaseView, IPlayerActionsView
{
    public event Action AttackClicked = () => { };
    public event Action WaitClicked = () => { };

    [SerializeField] private Button attackButton;
    [SerializeField] private Button waitButton;

    private void Awake()
    {
        attackButton.onClick.AddListener(OnAttackClicked);
        waitButton.onClick.AddListener(OnWaitClicked);
        HideAttackButton();
    }

    public void OnAttackClicked()
    {
        AttackClicked();
    }

    public void OnWaitClicked()
    {
        WaitClicked();
    }

    public void ShowAttackButton()
    {
        attackButton.gameObject.SetActive(true);
    }

    public void HideAttackButton()
    {
        attackButton.gameObject.SetActive(false);
    }
}
