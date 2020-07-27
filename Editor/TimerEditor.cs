using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Ezphera.TimerScore
{
    [CustomEditor(typeof(Timer))]
    public class TimerEditor : Editor
    {
        Timer timer;
        SerializedProperty timerFrom;

        private void OnEnable()
        {
            timer = (Timer)target;
            timerFrom = serializedObject.FindProperty("startFrom");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (Application.isPlaying)
            {
                GUIOnPlaying();
            }
            else
            {
                timer.timerType = (Timer.TimerType)EditorGUILayout.EnumPopup("Timer Type", timer.timerType);
                if (timer.timerType == Timer.TimerType.Countdown)
                {
                    EditorGUILayout.PropertyField(timerFrom, new GUIContent("Start Timer From"), true);
                }
            }
        }
        void GUIOnPlaying() 
        {
            EditorGUILayout.HelpBox("Timer isPlaying: " + timer.isPlaying, MessageType.Info);
            EditorGUILayout.Space();
            GUIStyle timerSectionStyle = new GUIStyle();
            timerSectionStyle.fontSize = 35;
            timerSectionStyle.alignment = TextAnchor.MiddleCenter;
            if (timer.isPlaying) timerSectionStyle.fontStyle = FontStyle.Bold;
            EditorGUILayout.BeginHorizontal();
            try
            {
                EditorGUILayout.LabelField(timer.GetTimeText(), timerSectionStyle);
            }
            finally
            {
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}