using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestrot : MonoBehaviour
{
    #region Singleton
    private static DontDestrot instance;
    public static DontDestrot Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
    }
    #endregion
}
