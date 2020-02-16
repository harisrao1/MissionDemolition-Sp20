using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; //static point of interest

    [Header("Set Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; //The desired Z pos of the camera

    private void Awake()
    {
        
        camZ = this.transform.position.z;
        
    }

    void FixedUpdate()
    {
        Vector3 destination;

        if(POI == null)
        {
            destination = Vector3.zero;
        }
        else{
            //get position of the poi
            destination = POI.transform.position;
            if(POI.tag == "Projectile")
            {
                //if it is sleeping (that is, not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }

        
        //limit X and Y minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //Force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        //Set the camera to the destination
        transform.position = destination;
        //Set the orthoggraphicSize of the Camera to keep Ground in View
        Camera.main.orthographicSize = destination.y + 10;


    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
