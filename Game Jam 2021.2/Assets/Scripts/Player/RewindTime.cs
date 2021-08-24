using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class RewindTime : MonoBehaviour
{
    List<Vector2> position; 
    List<Quaternion> rotation; 

   
    void Start()
    {
            position = new List<Vector2>();
            rotation = new List<Quaternion>();
    }

      
     void Update()
     {
           
     }


    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            RecordTime();
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            Rewindtime();
        }
    }

    void RecordTime()
    {
        if(rotation.Count > Mathf.RoundToInt (5f/Time.fixedDeltaTime) && rotation.Count > Mathf.RoundToInt (5f / Time.fixedDeltaTime))
;       {
            position.RemoveAt(position.Count - 1);
            rotation.RemoveAt(rotation.Count - 1);
        }
        position.Insert(0, transform.position);
        rotation.Insert(0, transform.rotation);
    }

    void Rewindtime()
    {
        if(rotation.Count > 0 && rotation.Count > 0)
        {
            transform.position = position[0];
            position.RemoveAt(0);
            transform.rotation = rotation[0];
            rotation.RemoveAt(0);
        }
    }
    


}
