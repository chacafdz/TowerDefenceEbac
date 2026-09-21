using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class Boss : EnemigoBase
{
    private void Awake()
    {
        vida = 60;
        _dano = 20;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (referenciaAdminJuego != null)
        {
            referenciaAdminJuego.enemigosJefeDerrotados++;
        }
    }
}