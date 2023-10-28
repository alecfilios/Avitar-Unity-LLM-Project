using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;

namespace BeliefEngine.HealthKit
{
	/*! @brief Wrapper around HKSourceRevision.
	 */
	public class SourceRevision : System.Object
	{
		public string name; /*!< the name of the source */
		public string bundleID; /*!< the bundle ID of the source */
		public string version; /*!< the version of the source */
		public string productType; /*!< the type of device, or null if unavailable */

		/*! @brief		The constructor used internally when reading health data.
			@param node	the XmlNode to create this object from.
		 */
		public SourceRevision(XmlNode node) {
			this.name = node["source"].InnerText;
			this.bundleID = node["bundleID"].InnerText;
			this.version = node["version"].InnerText;
			if (node["productType"] != null) this.productType = node["productType"].InnerText;
		}
	}

	/*! @brief Wrapper around HKObject.
	 */
	public class HObject : System.Object
	{
		public SourceRevision source; /*!< the source, if available */
		public XmlNode metadata; /*!< metadata dictionary */
		/*! @brief		The constructor used internally when reading health data.
			@param node	the XmlNode to create this object from.
		 */
		public HObject(XmlNode node) {
			if (node["source"] != null) this.source = new SourceRevision(node["source"]);
			if (node["metadata"] != null) this.metadata = node["metadata"];
		}
	}


	/*! @enum QuantityType
		@ingroup Enumerations
		@brief denotes whether a Quantity is cumulative, or discrete
	 */
	public enum QuantityType {
		cumulative = 0,
		// discrete - deprecated
		discreteArithmetic,
		discreteTemporallyWeighted,
		discreteEquivalentContinuousLevel
	}

	/*! @brief Wrapper around HKQuantity.
	 */
	public class Quantity : System.Object {
		public string unit; /*!< the unit of this quantity, as a string */
		public double doubleValue; /*!< the value of this quantity, as a double */

		/*! @brief		The constructor used internally when reading health data.
			@param node	the XmlNode to create this object from.
		 */
		public Quantity(XmlNode node) {
			this.unit = node["unit"].InnerText;
			this.doubleValue = Convert.ToDouble(node["value"].InnerText, CultureInfo.InvariantCulture);
		}

		/*! @brief				The default constructor.
			@param unitString	the string representation of the unit. For example, count, kg, or m/s^2.
								see: https://developer.apple.com/reference/healthkit/hkunit/1615733-unitfromstring
			@param value		the value of the quantity, as a double
		 */
		public Quantity(double value, string unitString) {
			this.unit = unitString;
			this.doubleValue = value;
		}

		/*! @brief convert to a reasonable string representation
		 */
		override public string ToString() {
			return string.Format("{0} {1}", this.doubleValue, this.unit);
		}
	}

	/*! @brief Wrapper around HKSample.
	 */
	public class Sample : HObject {
		public DateTimeOffset startDate; /*!< the starting date of this sample */
		public DateTimeOffset endDate; /*!< the ending date of this sample */
		// sample type

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public Sample(XmlNode node) : base(node) {
			this.startDate = DateTimeBridge.DateFromString(node["startDate"].InnerText);
			this.endDate = DateTimeBridge.DateFromString(node["endDate"].InnerText);
		}
	}

	/*! @brief Wrapper around NSDateInterval.
	 */
	public class DateInterval : System.Object
	{
		public DateTimeOffset startDate; /*!< the starting date of the interval */
		public DateTimeOffset endDate; /*!< the ending date of the interval */
		public double duration; /*!< the duration of the interval */

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public DateInterval(XmlNode node) {
			this.startDate = DateTimeBridge.DateFromString(node["startDate"].InnerText);
			this.endDate = DateTimeBridge.DateFromString(node["endDate"].InnerText);
			this.duration = Convert.ToDouble(node["duration"].InnerText, CultureInfo.InvariantCulture);
		}
	}

