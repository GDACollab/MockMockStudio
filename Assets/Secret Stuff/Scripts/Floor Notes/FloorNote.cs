using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorNote : MonoBehaviour
{
    // Internal Variables
    Journal.FloorNoteObj _floorNote;
    UIManager _uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
        _floorNote = _uiManager.journal.GetRandomFloorNote();
    }

    public void DisplayFloorNote(){
        _uiManager.DisplayNote(_floorNote.Title, _floorNote.Body, _floorNote.Author);
    }
}
