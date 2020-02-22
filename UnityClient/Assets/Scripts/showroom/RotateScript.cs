using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour
{
    public float rotationSpeed = 40f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
