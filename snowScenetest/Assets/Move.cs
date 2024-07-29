using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f;
    public float xValue;
    public float zValue;
    [SerializeField] GameObject target;

    public Vector3 MovementScale;
    void Update()
    {
        
        xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;


        transform.Translate(xValue, 0, zValue);

        if(zValue > 0.01f || xValue > 0.01f)
        {
            Vector3 objectScale = Vector3.zero;
            MovementScale = objectScale;
            MovementScale.x += Mathf.Abs(xValue/10);
            MovementScale.y += Mathf.Abs(xValue/20 + zValue/20);
            MovementScale.z += Mathf.Abs(zValue/10);

            gameObject.transform.localScale += MovementScale;
        }
    }
}