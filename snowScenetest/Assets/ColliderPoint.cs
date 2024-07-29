using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ColliderPoint : MonoBehaviour
{
    public GameObject rock;
    Move move;
    void Strar()
    {
        move = GetComponent<Move>();
    }

    void OnCollisionEnter(Collision other)
    {
       //switch(other.gameObject)
       //{
       //    case (rock):
       //    {
       //        move.MovementScale -= Mathf.Abs(move.xValue);
       //        gameObject.transform.localScale -= move.MovementScale;
       //        break;
       //    }
       //}
    }
}
