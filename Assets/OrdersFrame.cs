using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersFrame : MonoBehaviour {
    [SerializeField]
    private CanvasGroup canvasGroup;

    private GameObject followGO = null;

    public bool IsDisplayed
    {
        get {
            return canvasGroup.blocksRaycasts;
        }
    }

    void Awake() {
        SetShowState(false);
    }

    void Update () {
        if (this.gameObject.activeSelf && followGO != null) {
            Vector2 pos = InputManager.Instance.MainCamera.WorldToScreenPoint(followGO.transform.position);
            this.transform.position = pos;
        }
	}

    public void Init(Cat owner, GameObject followObj) {
        OrderButton[] buttons = this.GetComponentsInChildren<OrderButton>();

        foreach (OrderButton button in buttons) {
            button.Init(owner);
        }

        followGO = followObj;
    }

    public void SetShowState(bool b) {
        canvasGroup.alpha = b ? 1.0f : 0.0f;
        canvasGroup.blocksRaycasts = b;
    }
}
