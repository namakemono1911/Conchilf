//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//abstract public class Element
//{
//    public Element(Object t) { target = t; }

//    protected Object target;

//    abstract public void drawElement(SerializedProperty property, int arrayNum);
//}

//public class AnimationElement : Element
//{
//    public AnimationElement(Object t) : base(t) { }

//    uiAnimation.AnimationType beforeType;

//    public override void drawElement(SerializedProperty property, int arrayNum)
//    {
//        var get = target as uiAnimationController;

//        var type = property.FindPropertyRelative("type");
//        EditorGUILayout.PropertyField(type, new GUIContent(type.displayName), true);

//        if (beforeType != get.animation[arrayNum].type)
//        {
//            get.animation[arrayNum].table = createTable(get.animation[arrayNum].type);
//            beforeType = get.animation[arrayNum].type;
//        }

//        //タイプ別に表示
//        //drawType(get.animation[arrayNum].type, get.animation[arrayNum]);
//        var text = property.FindPropertyRelative("texts");
//        EditorGUILayout.PropertyField(text, new GUIContent(text.displayName), true);
//    }

//    private void drawType(uiAnimation.AnimationType type, uiAnimation animation)
//    {
//        switch (type)
//        {
//            case uiAnimation.AnimationType.waitAnime:
//                var table = (waitUiAnimation)animation.table;
//                table.waitTime = EditorGUILayout.FloatField("waitTime", table.waitTime);
//                break;
//        }
//    }

//    private uiAnimationTable createTable(uiAnimation.AnimationType type)
//    {
//        switch (type)
//        {
//            case uiAnimation.AnimationType.waitAnime:
//                return new waitUiAnimation();
//        }

//        return null;
//    }
//}

//public class DrawArrawEditor
//{
//    //配列表示
//    static public void drawArraw(SerializedProperty property, Element e)
//    {
//        // リストそのものの折りたたみ処理。
//        property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, property.displayName);
//        if (!property.isExpanded)
//        {
//            return;
//        }

//        EditorGUI.indentLevel++;
//        {
//            // リストのサイズ調整用処理。
//            int newArraySize = EditorGUILayout.IntField(new GUIContent("Size"), property.arraySize);
//            newArraySize = Mathf.Max(0, newArraySize);
//            property = adjustPersonListSize(newArraySize, property);

//            int i = 0;
//            foreach (SerializedProperty element in property)
//            {
//                element.isExpanded = EditorGUILayout.Foldout(element.isExpanded, element.displayName);
//                if (!element.isExpanded)
//                {
//                    continue;
//                }

//                EditorGUI.indentLevel++;
//                {
//                    e.drawElement(element, i);
//                }
//                EditorGUI.indentLevel--;
//                i++;
//            }
//        }
//        EditorGUI.indentLevel--;
//    }

//    //配列のサイズ変更
//    static private SerializedProperty adjustPersonListSize(int i_newArraySize, SerializedProperty property)
//    {
//        if (property.arraySize > i_newArraySize)
//        {
//            int decreasedCount = property.arraySize - i_newArraySize;
//            for (int i = 0; i < decreasedCount; ++i)
//            {
//                property.DeleteArrayElementAtIndex(property.arraySize - 1);
//            }
//        }
//        else if (property.arraySize < i_newArraySize)
//        {
//            int increasedCount = i_newArraySize - property.arraySize;
//            for (int i = 0; i < increasedCount; ++i)
//            {
//                property.InsertArrayElementAtIndex(property.arraySize);
//            }
//        }

//        return property;
//    }
//}

//[CustomEditor(typeof(uiAnimationController))]
//public class uiAnimationEditor : Editor
//{
//    private SerializedProperty animations = null;
//    private Element drawElement;

//    public void OnEnable()
//    {
//        animations = serializedObject.FindProperty("animation");
//        drawElement = new AnimationElement(target);
//    }

//    public override void OnInspectorGUI()
//    {
//        DrawArrawEditor.drawArraw(animations, drawElement);

//        serializedObject.ApplyModifiedProperties();
//    }

//}
