using UnityEngine;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Linq;

namespace BeliefEngine.HealthKit
{

	/*! @brief callback delegate for the native HealthKit methods */
	public delegate void ReceivedHealthData<T, Error>(T result, Error error);

	/*! @brief callback delegate for the various Write Sample methods */
	public delegate void WroteSample(bool success, Error error);

	/*! @brief callback delegate for the Authorize method */
	public delegate void AuthorizationHandler(bool success);

	/*! @brief Primary interface for HealthKit.
	 */
	public partial class HealthStore : MonoBehaviour {

		/*! @brief Does basic setup for the plugin */
		public void Awake() {
#if UNITY_IOS && !UNITY_EDITOR
			_InitializeNative(this.gameObject.name);
#endif

			receivedQuantityHandlers = new Dictionary< HKDataType, ReceivedHealthData<double, Error> >();
			receivedQuantitySamplesHandlers = new Dictionary< HKDataType, ReceivedHealthData<List<QuantitySample>, Error>>();
			receivedCategorySamplesHandlers = new Dictionary<HKDataType, ReceivedHealthData<List<CategorySample>, Error>>();
			receivedCharacteristicHandlers = new Dictionary<HKDataType, ReceivedHealthData<Characteristic, Error>>();
			receivedCorrelationSamplesHandlers = new Dictionary<HKDataType, ReceivedHealthData<List<CorrelationSample>, Error>>();
			receivedWorkoutSamplesHandlers = new Dictionary<WorkoutActivityType, ReceivedHealthData<List<WorkoutSample>, Error>>();
			receivedStatisticsHandlers = new Dictionary<HKDataType, ReceivedHealthData<HealthStatistics, Error>>();
			receivedStatisticsCollectionHandlers = new Dictionary<HKDataType, ReceivedHealthData<HealthStatisticsCollection, Error>>();

			observerHandlers = new Dictionary<HKDataType, ReceivedHealthData<List<Sample>, Error>>();

			wroteSampleHandlers = new Dictionary<HKDataType, WroteSample>();

			this.InitializeClinicalSupport();
		}

		/*! @brief returns true if HealthKit is available on this device. */
		public bool IsHealthDataAvailable() {
#if UNITY_IOS && !UNITY_EDITOR
			return _IsHealthDataAvailable();
#elif UNITY_EDITOR
			return true;
#else
			return false;
#endif
		}

		/*! @brief   returns authorization status for a given datatype.
			@details See [HKAuthorizationStatus](https://developer.apple.com/documentation/healthkit/hkauthorizationstatus) in the Apple documentation.
					 More useful for write permission; will not tell you if the user denies permission to read the data; it will merely appear as if there is no data.
			@param   dataType the HealthKit datatype to query	
		*/
		public HKAuthorizationStatus AuthorizationStatusForType(HKDataType dataType) {
			HKAuthorizationStatus status = HKAuthorizationStatus.NotDetermined;
		
			string identifier = HealthKitDataTypes.GetIdentifier(dataType);
			try {
#if UNITY_IOS && !UNITY_EDITOR
				status = (HKAuthorizationStatus)_AuthorizationStatusForType(identifier);
#endif
			}
			catch (System.Exception) {
				Debug.LogErrorFormat("error parsing authorization status: '{0}'", identifier);
			}

				return status;
		}

		/*! @brief requests authorization to read the supplied data types, with a completion handler. */
		public void Authorize(HealthKitDataTypes types, AuthorizationHandler handler) {
			if (handler != null) this.authorizationHandler += handler;
			Debug.Log("--- authorizing ---");
#if UNITY_IOS && !UNITY_EDITOR
			_Authorize(types.Transmit());
#endif
		}

		/*! @brief requests authorization to read the supplied data types. */
		public void Authorize(HealthKitDataTypes types) {
			this.Authorize(types, null);
		}

		/*! @brief Generates dummy data for the supplied data types. Mostly for testing in the Simulator. */
		public void GenerateDummyData(HealthKitDataTypes types) {
#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log("--- generating debug data ---");
			_GenerateDummyData(types.Transmit());
#else
			Debug.LogError("Dummy data is not currently available in the editor.");
#endif
		}

