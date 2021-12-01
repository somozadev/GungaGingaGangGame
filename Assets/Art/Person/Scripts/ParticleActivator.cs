using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleActivator : MonoBehaviour
{
    [SerializeField]private ParticleSystem stunParticle;
    [SerializeField]private ParticleSystem runparticle;
    [SerializeField] private ParticleSystem pushParticle;

    public bool running;
    public bool stuned;
    // Start is called before the first frame update
    void Start()
    {
        stuned = false;
        running = false;
        runparticle.Stop();
        //pushParticle.Stop();
        stunParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if(!runparticle.isPlaying)
             {
                 runparticle.Play();
             }
        }else{
            runparticle.Stop();
        }

        if (stuned)
        {
            if(!stunParticle.isPlaying)
             {
                 stunParticle.Play();
             }
        }else{
            stunParticle.Stop();
        }
    }

    public void SetStun(bool condition){
        stuned = condition;
    }

    public void SetRun(bool condition){
        running = condition;
    }
}
