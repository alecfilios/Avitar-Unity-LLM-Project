using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.Playables;

namespace BeliefEngine.HealthKit
{

	/*! @brief Property drawer for HealthKitDataTypes */
	[CustomEditor (typeof (HealthKitDataTypes))]
	public partial class HealthKitDataTypesEditor : Editor
	{
		private HealthKitDataTypes obj;

		private bool bodyMeasurementSection = true;
		private bool fitnessSection = true;
		private bool vitalsSection = true;
		private bool resultsSection = true;
		private bool nutritionSection = true;
		private bool mobilitySection = true;

		private bool categorySection = true;
		private bool symptomSection = true;
		private bool characteristicSection = true;
		private bool correlationSection = true;
		private bool otherSection = true;

		void Awake() {
			obj = (HealthKitDataTypes)target;
		}


		private SerializedProperty saveDataProperty;
		private SerializedProperty usageStringProperty;
		private SerializedProperty updateStringProperty;
		private SerializedProperty clinicalUsageStringProperty;
		void OnEnable() {
			this.saveDataProperty = serializedObject.FindProperty("saveData");
			this.usageStringProperty = serializedObject.FindProperty("healthShareUsageDescription");
			this.updateStringProperty = serializedObject.FindProperty("healthUpdateUsageDescription");
			this.clinicalUsageStringProperty = serializedObject.FindProperty("clinicalUsageDescription");
		}

		/*! @brief draws the GUI */
		public override void OnInspectorGUI() {
			serializedObject.Update();
		
			GUILayout.BeginVertical();

			EditorGUILayout.PropertyField(usageStringProperty, new GUIContent("Health Share Usage"), null);
			if (this.NeedsWriteAccess()) {
				EditorGUILayout.PropertyField(updateStringProperty, new GUIContent("Health Update Usage"), null);
			}
			if (this.NeedsClinicalAccess()) {
				EditorGUILayout.PropertyField(clinicalUsageStringProperty, new GUIContent("Clincal Data Usage"), null);
			}

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Datatype", EditorStyles.boldLabel, GUILayout.MaxWidth(240));
			EditorGUILayout.LabelField("read", EditorStyles.boldLabel, GUILayout.MaxWidth(40));
			EditorGUILayout.LabelField("write", EditorStyles.boldLabel, GUILayout.MaxWidth(40));
			GUILayout.EndHorizontal();

			int bodyStart = 1;
			int fitnessStart = Convert.ToInt32(HKDataType.HKQuantityTypeIdentifierStepCount);
			int vitalsStart = Convert.ToInt32(HKDataType.HKQuantityTypeIdentifierHeartRate);
			int resultsStart = Convert.ToInt32(HKDataType.HKQuantityTypeIdentifierOxygenSaturation);
			int nutritionStart = Convert.ToInt32(HKDataType.HKQuantityTypeIdentifierDietaryFatTotal);
			int mobilityStart = Convert.ToInt32(HKDataType.HKQuantityTypeIdentifierSixMinuteWalkTestDistance);
			int otherStart = Convert.ToInt32(HKDataType.HKQuantityTypeIdentifierUVExposure);
			int categoriesStart = Convert.ToInt32(HKDataType.HKCategoryTypeIdentifierSleepAnalysis);
			int symptomsStart = Convert.ToInt32(HKDataType.HKCategoryTypeIdentifierAbdominalCramps);
			int charStart = Convert.ToInt32(HKDataType.HKCharacteristicTypeIdentifierBiologicalSex);
			int corrStart = Convert.ToInt32(HKDataType.HKCorrelationTypeIdentifierBloodPressure);

			bodyMeasurementSection = EditorGUILayout.Foldout(bodyMeasurementSection, "Body Measurements");
			if (bodyMeasurementSection) {
				DrawDataTypes<HKDataType>(bodyStart, fitnessStart - 1);
			}
			fitnessSection = EditorGUILayout.Foldout(fitnessSection, "Fitness");
			if (fitnessSection) {
				DrawDataTypes<HKDataType>(fitnessStart, vitalsStart - 1);
			}
			vitalsSection = EditorGUILayout.Foldout(vitalsSection, "Vitals");
			if (vitalsSection) {
				DrawDataTypes<HKDataType>(vitalsStart, resultsStart - 1);
			}
			resultsSection = EditorGUILayout.Foldout(resultsSection, "Results");
			if (resultsSection) {
				DrawDataTypes<HKDataType>(resultsStart, nutritionStart - 1);
			}
			nutritionSection = EditorGUILayout.Foldout(nutritionSection, "Nutrition");
			if (nutritionSection) {
				DrawDataTypes<HKDataType>(nutritionStart, mobilityStart - 1);
			}
			mobilitySection = EditorGUILayout.Foldout(mobilitySection, "Mobility");
			if (mobilitySection) {
				DrawDataTypes<HKDataType>(mobilityStart, otherStart - 1);
			}

			// categories

			categorySection = EditorGUILayout.Foldout(categorySection, "Categories");
			if (categorySection) {
				DrawDataTypes<HKDataType>(categoriesStart, symptomsStart - 1);
			}
			symptomSection = EditorGUILayout.Foldout(categorySection, "Symptoms");
			if (symptomSection) {
				DrawDataTypes<HKDataType>(symptomsStart, charStart - 1);
			}

			characteristicSection = EditorGUILayout.Foldout(characteristicSection, "Characteristics");
			if (characteristicSection) {
				DrawDataTypes<HKDataType>(charStart, corrStart - 1);
			}
			correlationSection = EditorGUILayout.Foldout(correlationSection, "Correlations");
			if (correlationSection) {
				DrawDataTypes<HKDataType>(corrStart, corrStart + 2);
			}
			otherSection = EditorGUILayout.Foldout(otherSection, "Other");
			if (otherSection) {
				DrawDataTypes<HKDataType>(otherStart, categoriesStart);
			}

			// this will do nothing if the clinical data extension isn't included
			this.DrawClinicalSupport();

			GUILayout.EndVertical();

			serializedObject.ApplyModifiedProperties();
		}

