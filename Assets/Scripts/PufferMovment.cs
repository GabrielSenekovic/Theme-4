using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferMovment : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    private int timer;
    public int timer_max;
    public float bubbleSpeed;
    public float playerSpeed;
    public float shrinkRate;
    public float growthspeed;

    public float shrinkSpeed;

    private float startSizePrev; 
    public float min_size;
    public float max_size;
    public GameObject bubble;
    private bool shrink = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorToTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Quaternion q = Quaternion.AngleAxis(Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,q ,360);
        
        Vector3 tempScale = transform.localScale;
        if(Input.GetMouseButtonDown(0) && timer >= timer_max && tempScale.x > min_size)
        {
            GameObject tempBubble = GameObject.Instantiate(bubble, transform.position, Quaternion.identity);
            tempBubble.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * bubbleSpeed);
            rb.AddForce(transform.up * -playerSpeed);
            timer = 0;
            shrink = true;
            startSizePrev = tempScale.x;
        }
        
        Vector3 camPos = Camera.main.transform.position;
        float dist = (transform.position - camPos).magnitude;
        Vector2 tempPos = Vector2.Lerp(camPos, transform.position, 0.001f + dist * 0.001f);
        Camera.main.transform.position = new Vector3(tempPos.x, tempPos.y, camPos.z);
    }

    private void FixedUpdate() 
    {
        if(timer < timer_max)
        {
            timer++;
        }
        Vector3 tempScale = transform.localScale;
        if(shrink)
        {
            tempScale.x = Mathf.Lerp(tempScale.x, tempScale.x - shrinkRate, shrinkSpeed);
            tempScale.y =  Mathf.Lerp(tempScale.y, tempScale.y - shrinkRate, shrinkSpeed);
            if(tempScale.x <= startSizePrev - shrinkRate)
            {
                shrink = false;
            }
        }
        else
        {
            tempScale.x = Mathf.Lerp(tempScale.x, max_size, growthspeed);
            tempScale.y = Mathf.Lerp(tempScale.y, max_size, growthspeed);
        }
        transform.localScale = tempScale;
    }
}
