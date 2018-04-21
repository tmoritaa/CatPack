using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarDisplay : MonoBehaviour {
    [SerializeField]
    private Text text;

    void Update() {
        Player player = ObjectRefHolder.Instance.PlayerRef;
        text.text = "Time: " + Mathf.RoundToInt(player.CurTimeBar).ToString();
    }
}