		private void DrawDataTypes<T>(int start, int end) where T : System.Enum {
			for (int i = start; i <= end; i++) {
				T datatype = (T)Convert.ChangeType(i, Enum.GetUnderlyingType(typeof(T)));
				DrawDataType( datatype );
			}
		}
		
		private void DrawDataType<T>(T dataType) where T: System.Enum {
			GUILayout.BeginHorizontal();
			Dictionary<string, HKNameValuePair> data = obj.data;
			if (data != null) {
				string key = HealthKitDataTypes.GetIdentifier<T>(dataType);
				if (data.ContainsKey(key)) {
					EditorGUILayout.LabelField(data[key].name, GUILayout.MaxWidth(240));

					EditorGUI.BeginChangeCheck();
					bool readValue = EditorGUILayout.Toggle(data[key].read, GUILayout.MaxWidth(40));
					if (EditorGUI.EndChangeCheck()) {
						data[key].read = readValue;
						string saveData = obj.Save();
						this.saveDataProperty.stringValue = saveData;
					}

					if (!data[key].writable) GUI.enabled = false;

					EditorGUI.BeginChangeCheck();
					bool writeValue = EditorGUILayout.Toggle(data[key].write, GUILayout.MaxWidth(40));
					if (EditorGUI.EndChangeCheck()) {
						data[key].write = writeValue;
						// EditorUtility.SetDirty(prop.serializedObject.targetObject);
						string saveData = obj.Save();
						this.saveDataProperty.stringValue = saveData;
					}

					GUI.enabled = true;
				} else {
					EditorGUILayout.LabelField(key, GUILayout.MaxWidth(240));
					EditorGUILayout.LabelField("ERROR", GUILayout.MaxWidth(80));
				}
			}
			GUILayout.EndHorizontal();
		}

		private bool NeedsWriteAccess() {
			return obj.AskForUpdatePermission();
		}

		private bool NeedsClinicalAccess() {
			return obj.AskForClinicalPermission();
		}


		partial void DrawClinicalSupport();
	}

}