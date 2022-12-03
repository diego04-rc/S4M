using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject enemigo1;
    [SerializeField]
    private GameObject enemigo2;
    [SerializeField]
    private float ratioEnemigo1;
    [SerializeField]
    private float ratioEnemigo2;
    [SerializeField]
    private int minEnemigos;
    [SerializeField]
    private int maxEnemigos;
    [SerializeField]
    private GameObject enemyZone;
    [SerializeField]
    private int numPoints;
    [SerializeField]
    private int tiempoEsperaMin;
    [SerializeField]
    private int tiempoEsperaMax;
    [SerializeField]
    private float alturaGeneracion;
    private float posY;
    private float posX;
    private float posZ;
    private float longX;
    private float longY;
    private float longZ;
    private float tiempo;
    public bool activo;
    private Transform[] generatorPoints;

    void Start()
    {
        posY = transform.position.y;
        posX = transform.position.x;
        posZ = transform.position.z;
        longX = transform.localScale.x;
        longY = transform.localScale.y;
        longZ = transform.localScale.z;
        tiempo = 0f;
        activo = false;
        generatorPoints = new Transform[maxEnemigos];
        for(int i = 0; i < maxEnemigos; i++)
        {
            generatorPoints[i] = transform.GetChild(i);
        }
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while (true)
        {
            int t = UnityEngine.Random.Range(tiempoEsperaMin, tiempoEsperaMax);
            yield return new WaitForSeconds(t);
            GenerarEnemigo();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerarEnemigo()
    {
        /*if (generadorHorizontal)
        {
            float puntoGeneracionX = generadorNumero.Next((int)(posX - longX / 2), (int)(posX + longX / 2));
            GenerarEnemigoHorizontal(puntoGeneracionX);
        }
        else
        {
            float puntoGeneracionY = generadorNumero.Next((int)(posY - longY / 2), (int)(posY + longY / 2));
            GenerarEnemigoVertical(puntoGeneracionY);
        }*/
        if (activo)
        {
            int numEnemigos = UnityEngine.Random.Range(minEnemigos, maxEnemigos);

            int[] generatorPos = new int[numEnemigos];

            generatorPos[0] = UnityEngine.Random.Range(0, maxEnemigos);
            for (int i = 1; i < numEnemigos; i++)
            {
                int pos = 0;
                bool noRepetido = false;
                while (!noRepetido)
                {
                    pos = UnityEngine.Random.Range(0, maxEnemigos);
                    int j = 0;
                    for (j = 0; j < i && pos != generatorPos[j]; j++) ;
                    noRepetido = (j == i);
                }
                generatorPos[i] = pos;
            }

            int numEnemTipo1 =(int) (ratioEnemigo1 * numEnemigos);

            int numEnemTipo2 = (int)(ratioEnemigo2 * numEnemigos);

            /*float[] posEnemigosX = new float[numEnemigos];
            float[] posEnemigosZ = new float[numEnemigos];
            for (int i = 0; i < numEnemigos; i++)
            {
                float puntoGeneracionX = UnityEngine.Random.Range((posX - longX / 2), (posX + longX / 2));
                float puntoGeneracionZ = UnityEngine.Random.Range((posZ - longZ / 2), (posZ + longZ / 2));
                posEnemigosX[i] = puntoGeneracionX;
                posEnemigosZ[i] = puntoGeneracionZ;
            }*/
            for (int i = 0; i < numEnemTipo1; i++)
            {
                enemigo1.GetComponentInChildren<DetectorPatrolPoint>().setEnemyZone(enemyZone);
                enemigo1.GetComponentInChildren<DetectorPatrolPoint>().setNumPoints(numPoints);
                Instantiate(enemigo1, generatorPoints[generatorPos[i]].position, transform.rotation);
            }
            for (int i = numEnemTipo1; i < (numEnemTipo1 + numEnemTipo2); i++)
            {
                enemigo2.GetComponentInChildren<DetectorPatrolPoint>().setEnemyZone(enemyZone);
                enemigo2.GetComponentInChildren<DetectorPatrolPoint>().setNumPoints(numPoints);
                Instantiate(enemigo2, generatorPoints[generatorPos[i]].position, transform.rotation);
            }
        }
    }

    /* private void GenerarEnemigoHorizontal(float puntoGeneracionX)
     {
         float rand = UnityEngine.Random.value;
         rand *= 100;
         if (rand < 100 / 4)
             Instantiate(enemigo1, new Vector2(puntoGeneracionX, posY), transform.rotation);
         else if (rand < 2 * 100 / 4)
             Instantiate(enemigo2, new Vector2(puntoGeneracionX, posY), transform.rotation);
         else if (rand < 3 * 100 / 4)
             Instantiate(enemigo3, new Vector2(puntoGeneracionX, posY), transform.rotation);
         else
             Instantiate(enemigo4, new Vector2(puntoGeneracionX, posY), transform.rotation);
     }

     private void GenerarEnemigoCentral()
     {
         GameObject creado;
         float rand = UnityEngine.Random.value;
         rand *= 100;
         if (rand < 100 / 4)
             creado = Instantiate(enemigo1, transform.position, transform.rotation);
         else if (rand < 2 * 100 / 4)
             creado = Instantiate(enemigo2, transform.position, transform.rotation);
         else if (rand < 3 * 100 / 4)
             creado = Instantiate(enemigo3, transform.position, transform.rotation);
         else
             creado = Instantiate(enemigo4, transform.position, transform.rotation);
     }

     private void GenerarEnemigoVertical(float puntoGeneracionY)
     {
         float rand = UnityEngine.Random.value;
         rand *= 100;
         if (rand < 100 / 4)
             Instantiate(enemigo1, new Vector2(posX, puntoGeneracionY), transform.rotation);
         else if (rand < 2 * 100 / 4)
             Instantiate(enemigo2, new Vector2(posX, puntoGeneracionY), transform.rotation);
         else if (rand < 3 * 100 / 4)
             Instantiate(enemigo3, new Vector2(posX, puntoGeneracionY), transform.rotation);
         else
             Instantiate(enemigo4, new Vector2(posX, puntoGeneracionY), transform.rotation);
     }

     public void activar()
     {
         activo = true;
     }

     public void desactivar()
     {
         activo = false;
     }

     public void nivel(int nivel)
     {
         switch (nivel)
         {
             case 0:
                 enemigo1 = circulo;
                 enemigo2 = circulo;
                 enemigo3 = circulo;
                 enemigo4 = circulo;
                 tiempoEsperaMin = 5;
                 tiempoEsperaMax = 10;
                 break;
             case 1:
                 enemigo1 = circulo;
                 enemigo2 = circulo;
                 enemigo3 = circulo;
                 enemigo4 = triangulo;
                 tiempoEsperaMin = 5;
                 tiempoEsperaMax = 10;
                 break;
             case 2:
                 enemigo1 = circulo;
                 enemigo2 = circulo;
                 enemigo3 = circulo;
                 enemigo4 = triangulo;
                 tiempoEsperaMin = 4;
                 tiempoEsperaMax = 6;
                 break;
             case 3:
                 break;
             default:
                 break;
         }
     }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activo = false;
        }
    }*/

    public void setActivo(bool activo)
    {
        this.activo = activo;
    }
}
