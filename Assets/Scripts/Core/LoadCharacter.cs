using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
	public GameObject[] characterPrefabs;
	public Transform spawnPoint;
	//public TMP_Text label;

	void Start()
	{
		int selectedCharacter = PlayerPrefs.GetInt("selectedCharacterIndex");
		GameObject prefab = characterPrefabs[selectedCharacter];
		GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
		//label.text = prefab.name;
	}
}
