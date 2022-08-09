using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCarCollision : MonoBehaviour
{
    public Car car;
    public TouchCars touchCars;
    public GameObject mouseTutorial;

    // Start is called before the first frame update
    void Start()
    {
        car = GetComponentInParent<Car>();
        touchCars = GameObject.Find("GameCarsManager").GetComponent<TouchCars>();
        mouseTutorial = GameObject.Find("MouseTutorial");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NavMeshRoad")
        {
            car.carCanNowGoToHomePoint = true;
            car.touchCars.alreadyClicked = false;
            car.touchCars.cantTouchTheCar = true;
            touchCars.firstCarTutorial = false;

            car.anim.enabled = false;
            mouseTutorial.SetActive(false);
        }
        if (other.gameObject.tag == "FinishLine")
        {
            car.touchCars.howManyCarsFinished++;
            car.gameObject.SetActive(false);
        }
    }
}
