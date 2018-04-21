using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    private static InputManager instance;

    public static InputManager Instance
    {
        get {
            return instance;
        }
    }

    public delegate void LeftMouseUpEvent(Vector2 mousePos);

    public event LeftMouseUpEvent OnLeftMouseUp;

    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetMouseButtonUp(0)) {
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (OnLeftMouseUp != null) {
                OnLeftMouseUp(worldMousePos);
            }
        }
	}
}
