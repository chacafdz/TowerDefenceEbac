using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TorreAntena : TorreBase, IAtacante
{

    public float divisionesRayo = 10;
    public LineRenderer LRRayo;
    public List<Vector3> puntos;
    public int potenciaRayo;

    public void Start()
    {
        LRRayo = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        if (enemigo != null)
        {
            Disparar();
            Danar(potenciaRayo);

        }
        else
        {
            LRRayo.positionCount = 0;
        }
    }
    public override void Disparar()
    {
        puntos = ObtenerPuntos();
        puntos.Insert(0, puntasCanon[0].transform.position);
        var posEnemigo = enemigo.transform.position;
        posEnemigo.y += 1;
        puntos.Add(posEnemigo);
        LRRayo.positionCount = puntos.Count;
        LRRayo.SetPositions(puntos.ToArray());
    }
    private List<Vector3> ObtenerPuntos()
    {
        List<Vector3> puntosTemporales = new List<Vector3>();
        float divisor = 1f / divisionesRayo;
        float linear = 0f;

        bool esPosiitvo = false;
        if (divisionesRayo == 0)
        {
            Debug.LogError("no podemos dividir entre cero por favor ingresa otro valor en el prefab");
            return puntosTemporales;

        }
        if (divisionesRayo == 1)

        {
            var punto = Vector3.Lerp(puntasCanon[0].transform.position, enemigo.transform.position, .5f);
            puntosTemporales.Add(punto);
            return puntosTemporales;
        }
        for (int i = 0; i < divisionesRayo; i++)
        {
            if (i > 0)
            {
                linear = divisor / 2;
            }
            else
            {
                linear += divisor;

            }
            var punto = Vector3.Lerp(puntasCanon[0].transform.position, enemigo.transform.position, linear);
            if (esPosiitvo)
            {
                punto.x += Random.value * 2;
                esPosiitvo = false;
            }
            else
            {
                punto.x -= Random.value * 2;
                esPosiitvo = true;
            }
            puntosTemporales.Add(punto);
        }

        return puntosTemporales;
    }

    public void Danar(int dano = 0)
    {
        enemigo.GetComponent<EnemigoBase>().RecibirDano(potenciaRayo);
    }
}