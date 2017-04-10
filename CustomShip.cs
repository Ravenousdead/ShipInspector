using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (Ship))]
public class CustomShip : Editor {
	
	int minStats = 10;
	int maxStats = 100;
	int currentUsed;
	string displayProgress;
	int tabIndex;
	string[] tabTitles = new string[] { "Ship Guns", "Crew Members" };

	Ship cShip;

	void OnEnable () {
		cShip = (Ship)target;
	}

	public override void OnInspectorGUI ()
	{
		DrawShip ();
	}


	void DrawShip () {

		tabIndex = GUILayout.SelectionGrid (tabIndex, tabTitles, 2); 
		
		if (tabIndex == 0) {

			EditorGUILayout.BeginVertical ("Box");
			EditorGUILayout.LabelField (new GUIContent("Stats", "Armor, Attack and Agility share a pool of 100 points"), EditorStyles.boldLabel);
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.BeginVertical ();
			cShip.armor = EditorGUILayout.IntSlider ("Armor", cShip.armor, minStats, maxStats-(cShip.attack+cShip.agility));
			cShip.attack = EditorGUILayout.IntSlider ("Attack", cShip.attack, minStats, maxStats-(cShip.armor+cShip.agility));
			cShip.agility = EditorGUILayout.IntSlider ("Agility", cShip.agility, minStats, maxStats-(cShip.armor+cShip.attack));

			EditorGUILayout.EndVertical ();

			// +/- Buttons
			EditorGUILayout.BeginVertical ();
			// Armor
			EditorGUILayout.BeginHorizontal ();
			bool ArmorP = GUILayout.Button ("+", GUILayout.Width(20), GUILayout.Height(15));
			if (ArmorP && currentUsed < maxStats) {cShip.armor++;}
			bool ArmorN = GUILayout.Button ("-", GUILayout.Width(20), GUILayout.Height(15));
			if (ArmorN && cShip.armor > minStats) {cShip.armor--;}
			EditorGUILayout.EndHorizontal ();
			// Attack
			EditorGUILayout.BeginHorizontal ();
			bool AttackP = GUILayout.Button ("+", GUILayout.Width(20), GUILayout.Height(15));
			if (AttackP && currentUsed < maxStats) {cShip.attack++;}
			bool AttackN = GUILayout.Button ("-", GUILayout.Width(20), GUILayout.Height(15));
			if (AttackN && cShip.attack > minStats) {cShip.attack--;}
			EditorGUILayout.EndHorizontal ();
			// Agility
			EditorGUILayout.BeginHorizontal ();
			bool AgilityP = GUILayout.Button ("+", GUILayout.Width(20), GUILayout.Height(15));
			if (AgilityP && currentUsed < maxStats) {cShip.agility++;}
			bool AgilityN = GUILayout.Button ("-", GUILayout.Width(20), GUILayout.Height(15));
			if (AgilityN && cShip.agility > minStats) {cShip.agility--;}
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.EndVertical ();

			EditorGUILayout.EndHorizontal ();

			currentUsed = cShip.armor + cShip.attack + cShip.agility;
			displayProgress = currentUsed.ToString () + " / " + maxStats.ToString ();
			EditorGUI.ProgressBar (GUILayoutUtility.GetRect (0, 18, "TextField"), (float)currentUsed / maxStats, displayProgress);
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical ("Box");
			cShip.hitPoints = EditorGUILayout.IntSlider (new GUIContent("Hit Points","Not sure, Life maybe?"), cShip.hitPoints, 0, 100);
			EditorGUILayout.EndVertical ();

			serializedObject.Update ();
			SerializedProperty guns = serializedObject.FindProperty ("shipGuns");
			EditorGUILayout.PropertyField (guns, true);
			serializedObject.ApplyModifiedProperties ();

		} else {
			
			serializedObject.Update ();
			SerializedProperty members = serializedObject.FindProperty ("crewMembers");
			EditorGUILayout.PropertyField (members, true);
			serializedObject.ApplyModifiedProperties ();

		}

	}

}
