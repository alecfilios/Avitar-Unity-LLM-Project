using UnityEngine;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.InteropServices;


namespace BeliefEngine.HealthKit
{

	public partial class HealthStore : MonoBehaviour
	{
		private Dictionary<HKDataType, ReceivedHealthData<List<DocumentSample>, Error>> receivedHealthDocuments;
		private Dictionary<HKClinicalType, ReceivedHealthData<List<ClinicalRecord>, Error>> receivedClinicalSamples;

		partial void InitializeClinicalSupport() {
			receivedHealthDocuments = new Dictionary<HKDataType, ReceivedHealthData<List<DocumentSample>, Error>>();
			receivedClinicalSamples = new Dictionary<HKClinicalType, ReceivedHealthData<List<ClinicalRecord>, Error>>();
		}

		/*! @brief              Read health documents.
			@details
			@param predicate    The predicate used to filter the results.
			@param limit        The maximum number of docments to return.
			@param includeData  Send true to include all document data. send false to just return a summary.
			@param handler      Called when the function finishes executing.
			*/
		public void ReadHealthDocuments(Predicate predicate, int limit, bool includeData, ReceivedHealthData<List<DocumentSample>, Error> handler) {
			this.receivedHealthDocuments[HKDataType.HKDocumentTypeIdentifierCDA] = handler;
			this.ReadHealthDocuments(predicate, limit, includeData);
		}

		/*! @brief              Read clinical records.
			@details
			@param dataType     The datatype to read.
			@param handler      Called when the function finishes executing.
		 */
		public void ReadClinicalRecord(HKClinicalType dataType, ReceivedHealthData<List<ClinicalRecord>, Error> handler) {
			this.receivedClinicalSamples[dataType] = handler;
			this.ReadHealthRecord(dataType);
		}

		partial void ParseClinicalSupport(HealthData xml, ref bool handled) {
			string rootName = xml.RootName();
			switch (rootName) {
				case "documents":
					Debug.Log("attempting to parse health document");
					List<DocumentSample> samples = xml.ParseHealthDocuments();
					if (samples == null) Debug.LogError("no samples!");
					else Debug.Log($"found {samples.Count} samples");
					this.HandleResults<List<DocumentSample>>(this.receivedHealthDocuments, xml, samples);
					handled = true;
					return;
				case "records":
					List<ClinicalRecord> records = xml.ParseClinicalRecords();
					if (this.receivedClinicalSamples.ContainsKey(xml.clinicalType)) {
						var handler = this.receivedClinicalSamples[xml.clinicalType];
						handler(records, null);
						this.receivedClinicalSamples[xml.clinicalType] = null;
					}
					handled = true;
					return;
			}
		}

		private void ReadHealthDocuments(Predicate predicate, int limit, bool includeData) {
			if (this.IsHealthDataAvailable()) {
				string predicateString = (predicate != null) ? predicate.ToXMLString() : null;
#if UNITY_IOS && !UNITY_EDITOR
				_ReadDocument(predicateString, limit, includeData);
#elif UNITY_EDITOR
				this._ReadDocument_DUMMY(predicateString, limit, includeData);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void ReadHealthRecord(HKClinicalType dataType) {
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
#if UNITY_IOS && !UNITY_EDITOR
				_ReadHealthRecord(identifier);
#elif UNITY_EDITOR
				this._ReadHealthRecord_DUMMY(identifier);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		// ------------------------------------------------------------------------------
		// Interface to native implementation
		// ------------------------------------------------------------------------------

#if UNITY_IOS && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void _ReadHealthRecord(string identifier);
#endif
	}

}