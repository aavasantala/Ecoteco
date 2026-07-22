using UnityEngine;
using UnityEngine.UIElements;

public class itemChooser : MonoBehaviour
{

    private GameObject currentItem;
    public GameObject[] prefabs;
    public GameObject parentGameObject;

    private void Start()
    {
        parentGameObject = GameObject.Find("GameScreen");
        
    }
    private void OnEnable()
    {
        
    }
    public void ChooseItem()
    {
        try
        {
            int rand = Random.Range(0, prefabs.Length);
            Debug.Log(rand + " length= " + prefabs.Length);
            currentItem = prefabs[rand];
        }
        catch (System.Exception)
        {

            currentItem = prefabs[1];
            Debug.Log("Something went wrong");
        }
        
        SpawnItem(currentItem);
    }

    public void SpawnItem(GameObject item)
    {
        (Instantiate(item, new Vector2(0,0), Quaternion.identity) as GameObject).transform.parent=parentGameObject.transform;

    }

}

