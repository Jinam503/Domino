using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Domino : MonoBehaviour
{
    public float magnification;
    public Vector3 DominoScale;
    public Vector3 pos;

    public InputField inputField;
    public GameObject dominoGameobject;

    public GameObject firstDomino;

    private void Start()
    {
        magnification = 1.1f;
        DominoScale = new Vector3(0.2f, 2f, 1f);
        pos = new Vector3(-14f, 2f, 0);
        StartCoroutine("MakeDominoCoroutine");
    }
    public void MakeDomino()
    {
        magnification = float.Parse(inputField.text);
        DeleteAllDomino();
        DominoScale = new Vector3(0.2f, 2f, 1f);
        pos = new Vector3(-14f, 2f, 0);
        StopCoroutine("MakeDominoCoroutine");
        StartCoroutine("MakeDominoCoroutine");

    }
    public void StartDomino()
    {
        Rigidbody r = firstDomino.GetComponent<Rigidbody>();
        r.AddForce(Vector3.right, ForceMode.Impulse);
    }
    void DeleteAllDomino()
    {
        GameObject[] dominos = GameObject.FindGameObjectsWithTag("Domino");
        foreach (GameObject domino in dominos)
        {
            Destroy(domino);
        }
    }
    IEnumerator MakeDominoCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.3f);
            DominoScale = new Vector3(DominoScale.x * magnification, DominoScale.y * magnification, DominoScale.z * magnification);
            pos.y = (DominoScale.y /2) + 1f;
            GameObject domino = Instantiate(dominoGameobject, pos, Quaternion.identity);
            if(i == 0)
            {
                firstDomino = domino;
            }
            domino.transform.localScale = DominoScale;
            pos.x += (DominoScale.y / 2);
        }
    }
}
