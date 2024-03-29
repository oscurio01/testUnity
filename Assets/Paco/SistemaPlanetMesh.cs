using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SistemaPlanetMesh : MonoBehaviour
{

    public Mesh model;

    public Material mTierra;
    public Material mLuna;

    public float rotacionT = 30;
    public float distanciaT = 5;
    public float escalaT = 0.5f;
    public float separacionT = 2;

    public float velocidadRotacionT = 30.0f;

    public int numTierras = 3;

    public float rotacionL = 60;
    public float distanciaL = 3;
    public float escalaL = 0.2f;
    public float separacionL = 2;

    public float velocidadRotacionL = 30.0f;

    public int numEstrellas = 200;

    public Material mEstrella;

    public float anchoCampoEstrellas = 300;
    public float altoCampoEstrellas = 200;
    public float distanciaCampoEstrellas = 100;

    public float velocidadEscalaEstrellas = 0.1f;

    CommandBuffer commandsEstrellas;
    CommandBuffer commandsPlanetas;

    Vector3[] posicionesEstrellas;
    float[] escalasEstrellas;

    Matrix4x4[] matricesEstrella;
    Matrix4x4[] matricesEstrellaEscaladas;

    float escalaEstrella = 1;

    void Start()
    {
        posicionesEstrellas = new Vector3[numEstrellas];

        matricesEstrella = new Matrix4x4[numEstrellas];
        
        matricesEstrellaEscaladas = new Matrix4x4[numEstrellas];

        escalasEstrellas = new float[numEstrellas];

        for (int i = 0; i < numEstrellas; i++)
        {
            posicionesEstrellas[i] = new Vector3(
                                                  UnityEngine.Random.Range(-anchoCampoEstrellas/2, anchoCampoEstrellas/2),
                                                  UnityEngine.Random.Range(-altoCampoEstrellas/2, altoCampoEstrellas/2),
                                                  distanciaCampoEstrellas
                                                  );
            matricesEstrella[i] = Matrix4x4.Translate(posicionesEstrellas[i]);

            escalasEstrellas[i] = UnityEngine.Random.Range(1, 2.0f);

        }

        commandsEstrellas = new CommandBuffer();

        for (int i = 0; i < numEstrellas; i++)
        {
            float s = escalasEstrellas[i] * escalaEstrella;
            matricesEstrellaEscaladas[i] = matricesEstrella[i] * Matrix4x4.Scale(new Vector3(s, s, 1));
        }

        //Graphics.DrawMeshInstanced(model, 0, mEstrella, matricesEstrellaEscaladas);
        commandsEstrellas.DrawMeshInstanced(model, 0, mEstrella, -1, matricesEstrellaEscaladas, numEstrellas);

        //escalaEstrella += velocidadEscalaEstrellas * Time.deltaTime;

        //if (escalaEstrella > 3.0f)
        //{
        //    escalaEstrella = 0;
        //}

        Camera.main.AddCommandBuffer(CameraEvent.BeforeForwardAlpha, commandsEstrellas);
        

        commandsPlanetas = new CommandBuffer();

        Camera.main.AddCommandBuffer(CameraEvent.BeforeForwardAlpha, commandsPlanetas);
    }

    // Update is called once per frame
    void Update()
    {
        commandsPlanetas.Clear();

        Matrix4x4 MSol = transform.localToWorldMatrix;

        for(int i = 0; i < numTierras; i++)
        {
            Matrix4x4 MTierra = MSol * Matrix4x4.Rotate(Quaternion.Euler(0,0, rotacionT + i * (360.0f/ numTierras))) *
                           Matrix4x4.Translate(new Vector3(distanciaT + i * separacionT, 0, 0)) *
                           Matrix4x4.Scale(new Vector3(escalaT, escalaT, escalaT));

            //Graphics.DrawMesh(model, MTierra, mTierra, 0);
            commandsPlanetas.DrawMesh(model, MTierra, mTierra, 0, -1, null);
            

            Matrix4x4 MLuna = MTierra * Matrix4x4.Rotate(Quaternion.Euler(Vector3.forward * rotacionT)) *
                                       Matrix4x4.Translate(new Vector3(distanciaL, 0, 0)) *
                                       Matrix4x4.Scale(new Vector3(escalaL, escalaL, escalaL));

            //Graphics.DrawMesh(model, MLuna, mLuna, 0);
            commandsPlanetas.DrawMesh(model, MLuna, mLuna, 0, -1, null);
            
        }

        rotacionT += velocidadRotacionT * Time.deltaTime;
        rotacionL += velocidadRotacionL * Time.deltaTime;


    }

}
