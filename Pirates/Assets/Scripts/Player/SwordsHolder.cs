using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsHolder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.localScale = player.localScale;
    }
}
