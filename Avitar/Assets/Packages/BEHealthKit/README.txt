Table of Contents
=================

- Introduction
- Basic Usage
- Reading Health Data
	- Quantity Samples
	- Combined Quantity Samples
	- Reading Steps
	- Category Samples
	- Characteristics
	- Correlation Samples
	- Workout Samples
	- Quantity Observer Queries
	- Statistics
	- Statistics Collections
	- Clinical Records & Health Documents
	- Health Documents
	- Clinical Records
	- Pedometer Data
- Understanding the data
	- Quantity Samples
	- Category Samples
	- Characteristics
	- Correlation Samples
	- Workout Samples
	- Predicates
- Writing Data
- Submission
	- Permissions
	- Xcode Project Troubleshooting
- HealthStore Methods
- Scripts
	- Editor
	- Example
	- iOS
	- Plugins
- Support

--- ---- ----- ---- ---



Introduction
============

BEHealthKit is a simple Unity plugin for Apple's HealthKit framework. It allows you to read health data on iPhones or iPod Touches into a Unity app.

HealthKit was introduced in iOS 8.0. For an overview of its capabilities, [Apple's HealthKit page](https://developer.apple.com/healthkit/) is a good place to start.

This (and further) documentation is also available online here: <http://beliefengine.com/BEHealthKit/documentation/>.


Basic Usage
===========

First things first, since HealthKit was introduced in iOS 8 you'll need to set your target iOS version in Unity to at least 8.0. Also, I recommend setting the scripting backend to IL2CPP.

There are two key behavior scripts: HealthStore and HealthKitDataTypes. Attach HealthKitDataTypes to any object in your scene, and it provides inspector UI (in the editor) to check all the data types you want your app to be able to read. Attach HealthStore to any object, and call Authorize(), supplying the HealthKitDataTypes object (e.g. like this):

	this.healthStore = this.GetComponent<HealthStore>();
	this.healthStore.Authorize(this.dataTypes);

This'll pop up the native iOS UI where your user can choose to authorize your app to read the supplied data types. **NOTE:** *they can choose not to authorize some or all of the types, so be sure to handle this in your app!*

From there, it's fairly simple to read data. For the most part, each function takes a data type, a start time, an end time, and a delegate (to handle the response).

So for example, let’s say we want to read the user’s steps over the last 24 hours, which are stored as HKDataType.HKQuantityTypeIdentifierStepCount. Here's one way:

	DateTimeOffset now = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-1);
	this.healthStore.ReadQuantitySamples(HKDataType.HKQuantityTypeIdentifierStepCount, start, now, delegate(List<QuantitySample> samples, Error error) {
		foreach (QuantitySample sample in samples) {
			Debug.Log(String.Format(“ - {0} from {1} to {2}”, sample.quantity.doubleValue, sample.startDate, sample.endDate);
		}
	});

Alternatively, if you'd rather keep your parsing contained in its own method, you could do the following:

	public void ProcessData(List<QuantitySample> samples) {
		foreach (QuantitySample sample in samples) {
			Debug.Log(String.Format(“ - {0} from {1} to {2}”, sample.quantity.doubleValue, sample.startDate, sample.endDate);
		}
	}

And then somewhere else, do basically the same as before (but plug in that method instead of the inline function):

	DateTimeOffset now = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-1);
	this.healthStore.ReadQuantitySamples(HKDataType.HKQuantityTypeIdentifierStepCount, start, now, new ReceivedQuantitySamples(ProcessData));


--- ---- ----- ---- ---


Reading Health Data
===================

BEHealthKit supports many, but not all of the queries supplied by the native HealthKit framework.


Quantity Samples
----------------

For anything other then step count, or if you want full access to the step samples, there's **ReadQuantitySamples**. This method takes a datatype, a start & end date, and the delegate method reurns a list of **QuantitySample** objects. If you, for example, are writing an app that wants to see the user's inhaler usage over the past week, this would get you started:

	DateTimeOffset now = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-7);
	this.healthStore.ReadQuantitySamples(HKDataType.HKQuantityTypeIdentifierInhalerUsage, start, now, delegate(List<QuantitySample> samples, Error error) {
		foreach (QuantitySample sample in samples) {
			Debug.Log(String.Format(“ - {0} from {1} to {2}”, sample.quantity.doubleValue, sample.startDate, sample.endDate);
		}
	});

Combined Quantity Samples
-------------------------

In many cases, you won't need the full list of samples, but simply need the sum. **ReadCombinedQuantitySamples** is a convenience method that loops over the samples and returns the sum. This will output the total walking & running distance over the past week:

	DateTimeOffset now = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-7);
	this.healthStore.ReadCombinedQuantitySamples(HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning, start, now, delegate (double total, Error error)) {
		Debug.Log(String.Format("total distance: {0} miles", total));
	});


