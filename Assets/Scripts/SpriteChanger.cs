using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteChanger : MonoBehaviour
{
    public float seconds;
    private SpriteRenderer sR;
    public Sprite s1;

    public Sprite s2;
    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeSprite());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ChangeSprite()
    {
        yield return new WaitForSeconds(seconds);
        if (sR.sprite == s1)
        {
            sR.sprite = s2;
        }
        else
        {
            sR.sprite = s1;
        }

        yield return ChangeSprite();

    }

    public void LoadScene(int x)
    {
        SceneManager.LoadScene(x);
    }
}