		// ------------------------------------------------------------------------------
		// Delegate Interface
		// ------------------------------------------------------------------------------

		// Quantity types

		/*! @brief 				Read quantity samples & return the sum.
			@details
			@param dataType		The datatype to read.
			@param startDate	The date to start reading samples from.
			@param endDate		The end date to limit samples to.
			@param handler		Called when the function finishes executing.
		 */
		public void ReadCombinedQuantitySamples(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, ReceivedHealthData<double, Error> handler) {
			this.receivedQuantityHandlers[dataType] = handler;
			this.ReadQuantity(dataType, startDate, endDate, true);
		}

		/*! @brief 				Read quantity samples & return a list of QuantitySamples.
			@details
			@param dataType		The datatype to read.
			@param startDate	The date to start reading samples from.
			@param endDate		The end date to limit samples to.
			@param handler		Called when the function finishes executing.
		 */
		public void ReadQuantitySamples(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, ReceivedHealthData<List<QuantitySample>, Error> handler) {
			this.receivedQuantitySamplesHandlers[dataType] = handler;
			this.ReadQuantity(dataType, startDate, endDate, false);
		}

		/*! @brief 				Write a quantity sample.
			@details
			@param dataType		The datatype to write.
			@param quantity		the quantity to use to create a sample.
			@param startDate	The starting date of the sample to write.
			@param endDate		The ending date of the sample to write.
			@param handler		Called when the function finishes executing.
		 */
		public void WriteQuantitySample(HKDataType dataType, Quantity quantity, DateTimeOffset startDate, DateTimeOffset endDate, WroteSample handler) {
			this.wroteSampleHandlers[dataType] = handler;
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
#if UNITY_IOS && !UNITY_EDITOR
				_WriteQuantity(identifier, quantity.unit, quantity.doubleValue, DateTimeBridge.DateToString(startDate), DateTimeBridge.DateToString(endDate));
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		// Category types

		/*! @brief 				Read category samples & return a list of CategorySamples.
			@details
			@param dataType		The datatype to read.
			@param startDate	The date to start reading samples from.
			@param endDate		The end date to limit samples to.
			@param handler		Called when the function finishes executing.
		 */
		public void ReadCategorySamples(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, ReceivedHealthData<List<CategorySample>, Error> handler) {
			this.receivedCategorySamplesHandlers[dataType] = handler;
			this.ReadCategory(dataType, startDate, endDate);
		}

		/*! @brief 				Write a category sample.
			@details
			@param dataType		The datatype to write.
			@param value		the (integer) value to use to create a sample.
			@param startDate	The starting date of the sample to write.
			@param endDate		The ending date of the sample to write.
			@param handler		Called when the function finishes executing.
		 */
		public void WriteCategorySample(HKDataType dataType, int value, DateTimeOffset startDate, DateTimeOffset endDate, WroteSample handler) {
			this.wroteSampleHandlers[dataType] = handler;
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
#if UNITY_IOS && !UNITY_EDITOR
				_WriteCategory(identifier, value, DateTimeBridge.DateToString(startDate), DateTimeBridge.DateToString(endDate));
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		// Characteristic types

		/*! @brief 				Read a characteristic.
			@details
			@param dataType		The datatype to read.
			@param handler		Called when the function finishes executing.
		 */
		public void ReadCharacteristic(HKDataType dataType, ReceivedHealthData<Characteristic, Error> handler) {
			this.receivedCharacteristicHandlers[dataType] = handler;
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
#if UNITY_IOS && !UNITY_EDITOR
				_ReadCharacteristic(identifier);
#endif
			}

		}

		// Correlation types

		/*! @brief 				Read correlation samples & return a list of CorrelationSamples.
			@details
			@param dataType		The datatype to read.
			@param startDate	The date to start reading samples from.
			@param endDate		The end date to limit samples to.
			@param handler		Called when the function finishes executing.
		 */
		public void ReadCorrelationSamples(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, ReceivedHealthData<List<CorrelationSample>, Error> handler) {
			this.receivedCorrelationSamplesHandlers[dataType] = handler;
			ReadCorrelation(dataType, startDate, endDate, false);
		}

		// Workout types

		/*! @brief 				Read workout samples & return a list of WorkoutSamples.
			@details
			@param activityType	The activity type to read.
			@param startDate	The date to start reading samples from.
			@param endDate		The end date to limit samples to.
			@param handler		Called when the function finishes executing.
		 */
		public void ReadWorkoutSamples(WorkoutActivityType activityType, DateTimeOffset startDate, DateTimeOffset endDate, ReceivedHealthData<List<WorkoutSample>, Error> handler) {
			this.receivedWorkoutSamplesHandlers[activityType] = handler;
			ReadWorkout(activityType, startDate, endDate, false);

		}

		/*! @brief 				Write a workout sample.
			@details
			@param activityType	The workout activity type to write.
			@param startDate	The starting date of the sample to write.
			@param endDate		The ending date of the sample to write.
			@param handler		Called when the function finishes executing.
		 */
		public void WriteWorkoutSample(WorkoutActivityType activityType, DateTimeOffset startDate, DateTimeOffset endDate, WroteSample handler) {
			this.wroteSampleHandlers[HKDataType.HKWorkoutTypeIdentifier] = handler;
			if (this.IsHealthDataAvailable()) {
				int identifier = (int)activityType;
#if UNITY_IOS && !UNITY_EDITOR
				_WriteWorkoutSimple(identifier, DateTimeBridge.DateToString(startDate), DateTimeBridge.DateToString(endDate));
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		/*! @brief 				Write a workout sample.
			@details
			@param activityType	The workout activity type to write.
			@param startDate	The starting date of the sample to write.
			@param endDate		The ending date of the sample to write.
			@param calories		The kilocalories burned during the activity
			@param distance		The distance traveled during the activity (for e.g. running)
			@param handler		Called when the function finishes executing.
		 */
		public void WriteWorkoutSample(WorkoutActivityType activityType, DateTimeOffset startDate, DateTimeOffset endDate, Quantity calories, Quantity distance, WroteSample handler) {
			this.wroteSampleHandlers[HKDataType.HKWorkoutTypeIdentifier] = handler;
			if (this.IsHealthDataAvailable()) {
				int identifier = (int)activityType;
#if UNITY_IOS && !UNITY_EDITOR
				_WriteWorkout(identifier, DateTimeBridge.DateToString(startDate), DateTimeBridge.DateToString(endDate), calories.doubleValue, calories.unit, distance.doubleValue, distance.unit);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		/*! @brief 				Start an Observer Query.
			@details
			@param dataType		The datatype to read.
			@param handler		Called each time the observer query fires.
		 */
		public void AddObserverQuery(HKDataType dataType, ReceivedHealthData<List<Sample>, Error> handler) {
			// the response handler will check if this is available & use it before the receivedQuantityHandlers
			this.observerHandlers[dataType] = handler;
			this.BeginObserverQuery(dataType);
		}

		public void StopObserverQuery(HKDataType dataType) {
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
#if UNITY_IOS && !UNITY_EDITOR
				_StopObserverQuery(identifier);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		
		public void AddObserverQueryHandler(HKDataType dataType, ReceivedHealthData<List<Sample>, Error> handler) {
			// the response handler will check if this is available & use it before the receivedQuantityHandlers
			this.observerHandlers[dataType] = handler;
		}

		/*! @brief 				start reading from the pedometer.
			@details			Start reading from the pedometer & register a delegate to parse the samples.
			@param startDate	The date to start reading samples from.
			@param handler		Called when a sample is received.
		 */
		public void BeginReadingPedometerData(DateTimeOffset startDate, ReceivedHealthData<List<PedometerData>, Error> handler) {
			this.receivedPedometerDataHandler += handler;
			this.BeginReadingPedometer(startDate);
		}


		/*! @brief 				stop reading from the pedometer.
			@details
		 */
		public void StopReadingPedometerData() {
			this.StopReadingPedometer();
			this.receivedPedometerDataHandler = null;
		}

		/*! @brief 				Perform a statistics query with the cumulative sum statistics option.
			@details
			@param dataType		The datatype to read.
			@param startDate	The date to start reading samples from.
			@param endDate		The end date to limit samples to.
			@param handler		Called when the function finishes executing.
		 */
		[Obsolete("May be removed or rewritten in a future update. Use ReadStatistics with CumulativeSum option.")]
		public void ReadCombinedQuantityStatistics(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, ReceivedHealthData<HealthStatistics, Error> handler) {
			this.receivedStatisticsHandlers[dataType] = handler;
			this.ReadCombinedQuantity(dataType, startDate, endDate);
		}

		/*! @brief 				Perform a statistics query.
			@details
			@param dataType		The datatype to read.
			@param startDate	The date to start reading samples from.
			@param endDate		The end date to limit samples to.
			@param options      the statistics options.
			@param handler		Called when the function finishes executing.
		 */
		public void ReadStatistics(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, StatisticsOptions options, ReceivedHealthData<HealthStatistics, Error> handler) {
			this.receivedStatisticsHandlers[dataType] = handler;
			this.ReadHealthStatistics(dataType, startDate, endDate, options);
		}

		/*! @brief 				Perform a statistics collection query.
			@details
			@param dataType		The datatype to read.
			@param predicate    The predicate used to filter the results.
			@param options      An option that defines the type of statistical calculations performed or the way in which data from multiple sources are merged.
			@param anchorDate   The date used to anchor the collection’s time intervals.
			@param interval     The date components that define the time interval for each statistics object in the collection.
			@param handler		Called when the function finishes executing.
		 */
		public void ReadStatisticsCollection(HKDataType dataType, Predicate predicate, StatisticsOptions options, DateTimeOffset anchorDate, TimeSpan interval, ReceivedHealthData<HealthStatisticsCollection, Error> handler) {
			this.receivedStatisticsCollectionHandlers[dataType] = handler;
			this.ReadHealthStatisticsCollection(dataType, predicate, options, anchorDate, interval);
		}

		// Convenience

		/*! @brief a convenience method to read HKQuantityTypeIdentifierStepCount quantity samples */
		public void ReadSteps(DateTimeOffset startDate, DateTimeOffset endDate, ReceivedHealthData<double, Error> handler) {
			this.receivedQuantityHandlers[HKDataType.HKQuantityTypeIdentifierStepCount] = handler;
			this.ReadQuantity(HKDataType.HKQuantityTypeIdentifierStepCount, startDate, endDate, true);
		}

		public void EnableBackgroundDelivery(HKDataType dataType, HKUpdateFrequency frequency) {
			if (this.IsHealthDataAvailable()) {
				#if UNITY_IOS && !UNITY_EDITOR
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
				int freq = (int)frequency;
				_EnableBackgroundDelivery(identifier, freq);
				#endif
			}
		}

		// ------------------------------------------------------------------------------
		// Lambda Functions
		// ------------------------------------------------------------------------------



		// ------------------------------------------------------------------------------
		// Internal
		// ------------------------------------------------------------------------------
	
		private event AuthorizationHandler authorizationHandler;

		private Dictionary<HKDataType, ReceivedHealthData<double, Error>> receivedQuantityHandlers;
		private Dictionary<HKDataType, ReceivedHealthData<List<QuantitySample>, Error>> receivedQuantitySamplesHandlers;
		private Dictionary<HKDataType, ReceivedHealthData<List<CategorySample>, Error>> receivedCategorySamplesHandlers;
		private Dictionary<HKDataType, ReceivedHealthData<Characteristic, Error>> receivedCharacteristicHandlers;
		private Dictionary<HKDataType, ReceivedHealthData<List<CorrelationSample>, Error>> receivedCorrelationSamplesHandlers;
		private Dictionary<WorkoutActivityType, ReceivedHealthData<List<WorkoutSample>, Error>> receivedWorkoutSamplesHandlers;
		private Dictionary<HKDataType, ReceivedHealthData<HealthStatistics, Error>> receivedStatisticsHandlers;
		private Dictionary<HKDataType, ReceivedHealthData<HealthStatisticsCollection, Error>> receivedStatisticsCollectionHandlers;

		private Dictionary<HKDataType, ReceivedHealthData<List<Sample>, Error>> observerHandlers;

		private Dictionary<HKDataType, WroteSample> wroteSampleHandlers;

		private event ReceivedHealthData<List<PedometerData>, Error> receivedPedometerDataHandler;

		private void ReadQuantity(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, bool combineSamples) {
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
				string startStamp = DateTimeBridge.DateToString(startDate);
				string endStamp = DateTimeBridge.DateToString(endDate);
				Debug.LogFormat("reading quantity from:\n-{0} ({1})\nto:\n-{2} ({3})", startDate, startStamp, endDate, endStamp);
#if UNITY_IOS && !UNITY_EDITOR
				_ReadQuantity(identifier, startStamp, endStamp, combineSamples);
#else
				this._ReadQuantity_DUMMY(identifier, startStamp, endStamp, combineSamples);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void ReadCategory(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate) {
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
#if UNITY_IOS && !UNITY_EDITOR
				_ReadCategory(identifier, DateTimeBridge.DateToString(startDate), DateTimeBridge.DateToString(endDate));
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void ReadCorrelation(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, bool combineSamples) {
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
#if UNITY_IOS && !UNITY_EDITOR
				_ReadCorrelation(identifier, DateTimeBridge.DateToString(startDate), DateTimeBridge.DateToString(endDate), combineSamples);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void ReadWorkout(WorkoutActivityType activityType, DateTimeOffset startDate, DateTimeOffset endDate, bool combineSamples) {
			if (this.IsHealthDataAvailable()) {
				int identifier = (int)activityType;
#if UNITY_IOS && !UNITY_EDITOR
				_ReadWorkout(identifier, DateTimeBridge.DateToString(startDate), DateTimeBridge.DateToString(endDate), combineSamples);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void BeginReadingPedometer(DateTimeOffset startDate) {
			if (this.IsHealthDataAvailable()) {
#if UNITY_IOS && !UNITY_EDITOR
				_StartReadingPedometerFromDate(DateTimeBridge.DateToString(startDate));
#endif
			} else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}
	
		private void BeginObserverQuery(HKDataType dataType) {
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
#if UNITY_IOS && !UNITY_EDITOR
				_BeginObserverQuery(identifier);
#else
				this._BeginObserverQuery_DUMMY(identifier);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void ReadCombinedQuantity(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate) {
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
				string startStamp = DateTimeBridge.DateToString(startDate);
				string endStamp = DateTimeBridge.DateToString(endDate);
				Debug.LogFormat("reading quantity statistics from:\n-{0} ({1})\nto:\n-{2} ({3})", startDate, startStamp, endDate, endStamp);
#if UNITY_IOS && !UNITY_EDITOR
				_ReadCombinedQuantityStatistics(identifier, startStamp, endStamp);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void ReadHealthStatistics(HKDataType dataType, DateTimeOffset startDate, DateTimeOffset endDate, StatisticsOptions options) {
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
				string startStamp = DateTimeBridge.DateToString(startDate);
				string endStamp = DateTimeBridge.DateToString(endDate);
				string optionsString = options.ToString();
				Debug.LogFormat("reading quantity statistics from:\n-{0} ({1})\nto:\n-{2} ({3})", startDate, startStamp, endDate, endStamp);
#if UNITY_IOS && !UNITY_EDITOR
				_ReadStatistics(identifier, startStamp, endStamp, optionsString);
#elif UNITY_EDITOR
				this._ReadStatistics_DUMMY(identifier, startStamp, endStamp, optionsString);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void ReadHealthStatisticsCollection(HKDataType dataType, Predicate predicate, StatisticsOptions options, DateTimeOffset anchorDate, TimeSpan interval)
		{
			if (this.IsHealthDataAvailable()) {
				string identifier = HealthKitDataTypes.GetIdentifier(dataType);
				string predicateString = (predicate != null) ? predicate.ToXMLString() : null;
				string optionsString = options.ToString();
				string anchorStamp = DateTimeBridge.DateToString(anchorDate);
				string intervalString = TimeSpanBridge.TimeSpanToXML(interval);
				//Debug.LogFormat("reading statistics collection from:\n-{0} ({1})\nto:\n-{2} ({3})", startDate, startStamp, endDate, endStamp);
#if UNITY_IOS && !UNITY_EDITOR
				_ReadStatisticsCollection(identifier, predicateString, optionsString, anchorStamp, intervalString);
#elif UNITY_EDITOR
				this._ReadStatisticsCollection_DUMMY(identifier, predicateString, optionsString, anchorStamp, intervalString);
#endif
			}
			else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void StopReadingPedometer() {
			if (this.IsHealthDataAvailable()) {
#if UNITY_IOS && !UNITY_EDITOR
				_StopReadingPedometer();
#endif
			} else {
				Debug.LogError("Error: no health data is available. Are you running on an iOS device that supports HealthKit?");
			}
		}

		private void AuthorizeComplete(string response) {
			bool success = (response == "true");
			if (this.authorizationHandler != null) {
				this.authorizationHandler(success);
				this.authorizationHandler = null;
			}
		}

		internal void ParseHealthXML(string xmlString) {
			if (xmlString == null) {
				Debug.LogError("ERROR! no XML string!!");
				return;
			}
			HealthData xml = new HealthData(xmlString);
			if (xml == null) {
				Debug.LogError("uncaught errror parsing health XML");
				return;
			}
			string rootName = xml.RootName();
			double total;
			switch (rootName) {
				case "quantity":
					List<QuantitySample> qSamples = xml.ParseQuantitySamples();
					if (this.observerHandlers.ContainsKey(xml.datatype)) {
						List<Sample> samples = qSamples.Cast<Sample>().ToList();
						this.observerHandlers[xml.datatype](samples, xml.error);
						// keep observer alive
					}
					else if (this.receivedQuantitySamplesHandlers.ContainsKey(xml.datatype)) {
						var handler = this.receivedQuantitySamplesHandlers[xml.datatype];
						this.receivedQuantitySamplesHandlers[xml.datatype] = null;
						handler(qSamples, xml.error);
					}
					return;
				case "category":
					List<CategorySample> catSamples = xml.ParseCategorySamples();
					this.HandleResults<List<CategorySample>>(this.receivedCategorySamplesHandlers, xml, catSamples);
					return;
				case "characteristic":
					Characteristic c = xml.ParseCharacteristic();
					this.HandleResults<Characteristic>(this.receivedCharacteristicHandlers, xml, c);
					return;
				case "correlation":
					List<CorrelationSample> corSamples = xml.ParseCorrelationSamples();
					this.HandleResults<List<CorrelationSample>>(this.receivedCorrelationSamplesHandlers, xml, corSamples);
					return;
				case "workout":
					List<WorkoutSample> wSamples = xml.ParseWorkoutSamples();
					if (this.observerHandlers.ContainsKey(xml.datatype)) {
						List<Sample> samples = wSamples.Cast<Sample>().ToList();
						this.observerHandlers[xml.datatype](samples, xml.error);
						// keep observer alive
					}
					else if (this.receivedWorkoutSamplesHandlers.ContainsKey(xml.workoutType)) {
						var handler = this.receivedWorkoutSamplesHandlers[xml.workoutType];
						handler(wSamples, xml.error);
						this.receivedWorkoutSamplesHandlers[xml.workoutType] = null;
					}
					return;
				case "pedometer":
					List<PedometerData> qData = xml.ParsePedometerData();
					if (this.receivedPedometerDataHandler != null) {
						this.receivedPedometerDataHandler(qData, xml.error);
						// this.receivedPedometerDataHandler = null;
					}
					return;
				case "write":
					if (this.wroteSampleHandlers.ContainsKey(xml.datatype)) {
						var handler = this.wroteSampleHandlers[xml.datatype];
						handler(true, xml.error);
						this.wroteSampleHandlers[xml.datatype] = null;
					}
					return;
				case "combined":
					total = xml.ParseTotal();
					this.HandleResults<double>(this.receivedQuantityHandlers, xml, total);
					return;
				case "statistics":
					Debug.Log("parsing statistics");
					HealthStatistics stats = xml.ParseStatistics();
					this.HandleResults<HealthStatistics>(this.receivedStatisticsHandlers, xml, stats);
					return;
				case "statisticsCollection":
					HealthStatisticsCollection collection = xml.ParseStatisticsCollection();
					this.HandleResults<HealthStatisticsCollection>(this.receivedStatisticsCollectionHandlers, xml, collection);
					return;
				default:
					break;
			}

			// this will do nothing if the clinical data extension isn't included
			bool handled = false;
			this.ParseClinicalSupport(xml, ref handled);

			if (!handled) {
				Debug.LogError("error; unrecognized root node:" + rootName);
			}
		}

		private void HandleResults<T>(Dictionary<HKDataType, ReceivedHealthData<T, Error>> handlers, HealthData xml, T response) {
			if (handlers.ContainsKey(xml.datatype)) {
				var handler = handlers[xml.datatype];
				handlers[xml.datatype] = null;
				handler(response, xml.error);
			}
		}
	
		private void HealthKitErrorOccurred(string xmlString) {
				XmlDocument xml = new XmlDocument();
				xml.LoadXml(xmlString);
				Error error = new Error(xml.SelectSingleNode("error"));
				Debug.LogError("error from HealthKit plugin: ERROR domain:" + error.domain + " code:" + error.code + " \"" + error.localizedDescription + "\"");
				BroadcastMessage("ErrorOccurred", error);
		}

		// ------------------------------------------------------------------------------
		// Extensions
		// ------------------------------------------------------------------------------

		partial void InitializeClinicalSupport();
		partial void ParseClinicalSupport(HealthData xml, ref bool handled);

		// ------------------------------------------------------------------------------
		// Interface to native implementation
		// ------------------------------------------------------------------------------

#if UNITY_IOS && !UNITY_EDITOR

		[DllImport ("__Internal")]
		private static extern void _InitializeNative(string controllerName);

		[DllImport ("__Internal")]
		private static extern void _Authorize(string dataTypes);

		[DllImport ("__Internal")]
		private static extern int _AuthorizationStatusForType(string dataType);

		[DllImport ("__Internal")]
		private static extern bool _IsHealthDataAvailable();

		[DllImport ("__Internal")]
		private static extern void _ReadQuantity(string identifier, string startDate, string endDate, bool combineSamples);

		[DllImport ("__Internal")]
		private static extern void _WriteQuantity(string identifier, string unitString, double doubleValue, string startDateString, string endDateString);

		[DllImport ("__Internal")]
		private static extern void _ReadCategory(string identifier, string startDate, string endDate);

		[DllImport ("__Internal")]
		private static extern void _WriteCategory(string identifier, int value, string startDateString, string endDateString);

		[DllImport ("__Internal")]
		private static extern void _ReadCharacteristic(string identifier);

		[DllImport ("__Internal")]
		private static extern void _ReadCorrelation(string identifier, string startDateString, string endDateString, bool combineSamples);

		[DllImport ("__Internal")]
		private static extern void _ReadWorkout(int activityID, string startDateString, string endDateString, bool combineSamples);

		[DllImport ("__Internal")]
		private static extern void _WriteWorkoutSimple(int activityID, string startDateString, string endDateString);

		[DllImport ("__Internal")]
		private static extern void _WriteWorkout(int activityID, string startDateString, string endDateString, double energyBurned, string energyUnits, double distance, string distanceUnits);

		[DllImport ("__Internal")]
		private static extern void _BeginObserverQuery(string identifier);

		[DllImport ("__Internal")]
		private static extern void _StopObserverQuery(string identifier);
	
		[DllImport ("__Internal")]
		private static extern void _ReadCombinedQuantityStatistics(string identifier, string startDate, string endDate);

		[DllImport ("__Internal")]
		private static extern void _ReadStatistics(string identifier, string startDate, string endDate, string options);

		[DllImport ("__Internal")]
		private static extern void _ReadStatisticsCollection(string identifier, string predicateString, string optionsString, string anchorStamp, string intervalString);

		[DllImport ("__Internal")]
		private static extern void _ReadDocument(string predicateString, int limit, bool includeData);

		[DllImport ("__Internal")]
		private static extern void _EnableBackgroundDelivery(string identifier, int frequency);

		// ------------------------

		[DllImport ("__Internal")]
		private static extern void _ReadPedometer(string startDateString, string endDateString);

		[DllImport ("__Internal")]
		private static extern void _StartReadingPedometerFromDate(string startDateString);

		[DllImport ("__Internal")]
		private static extern void _StopReadingPedometer();

		// ------------------------

		[DllImport ("__Internal")]
		private static extern void _GenerateDummyData(string dataTypesString);

#endif
	}
}
