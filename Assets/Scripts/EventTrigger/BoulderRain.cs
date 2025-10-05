using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRain : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BOULDER;

    public float boulderSpawnRate;

    public bool startBoulderRain;


    public float xminLimit, xmaxLimit;
    public float zminLimit, zmaxLimit;
    public float y;


    public float destroyTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startSpawningBoulders()
    {
        if (!startBoulderRain)
        {
            InvokeRepeating("boulder", 0, boulderSpawnRate);
            startBoulderRain = true;
        }
    }


    void boulder()
    {
        float x = Random.Range(xminLimit, xmaxLimit);
        float z = Random.Range(zminLimit, zmaxLimit);

        GameObject b = Instantiate(BOULDER, new Vector3(x, y, z), Quaternion.identity);

        Destroy(b, destroyTime);

    }
}
