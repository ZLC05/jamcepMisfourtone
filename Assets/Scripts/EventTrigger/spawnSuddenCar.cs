using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSuddenCar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] cars;

    public GameObject spawnTransform;
    public GameObject endpoint;

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
            spawnCar();
        }
    }


   void spawnCar()
    {
        int carID = Random.Range(0, cars.Length);
        Debug.Log("Spawned Car = " + carID);
        GameObject car = Instantiate(cars[carID], spawnTransform.transform.position, Quaternion.identity);

        car.GetComponent<carMove>().endPoint = endpoint;

        Destroy(car, 3f);
    }
}
