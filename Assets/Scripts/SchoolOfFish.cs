using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolOfFish : MonoBehaviour
{
    public GameObject fishPrefab;
    public int amountOfFish;
    public float schoolRadius;
    
    int waveValue;

    public bool clockwise;
    public float direction;
    public float turnspeed;

    public int turnTimer;
    public int turnTimer_Max;

    public float schoolSpeed;

    [System.Serializable]public class FishData
    {
        public Vector3 originalPosition;
        public Transform trans;

        public float speed;
        public float vibrationReach;

        public FishData(Transform trans_in, Vector3 pos_in, float speed_in, float vibrationReach_in)
        {
            originalPosition = pos_in;
            trans = trans_in;
            speed = speed_in;
            vibrationReach = vibrationReach_in;
        }
    }
    public List<FishData> fish;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amountOfFish; i++)
        {
            Vector3 pos = new Vector3(Mathf.Cos(i), Mathf.Sin(i),0) * Random.Range(0, schoolRadius);
            GameObject temp = Instantiate(fishPrefab, pos, Quaternion.identity);
            temp.transform.parent = transform;
            fish.Add(new FishData(temp.transform, pos, Random.Range(0.1f, 0.25f), Random.Range(0.05f, 0.15f)));
        }
    }
    private void FixedUpdate() 
    {
        for(int i = 0; i < fish.Count; i++)
        {
            fish[i].trans.localPosition = new Vector3(fish[i].originalPosition.x + Mathf.Cos((float)waveValue * fish[i].speed) * fish[i].vibrationReach, fish[i].originalPosition.y, 0);
        }
        waveValue++;

        direction += clockwise? turnspeed : -turnspeed;
        Vector3 normal = (new Vector3(Mathf.Cos(direction), Mathf.Sin(direction))).normalized;
        Quaternion target = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;
        float directionChangeSpeed = 0.1f;
        target = new Quaternion(0,
                                0,
                                Mathf.Lerp(transform.rotation.z, target.z, directionChangeSpeed),
                                Mathf.Lerp(transform.rotation.w, target.w, directionChangeSpeed));
        transform.rotation = target;
        transform.position += normal * schoolSpeed;

        turnTimer++;
        if(turnTimer >= turnTimer_Max)
        {
            turnTimer = 0;
            turnTimer_Max = Random.Range(20, 100);
            clockwise = !clockwise;
        }
    }
}