	/*! @brief   Wrapper around HKQuantitySample.
        @details A quantity sample has a quantity and a type. In iOS 13 and later, HKQuantitySample is actually an abstract superclass for either an
                 HKDiscreteQuantitySample or HKCumulativeQuantitySample. In those cases, a single sample can actually represent multiple samples, and there
                 are properties to examine them in aggregate.
	 */
	public class QuantitySample : Sample {
		public QuantityType quantityType; /*!< the aggregation style of this sample, either cumulative or discrete */
		public Quantity quantity; /*!< the quantity */

		public double sumQuantity = 0;                             /*!< If this is an cumulative quantity ,the sum. Otherwise 0. */
        /*! @brief    If this is an HKDiscreteQuantitySample ,the date interval for the most recent quantity. Otherwise null. */
		public DateInterval mostRecentQuantityDateInterval = null; 
		public double mostRecentQuantity = 0;                      /*!< If this is an discrete quantity ,the most recent quantity. Otherwise 0. */
		public double minimumQuantity = 0;                         /*!< If this is an discrete quantity ,the minimum of the samples. Otherwise 0. */
		public double maximumQuantity = 0;                         /*!< If this is an discrete quantity ,the maximum of the samples. Otherwise 0. */
		public double averageQuantity = 0;                         /*!< If this is an discrete quantity ,the average of the samples. Otherwise 0. */

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public QuantitySample(XmlNode node) : base(node) {
			this.quantity = new Quantity(node["quantity"]);
			string aggregationStyle = node.SelectSingleNode("quantityType/aggregationStyle").InnerText;
			this.quantityType = HealthData.QuantityTypeFromString(aggregationStyle);

			if (node["minimumQuantity"] != null) this.minimumQuantity = Convert.ToDouble(node["minimumQuantity"].InnerText, CultureInfo.InvariantCulture);
			if (node["maximumQuantity"] != null) this.maximumQuantity = Convert.ToDouble(node["maximumQuantity"].InnerText, CultureInfo.InvariantCulture);
			if (node["averageQuantity"] != null) this.averageQuantity = Convert.ToDouble(node["averageQuantity"].InnerText, CultureInfo.InvariantCulture);
			if (node["mostRecentQuantity"] != null) this.mostRecentQuantity = Convert.ToDouble(node["mostRecentQuantity"].InnerText, CultureInfo.InvariantCulture);
			if (node["mostRecentQuantityDateInterval"] != null) this.mostRecentQuantityDateInterval = new DateInterval(node["mostRecentQuantityDateInterval"]);

			if (node["sumQuantity"] != null) this.sumQuantity = Convert.ToDouble(node["sumQuantity"].InnerText, CultureInfo.InvariantCulture);
		}

		/*! @brief convert to a reasonable string representation
		 */
		override public string ToString() {
			return string.Format("[{0}-{1} : {2}]", this.startDate, this.endDate, this.quantity);
		}
	}

	/*! @brief Wrapper around HKCategorySample.
	 */
	public class CategorySample : Sample {
		/*! @brief 		the value of this sample
			@details	This is an int, and it's probably worth reading the [HKCategoryValueSleepAnalysis](https://developer.apple.com/library/ios/documentation/HealthKit/Reference/HealthKit_Constants/#//apple_ref/c/tdef/HKCategoryValueSleepAnalysis)
						documentation to understand what you're looking at. Basically a value of 0 means "in bed", and a 1 means "asleep".  These *will* overlap, assuming a good HealthKit citizen is writing the data.
		 */
		public int value;

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public CategorySample(XmlNode node) : base(node) {
			this.value = Int32.Parse(node["value"].InnerText);
		}

		/*! @brief convert to a reasonable string representation
		 */
		override public string ToString() {
			return string.Format("[{0}-{1} : {2}]", this.startDate, this.endDate, this.value);
		}
	}

