using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

abstract public class Element
{
    abstract public void drawElement(SerializedProperty property);
}

public class AnimationElement : Element
{
    public override void drawElement(SerializedProperty property)
    {
        var name = property.FindPropertyRelative("type");
        EditorGUILayout.PropertyField(name, new GUIContent(name.displayName), true);
    }
}

[CustomEditor(typeof(uiAnimationController))]
public class uiAnimationEditor : Editor
{
    private SerializedProperty animations = null;

    public void OnEnable()
    {
        animations = serializedObject.FindProperty("animation");
    }

    public override void OnInspectorGUI()
    {
        drawArraw(animations, new AnimationElement());

        var controller = target as uiAnimationController;
        
    }

    //配列表示
    public void drawArraw(SerializedProperty property, Element e)
    {
        // リストそのものの折りたたみ処理。
        property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, property.displayName);
        if (!property.isExpanded)
        {
            return;
        }

        EditorGUI.indentLevel++;
        {
            // リストのサイズ調整用処理。
            int newArraySize = EditorGUILayout.IntField(new GUIContent("Size"), property.arraySize);
            newArraySize = Mathf.Max(0, newArraySize);
            adjustPersonListSize(newArraySize);

            foreach (SerializedProperty element in property)
            {
                element.isExpanded = EditorGUILayout.Foldout(element.isExpanded, element.displayName);
                if (!element.isExpanded)
                {
                    continue;
                }

                EditorGUI.indentLevel++;
                {
                    e.drawElement(element);
                }
                EditorGUI.indentLevel--;
            }
        }
        EditorGUI.indentLevel--;
    }
    
    //配列のサイズ変更
    private void adjustPersonListSize(int i_newArraySize)
    {
        if (animations.arraySize > i_newArraySize)
        {
            int decreasedCount = animations.arraySize - i_newArraySize;
            for (int i = 0; i < decreasedCount; ++i)
            {
                animations.DeleteArrayElementAtIndex(animations.arraySize - 1);
            }
        }
        else if (animations.arraySize < i_newArraySize)
        {
            int increasedCount = i_newArraySize - animations.arraySize;
            for (int i = 0; i < increasedCount; ++i)
            {
                animations.InsertArrayElementAtIndex(animations.arraySize);
            }
        }
    }
}
