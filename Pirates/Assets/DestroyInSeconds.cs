using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] private float secondToDesTroy = 1f;
    void Start()
    {
        Destroy(gameObject,secondToDesTroy);
    }
}