Reading Steps
-------------

Taking that one step further, if all you want to do is get the user's step count, the **ReadSteps** convenience method is a specialized query that returns the user's step count. The method takes a start date, an end date, and a callback delegate(double, Error).

	DateTimeOffset end = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-1);
	this.healthStore.ReadSteps(start, end, delegate (double steps, Error error) {
		Debug.Log(string.Format("total steps: {0}", steps));
	});


Category Samples
----------------

If you want to read something that's not a quantity, like sleep analysis, menstrual flow, or others, you'll need **ReadCategorySamples**. The method is composed exactly the same as **ReadQuantitySamples**, but it returns a list of **CategorySample** objects. Here's a simple example of reading sleep analysis:

	DateTimeOffset end = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-14);
	this.healthStore.ReadCategorySamples(HKDataType.HKCategoryTypeIdentifierSleepAnalysis, start, end, delegate(List<CategorySample> samples, Error error) {
		foreach (CategorySample sample in samples) {
			string valueString = ((SleepAnalysis)sample.value == SleepAnalysis.Asleep) ? "Sleeping" : "In Bed";
			Debug.Log(string.Format("- {0} from {1} to {2}", valueString, sample.startDate, sample.endDate));
		}
	});


Characteristics
---------------

Characteristicss are things that don't generally change. **ReadCharacteristic**, therefore, doesn't take a date range. Things like biological sex, date of birth, and wheelchair use. 

	this.healthStore.ReadCharacteristic(HKDataType.HKCharacteristicTypeIdentifierDateOfBirth, delegate(Characteristic characteristic, Error error) {
		Debug.Log(string.Format("{0} = {1}", dataType, characteristic));
	});


Correlation Samples
-------------------

Correlation samples are a little different. Like many of the other methods, **ReadCorrelationSamples** takes a datatype, a start & end date, and a delegate callback that gives you a list of **CorrelationSample** objects. At the moment, BEHealthKit's support for correlation queries is somewhat limited, however, as this method does not yet support Predicates for filtering the results (you would use these, for example, to run a query for high-calorie foods). Here's how to perform a blood pressure query:

	DateTimeOffset end = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-7);
	this.healthStore.ReadCorrelationSamples(HKDataType.HKCorrelationTypeIdentifierBloodPressure, start, end, delegate(List<CorrelationSample> samples, Error error) {
		foreach (CorrelationSample correlation in samples) {
			string str = "";
			foreach (Sample sample in correlation.objects) {
				QuantitySample s = (QuantitySample)sample;
				str += string.Format("[{0}:{1}] ", s.quantityType, s.quantity.doubleValue);
			}
			Debug.Log("- " + str);
		}
	});


Workout Samples
---------------

**ReadWorkoutSamples** behaves much the same as the other methods, but takes a **WorkoutActivityType** instead of an **HKDataType**. Here's a query that looks at the fencing workouts over the past week:

	DateTimeOffset end = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-7);
	this.healthStore.ReadWorkoutSamples(WorkoutActivityType.Fencing, start, end, delegate(List<WorkoutSample> samples, Error error) {
		foreach (WorkoutSample sample in samples) {
			Debug.Log(string.Format(" - {0} Calories burned from {1} to {2}"), sample.totalEnergyBurned.doubleValue, sample.startDate, sample.endDate));
		}
	});


Observer Queries
----------------

