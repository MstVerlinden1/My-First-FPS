using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePointLogic : MonoBehaviour
{
    [SerializeField] private bool reusable;
    private bool canGrapple;
    private bool grappled;
    /// <summary>
    /// Read only
    /// </summary>
    public bool CanGrapple
    {
        get { return canGrapple; }
    }

    public void Grappled()
    {
        if (!reusable)
        {
            canGrapple = false;
        }
    }
}
