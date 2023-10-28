using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

namespace BeliefEngine.HealthKit
{


	public partial class HealthKitDataTypesEditor : Editor
	{
		private bool clinicalDataSection = false;

		partial void DrawClinicalSupport() {
			int clinicalStart = Convert.ToInt32(HKClinicalType.HKClinicalTypeIdentifierAllergyRecord);
			int clinicalEnd = Convert.ToInt32(HKClinicalType.HKClinicalTypeIdentifierVitalSignRecord);

			clinicalDataSection = EditorGUILayout.Foldout(clinicalDataSection, "Clinical Data");
			if (clinicalDataSection) {
				DrawDataTypes<HKClinicalType>(clinicalStart, clinicalEnd);
			}
		}
	}
}
