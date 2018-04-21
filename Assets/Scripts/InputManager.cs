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

    public delegate void LeftMouseDownEvent(Vector2 mousePos);
    public delegate void RightMouseUpEvent();

    public event LeftMouseDownEvent OnLeftMouseDown;
    public event RightMouseUpEvent OnRightMouseUp;

    private Camera mainCamera;
    public Camera MainCamera
    {
        get {
            return mainCamera;
        }
    }

    [SerializeField]
    private CursorImage cursor;
    public CursorImage Cursor
    {
        get {
            return cursor;
        }
    }

    void Awake() {
        instance = this;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetMouseButtonDown(0)) {
            Vector2 worldMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (OnLeftMouseDown != null) {
                OnLeftMouseDown(worldMousePos);
            }
        }

        if (Input.GetMouseButtonUp(1)) {
            if (OnRightMouseUp != null) {
                OnRightMouseUp();
            }
        }
	}
}
