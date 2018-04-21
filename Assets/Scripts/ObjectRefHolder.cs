using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRefHolder : MonoBehaviour {
    private static ObjectRefHolder instance;
    public static ObjectRefHolder Instance
    {
        get {
            return instance;
        }
    }

    [SerializeField]
    private Player playerRef;
    public Player PlayerRef
    {
        get {
            return playerRef;
        }
    }

    [SerializeField]
    private List<Cat> catRefs;
    public List<Cat> CatRefs
    {
        get {
            return catRefs;
        }
    }

    void Awake() {
        instance = this;
    }
}
