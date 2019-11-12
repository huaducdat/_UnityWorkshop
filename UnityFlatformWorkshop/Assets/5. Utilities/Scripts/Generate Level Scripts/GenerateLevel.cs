using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] private Button generateButton;
    [SerializeField] private Transform elementParent;
    private Dictionary<float, List<LayoutElementPosY>> LayoutGroups = new Dictionary<float, List<LayoutElementPosY>>();
    ElementPosition currentPosition;


    public int sceneLevel = 0;
   
  

    void Start()
    {
        generateButton.onClick.AddListener(GenerateListElement);

        GameManager.instance.OnPlayerDead += ActionWhenPlayerDead;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnPlayerDead -= ActionWhenPlayerDead;
    }

    private void ActionWhenPlayerDead()
    {
        // do something
        Debug.Log("Player dead");
    }

    [SerializeField] private Text showText;
    public void GenerateListElement()
    {
        showText.text = "1234";


        for (int i = 0; i < elementParent.childCount; i++)
        {
            currentPosition = GetElementPosition(elementParent.GetChild(i).position);
            Debug.LogError(elementParent.GetChild(i).position);
            if(LayoutGroups.ContainsKey(currentPosition.xPos))
            {
                LayoutGroups[currentPosition.xPos].Add(new LayoutElementPosY
                {
                    PosY = currentPosition.yPos,
                    ElementNo = elementParent.GetChild(i).GetComponent<ElementInfor>().GetBlockID()
                });
            }
            else
            {
                LayoutGroups.Add(currentPosition.xPos, new List<LayoutElementPosY>()
                {
                    new LayoutElementPosY
                    {
                        PosY = currentPosition.yPos,
                        ElementNo = elementParent.GetChild(i).GetComponent<ElementInfor>().GetBlockID()
                    }
                });
            }
        }
        layoutData.LayoutGroups = LayoutGroups;
        WriteJsonLayoutData();
    }

    private ElementPosition GetElementPosition(Vector3 elementPos)
    {
       

        return new ElementPosition { xPos = elementPos.x, yPos = elementPos.y};
    }


    private LayoutData layoutData = new LayoutData();
    private void WriteJsonLayoutData()
    {
        string connectionString = Application.persistentDataPath + "/" + string.Format("Level_{0}.json", sceneLevel);
        string json = JsonConvert.SerializeObject(layoutData);
        Debug.LogError(connectionString);
        File.WriteAllText(connectionString, json);
    }
}

public class ElementPosition
{
    public float xPos;
    public float yPos;
}
