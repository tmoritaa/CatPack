using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorImage : MonoBehaviour {
    [SerializeField]
    private float displayAlpha;

    private Image image;

    void Awake() {
        image = GetComponent<Image>();
    }

    void Start() {
        SetImageToDefault();
    }

    void Update() {
        Vector2 worldMousePos = InputManager.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);

        this.transform.position = worldMousePos;
    }

    public void SetImageToDefault() {
        image.color = new Color(0, 0, 0, 0);
    }

    public void SetImageToTarget() {
        image.color = new Color(0, 1, 0, displayAlpha);
    }
}
