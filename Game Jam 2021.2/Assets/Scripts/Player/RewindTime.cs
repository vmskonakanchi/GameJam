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
        RecordTime();
        if (Input.GetKey(KeyCode.Tab))
        {
            Rewindtime();
        }
    }
    private void RecordTime()
    {
        position.Insert(0, transform.position);
        rotation.Insert(0, transform.rotation);
        // if (position.Count > Mathf.RoundToInt(5f / Time.fixedDeltaTime))
        //{
        //     position.RemoveAt(position.Count - 1);
        //}
        // else if (rotation.Count > Mathf.RoundToInt(5f / Time.fixedDeltaTime))
        //{
        //     rotation.RemoveAt(rotation.Count - 1);
        // }     
        if (position.Count > 0)
        {
            Debug.Log("recording Positons");
        }
    }
    void Rewindtime()
    {
        if (position.Count > 0)
        {
            transform.position = position[0];
            position.RemoveAt(0);
        }
        else if (position.Count == 0)
        {
            Debug.Log("Not Recording positions");
        }
        else if (rotation.Count > 0)
        {
            transform.rotation = rotation[0];
            rotation.RemoveAt(0);
        }
    }
}
