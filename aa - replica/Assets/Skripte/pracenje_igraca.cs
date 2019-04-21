using UnityEngine;
using System.Collections;

public class pracenje_igraca : MonoBehaviour
{
    private float t;
    private float startTime;
    private GameObject krug;

    [HideInInspector]
    public int kontrola_animacije = 0;

    [HideInInspector]
    public Vector3 startpositon;
    private Color startColor;

    [HideInInspector]
    public bool set_start_time = false;


    private GameObject[] kanvas;


    public float brzina_vremena = 1.1f;
    public Color za_game_over = Color.red, za_win = new Color32(75, 143, 48, 255);


    private float start_size;

    private void Start()
    {
        kanvas = GameObject.FindGameObjectsWithTag("drugi_kanvas");
        krug = GameObject.Find("Krug"); 
        //krug.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));   
        startpositon = krug.transform.position;
        //Debug.Log(startpositon);
        startColor = Camera.main.backgroundColor;
        start_size = Camera.main.orthographicSize;
    }


    private void Update()
    {
        // Debug.Log(kontrola_animacije);

        // Debug.Log(set_start_time);

        if (kontrola_animacije == 2)
        {
            Set_the_start_time();
            level_completed();

        }

        else if (kontrola_animacije == 1)
        {
            Set_the_start_time();
            game_over();

        }

        else if (kontrola_animacije == 3)
        {
            Set_the_start_time();
            idle();

        }

        foreach(GameObject child in kanvas)
        {
            child.transform.position = krug.transform.position;
        }

    }

    private void game_over()
    {

        t = (Time.time - startTime) / brzina_vremena;
        Camera.main.backgroundColor = za_game_over;
        Camera.main.orthographicSize = Mathf.SmoothStep(start_size, 2.71f, t);


        krug.transform.position = new Vector3(Mathf.SmoothStep(krug.transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)).x, t), Mathf.SmoothStep(krug.transform.position.y, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)).y, t),0);  //stavljavmo krug na sredninu ekrana
        //Debug.Log(krug.transform.position);


    }

    private void level_completed()
    {
        t = (Time.time - startTime) / brzina_vremena;
        Camera.main.backgroundColor = za_win;
        Camera.main.orthographicSize = Mathf.SmoothStep(start_size, 2.71f, t);
        krug.transform.position = new Vector3(Mathf.SmoothStep(krug.transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)).x, t), Mathf.SmoothStep(krug.transform.position.y, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)).y, t), 0);
    }


    private void idle()
    {
        t = (Time.time - startTime) / brzina_vremena;
        Camera.main.backgroundColor = startColor;
        Camera.main.orthographicSize = Mathf.SmoothStep(2.71f, start_size, t+0.3f);
        krug.transform.position = new Vector3(Mathf.SmoothStep(krug.transform.position.x, startpositon.x,t), Mathf.SmoothStep(krug.transform.position.y, startpositon.y, t), 0);
        if (Camera.main.orthographicSize >= start_size) 
        {
            kontrola_animacije = 0;
            set_start_time = false;
        }
    }

    private void Set_the_start_time()
    {
        
        if (!set_start_time)
        {
            set_start_time = true;
            startTime = Time.time;
        }
    }
        





}