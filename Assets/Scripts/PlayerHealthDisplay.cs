using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour {
    [SerializeField]
    private Text text;

    void Update() {
        Player player = ObjectRefHolder.Instance.PlayerRef;
        text.text = player.Health.ToString();
    }
}
