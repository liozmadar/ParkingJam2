using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Car : MonoBehaviour
{
    public TouchCars touchCars;

    public bool carCanNowGoToHomePoint;
    public GameObject carsGoHomePoint;

    private bool CarHitObjectFromFront;
    private bool CarHitObjectFromBack;

    public bool carCanDrive;
    public bool carCanDriveBackward;

    private ParticleSystem carhitSparkFront;
    public ParticleSystem carhitSparkBack;

    public Animator anim;

    public GameObject exclamation;
    private bool exclamationBool;
    private bool exclamationTheCollisionCar = true;
    private float exclamationTimer = 1;

    public CarGoHomePath carGoHomePath;

    // Start is called before the first frame update
    void Start()
    {
        carhitSparkFront = GetComponentInChildren<ParticleSystem>(carhitSparkFront);
        anim = GetComponent<Animator>();
        carGoHomePath = GetComponent<CarGoHomePath>();
    }
    // Update is called once per frame
    void Update()
    {
        MoveCarForward();
        MoveCarBackward();

        CarHitAnObjectFromFront();
        CarHitAnObjectFromBack();

        if (exclamationBool)
        {
            exclamation.SetActive(true);
            exclamationTimer -= Time.deltaTime;
            if (exclamationTimer < 0)
            {
                exclamationBool = false;
                exclamationTimer = 1;
                exclamation.SetActive(false);
            }
        }
    }
    void CarHitAnObjectFromFront()
    {
        if (CarHitObjectFromFront)
        {
            touchCars.hitInfo.transform.position -= transform.forward * touchCars.speed * Time.deltaTime;
        }
    }
    void CarHitAnObjectFromBack()
    {
        if (CarHitObjectFromBack)
        {
            touchCars.hitInfo.transform.position += transform.forward * touchCars.speed * Time.deltaTime;
        }
    }
    void MoveCarForward()
    {
        if (carCanDrive)
        {

            touchCars.hitInfo.transform.position += transform.forward * touchCars.speed * Time.deltaTime;
            touchCars.cantTouchTheCar = false;

            touchCars.alreadyClicked = true;
            exclamationTheCollisionCar = false;
        }
    }
    void MoveCarBackward()
    {
        if (carCanDriveBackward)
        {
            touchCars.hitInfo.transform.position -= transform.forward * touchCars.speed * Time.deltaTime;
            touchCars.cantTouchTheCar = false;

            touchCars.alreadyClicked = true;
            exclamationTheCollisionCar = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car" || collision.gameObject.tag == "CarRight" || collision.gameObject.tag == "ParkingObjects")
        {
            if (carCanDrive == true)
            {
                carCanDrive = false;
                CarHitObjectFromFront = true;
                touchCars.cantTouchTheCar = true;
                Invoke("CarHitObjectFromFrontStop", 0.03f);
                carhitSparkFront.Play(true);
            }
            else if (carCanDriveBackward == true)
            {
                carCanDriveBackward = false;
                CarHitObjectFromBack = true;
                touchCars.cantTouchTheCar = true;
                Invoke("CarHitObjectFromBackStop", 0.03f);
                carhitSparkBack.Play(true);
            }
            touchCars.alreadyClicked = false;
            anim.SetTrigger("GetHit");
            if (exclamationTheCollisionCar)
            {
                exclamationBool = true;
            }
            else if (!exclamationTheCollisionCar)
            {
                exclamationTheCollisionCar = true;
            }
        }     
    }
    void CarHitObjectFromFrontStop()
    {
        CarHitObjectFromFront = false;
    }
    void CarHitObjectFromBackStop()
    {
        CarHitObjectFromBack = false;
    }
}
