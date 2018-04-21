using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatHealthDisplay : MonoBehaviour {
    [SerializeField]
    private Text text;

    private GameObject followGO;
    private Cat owner;

    void Update() {
        if (followGO != null) {
            Vector2 pos = InputManager.Instance.MainCamera.WorldToScreenPoint(followGO.transform.position);
            this.transform.position = pos;
        }

        text.text = "Health: " + owner.Health.ToString();
    }

    public void Init(Cat ownerRef, GameObject followObj) {
        followGO = followObj;
        owner = ownerRef;
    }
}
