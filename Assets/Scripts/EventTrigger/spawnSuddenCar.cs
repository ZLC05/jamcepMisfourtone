using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSuddenCar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] cars;

    public List<Transform> nodes;

    public bool spanedCar;

    public float spawnDelay;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Spawning Car");
            //audio file here
            Invoke("spawnCar", spawnDelay);
        }
    }


   void spawnCar()
    {
        if (!spanedCar)
        {
            int carID = Random.Range(0, cars.Length);
            Debug.Log("Spawned Car = " + carID);
            GameObject car = Instantiate(cars[carID], nodes[0].transform.position, Quaternion.identity);

            car.GetComponent<carMove>().nodes = nodes;


            spanedCar = true;

            

        }

    }
}
