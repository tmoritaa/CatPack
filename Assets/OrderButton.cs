using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OrderButton : MonoBehaviour {
    [SerializeField]
    private Order.OrderType orderId;

    public void Init(Cat owner) {
        Button button = this.GetComponent<Button>();

        button.onClick.AddListener(() => { owner.OrderButtonClicked(orderId); });
    }
}
