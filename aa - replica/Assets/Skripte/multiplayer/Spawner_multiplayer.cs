using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_multiplayer : MonoBehaviour
{
    public bool kontrola_izbacivanja;  //komentar napisan u skripti Pin_multiplayer
    public bool smer = true; //ako je true igrac 1, ako je false igrac2

    public GameObject igrac1;  //igrac1
    public GameObject igrac2;  //igrac2

    public GameObject pin_igrac1;  //pin za igrac1
    public GameObject pin_igrac2;  //pin za igrac2

   

    private void Update()
    {
        

        if (kontrola_izbacivanja == true)
        {
            kontrola_izbacivanja = false;

            //stvara se ili igrac1 ili igrac2, u zavisnosti od smera

            if (smer == true)
            {
                Instantiate(pin_igrac1, igrac1.transform.position, igrac1.transform.rotation);
                
               // Debug.Log("Kreiran pin za igraca1");
            }
            else 
            {
                Instantiate(pin_igrac2, igrac2.transform.position,igrac2.transform.rotation);
                         
                //Debug.Log("Kreiran pin za igraca2");
            }
        }
     }







}