	/*! @brief Wrapper around HKCorrelationSample.
	 */
	public class CorrelationSample : Sample {
		public string correlationType; /*!< TODO the correlation type. */
		public List<Sample> objects; /*!< the list of samples */

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public CorrelationSample(XmlNode node) : base(node) {
			this.correlationType = node["correlationType"].InnerText;
			this.objects = new List<Sample>();
			XmlNodeList sampleNodes = node.SelectNodes("objects");
			foreach (XmlNode sample in sampleNodes) {
				// can these be something other than Quantity Samples?
				this.objects.Add(new QuantitySample(sample));
			}
		}

		/*! @brief convert to a reasonable string representation
		 */
		override public string ToString() {
			string s = string.Format("[{0}:\n", correlationType);
			foreach (Sample sample in this.objects) {
				s = s + sample + "\n";
			}
			s = s + "]";
			return s;
		}
	}

	/*! @enum WorkoutEventType
		@ingroup Enumerations
		@brief denotes the type of Workout Event (Pause or Resume)
	 */
	public enum WorkoutEventType {
		Pause = 1,
		Resume
	}

	/*! @brief Wrapper around HKWorkoutEvent.
	 */
	public class WorkoutEvent : System.Object {
		public DateTimeOffset date; /*!< @brief time of the event */
		public WorkoutEventType eventType;	/*!< either Pause or Resume  */

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public WorkoutEvent(XmlNode node) {
			this.date = DateTimeBridge.DateFromString(node["date"].InnerText);
			this.eventType = (WorkoutEventType)Int32.Parse(node["eventType"].InnerText);
		}
	}

	/*! @brief Wrapper around HKWorkoutSample.
	 */
	public class WorkoutSample : Sample {
		public double duration;						/*!< @brief duration of the sample, in seconds */
		public Quantity totalDistance;				/*!< @brief total distance walked/run/etc. during the workout */
		public Quantity totalEnergyBurned;			/*!< @brief total energy burned during the workout */
		public WorkoutActivityType activityType;	/*!< @brief type of the workout */
		public List<WorkoutEvent> workoutEvents; 	/*!< @brief workout events contained in this sample */

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public WorkoutSample(XmlNode node) : base(node) {
			if (node["duration"] != null)      this.duration = Double.Parse(node["duration"].InnerText);
			if (node["totalDistance"] != null) this.totalDistance = new Quantity(node["totalDistance"]);
			if (node["energyBurned"] != null)  this.totalEnergyBurned = new Quantity(node["energyBurned"]);
			this.activityType = (WorkoutActivityType)Int32.Parse(node["activityType"].InnerText);

			this.workoutEvents = new List<WorkoutEvent>();
			XmlNodeList eventNodes = node.SelectNodes("events");
			foreach (XmlNode sample in eventNodes) {
				this.workoutEvents.Add(new WorkoutEvent(sample));
			}
		}

		// override public string ToString() {
		// 	return string.Format("[WORKOUT SAMPLE]");
		// }
	}

	/*! @brief Wrapper around HKCharacteristic.
	 */
	public class Characteristic : System.Object
	{

	}

	/*! @brief Wrapper around HKBiologicalSexCharacteristic.
	 */
	public class BiologicalSexCharacteristic : Characteristic
	{
		/*! @brief		the biological sex
			@details	This can either be NotSet, Male, Female, or Other.
		 */
		public BiologicalSex value;

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public BiologicalSexCharacteristic(XmlNode node) {
			this.value = (BiologicalSex)Int32.Parse(node.InnerText);
		}

		/*! @brief string representation. */
		override public string ToString() {
			return string.Format("[biological sex:{0}]", this.value);
		}
	}

	/*! @brief Wrapper around HKBloodTypeCharacteristic.
	 */
	public class BloodTypeCharacteristic : Characteristic
	{
		/*! the blood type. NotSet is also a valid value. */
		public BloodType value;

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public BloodTypeCharacteristic(XmlNode node) {
			this.value = (BloodType)Int32.Parse(node.InnerText);
		}

		/*! @brief string representation. */
		override public string ToString() {
			return string.Format("[blood type:{0}]", this.value);
		}
	}

