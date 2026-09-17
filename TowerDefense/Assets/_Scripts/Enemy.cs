using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject objetivo;
    public int vida = 70;
    public Animator Anim;

    private NavMeshAgent agent;

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
            Debug.LogError("Enemy: no se encontró un GameObject llamado 'Objetivo' en la escena.", this);
            return;
        }

        agent.SetDestination(objetivo.transform.position);
        Anim.SetBool("IsMoving", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objetivo"))
        {
            Anim.SetBool("IsMoving", false);
            Anim.SetTrigger("OnObjectiveReached");
        }
    }

    public void Danar()
    {
        if (objetivo == null) return;

        Objetivo objetivoScript = objetivo.GetComponent<Objetivo>();
        if (objetivoScript != null)
        {
            objetivoScript.RecibirDano(40);
        }
    }

    public void RecibirDano(int dano = 7)
    {
        vida -= dano;
    }
}