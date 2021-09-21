using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    public GameObject[] dosky = new GameObject[6];
    public GameObject podlaha;
    private AudioSource _source;
    public AudioClip _sound;

    public BoxCollider2D podlahaCollider;
    public PolygonCollider2D doska1Collider;
    public PolygonCollider2D doska2Collider;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Doska"))
        {
            podlahaCollider.isTrigger = true;

            _source.clip = _sound;
            _source.Play();
            Invoke("StopSound", 1);

            foreach (GameObject doska in dosky)
            {
                doska.AddComponent<Rigidbody2D>();
            }

            doska1Collider.isTrigger = false;
            doska2Collider.isTrigger = false;
        }
    }

    private void StopSound()
    {
        _source.clip = null;
    }

}
