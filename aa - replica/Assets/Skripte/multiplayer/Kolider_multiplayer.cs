using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kolider_multiplayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("igrac1") || collision.CompareTag("igrac2"))
        {
            collision.gameObject.transform.Find("Spheare").gameObject.SetActive(true);  //stvara donji deo pina
        }


    }





}