	/*! @brief Wrapper around HKDateOfBirthCharacteristic.
	 */
	public class DateOfBirthCharacteristic : Characteristic
	{
		/*! the user's birthday. */
		public DateTimeOffset value;

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public DateOfBirthCharacteristic(XmlNode node) {
			this.value = DateTimeBridge.DateFromString(node.InnerText);
		}

		/*! @brief string representation. */
		override public string ToString() {
			return string.Format("[date of birth:{0}]", this.value);
		}
	}

	/*! @brief Wrapper around HKFitzpatrickSkinTypeObject.
	 */
	public class FitzpatrickSkinTypeCharacteristic : Characteristic
	{
		/*! the skin type. NotSet is also a valid value. */
		public FitzpatrickSkinType value;

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public FitzpatrickSkinTypeCharacteristic(XmlNode node) {
			this.value = (FitzpatrickSkinType)Int32.Parse(node.InnerText);
		}

		/*! @brief string representation. */
		override public string ToString() {
			return string.Format("[skin type:{0}]", this.value);
		}
	}

	/*! @brief Wrapper around HKWheelchairUseObject.
	 */
	public class WheelchairUseCharacteristic : Characteristic
	{
		/*! the skin type. NotSet is also a valid value. */
		public WheelchairUse value;

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public WheelchairUseCharacteristic(XmlNode node) {
			this.value = (WheelchairUse)Int32.Parse(node.InnerText);
		}

		/*! @brief string representation. */
		override public string ToString() {
			return string.Format("[wheelchair use:{0}]", this.value);
		}
	}

	/*! @brief Wrapper around HKStatistics.
	 */
	public class HealthStatistics : System.Object
	{
		public DateTimeOffset startDate; /*!< the starting date of this sample */
		public DateTimeOffset endDate; /*!< the ending date of this sample */
		public QuantityType quantityType; /*!< the aggregation style of this sample, either cumulative or discrete */
		public string unit; /*!< the unit of this quantity, as a string */

		public double sumQuantity; /*!< the sum of all the samples that match the query */
		public double minimumQuantity; /*!< the minimum value from all the samples that match the query */
		public double maximumQuantity; /*!< the maximum value from all the samples that match the query */
		public double averageQuantity; /*!< the average value from all the samples that match the query */
		public double mostRecentQuantity; /*!< the average value from all the samples that match the query */

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public HealthStatistics(XmlNode node) {
			this.startDate = DateTimeBridge.DateFromString(node["startDate"].InnerText);
			this.endDate = DateTimeBridge.DateFromString(node["endDate"].InnerText);
			string aggregationStyle = node.SelectSingleNode("aggregationStyle").InnerText;
			this.quantityType = HealthData.QuantityTypeFromString(aggregationStyle);

			if (node["sumQuantity"] != null)        this.sumQuantity = Convert.ToDouble(node["sumQuantity"].InnerText, CultureInfo.InvariantCulture);
			if (node["minimumQuantity"] != null)    this.minimumQuantity = Convert.ToDouble(node["minimumQuantity"].InnerText, CultureInfo.InvariantCulture);
			if (node["maximumQuantity"] != null)    this.maximumQuantity = Convert.ToDouble(node["maximumQuantity"].InnerText, CultureInfo.InvariantCulture);
			if (node["averageQuantity"] != null)    this.averageQuantity = Convert.ToDouble(node["averageQuantity"].InnerText, CultureInfo.InvariantCulture);
			if (node["mostRecentQuantity"] != null) this.mostRecentQuantity = Convert.ToDouble(node["mostRecentQuantity"].InnerText, CultureInfo.InvariantCulture);
		}

		/*! @brief		 Construct a HealthStatistics object with an XML node and a unit string.
			@param node  the XmlNode to create this object from.
			@param unit  the unit of this sample, as a string
		 */
		public HealthStatistics(XmlNode node, string unit) : this(node) {
			this.unit = unit;
		}
	}

