using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public UiGame uiGame;
    public Spawner spawner;

    private void Awake()
    {

        //Debug.LogFormat("monster: {0}", monster);
    }

    void Start()
    {
        this.spawner.onCreateDamageText = (pos, value) => {
            this.uiGame.CreateDamageText(pos, value);
        };

    }
}