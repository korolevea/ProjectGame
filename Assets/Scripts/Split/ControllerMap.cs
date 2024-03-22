using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMap : MonoBehaviour
{
    public List<GameObject> gameObjectsForDestroy;
    public List<GameObject> gameObjectsForDestroySave;
    private void Start()
    {
        gameObjectsForDestroy = new List<GameObject>();
        gameObjectsForDestroySave = new List<GameObject>();
    }
    public void AddGameObject(GameObject _go)
    {
        gameObjectsForDestroySave.Add(_go);
    }
    public void RemoveAllList()
    {
        for (int i = 0; i < gameObjectsForDestroy.Count; i++)
        {
            if (gameObjectsForDestroy[i] != null)
            {
                if (gameObjectsForDestroy[i].transform.parent != null)
                {
                    Destroy(gameObjectsForDestroy[i].transform.parent.gameObject);
                }
                Destroy(gameObjectsForDestroy[i]);
                gameObjectsForDestroy.RemoveAt(i);
            }
            else
            {
                gameObjectsForDestroy.RemoveAt(i);
            }   
        }
        gameObjectsForDestroy.Clear();
        gameObjectsForDestroy = new List<GameObject>( gameObjectsForDestroySave);
        gameObjectsForDestroySave.Clear();
    }
}
