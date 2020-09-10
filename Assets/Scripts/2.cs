using UnityEngine;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {

    public GameObject player;
    private NewBehaviourScript playerScript;
    // Use this for initialization
    void Start()
    {
        playerScript = player.GetComponent<NewBehaviourScript>();  // הכנסת מאפיינים של 'שחקן' ל'קוד שחקן'
        playerScript.PlayerDamage(5); // שימוש בפעולה ממחלקה אחרת

    }

    // Update is called once per frame
    void Update()
    {
      
        if (playerScript.health <= 0)
        {
            print("cube (1) found cube dead");   // הדפסת במערכת של יוניטי
        }
    }
}