	/*! @brief Wrapper around HKStatisticsCollection.
	 */
	public class HealthStatisticsCollection : System.Object
	{
		public string unit; /*!< the unit of this quantity, as a string */
		public List<HealthStatistics> statistics; /*!< the collection of HealthStatistics objects covered by the query */

		/*! @brief      The default constructor.
			@param node the XmlNode to create this object from.
		 */
		public HealthStatisticsCollection(XmlNode node) {
			Debug.Log("creating health statistics collection");
			this.unit = node["unit"].InnerText;
			this.statistics = new List<HealthStatistics>();
			XmlNodeList statisticsNodes = node.SelectNodes("statistics");
			Debug.Log($"found {statisticsNodes.Count} statistics");
			foreach (XmlNode sub in statisticsNodes) {
				this.statistics.Add(new HealthStatistics(sub, unit));
			}
		}
	}

	/*! @brief   Wrapper around HKCDADocument.
	 *  @details An HKCDADocument object represents a Clinical Document Architecture (CDA) document in HealthKit.
	 */
	public class CDADocument : System.Object
	{
		public string author;      /*!< the author of the document */
		public string custodian;  /*!< the custodian of the document */
		public string patient;   /*!< the patient the document is for */
		public string title;    /*!< the title of the document */
		public XmlNode data;   /*!< the CDA document, stored as XML */

		/*! @brief      The default constructor.
			@param node the XmlNode to create this object from.
		 */
		public CDADocument(XmlNode node) {
			if (node["author"] != null) this.author = node["author"].InnerText;
			if (node["custodian"] != null) this.custodian = node["custodian"].InnerText;
			if (node["patient"] != null) this.patient = node["patient"].InnerText;
			if (node["title"] != null) this.title = node["title"].InnerText;
			if (node["data"] != null) this.data = node["data"];
		}
	}

	/*! @brief   Wrapper around HKDocumentSample.
	 */
	public class DocumentSample : Sample
	{
		public CDADocument document; /*!< the CDADocument object for this sample */

		/*! @brief      The default constructor.
			@param node the XmlNode to create this object from.
		 */
		public DocumentSample(XmlNode node) : base(node) {
			this.document = new CDADocument(node["document"]);
		}
	}

	/*! @brief   Wrapper around HKFHIRResource.
	 *  @details An object containing Fast Healthcare Interoperability Resources (FHIR) data.
	 */
	public class FHIRResource : System.Object
	{
		public string identifier;    /*!< the value from the FHIR resource's id field */
		public string resourceType; /*!< the value from the FHIR resource's resourceType field */
		public string sourceURL;   /*!< the full URL for this FHIR resource */
		public XmlNode data;      /*!< the full representation of this FHIR resource, converted from JSON to XML */

		/*! @brief      The default constructor.
			@param node the XmlNode to create this object from.
		 */
		public FHIRResource(XmlNode node) {
			if (node["identifier"] != null) this.identifier = node["identifier"].InnerText;
			if (node["resourceType"] != null) this.resourceType = node["resourceType"].InnerText;
			if (node["sourceURL"] != null) this.sourceURL = node["sourceURL"].InnerText;
			if (node["data"] != null) this.data = node["data"];
		}
	}


	/*! @brief Used to send HealthKit data back to Unity.
	 */
		public partial class HealthData : System.Object {
	
		public HKDataType datatype;  /*!< @brief the type of health data */
		public HKClinicalType clinicalType; /*!< @brief the clinical type of health data, if applicable */
		public WorkoutActivityType workoutType;  /*!< @brief the workout type, if applicable */
		public Error error = null; /*!< @brief if there was an error, the error, otherwise null */

		protected internal XmlDocument xml;

