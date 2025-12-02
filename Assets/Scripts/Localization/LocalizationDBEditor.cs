using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationDB))]
public class LocalizationDBEditor : Editor
{
    private SerializedProperty entries;
    private string searchKey = ""; // поле для поиска

    private void OnEnable()
    {
        entries = serializedObject.FindProperty("entries");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Localization Table", EditorStyles.boldLabel);

        // 🔍 Поле поиска
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Search Key:", GUILayout.Width(80));
        searchKey = EditorGUILayout.TextField(searchKey);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);

        // Заголовки таблицы
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Key", GUILayout.Width(150));
        EditorGUILayout.LabelField("English", GUILayout.Width(250));
        EditorGUILayout.LabelField("Russian", GUILayout.Width(250));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(3);

        // Все строки
        for (int i = 0; i < entries.arraySize; i++)
        {
            SerializedProperty entry = entries.GetArrayElementAtIndex(i);
            SerializedProperty key = entry.FindPropertyRelative("key");
            SerializedProperty en = entry.FindPropertyRelative("en");
            SerializedProperty ru = entry.FindPropertyRelative("ru");

            // 🔹 Фильтр по поиску
            if (!string.IsNullOrEmpty(searchKey) &&
                !key.stringValue.ToLower().Contains(searchKey.ToLower()))
            {
                continue;
            }

            EditorGUILayout.BeginHorizontal();

            // Key
            key.stringValue = EditorGUILayout.TextField(key.stringValue, GUILayout.Width(150));

            // English
            en.stringValue = EditorGUILayout.TextArea(en.stringValue, GUILayout.Height(45), GUILayout.Width(250));

            // Russian
            ru.stringValue = EditorGUILayout.TextArea(ru.stringValue, GUILayout.Height(45), GUILayout.Width(250));

            // Кнопка удаления строки
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                entries.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
        }

        EditorGUILayout.Space(10);

        // Кнопка добавления новой строки
        if (GUILayout.Button("+ Add Entry", GUILayout.Height(25)))
        {
            entries.InsertArrayElementAtIndex(entries.arraySize);
            var newEntry = entries.GetArrayElementAtIndex(entries.arraySize - 1);
            newEntry.FindPropertyRelative("key").stringValue = "";
            newEntry.FindPropertyRelative("en").stringValue = "";
            newEntry.FindPropertyRelative("ru").stringValue = "";
        }

        serializedObject.ApplyModifiedProperties();
    }
}