If, rather than querying historical data, you want to set up an observer that will notify you when new data is added, there's **AddObserverQuery**. It's important to note that, at least in iOS 12, some data (like step count), may not be added unless you switch to the Health app to "trigger" samples getting added, greatly limiting observer queries' usefulness.

	this.healthStore.AddObserverQuery(HKDataType.HKQuantityTypeIdentifierStepCount, delegate(List<Sample> samples, Error error) {
		int steps = 0;
		foreach (Sample sample in samples) {
			QuantitySample quantitySample = sample as QuantitySample;
			if (quantitySample != null) {
				steps += (int)sample.quantity.doubleValue;
			}
		}
		Debug.Log(string.Format("- {0} steps\n", steps));
	});


Statistics
----------

If you want a to query the maximum or minimum of a set of samples, or you need a query that removes duplicate entries from multiple sources (say, duplicate samples from a watch and a phone), you can use Statistics Queries. **ReadStatistics** is the basic query, which takes the usual datatype, start & end dates, and also a StatisticsOptions. The choices are:

 - None
 - SeparateBySource
 - DiscreteAverage
 - DiscreteMin
 - DiscreteMax
 - CumulativeSum
 - DiscreteMostRecent

At the moment, you can only supply one option at a time.

	DateTimeOffset now = DateTimeOffset.UtcNow;
	DateTimeOffset start = now.AddDays(-1);
	this.healthStore.ReadStatistics(HKDataType.HKQuantityTypeIdentifierStepCount, start, now, StatisticsOptions.SeparateBySource, (statistics) => {
		string str = "";
		str += $"-     sum: {statistics.sumQuantity}\n";
		str += $"- minimum: {statistics.minimumQuantity}\n";
		str += $"- maximum: {statistics.maximumQuantity}\n";
		str += $"- average: {statistics.averageQuantity}\n";
		str += $"-  recent: {statistics.mostRecentQuantity}";
		Debug.Log(str + "\n");		
	});

**ReadCombinedQuantityStatistics** is the same as the above, with the **CumulativeSum** option selected by default. This convenience method may go away or be reworked in a future version, since I don't think it's all that useful...


Statistics Collections
----------------------

If, rather than calculating statistics in a time period, you want to calculate statistics over a series of fixed-length time intervals, there's **ReadStatisticsCollection**. It takes a dataType, a Predicate, statistics options, an anchor date, a time interval, and the usual delegate function. Predicate support is new and somewhat rudimentary, but if you don't want to filter out certain samples, you can simply pass null. For more information on Predicates, see the section below. Choice of options are the same as the regular Statistics Query, and likewise, currently only one at a time is supported. The anchor date and time interval are used to partition the samples. See Apple's documentation on [HKStatisticsCollectionQuery](https://developer.apple.com/documentation/healthkit/hkstatisticscollectionquery) for more information on how the partitioning is done.

This example, while not particularly *useful*, shows you how to construct a query to perform a statistics collection query anchored to precisely one day ago, using hour-long intervals:

	DateTimeOffset now = DateTimeOffset.UtcNow;
	DateTimeOffset anchor = now.AddDays(-1);
	TimeSpan interval = new TimeSpan(1, 0, 0);
	this.healthStore.ReadStatisticsCollection(HKDataType.HKQuantityTypeIdentifierStepCount, null, StatisticsOptions.None, anchor, interval, (collection) => {
		string str = "";
		str += $"statistics: {collection.statistics.Count}\n-----------------------\n";
		if (collection.statistics.Count > 0) {
			var statistics = collection.statistics[0];
			str += $"-     sum: {statistics.sumQuantity}\n";
			str += $"- minimum: {statistics.minimumQuantity}\n";
			str += $"- maximum: {statistics.maximumQuantity}\n";
			str += $"- average: {statistics.averageQuantity}\n";
			str += $"-  recent: {statistics.mostRecentQuantity}";
		}
	});


Clinical Records & Health Documents
-----------------------------------

Before you start, because of the extra security concerns, the support for reading Clinical Records & Health Documents has been broken out into a separate package. In the Extensions folder, unpack "Clinical Record Support.unitypackage".


