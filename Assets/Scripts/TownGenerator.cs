using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TownGenerator : MonoBehaviour
{
   public List<GameObject> TownSectors;
    void Start()
    {
        TownSectors[Random.Range(0, TownSectors.Count)].SetActive(true);
    }
}
