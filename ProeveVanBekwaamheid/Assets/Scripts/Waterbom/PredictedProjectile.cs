﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictedProjectile : MonoBehaviour
{
    public Transform firePoint;
    public Rigidbody bullet;
    public GameObject predictedPosition;
    public LayerMask layer;

    public Camera downCam;
    public Camera rightCam;
    public Camera upCam;
    public Camera leftCam;
    private Camera cam;
    // Use this for initialization
    void Start()
    {
        cam = rightCam; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cam = downCam;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cam = upCam;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cam = rightCam;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cam = leftCam;
        }

        fireProjectile();

    }

    void fireProjectile()
    {
        //Calcutates the position of the mouse in the game world
        Ray CamRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

      
        if (Physics.Raycast(CamRay, out hit, 100f, layer))
        { 
            predictedPosition.SetActive(true);
            //Makes the predictPosition follow the mouse 
            predictedPosition.transform.position = hit.point + Vector3.up * 1f;

            Vector3 Vo = calculateVelocity(hit.point, transform.position, 1f);
            transform.rotation = Quaternion.LookRotation(Vo);
            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody obj = Instantiate(bullet, firePoint.position, Quaternion.identity);
                obj.velocity = Vo;
            }
           
        }
    }

    Vector3 calculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //Defines the distance x and y
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
    }