		/*! @brief				The default constructor.
			@param xmlString	the XML string to create this object from.
		 */
		public HealthData(string xmlString) {
			xml = new XmlDocument();
			xml.LoadXml(xmlString);

			if (this.RootName() == "pedometer") {
				// the pedometer isn't part of HealthKit, so it won't have a datatype
				return;
			}
			Debug.LogFormat("root name: {0}", this.RootName());

			XmlNode node = xml.FirstChild["datatype"];
			if (node != null && node.InnerText != null) {
				HKDataType dt;
				if (Enum.TryParse<HKDataType>((string)node.InnerText, false, out dt)) {
					this.datatype = dt;
				} else {
					HKClinicalType ct;
					if (Enum.TryParse<HKClinicalType>((string)node.InnerText, false, out ct)) {
						this.clinicalType = ct;
					} else {
						Debug.LogError("no datatype or clinical type");
					}
				}
			} else {
				Debug.LogError("datatype node is missing or invalid");
			}

			XmlNode errorNode = xml.FirstChild["error"];
			if (errorNode != null) {
				this.error = new Error(errorNode);
			}

			if (this.datatype == HKDataType.HKWorkoutTypeIdentifier) {
				node = xml.FirstChild["workoutType"];
				if (node != null && node.InnerText != null) {
					int workoutID = Int32.Parse(node.InnerText);
					this.workoutType = (WorkoutActivityType)Enum.ToObject(typeof(WorkoutActivityType), workoutID);
				}
				else Debug.LogError("workoutType node is missing or invalid");
				Debug.Log("xml:\n" + xmlString);
			}
		}

		/*! @brief		The name of the root node of the XML document.
			@details	This is used to determine what kind of HealthKit data to process as.
		 */
		public string RootName() {
			return xml.DocumentElement.Name;
		}

		/*! @brief parse XML containing QuantitySamples, and return a list. */
		public List<QuantitySample> ParseQuantitySamples() {
			XmlNodeList sampleNodes = xml.SelectNodes("/quantity/quantitySample");
			List<QuantitySample> samples = new List<QuantitySample>();
			foreach (XmlNode node in sampleNodes) {
				try {
					samples.Add(new QuantitySample(node));
				}
				catch (Exception e) {
					Debug.LogError($"error parsing Quantity Sample. Node: {node.OuterXml}");
					throw e;
				}
			}

			return samples;
		}

		/*! @brief parse XML containing CategorySamples, and return a list. */
		public List<CategorySample> ParseCategorySamples() {
			XmlNodeList sampleNodes = xml.SelectNodes("/category/categorySample");
			List<CategorySample> samples = new List<CategorySample>();
			foreach (XmlNode node in sampleNodes) {
				samples.Add(new CategorySample(node));
			}

			return samples;
		}

		/*! @brief parse XML containing a combined total of QuantitySamples, and return a double. */
		public double ParseTotal() {
			XmlNode node = xml.SelectSingleNode("combined/total");
			if (node == null) return 0;

			return Double.Parse(node.InnerText);
		}
	
		/*! @brief parse XML containing Statistics result, and return a HealthStatistics object. */
		public HealthStatistics ParseStatistics() {
			XmlNode node = xml.SelectSingleNode("statistics");
			if (node == null) return null;

			string unit = null;
			if (node["unit"] != null) {
				unit = node["unit"].InnerText;
			}

			try {
				var result = new HealthStatistics(node.SelectSingleNode("statistics"), unit);
				return result;
			} catch (Exception) {
				return null;
			}
		}

		/*! @brief parse XML containing a Statistics Collection result, and return a HealthStatisticsCollection object. */
		public HealthStatisticsCollection ParseStatisticsCollection() {
			XmlNode node = xml.SelectSingleNode("statisticsCollection");
			if (node == null) return null;

			try {
				var result = new HealthStatisticsCollection(node);
				return result;
			} catch (Exception) {
				return null;
			}
		}

		/*! @brief parse XML containing a list of CDA documents. */
		public List<DocumentSample> ParseHealthDocuments() {
			XmlNodeList sampleNodes = xml.SelectNodes("/documents/documents");
			List<DocumentSample> documentSamples = new List<DocumentSample>();
			foreach (XmlNode node in sampleNodes) {
				documentSamples.Add(new DocumentSample(node));
			}

			return documentSamples;
		}


