using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Container))]
public class ContainerEditor : Editor
{
    private Container ct;
    private SerializedProperty createEnemy;
    private bool cannotBeChanged;

    //Zombie
    private SerializedProperty createZombie;
    private SerializedProperty zombieSpawnChance;

    //Bat
    private SerializedProperty createBat;
    private SerializedProperty batSpawnChance;

    //Sward
    private SerializedProperty createSward;
    private SerializedProperty swardSpawnChance;

    //Wizard
    private SerializedProperty createWizard;
    private SerializedProperty wizardSpawnChance;

    private void OnEnable()
    {
        ct = (Container) target;
        createEnemy = serializedObject.FindProperty("createEnemy");
        //************************************
        createZombie = serializedObject.FindProperty("createZombie");
        zombieSpawnChance = serializedObject.FindProperty("zombieChance");
        //************************************
        createBat = serializedObject.FindProperty("createBat");
        batSpawnChance = serializedObject.FindProperty("batChance");
        //************************************
        createSward = serializedObject.FindProperty("createSward");
        swardSpawnChance = serializedObject.FindProperty("swardChance");
        //************************************
        createWizard = serializedObject.FindProperty("createWizard");
        wizardSpawnChance = serializedObject.FindProperty("wizardChance");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        var create = GUILayout.Button("Create/Delete enemy");
        cannotBeChanged = GUILayout.Toggle(cannotBeChanged, "Cannot be changed");
        EditorGUILayout.EndHorizontal();
        if (create && !cannotBeChanged)
        {
            ct.createEnemy = !ct.createEnemy;
        }
        if (ct.createEnemy)
        {
            if (!cannotBeChanged)
                createZombie.boolValue = GUILayout.Toggle(createZombie.boolValue, "Create zombie");
            if (createZombie.boolValue)
            {
                ParametersSpawn(ct.ZombieSpawnParent, ct.ZombieSpawnPool, cannotBeChanged,"zombie");
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Spawn chance");
                zombieSpawnChance.animationCurveValue = EditorGUILayout.CurveField("Zombie :",zombieSpawnChance.animationCurveValue);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                ct.DeleteSpawn(ct.ZombieSpawnPool, false, true);
            }

            if (!cannotBeChanged)
                createBat.boolValue = GUILayout.Toggle(createBat.boolValue, "Create bat");
            if (createBat.boolValue)
            {
                ParametersSpawn(ct.BatSpawnParent, ct.BatSpawnPool, cannotBeChanged,"bat");
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Spawn chance");
                batSpawnChance.animationCurveValue = EditorGUILayout.CurveField("Bat :",batSpawnChance.animationCurveValue);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                ct.DeleteSpawn(ct.BatSpawnPool, false, true);
            }

            if (!cannotBeChanged)
                createSward.boolValue = GUILayout.Toggle(createSward.boolValue, "Create sward");
            if (createSward.boolValue)
            {
                ParametersSpawn(ct.SwardSpawnParent, ct.SwardSpawnPool, cannotBeChanged,"sward");
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Spawn chance"); swardSpawnChance.animationCurveValue = EditorGUILayout.CurveField("Sward :",swardSpawnChance.animationCurveValue);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                ct.DeleteSpawn(ct.SwardSpawnPool, false, true);
            }

            if (!cannotBeChanged)
                createWizard.boolValue = GUILayout.Toggle(createWizard.boolValue, "Create wizard");

            if (createWizard.boolValue)
            {
                ParametersSpawn(ct.WizardSpawnParent, ct.WizardSpawnPool, cannotBeChanged, "wizard");
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Spawn chance"); wizardSpawnChance.animationCurveValue = EditorGUILayout.CurveField("Wizard :",wizardSpawnChance.animationCurveValue);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                ct.DeleteSpawn(ct.WizardSpawnPool, false, true);
            }
        }
        else
        {
            createZombie.boolValue = false;
            createSward.boolValue = false;
            createWizard.boolValue = false;
            createBat.boolValue = false;
            ct.DeleteSpawn(ct.ZombieSpawnPool, true, false);
        }

        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndVertical();

        void ParametersSpawn(GameObject parent, List<Transform> spawnPool, bool cantBeChanged, string nameEnemy)
        {
            GUILayout.Label("Spawn " + nameEnemy);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add new") && !cantBeChanged)
            {
                ct.AddNewSpawn(parent, spawnPool);
            }

            if (GUILayout.Button("Delete") && !cantBeChanged)
            {
                ct.DeleteSpawn(spawnPool, false, false);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
