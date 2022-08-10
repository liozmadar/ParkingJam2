using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEmote : MonoBehaviour
{
    public Sprite emote1;
    public Sprite emote2;
    public Sprite emote3;

    private SpriteRenderer SP;

    // Start is called before the first frame update
    void Start()
    {
        SP = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float random = Random.Range(1, 3);
        if (random == 1)
        {
            SP.sprite = emote1;
        }if (random == 2)
        {
            SP.sprite = emote2;
        }if (random == 3)
        {
            SP.sprite = emote3;
        }
    }
}