		/*! @brief parse XML & determine if writing was a success. */
		public bool ParseSuccess() {
			XmlNode node = xml.SelectSingleNode("write/success");
			return bool.Parse(node.InnerText);
		}

		/*! @brief if there was an error, parse & return it; otherwise null. */
		public Error ParseError() {
			XmlNode node = xml.SelectSingleNode("write/error");
			if (node != null) return new Error(node);
			else return null;
		}

		/*! @brief parse XML containing CorrelationSamples, and return a list. */
		public List<CorrelationSample> ParseCorrelationSamples() {
			XmlNodeList sampleNodes = xml.SelectNodes("/correlation/correlationSample");
			List<CorrelationSample> samples = new List<CorrelationSample>();
			foreach (XmlNode node in sampleNodes) {
				samples.Add(new CorrelationSample(node));
			}

			return samples;
		}

		/*! @brief parse XML containing WorkoutSamples, and return a list. */
		public List<WorkoutSample> ParseWorkoutSamples() {
			XmlNodeList sampleNodes = xml.SelectNodes("/workout/workoutSample");
			List<WorkoutSample> samples = new List<WorkoutSample>();
			foreach (XmlNode node in sampleNodes) {
				samples.Add(new WorkoutSample(node));
			}

			return samples;
		}

		/*! @brief parse XML containing a Characteristic, and return it. */
		public Characteristic ParseCharacteristic() {
			foreach (XmlNode node in xml["characteristic"].ChildNodes) {
				switch (node.Name) {
				case "sex":
					return new BiologicalSexCharacteristic(node);
				case "bloodType":
					return new BloodTypeCharacteristic(node);
				case "skinType":
					return new FitzpatrickSkinTypeCharacteristic(node);
				case "wheelchairUse":
					return new WheelchairUseCharacteristic(node);
				case "DOB":
					return new DateOfBirthCharacteristic(node);
				case "datatype":
					continue;
				default:
					Debug.LogErrorFormat("Error: unrecognized characteristic '{0}'", node.Name);
					break;
			}
			}
		
			return null;
		}

		/*! @brief parse XML containing pedometer data, and return it. */
		public List<PedometerData> ParsePedometerData() {
			XmlNodeList sampleNodes = xml.SelectNodes("/pedometer/pedometerData");
			List<PedometerData> samples = new List<PedometerData>();
			foreach (XmlNode node in sampleNodes) {
				samples.Add(new PedometerData(node));
			}

			return samples;
		}

		internal static QuantityType QuantityTypeFromString(string aggregationStyle) {
			switch (aggregationStyle) {
				case "cumulative":
					return QuantityType.cumulative;
				case "discrete":
				case "discreteArithmetic":
					return QuantityType.discreteArithmetic;
				case "discreteTemporallyWeighted":
					return QuantityType.discreteTemporallyWeighted;
				case "discreteEquivalentContinuousLevel":
					return QuantityType.discreteEquivalentContinuousLevel;
				default:
					Debug.LogErrorFormat("error; unrecognized aggregation style '{0}'", aggregationStyle);
					return QuantityType.cumulative;
			}
		}
	}

	/*! @brief Wrapper around CMPedometerData.
	 */
	public class PedometerData : System.Object {
		public DateTimeOffset startDate; /*!< the starting date of this sample */
		public DateTimeOffset endDate; /*!< the ending date of this sample */
		public int numberOfSteps; /*!< the number of steps taken in this sample */
		// sample type

		/*! @brief		The default constructor.
			@param node	the XmlNode to create this object from.
		 */
		public PedometerData(XmlNode node) {
			this.startDate = DateTimeBridge.DateFromString(node["startDate"].InnerText);
			this.endDate = DateTimeBridge.DateFromString(node["endDate"].InnerText);
			this.numberOfSteps = Convert.ToInt32(node["numberOfSteps"].InnerText);
		}
	}

}
