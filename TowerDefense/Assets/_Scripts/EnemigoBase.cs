using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemigoBase : MonoBehaviour, IAtacante, IAtacable
{
    public GameObject objetivo;
    public int vida = 100;
    public int _dano = 5;
    public int recursosGanados = 200;
    public AdminJuego referenciaAdminJuego;
    public SpawnerEnemigos referenciaSpawner;
    public Animator Anim;
    public float tiempoAnimacionMuerte = 3f;

    private NavMeshAgent agent;
    private bool estaMuerto = false;

    private void OnEnable()
    {
        objetivo = GameObject.Find("Objetivo");
        referenciaAdminJuego = GameObject.Find("AdminJuego").GetComponent<AdminJuego>();
        referenciaSpawner = GameObject.Find("SpawnerEnemigos").GetComponent<SpawnerEnemigos>();
        objetivo.GetComponent<Objetivo>().EnObjetivoDestruido += Detener;

    }


    private void OnDisable()
    {
        if (objetivo != null)
        {
            objetivo.GetComponent<Objetivo>().EnObjetivoDestruido -= Detener;
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();

        if (objetivo == null)
        {
            objetivo = GameObject.Find("Objetivo");
        }

        if (objetivo == null)
        {
            Debug.LogError("Boss: no se encontró un GameObject llamado 'Objetivo' en la escena.", this);
            return;
        }

        agent.SetDestination(objetivo.transform.position);
        Anim.SetBool("IsMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0 && !estaMuerto)
        {
            Morir();
        }
    }

    private void Morir()
    {
        estaMuerto = true;

        agent.isStopped = true;
        Anim.SetBool("IsMoving", false);
        Anim.SetTrigger("OnDeath");

        GetComponent<Collider>().enabled = false;

        Invoke(nameof(DestruirEnemigo), tiempoAnimacionMuerte);
    }

    private void DestruirEnemigo()
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        referenciaAdminJuego.ModificarRecursos(recursosGanados);
        referenciaSpawner.EnemigosGenerados.Remove(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objetivo"))
        {
            Anim.SetBool("IsMoving", false);
            Anim.SetTrigger("OnObjectiveReached");
        }
    }

    private void Detener()
    {
        Anim.SetTrigger("OnObjectiveDestroy");
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }

    public void Danar(int dano)
    {
        if (dano == 0) dano = _dano;
        {
            if (objetivo == null) return;

            Objetivo objetivoScript = objetivo.GetComponent<Objetivo>();
            if (objetivoScript != null)
            {
                objetivoScript.RecibirDano(40);
            }
        }
    }


    public void RecibirDano(int dano = 5)
    {
        vida -= dano;
    }
}