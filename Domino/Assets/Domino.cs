using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Domino : MonoBehaviour
{
    public float magnification;
    public float weight;

    Vector3 DominoScale;
    Vector3 DominoPosition;

    public InputField inputField;
    public GameObject dominoGameobject;

    public GameObject firstDomino;

    private bool started;
    
    private void Start()
    {
        /* Initialize */
        started = false;    
        weight = 1f;
        magnification = 1.1f;
        DominoScale = new Vector3(0.2f, 2f, 1f);
        DominoPosition = new Vector3(-14f, 2f, 0);
        /**/

        // Making dominos couroutine ( magnification = 1.1f )
        StartCoroutine("MakeDominoCoroutine");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //inputField.ActivateInputField();
            inputField.Select();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            started = true;
            StartDomino();
        }
    }
    public void MakeDomino()
    {
        // Check if float
        if(!float.TryParse(inputField.text, out magnification)) return;
        
        // Set Magnification
        if (magnification < 1.0f || magnification > 2f) return;

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
        GameObject domino = Instantiate(dominoGameobject, DominoPosition, Quaternion.identity);

        firstDomino = domino;

        for (int i = 0; i < 9; i++) // Make 10 dominos
        {
            yield return new WaitForSeconds(0.3f);

            // Set scale
            DominoScale = new Vector3(DominoScale.x * magnification, DominoScale.y * magnification, DominoScale.z * magnification);

            // Raise the domino by 1f off the ground
            DominoPosition.y = (DominoScale.y / 2f) + 1f;

            // Placed at a certain distance
            DominoPosition.x += (DominoScale.y / 2);

            // Instantiate
            domino = Instantiate(dominoGameobject, DominoPosition, Quaternion.identity);

            domino.transform.localScale = DominoScale;

            // Set Domino weight
            Rigidbody rigidbody = domino.GetComponent<Rigidbody>();
            weight = DominoScale.x * DominoScale.y * DominoScale.z;
            rigidbody.mass = weight;
            if (i == 8) started = false;
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
