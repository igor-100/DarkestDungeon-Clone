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
    [SerializeField] private TextMeshProUGUI pickEnemyText;

    private void Awake()
    {
        attackButton.onClick.AddListener(OnAttackClicked);
        waitButton.onClick.AddListener(OnWaitClicked);
        HidePickEnemyText();
    }

    public void OnAttackClicked()
    {
        AttackClicked();
    }

    public void OnWaitClicked()
    {
        WaitClicked();
    }

    public void ShowPickEnemyText()
    {
        pickEnemyText.gameObject.SetActive(true);
    }

    public void HidePickEnemyText()
    {
        pickEnemyText.gameObject.SetActive(false);
    }
}
