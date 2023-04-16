using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Domino : MonoBehaviour
{
    public float magnification;
    public float weight;

    public Vector3 DominoScale;
    public Vector3 DominoPosition;

    public InputField inputField;
    public GameObject dominoGameobject;

    public GameObject firstDomino;
    
    private void Start()
    {
        /* Initialize */
        weight = 1f;
        magnification = 1f;
        DominoScale = new Vector3(0.2f, 2f, 1f);
        DominoPosition = new Vector3(-14f, 2f, 0);
        /**/

        // Making dominos couroutine ( magnification = 1.1f )
        StartCoroutine("MakeDominoCoroutine");
    }
    public void MakeDomino()
    {
        // Set Magnification
        magnification = float.Parse(inputField.text); 

        /* Initialize */
        weight = 1f;
        DeleteAllDominos();
        DominoScale = new Vector3(0.2f, 2f, 1f);
        DominoPosition = new Vector3(-14f, 2f, 0);
        /**/

        // Making dominos couroutine 
        StopCoroutine("MakeDominoCoroutine");
        StartCoroutine("MakeDominoCoroutine");
    }

    IEnumerator MakeDominoCoroutine()
    {
        for (int i = 0; i < 10; i++) // Make 10 dominos
        {
            yield return new WaitForSeconds(0.3f);

            // Raise the domino by 1f off the ground
            DominoPosition.y = (DominoScale.y / 2) + 1f;

            // Placed at a certain distance
            DominoPosition.x += (DominoScale.y / 2);

            // Instantiate
            GameObject domino = Instantiate(dominoGameobject, DominoPosition, Quaternion.identity);

            // Set scale
            DominoScale = new Vector3(DominoScale.x * magnification, DominoScale.y * magnification, DominoScale.z * magnification);
            domino.transform.localScale = DominoScale;

            // Set firstdomino
            if (i == 0) 
                firstDomino = domino;

            // Set Domino weight
            Rigidbody rigidbody = domino.GetComponent<Rigidbody>();
            weight *= magnification;
            rigidbody.mass = weight;
        }
    }

    public void StartDomino()
    {
        // Push First Domino
        Rigidbody r = firstDomino.GetComponent<Rigidbody>();
        r.AddForce(Vector3.right, ForceMode.Impulse);
    }
    void DeleteAllDominos() // Delete all dominos
    {
        GameObject[] dominos = GameObject.FindGameObjectsWithTag("Domino");
        foreach (GameObject domino in dominos)
        {
            Destroy(domino);
        }
    }
    
}
