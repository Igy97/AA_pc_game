using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class podaci : MonoBehaviour {

    [HideInInspector]
    public string igrac1, igrac2;
    [HideInInspector]
    public Color igrac1_boja, igrac2_boja;
    [HideInInspector]
    public float brzina_kruga = 0f;
    [HideInInspector]
    public int poeni = 1;
    [HideInInspector]
    public bool random_direction = false, increase_speed = false, pin_change_direction = false;

    bool only_once = true;

    private void Awake()
    {

        if (only_once == true)
        {
            DontDestroyOnLoad(gameObject);
            only_once = false;
        }
        
    }
}
