using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

abstract public class reticleController : MonoBehaviour
{
    [SerializeField]
    protected RectTransform reticle;

    abstract public bool whetherShot();
    abstract public bool whetherGuard();
}
