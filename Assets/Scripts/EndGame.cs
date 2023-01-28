using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TMP_FontAsset font;

    void Start()
    {
        GetComponent<TextMeshProUGUI>().font = font;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
