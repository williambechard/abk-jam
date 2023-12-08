using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    LevelSetup levelSetup;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Present"))
        {
            other.gameObject.GetComponent<PresentFall>().PresentScore();
            if (GameManager.IsGoalMet == false)
                StartCoroutine(WaitForAllPresents());
            GameManager.IsGoalMet = true;

        }

    }

    IEnumerator WaitForAllPresents()
    {

        bool presentsExist = true;
        while (presentsExist)
        {
            //get all gameobjects that are Present
            PresentFall[] presents = FindObjectOfType<WalkPath>().GetComponentsInChildren<PresentFall>();
            if (presents.Length == 0)
                presentsExist = false;

            yield return new WaitForSeconds(.1f);

        }
        yield return new WaitForSeconds(1f);

        //destroy all gameobjects that are tag Present
        PresentFall[] presents2 = FindObjectsOfType<PresentFall>();
        foreach (PresentFall present in presents2)
        {
            Destroy(present.gameObject);
        }

        //should also reset score
        EventManager.TriggerEvent("ScoreUpdate", new Dictionary<string, object> { { "score", 0 } });

        //call reset
        //GameManager.Instance.Reset();
        levelSetup.quickSetup();



    }


    // Start is called before the first frame update
    void Start()
    {
        levelSetup = FindObjectOfType<LevelSetup>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
