using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Car : MonoBehaviour
{
    public TouchCars touchCars;

    public NavMeshAgent agent;
    public bool carCanNowGoToHomePoint;
    public GameObject carsGoHomePoint;

    private bool CarHitObjectFromFront;
    private bool CarHitObjectFromBack;

    public bool carCanDrive;
    public bool carCanDriveBackward;

    private ParticleSystem carhitSparkFront;
    public ParticleSystem carhitSparkBack;

    public Animator anim;
    private MeshCollider meshCollider;

    public GameObject exclamation;
    private bool exclamationBool;
    private bool exclamationTheCollisionCar = true;
    private float exclamationTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        carhitSparkFront = GetComponentInChildren<ParticleSystem>(carhitSparkFront);
        anim = GetComponent<Animator>();
        meshCollider = GetComponent<MeshCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        CarsGoHome();

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
    void CarsGoHome()
    {
        if (carCanNowGoToHomePoint)
        {
            agent.SetDestination(carsGoHomePoint.gameObject.transform.position);
            meshCollider.enabled = false;
        }
    }
    void MoveCarForward()
    {
        if (carCanDrive && !carCanNowGoToHomePoint)
        {

            touchCars.hitInfo.transform.position += transform.forward * touchCars.speed * Time.deltaTime;
            touchCars.cantTouchTheCar = false;

            touchCars.alreadyClicked = true;
            exclamationTheCollisionCar = false;
        }
    }
    void MoveCarBackward()
    {
        if (carCanDriveBackward && !carCanNowGoToHomePoint)
        {
            touchCars.hitInfo.transform.position -= transform.forward * touchCars.speed * Time.deltaTime;
            touchCars.cantTouchTheCar = false;

            touchCars.alreadyClicked = true;
            exclamationTheCollisionCar = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car" || collision.gameObject.tag == "FirstCarTutorial" || collision.gameObject.tag == "ParkingObjects")
        {
            if (carCanDrive == true)
            {
                carCanDrive = false;
                CarHitObjectFromFront = true;
                touchCars.cantTouchTheCar = true;
                Invoke("CarHitObjectFromFrontStop", 0.05f);
                carhitSparkFront.Play(true);
            }
            else if (carCanDriveBackward == true)
            {
                carCanDriveBackward = false;
                CarHitObjectFromBack = true;
                touchCars.cantTouchTheCar = true;
                Invoke("CarHitObjectFromBackStop", 0.05f);
                carhitSparkBack.Play(true);
            }
            touchCars.alreadyClicked = false;
            anim.SetTrigger("GetHit");
            if (exclamationTheCollisionCar)
            {
                exclamationBool = true;
            }
            else if(!exclamationTheCollisionCar)
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
