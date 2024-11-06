using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Journal", menuName = "Inventory/Journal")]
public class Journal : ScriptableObject
{
    [System.Serializable]
    public class FloorNoteObj
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Author { get; private set; }
        [field: TextArea(1, 5)]
        [field: SerializeField] public string Body { get; private set; }
        
        public FloorNoteObj(string title, string author, string body)
        {
            Title = title;
            Author = author;
            Body = body;
        }
    }

    [SerializeField][Tooltip("Disable if adding custom notes")] private bool regenerateOnPlay = true;
    [SerializeField] private List<FloorNoteObj> floorNotes = new List<FloorNoteObj>();
    private List<FloorNoteObj> _randomNotes = new List<FloorNoteObj>();

    public void CreateFloorNotes()
    {
        if (!regenerateOnPlay){return;}
        
        string[] noteNames = AssetDatabase.FindAssets("t:TextAsset", new[] {"Assets/CONTENT/Writing/NotesText"});
        
        floorNotes.Clear();
        foreach (string name in noteNames){
            string filePath = AssetDatabase.GUIDToAssetPath(name);
            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
            
            string fileName = Path.GetFileName(filePath);
            string noteTitle = Regex.Match(fileName, @"(.*)\(").Groups[1].Value;
            noteTitle = noteTitle.Replace("_", " ").ToUpper();
            string noteAuthor = Regex.Match(fileName, @"\((.*)\)").Groups[1].Value;
            noteAuthor = string.IsNullOrEmpty(noteAuthor) ? "404_INVALID_IMAGE_NAME" : noteAuthor;
            floorNotes.Add(new FloorNoteObj(noteTitle, noteAuthor, textAsset.ToString()));
            
        }
        _randomNotes = new List<FloorNoteObj>(floorNotes);
    }

    public string ReadFloorNote(string id)
    {
        int index = floorNotes.FindIndex(x => x.Title == id);
        if (index >= 0)
        {
            return floorNotes[index].Body;
        }
        return "Failed";
    }
    
    public FloorNoteObj GetRandomFloorNote(){
        if (_randomNotes.Count <= 0){
            _randomNotes = new List<FloorNoteObj>(floorNotes);
        }
        
        int ind = Random.Range(0, _randomNotes.Count);
        FloorNoteObj randNote = _randomNotes[ind];
        _randomNotes.RemoveAt(ind);
        
        return randNote;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Journal))]
public class JournalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Floor Notes"))
        {
            Journal journal = (Journal)target;
            journal.CreateFloorNotes();
        }
    }
}
#endif