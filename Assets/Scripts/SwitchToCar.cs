using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SwitchToCar : MonoBehaviour
{
    [SerializeField] private RectTransform buttonTransform;

    [SerializeField] private Vector2 showPos;
    [SerializeField] private Vector2 hidePos;
    private void OnEnable()
    {
        MetaverseEvents.OnSwitchPlayer.AddListener(InteractButton);
    }

    private void OnDisable()
    {
        MetaverseEvents.OnSwitchPlayer.RemoveListener(InteractButton);
    }

    private void InteractButton(bool show)
    {
        if (show)
            buttonTransform.DOAnchorPos(showPos, .25f);
        else
            buttonTransform.DOAnchorPos(hidePos, .25f);
    }

    public void SwitchControl()
    {
        buttonTransform.DOAnchorPos(hidePos, .25f);
        InputControlSwitch.Instance.SwitchPlayer(InputControlSwitch.SetPlayer.RCC);
    }
}
