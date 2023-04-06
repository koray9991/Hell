using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang : MonoBehaviour
{
    public ParticleSystem blood;
    public void Blood()
    {
        blood.Play();
    }
}
