using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public bool CallDrop()
    {
        return boxCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
    } 
}
