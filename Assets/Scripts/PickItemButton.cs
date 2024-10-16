using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItemButton : MonoBehaviour
{
    [SerializeField] private RectTransform buttonTransform;

    [SerializeField] private Vector2 showPos;
    [SerializeField] private Vector2 hidePos;

    public void InteractButton(bool show)
    {
        if (show)
            buttonTransform.DOAnchorPos(showPos, .25f);
        else
            buttonTransform.DOAnchorPos(hidePos, .25f);
    }

    public void PickItem()
    {
        
    }
}
