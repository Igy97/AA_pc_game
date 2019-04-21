using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kolider : MonoBehaviour {

    GameObject krug;

    private void Start()
    {
        krug = GameObject.Find("Krug").gameObject;
    }

    private void Update()
    {
        //gameObject.transform.localPosition = krug.transform.localPosition;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin"))
        {
            collision.gameObject.transform.Find("Spheare").gameObject.SetActive(true); //naredba koja pali donje deo pina
                                                                                       //objekat.SetActive(true); 
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Pin"))
        {
            collision.gameObject.transform.Find("Spheare").gameObject.SetActive(true); //naredba koja pali donje deo pina
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;                                                                       //objekat.SetActive(true); 

        }
    }

   


}
