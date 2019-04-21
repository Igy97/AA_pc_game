using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class fadein : MonoBehaviour
{
    private float alpha;  //uzmimao vrednos alpha
    private Image img;  //uzimamo odrednju sliku

    private void Start()
    {
        alpha = gameObject.GetComponent<Image>().color.a;
        //Debug.Log(alpha);
        img = gameObject.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        InvokeRepeating("Alpha_change", 0f, 0.002f);  //pozivaku funkciju koja se ponavlja za odredjeno vreme
    }


    private void Alpha_change()
    {
        if (alpha > 0)
        {
            //Debug.Log(alpha);
            alpha -= 0.005f;  //smanjujemo za odredjneu vrednst
            gameObject.GetComponent<Image>().color = new Color(img.color.r, img.color.g, img.color.b, alpha); //menjamo boju slike
        }
        else 
        {
            CancelInvoke(); //prestajemo da pozivamo tu funkciju
            gameObject.transform.parent.gameObject.SetActive(false); //gasimo taj objekat
            GameObject.Find("Spawner").GetComponent<Spawner>().enabled = true;  //omugacavo da se spawner koristi
        }
    }











}

	
