using UnityEngine;
using System.Collections;

public class StartText : MonoBehaviour {


    TextMesh text;
    public float FadeSpeed;
    public int current;
    public int length;

    // Use this for initialization
    void Start () {

        StartCoroutine(AnimationText());

    }
	
	// Update is called once per frame
	void FixedUpdate() {

        if (Input.GetKey(KeyCode.E))
            Destroy(gameObject);
	
	}


    IEnumerator AnimationText()
    {
         length = transform.childCount;
         current = 0;

        for (int corrent = 0; corrent < length; corrent++)
        {

            GameObject CurrentText = transform.GetChild(current).gameObject;
            CurrentText.SetActive(true);
            text = CurrentText.GetComponent<TextMesh>();
            StartCoroutine(FadeIn());
            yield return new WaitForSeconds(2.5f);

            // StartCoroutine(FadeOut());  CrossHair.transform.GetChild(0).gameObject.SetActive(true);

            Destroy(CurrentText);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FadeIn()
    {
        float a = 0f;

        while (a < 1f)
        {
            a += Time.deltaTime * 1f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            yield return 0;
        }

        yield return new WaitForSeconds(1f);

         a = 1f;

        while (a > 0f)
        {
            a -= Time.deltaTime * 2.2f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut()
    {
            float a = 1f;

            while (a > 0f)
            {
                a -= Time.deltaTime;
                text.color = new Color(text.color.r, text.color.g, text.color.b, a);
                yield return 0;         
            }
     
    }



}
