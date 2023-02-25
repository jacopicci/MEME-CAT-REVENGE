using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class DeathCube : MonoBehaviour
{
    [SerializeField] Sprite coots;
    [SerializeField] GameObject cuts;
    [SerializeField] GameObject gameOver;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] AudioMixerSnapshot audioMixerAmbience;
    bool OneMore;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.gameObject.tag == "Enemy")
        {
            if (OneMore)
            {
                cuts.GetComponent<SpriteRenderer>().sprite = coots;
            }
            else
            {
                gameOver.SetActive(true);
                audioMixerAmbience.TransitionTo(3);
                GetComponent<AudioSource>().Play();
                Time.timeScale = 0;
                playerManager.boughtToSpawn = false;
            }
            
        }
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
