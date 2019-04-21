using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject pinprefab;

    public  int postavljen_pin = 0;
    public int broj_pinova;


    private void Start()
    {
        broj_pinova = GameObject.Find("Cela_scena").GetComponent<GameManager>().poeni_na_pocetku_nivoa;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && postavljen_pin == 0) 
        {
            SpawnPin();
            postavljen_pin = 1;
        }  

        

    }

    private void SpawnPin()
    {    
        if(broj_pinova != 0)
        {
            broj_pinova--;
            Instantiate(pinprefab, transform.position, transform.rotation); //za spawner
          //  Debug.Log("Spawner");
        }
        
    }

   



}
