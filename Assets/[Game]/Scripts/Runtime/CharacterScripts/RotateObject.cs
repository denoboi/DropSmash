using System.Collections;
using System.Collections.Generic;
using UnityEngine;



	//This simple script continually rotates the gameobject it is attached to around a chosen axis;
	//It is used in the 'ExternalCameraScene' to demonstrate a camera setup where camera and character are separate gameobjects;
    public class RotateObject : MonoBehaviour
    {
		Transform tr;
		//Speed of rotation;
		public float rotationSpeed = 20f;
		//Axis used for rotation;
		public Vector3 rotationAxis = new Vector3(0f, 1f, 0f);

        
        void Start()
        {
            //Get transform component reference;
            tr = transform;
            
        }

        
        void Update()
        {
            Rotate();
        }

        // if hammer object collides with the ground, destroy this script in order to stop rotation of the hammer.
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                Destroy(this);
            }
        }

        public void Rotate()
        {
            tr.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        }
       
    }


