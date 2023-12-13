using UnityEngine;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace BeliefEngine.HealthKit
{

	internal static class HealthStoreDummyExtensions
	{
		public static void _InitializeNative_DUMMY(this HealthStore store, string controllerName) {
			// DO NOTHING
		}

		public static void _Authorize_DUMMY(this HealthStore store, string dataTypes) {
			// DO NOTHING
		}

		public static int _AuthorizationStatusForType_DUMMY(this HealthStore store, string dataType) {
			return 2;
		}

		public static bool _IsHealthDataAvailable_DUMMY(this HealthStore store) {
			return true;
		}

		public static void _ReadQuantity_DUMMY(this HealthStore store, string identifier, string startDate, string endDate, bool combineSamples) {
			if (identifier == HKDataType.HKQuantityTypeIdentifierHeartRate.ToString()) {
				string xmlString = Resources.Load<TextAsset>("HeartRateResponse").text;
				store.ParseHealthXML(xmlString);
			}
			if (identifier == HKDataType.HKQuantityTypeIdentifierDistanceCycling.ToString()) {
				string xmlString = Resources.Load<TextAsset>("CyclingResponse").text;
				store.ParseHealthXML(xmlString);
			}
			if (identifier == HKDataType.HKQuantityTypeIdentifierStepCount.ToString()) {
				string xmlString = Resources.Load<TextAsset>("StepsResponse").text;
				store.ParseHealthXML(xmlString);
			}
		}

		public static void _WriteQuantity_DUMMY(this HealthStore store, string identifier, string unitString, double doubleValue, string startDateString, string endDateString) {
			// do nothing...
		}

		public static void _ReadCategory_DUMMY(this HealthStore store, string identifier, string startDate, string endDate) {

		}

		public static void _WriteCategory_DUMMY(this HealthStore store, string identifier, int value, string startDateString, string endDateString) {
			// do nothing...
		}

		public static void _ReadCharacteristic_DUMMY(this HealthStore store, string identifier) {

		}

		public static void _ReadCorrelation_DUMMY(this HealthStore store, string identifier, string startDateString, string endDateString, bool combineSamples) {

		}

		public static void _ReadWorkout_DUMMY(this HealthStore store, int activityID, string startDateString, string endDateString, bool combineSamples) {

		}

		public static void _WriteWorkoutSimple_DUMMY(this HealthStore store, int activityID, string startDateString, string endDateString) {
			// do nothing...
		}

		public static void _WriteWorkout_DUMMY(this HealthStore store, int activityID, string startDateString, string endDateString, double kilocaloriesBurned, double distance) {
			// do nothing...
		}

		public static void _BeginObserverQuery_DUMMY(this HealthStore store, string identifier) {
			store.StartCoroutine(_observer(store));
		}

		public static void _ReadCombinedQuantityStatistics_DUMMY(this HealthStore store, string identifier, string startDate, string endDate) {

		}

		public static void _ReadStatistics_DUMMY(this HealthStore store, string identifier, string startDate, string endDate, string options) {
			if (options == StatisticsOptions.CumulativeSum.ToString()) {
				string xmlString = Resources.Load<TextAsset>("StatisticsResponse").text;
				store.ParseHealthXML(xmlString);
			} else if (options == StatisticsOptions.SeparateBySource.ToString()) {
				string xmlString = Resources.Load<TextAsset>("SeparatedStatisticsResponse").text;
				store.ParseHealthXML(xmlString);
			} else {
				string xmlString = $"<statistics><error><code>11</code><domain>com.apple.healthkit</domain><userInfo><NSLocalizedDescription>No data available for the specified predicate.</NSLocalizedDescription></userInfo></error><datatype>{identifier}</datatype><unit>count</unit></statistics>";
				store.ParseHealthXML(xmlString);
			}
		}

		public static void _ReadStatisticsCollection_DUMMY(this HealthStore store, string identifier, string predicateString, string optionsString, string anchorStamp, string intervalString) {
			string xmlString = Resources.Load<TextAsset>("StatisticsCollectionResponse").text;
			store.ParseHealthXML(xmlString);
		}

		public static void _ReadDocument_DUMMY(this HealthStore store, string predicateString, int limit, bool includeData) {
			string xmlString = Resources.Load<TextAsset>("HealthDocumentResponse").text;
			store.ParseHealthXML(xmlString);
		}

		public static void _ReadHealthRecord_DUMMY(this HealthStore store, string ident) {

		}

		// ------------------------

		public static void _ReadPedometer_DUMMY(this HealthStore store, string startDateString, string endDateString) {

		}

		public static void _StartReadingPedometerFromDate_DUMMY(this HealthStore store, string startDateString) {
			store.StartCoroutine(_pedometer(store));
		}

		public static void _StopReadingPedometer_DUMMY(this HealthStore store) {
			store.StopCoroutine(_pedometer(store));
		}

		// ------------------------

		public static void _GenerateDummyData_DUMMY(this HealthStore store, string dataTypesString) {
			// do nothing...
		}


		// ------------------------

		private static IEnumerator _pedometer(HealthStore store) {
			string template = "<pedometer><pedometerData> <startDate>{0}</startDate> <endDate>{1}</endDate> <numberOfSteps>5</numberOfSteps> </pedometerData></pedometer>";
			while (true) {
				DateTimeOffset now = DateTimeOffset.UtcNow;
				DateTimeOffset start = now.AddSeconds(-2);
				string xml = string.Format(template, DateTimeBridge.DateToString(start), DateTimeBridge.DateToString(now));

				store.ParseHealthXML(xml);
				yield return new WaitForSeconds(2);
			}
		}

		private static IEnumerator _observer(HealthStore store) {
			string xml = "<quantity><datatype>HKQuantityTypeIdentifierStepCount</datatype></quantity>";
			while (true) {
				DateTimeOffset now = DateTimeOffset.UtcNow;
				DateTimeOffset start = now.AddSeconds(-2);
				store.ParseHealthXML(xml);
				yield return new WaitForSeconds(5);
			}
		}
	}
}