Health Documents
----------------

To retrieve a snapshot of health documents, use **ReadHealthDocuments**, which is a simplified version of [HKDocumentQuery](https://developer.apple.com/documentation/healthkit/hkdocumentquery). Since as of iOS 12, HealthKit only supports CDA documents, there's no need to supply a document type. **ReadHealthDocuments** does take a Predicate for filtering, a limit – that is, the number of documents to limit the query to, and a boolean describing whether to return the full documents, or just a summary.

	this.healthStore.ReadHealthDocuments(null, 10, true, (documents) => {
		Debug.Log(string.Format("retreived {0} documents.", documents.Count));
		foreach (DocumentSample sample in documents) {
			CDADocument document = sample.document;
			Debug.Log("- " + document.title);
		}
	});

Clinical Records
----------------

First, install the extension. Then, be sure you select a clinical data type to read, and supply a usage string. Apple is *very* careful about apps that even include references to the clinical data API, and will reject an app that includes references to it, even if you don't ever call it.

Once you've done that, to read clinical records (FHIR), there's the **ReadClinicalRecord** method. This is *very* preliminary, so at the moment, the method is very simple: simply supply an HKClinicalType to query, and the usual delegate method. It's important to note that by default, clinical records require authorization for *every new record*, rather than a blanket authorization like other data types. For more information, see Apple's documentation on [accessing health records](https://developer.apple.com/documentation/healthkit/samples/accessing_health_records).

	this.healthStore.ReadClinicalRecord(HKClinicalType.HKClinicalTypeIdentifierAllergyRecord, (records) => {
		Debug.Log(string.Format("retreived {0} records.", records.Count));
		foreach (ClinicalRecord record in records) {
			Debug.Log(string.Format("- {0}: '{1}'", record.clinicalType, record.displayName);
		}
	});

Pedometer Data
--------------

Not technically part of HealthKit, I do also offer an interface for CoreMotion's CMPedometer class. You use **BeginReadingPedometerData** to start reading (supplying a start time, if you want to start reading after a delay). To end, you simply call **StopReadingPedometerData**. This gives much finer-grain results for steps than an observer query.

	DateTimeOffset start = DateTimeOffset.UtcNow;
	this.healthStore.BeginReadingPedometerData(start, delegate(List<PedometerData> data, Error error) {
		int steps = 0;
		foreach (PedometerData sample in data) {
			steps += sample.numberOfSteps;
		}
		Debug.Log(string.Format("{0} steps", steps));
	});

	this.healthStore.StopReadingPedometerData();

**NOTE** If you use the pedometer, you will also need to include an **NSMotionUsageDescription** entry in your Info.plist.


Understanding the data
======================

All of the delegates (except for a few convenience methods) return their data in classes pretty closely modeled after their HealthKit counterparts. It's probably worthwhile to check out Apple's [official documentation](https://developer.apple.com/library/prerelease/ios/documentation/HealthKit/Reference/HealthKit_Framework/index.html#//apple_ref/doc/uid/TP40014707), but by no means necessary. The C# classes are all contained in HealthData.cs.

Probably the most useful classes are QuantitySample / CategorySample, and Quantity (these mirror HKQuantitySample, HKCategorySample, and HKQuantity). QuantitySample is used for body measurements, fitness, vitals, test results & nutrition. CategorySamples are used for things like sleep or ovulation tracking.

Quantity Samples
----------------

These all have a startDate and an endDate (as DateTimeOffset). They also have a quantityType, which is either QuantityType.cumulative or QuantityType.discrete.
Cumulative is for values that can be summed over time, like steps or nutritional information. Discrete is for things like body mass or heart rate. Finally, the actual
quantity is stored in a Quantity object. This wraps a unit (as a string) and, for simplicity's sake, all values as a doubleValue. Unlike the HealthKit library, I don't
currently support requesting values in arbitrary compatible units, so some conversion will probably be necessary. For example, the default unit for volume is liters, so if you want something else you'll have to do the conversion. Likewise, the default unit for mass is either pounds or kilograms, depending on the user's locale. These are probably fine for body mass, but won't be as useful for nutritional data.

I do plan on adding support for arbitrary units in a future update (soon).

Category Samples
----------------

Like all Sample types, this also has a startDate and an endDate. CategorySamples, however, only have one other property: value. This is an returned as an int, although it should be converted to the appropriate enum in HealthKitDataTypes.

For sleep data, it's probably worth reading the [HKCategoryValueSleepAnalysis](https://developer.apple.com/library/ios/documentation/HealthKit/Reference/HealthKit_Constants/#//apple_ref/c/tdef/HKCategoryValueSleepAnalysis) documentation to understand what you're looking at. Basically a value of 0 means "in bed", and a 1 means "asleep".  These *will* overlap, assuming a good HealthKit citizen is writing the data.

Likewise, for menstrual flow, the [HKCategoryValueMenstrualFlow](https://developer.apple.com/library/ios/documentation/HealthKit/Reference/HealthKit_Constants/#//apple_ref/c/tdef/HKCategoryValueMenstrualFlow) documentation explains how the values are represented. For these, the same period may be represented by multiple samples.

The others are generally self-explanatory, except for IntermenstrualBleeding & SexualActivity – these will always be 0 (HKCategoryValueNotApplicable). Sexual activity samples, in particular, may include metadata indicating whether or not protection was used – SexualActivityProtectionUsed.

Characteristics
---------------

Characteristics are immutable, so reading them doesn't take date ranges. As of iOS 9.0, there are 4 possible characteristics: Biological Sex, Blood Type, Date of Birth, and Fitzpatrick Skin Type. Any of these can have a value of "NotSet" (or null, for date of birth).

Correlation Samples
-------------------

Correlations are a little trickier. Like other Samples, they have a startDate and an endDate. But Correlations are used to examine multiple pieces of information. For example, blood pressure is stored as a correlation type: it contains 2 discrete samples; one for systolic and and one for diastolic values. Nutrition correlation samples can contain a range of dietary information, such as fat, protein, carbohydrates etc.

Workout Samples
---------------

Workout samples, in addition to a start & end date, have a duration (which, in nearly all cases, will probably be the difference between the start & end date. But it's possible it could be different, for example, if an app decided to subtract a rest period between the start & end).  It also has a workout type (e.g. running, cardio, etc.), and totalDistance & totalEnergyBurned properties. Not all workouts will record distance & energy burned, however.

Finally, some workouts will include a list of WorkoutEvents. These include a date & a type, which will be either Pause or Resume.

Predicates
----------

NSPredicate is Apple's class to encapsulate search & filtering operations. BEHealthKit includes preliminary support for basic & compound predicates. The C# wrapper classes **Predicate** and **CompoundPredicate** are used to create their corresponding native representations on the Objective-C side. The most important part of a Predicate is its format string, described in full in Apple's [documentation](https://developer.apple.com/library/archive/documentation/Cocoa/Conceptual/Predicates/Articles/pSyntax.html#//apple_ref/doc/uid/TP40001795). Compound predicates take a logical operator – NotPredicate, AndPredicate, or OrPredicate – and joins a list of sub-Predicates.


Writing data
============

Writing health data is similar to reading it. To write a sample, you'll need a start date and an end date. It it's a quantity sample (e.g. walking/running distance), you'll need to create a Quantity object. This takes a quantity (as a double), and a unit (as a string). A description of the valid strings can be found in [Apple's documentation](https://developer.apple.com/reference/healthkit/hkunit/1615733-unitfromstring). Then simply call WriteQuantitySample on the HealthStore object, supplying the data type, quantity, and start & end date of the sample.

		DateTimeOffset now = DateTimeOffset.UtcNow;
		DateTimeOffset start = now.AddMinutes(-10);
		Quantity quantity = new Quantity(0.5, "mi");
		
		this.healthStore.WriteQuantitySample(HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning, quantity, start, now);


--- ---- ----- ---- ---

Submission
==========

There's a few extra things to check before submitting to the store. In addition to the usual iOS build process, make sure that the HealthKit capability is checked, and that the Info.plist contains the necessary usage strings. Finally, Apple requires that any app using HealthKit include a privacy policy ([see here](https://developer.apple.com/documentation/healthkit/protecting_user_privacy)).

Permissions
-----------

In order to submit to the iTunes store, it's required that any app that reads health data supply a "Health Share Usage Description". This text is presented to the user and describes what your app intends to do with the information, along with what data types you are requesting to read. Likewise, the "Health Update Usage Description" describes your intent to write health data, if you do. For more information on these two keys, see 
[the documentation](https://developer.apple.com/library/content/documentation/General/Reference/InfoPlistKeyReference/Articles/CocoaKeys.html#//apple_ref/doc/uid/TP40009251-SW48).

The build postprocessor script included with BEHealthKit automatically determines which keys need to be included in the Xcode project, based on the data types you've indicated as wanting read or write permission, so it's only necessary to supply text for the ones you need. Otherwise, the placeholder "for testing" text is fine. 

*NOTE* if you're using the pedometer function, after iOS 10 you will also need to include an NSMotionUsageDescription. Unlike the basic health usage, this is not added to the Xcode project automatically. However, if you're not using the real-time pedometer, you may safely ignore this.


Xcode Project Troubleshooting
-----------------------------

If for some reason you don't want to use the post-processing script, or something goes wrong (such as another script trampling over the HealthKit settings), these are the steps you need to take to get the Xcode project ready to build:

First, Unity will do some weird stuff when it initially creates the Xcode project. You need to change the Base from "iphone" "iphonesimulator" to "iOS". Xcode doesn't fully recognize Unity's setting, and won't let you access the Capabilities tab otherwise. Then, *and this is important*, when you build your Xcode project, make sure you add HealthKit to your target capabilities. Step-by-step:

  1. Select your project in the project navigator (this will most likely be the very first thing, called "Unity-iPhone" in the left-hand navigation)
  2. In the left-hand side of the main view, you'll see one project and two targets. Select the main target (also probably called "Unity-iPhone")
  3. Before the next step, you will probably need to change the Base SDK to "Latest iOS" – Unity sets this to something Xcode doesn't quite understand, and several of the tabs (like "Capabilities") will be missing or empty. Then close & reopen the project (or restart Xcode).
  4. You should see a few "tabs" – General, Capabilities, Info, Build Settings, et cetera. Select "Capabilities"
  5. Scroll down until you see HealthKit (probably near the bottom) and turn it to "ON". This'll automatically do a few required things, like add the HealthKit framework to your Xcode project.

And finally, you need to set some Info.plist keys. The two most important ones are **NSHealthShareUsageDescription** and **NSHealthUpdateUsageDescription**. This requirement was introduced in iOS 10 – you need to add a short description of what your app intends to use the health data for.
**NSHealthShareUsageDescription** describes the reasons for reading the user's health data. The corresponding **NSHealthUpdateUsageDescription** describes why your app wants to write health data.
Also, in iOS 12, if you want to access clinical health data specifically, you will *additionally* need to set **NSHealthClinicalHealthRecordsShareUsageDescription**. Unlike other health data, you *may* need to ask for this authorization every time new data is added to the health store.
For more information, see
[Info.plist key reference](https://developer.apple.com/library/content/documentation/General/Reference/InfoPlistKeyReference/Articles/CocoaKeys.html#//apple_ref/doc/uid/TP40009251-SW48).

 1. In the project navigator, select Info.plist
 2. Hover over the header of the first column (*Key*), revealing a "+", and click it to add a new entry
 3. For the key, enter "NSHealthShareUsageDescription"
 4. For the type, select *String* if it isn't already
 5. Enter the required text in the *Value* column.
 6. (optional) If your app intends to write health data as well, repeat 1-5 for "NSHealthUpdateUsageDescription".
 7. (optional) If your app intends to read clinical health data, repeat 1-5 for "NSHealthClinicalHealthRecordsShareUsageDescription".
 8. All done! You should be good to go.


--- ---- ----- ---- ---


HealthStore Methods
===================

First is the IsHealthDataAvailable() method. This simply returns a boolean; it will return true if HealthKit is supported by the device, false otherwise.

Then there's Authorize() which takes a single parameter, the HealthKitDataTypes object where you select which data types to request authorization for.

Finally, there's the methods to actually read the data:

 - ReadQuantitySamples: read quantity samples for the given datatype, between a start & end date.
 - ReadCombinedQuantitySamples: same as above, but combine them into a single value. Useful for things like steps in a given period, or Calories / day.
 - ReadCategorySamples: read category data between a start & end date.


Scripts
=======

Editor
------

 - HealthKitBuildProcessor :	Automatically adds the proper settings to the Xcode project.
 - HealthKitDataTypesDrawer :	Custom editor for HealthKitDataTypes. Attach that (HealthKitDataTypes) to any object in your scene & check the data types you want your app to read. In v1, this was a Property Drawer.

Example
-------

 - HealthKitTest : 				Contains the logic for the example scene. It demonstrates authorization & if you tap on the "Read Data" button, will read step samples for the
 								last 24 hours.
 - HealthKitFullTest :			Contains the logic for a more detailed example scene. This includes a couple different example methods, as well as a drop-down to read any of the
 								data types that HealthKit supports.

iOS
---

This is all the native code. Of use if you want a better understanding of how the plugin works, or want to extend / change it.

 - BEHealthKit : 				Handles HealthKit requests from Unity. This is the heart of the plugin.
 - BEHealthKit+dummyData : 		Category for generating dummy data (mainly for use in the iOS Simulator)
 - BEHealthKit+read :			Point-of-contact for the Unity plugin (read methods).
 - BEHealthKit+write :			Point-of-contact for the Unity plugin (write methods).
 - BEPedometer :				Handles reading from the pedometer, for real-time step data.
 - HealthData : 				Helper class to generate XML from HealthKit data, for sending to Unity.
 - NSDate+bridge :	 			Category for bridging dates between C# and Objective-C
 - NSDateComponents+bridge :	Category for bridging date components between C# and Objective-C
 - NSError+XML : 				Helper category to generate XML from an NSError, for sending to Unity.
 - XMLDictionary : 				Third-party helper class to ease XML generation from an NSDictionary.
 - Extensions :					Currently, contains categories for reading clinical records

Plugins
-------

 - DateTimeBridge :				A small helper class to bridge dates between C# and Objective-C. C# counterpart to NSDate+bridge.
 - Error :						Wraps information about an error from HealthKit. C# counterpart to NSError+XML.
 - HealthData :					Used to send HealthKit data back to Unity. In addition to the primary HealthData class which parses the XML sent by the plugin, contains a
 								collection of C# classes to mirror HealthKit's data model.
 - HealthKitDataTypes :			Contains information about all the HealthKit data types, and is a wrapper for the data types to authorize. Used to create the editor UI.
 								You'll need it for the authorization step.
 - HealthStore :				This is the primary interface for HealthKit. Allows you to request authorization to read data (supplying a HealthKitDataTypes object), and
 								read the various types of health data.
 - Healthstore_dummy :			Methods for spoofing data & running in the editor, in a limited capacity.
 - Predicate :					A wrapper class for NSPredicates. Used internally for some queries.
 - TimeSpanBridge : 			A small helper class to bridge dates between C# and Objective-C


--- ---- ----- ---- ---


TODO
====

There's still a lot more to be done with this. My primary goal is to keep this up-to-date with new releases of iOS & Unity. There's still a few more complicated or niche corners of HealthKit that I haven't implemented, such as:

 - extend the dummy data system to be more useful
 - add support for the more specialized areas of HealthKit
   - more robust metadata support
   - support for heartbeat series
   - support for audiogram samples
   - high/low/irregular heart rate notifications
   - etc.
 - let you supply units if you don't want whatever the default is
 - improve the documentation
 - realtime heart rate? (I don't know how feasible this is)
 - other general improvements

But mainly, I drive feature development based on feedback, so if there's something I'm missing that you need, reach out! 
Oftentimes, I'm able to work requests into the next release.


Support
=======

For questions, bug reports, suggestions, or if you just want to chat, email <support@beliefengine.com>.
