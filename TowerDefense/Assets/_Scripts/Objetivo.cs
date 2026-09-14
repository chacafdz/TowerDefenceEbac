using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Objetivo : MonoBehaviour
{
    public int vida = 100;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void RecibirDano(int dano = 20)
    {
        vida -= dano;
    }
}
