using UnityEngine;
using System.Collections;

public class carController : MonoBehaviour {



    public static carController instance;
	public WheelJoint2D frontwheel;
	public WheelJoint2D backwheel;

	JointMotor2D motorFront;

	JointMotor2D motorBack;

	public float speedF;
	public float speedB;

	public float torqueF;
	public float torqueB;


	public bool TractionFront = true;
	public bool TractionBack = true;


	public float carRotationSpeed;

	// Use this for initialization

    private void Awake(){

        if(instance == null){

            instance = this;
        }
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	

    public void StartCarMovement()
    {
        if (TractionFront)
        {
            motorFront.motorSpeed = speedF * -1;
            motorFront.maxMotorTorque = torqueF;
            frontwheel.motor = motorFront;
        }

        if (TractionBack)
        {
            motorBack.motorSpeed = speedF * -1;
            motorBack.maxMotorTorque = torqueF;
            backwheel.motor = motorBack;
        }
    }

	public void StopCarMovement()
	{
        frontwheel.useMotor = false;
        backwheel.useMotor = false;
    }

}
