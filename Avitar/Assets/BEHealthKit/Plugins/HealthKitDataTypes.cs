using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BeliefEngine.HealthKit
{

	/*!
		@defgroup Enumerations Enumerations
		Public enumeration types
	*/

	/*! @enum HKAuthorizationStatus
		@ingroup Enumerations
		@brief Identifiers for the health data sharing authorization.
	 */
	public enum HKAuthorizationStatus {
		NotDetermined,
		SharingDenied,
		SharingAuthorized
	}

	/*! @enum HKDataType
		@ingroup Enumerations
		@brief Identifiers for the datatypes that HealthKit supports.
	 */
	public enum HKDataType {
		/*--------------------------------*/
		/*   HKQuantityType Identifiers   */
		/*--------------------------------*/

		// Body Measurements
		HKQuantityTypeIdentifierBodyMassIndex,              	// Scalar(Count),               Discrete
		HKQuantityTypeIdentifierBodyFatPercentage,          	// Scalar(Percent, 0.0 - 1.0),  Discrete
		HKQuantityTypeIdentifierHeight,                     	// Length,                      Discrete
		HKQuantityTypeIdentifierBodyMass,                   	// Mass,                        Discrete
		HKQuantityTypeIdentifierLeanBodyMass,               	// Mass,                        Discrete
		HKQuantityTypeIdentifierWaistCircumference,         	// Length,                      Discrete    - iOS 11.0
		HKQuantityTypeIdentifierAppleSleepingWristTemperature,	// Temperature					Discrete	- iOS 16.0

		// Fitness
		HKQuantityTypeIdentifierStepCount,                  // Scalar(Count),               Cumulative
		HKQuantityTypeIdentifierDistanceWalkingRunning,     // Length,                      Cumulative
		HKQuantityTypeIdentifierDistanceCycling,            // Length,                      Cumulative
		HKQuantityTypeIdentifierDistanceWheelchair,         // Length,                      Cumulative  - iOS 10.0
		HKQuantityTypeIdentifierBasalEnergyBurned,          // Energy,                      Cumulative
		HKQuantityTypeIdentifierActiveEnergyBurned,         // Energy,                      Cumulative
		HKQuantityTypeIdentifierFlightsClimbed,             // Scalar(Count),               Cumulative
		HKQuantityTypeIdentifierNikeFuel,                   // Scalar(Count),               Cumulative
		HKQuantityTypeIdentifierAppleExerciseTime,			// Time							Cumulative	- iOS 9.3
		HKQuantityTypeIdentifierPushCount,                  // Scalar(Count),               Cumulative  - iOS 10.0
		HKQuantityTypeIdentifierDistanceSwimming,           // Length,                      Cumulative  - iOS 10.0
		HKQuantityTypeIdentifierSwimmingStrokeCount,        // Scalar(Count),               Cumulative  - iOS 10.0
		HKQuantityTypeIdentifierVO2Max,                     // ml/(kg*min)                  Discrete    - iOS 11.0
		HKQuantityTypeIdentifierDistanceDownhillSnowSports, // Length,                      Cumulative  - iOS 11.2
		HKQuantityTypeIdentifierAppleStandTime,				// Time,                        Cumulative	- iOS 13
		HKQuantityTypeIdentifierAppleMoveTime,				// Time,                        Cumulative	- iOS 14.5
		HKQuantityTypeIdentifierAppleWalkingSteadiness,		// Scalar(Percent, 0.0 - 1.0),  Discrete	- iOS 15
		HKQuantityTypeIdentifierRunningStrideLength,		// Length, 						Discrete	- iOS 16
		HKQuantityTypeIdentifierRunningVerticalOscillation,	// Length, 						Discrete	- iOS 16
		HKQuantityTypeIdentifierRunningGroundContactTime,	// Time, 						Discrete	- iOS 16
		HKQuantityTypeIdentifierRunningPower,				// Power, 						Discrete	- iOS 16
		HKQuantityTypeIdentifierRunningSpeed,				// m/s, 						Discrete	- iOS 16

	
		// Vitals
		HKQuantityTypeIdentifierHeartRate,                  // Scalar(Count)ime,          Discrete
		HKQuantityTypeIdentifierBodyTemperature,            // Temperature,                 Discrete
		HKQuantityTypeIdentifierBasalBodyTemperature,		// Basal Body Temperature,		Discrete	- iOS 9.0
		HKQuantityTypeIdentifierBloodPressureSystolic,      // Pressure,                    Discrete
		HKQuantityTypeIdentifierBloodPressureDiastolic,     // Pressure,                    Discrete
		HKQuantityTypeIdentifierRespiratoryRate,            // Scalar(Count)/Time,          Discrete
		HKQuantityTypeIdentifierRestingHeartRate,           // Scalar(Count)/Time,          Discrete    - iOS 11.0
		HKQuantityTypeIdentifierWalkingHeartRateAverage,    // Scalar(Count)/Time,          Discrete    - iOS 11.0
		HKQuantityTypeIdentifierHeartRateVariabilitySDNN,   // Time (ms),                   Discrete    - iOS 11.0
		HKQuantityTypeIdentifierHeartRateRecoveryOneMinute,	// Count/Time					Discrete	- iOS 16.0

		// Results
		HKQuantityTypeIdentifierOxygenSaturation,           // Scalar (Percent, 0.0 - 1.0,  Discrete
		HKQuantityTypeIdentifierPeripheralPerfusionIndex,   // Scalar(Percent, 0.0 - 1.0),  Discrete
		HKQuantityTypeIdentifierBloodGlucose,               // Mass/Volume,                 Discrete
		HKQuantityTypeIdentifierNumberOfTimesFallen,        // Scalar(Count),               Cumulative
		HKQuantityTypeIdentifierElectrodermalActivity,      // Conductance,                 Discrete
		HKQuantityTypeIdentifierInhalerUsage,               // Scalar(Count),               Cumulative
		HKQuantityTypeIdentifierInsulinDelivery,            // Pharmacology (IU)            Cumulative  - iOS 11.0
		HKQuantityTypeIdentifierBloodAlcoholContent,        // Scalar(Percent, 0.0 - 1.0),  Discrete
		HKQuantityTypeIdentifierForcedVitalCapacity,        // Volume,                      Discrete
		HKQuantityTypeIdentifierForcedExpiratoryVolume1,    // Volume,                      Discrete
		HKQuantityTypeIdentifierPeakExpiratoryFlowRate,     // Volume/Time,                 Discrete
		HKQuantityTypeIdentifierEnvironmentalAudioExposure,	// Pressure,                    DiscreteEquivalentContinuousLevel - iOS 13
		HKQuantityTypeIdentifierHeadphoneAudioExposure,		// Pressure,                    DiscreteEquivalentContinuousLevel - iOS 13
		HKQuantityTypeIdentifierNumberOfAlcoholicBeverages,	// Scalar(Count),               Cumulative

		// Nutrition
		HKQuantityTypeIdentifierDietaryFatTotal,            // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietaryFatPolyunsaturated,  // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietaryFatMonounsaturated,  // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietaryFatSaturated,        // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietaryCholesterol,         // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietarySodium,              // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietaryCarbohydrates,       // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietaryFiber,               // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietarySugar,               // Mass,   						Cumulative
		HKQuantityTypeIdentifierDietaryEnergyConsumed,      // Energy, 						Cumulative
		HKQuantityTypeIdentifierDietaryProtein,             // Mass,   						Cumulative

		HKQuantityTypeIdentifierDietaryVitaminA,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryVitaminB6,           // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryVitaminB12,          // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryVitaminC,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryVitaminD,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryVitaminE,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryVitaminK,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryCalcium,             // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryIron,                // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryThiamin,             // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryRiboflavin,          // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryNiacin,              // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryFolate,              // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryBiotin,              // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryPantothenicAcid,     // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryPhosphorus,          // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryIodine,              // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryMagnesium,           // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryZinc,                // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietarySelenium,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryCopper,              // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryManganese,           // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryChromium,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryMolybdenum,          // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryChloride,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryPotassium,           // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryCaffeine,            // Mass, 						Cumulative
		HKQuantityTypeIdentifierDietaryWater,               // Volume, 						Cumulative	- iOS 9.0

		// Mobility
		HKQuantityTypeIdentifierSixMinuteWalkTestDistance,      // Length,         - iOS 14
		HKQuantityTypeIdentifierWalkingSpeed,                   // Distance/Time,  - iOS 14
		HKQuantityTypeIdentifierWalkingStepLength,              // Length,         - iOS 14
		HKQuantityTypeIdentifierWalkingAsymmetryPercentage,     // Percent,        - iOS 14
		HKQuantityTypeIdentifierWalkingDoubleSupportPercentage, // Percent,        - iOS 14
		HKQuantityTypeIdentifierStairAscentSpeed,               // Distance/Time,  - iOS 14
		HKQuantityTypeIdentifierStairDescentSpeed,              // Distance/Time,  - iOS 14

		HKQuantityTypeIdentifierUVExposure,                 // Count,				Discrete	- iOS 9.0
		HKQuantityTypeIdentifierAtrialFibrillationBurden,	// Percent,				Discrete	- iOS 16.0
		HKQuantityTypeIdentifierUnderwaterDepth,			// Length				Discrete	- iOS 16.0
		HKQuantityTypeIdentifierWaterTemperature,			// Temperature			Discrete	- iOS 16.0


		/*--------------------------------*/
		/*   HKCategoryType Identifiers   */
		/*--------------------------------*/

		HKCategoryTypeIdentifierSleepAnalysis,  			// HKCategoryValueSleepAnalysis
		HKCategoryTypeIdentifierAppleStandHour, 			// HKCategoryValueAppleStandHour			- iOS 9.0
		HKCategoryTypeIdentifierCervicalMucusQuality,		// HKCategoryValueCervicalMucusQuality		- iOS 9.0
		HKCategoryTypeIdentifierOvulationTestResult,		// HKCategoryValueOvulationTestResult		- iOS 9.0
		HKCategoryTypeIdentifierMenstrualFlow,				// HKCategoryValueMenstrualFlow				- iOS 9.0
		HKCategoryTypeIdentifierIntermenstrualBleeding,		// (Spotting) HKCategoryValue				- iOS 9.0
		HKCategoryTypeIdentifierPersistentIntermenstrualBleeding,	// HKCategoryValue					- iOS 16.0
		HKCategoryTypeIdentifierProlongedMenstrualPeriods,			// HKCategoryValue					- iOS 16
		HKCategoryTypeIdentifierIrregularMenstrualCycles,			// HKCategoryValue					- iOS 16
		HKCategoryTypeIdentifierInfrequentMenstrualCycles,			// HKCategoryValue					- iOS 16
		HKCategoryTypeIdentifierSexualActivity,				// HKCategoryValue							- iOS 9.0
		HKCategoryTypeIdentifierMindfulSession,             // HKCategoryValue                          - iOS 10.0
		HKCategoryTypeIdentifierHighHeartRateEvent,         // HKCategoryValue                          - iOS 12.2
		HKCategoryTypeIdentifierLowHeartRateEvent,          // HKCategoryValue                          - iOS 12.2
		HKCategoryTypeIdentifierIrregularHeartRhythmEvent,  // HKCategoryValue                          - iOS 12.2
		HKCategoryTypeIdentifierAudioExposureEvent,         // HKCategoryValueAudioExposureEvent        - iOS 13
		HKCategoryTypeIdentifierToothbrushingEvent,         // HKCategoryValue                          - iOS 13
		HKCategoryTypeIdentifierPregnancy,                  // HKCategoryValue,							- iOS 14.3
		HKCategoryTypeIdentifierLactation,                  // HKCategoryValue,							- iOS 14.3
		HKCategoryTypeIdentifierContraceptive,              // HKCategoryValueContraceptive,			- iOS 14.3
		HKCategoryTypeIdentifierEnvironmentalAudioExposureEvent,// HKCategoryValueEnvironmentalAudioExposureEvent	- iOS 14.0
		HKCategoryTypeIdentifierHeadphoneAudioExposureEvent,	// HKCategoryValueHeadphoneAudioExposureEvent		- iOS 14.2
		HKCategoryTypeIdentifierHandwashingEvent,				// HKCategoryValue									- iOS 14.0
		HKCategoryTypeIdentifierLowCardioFitnessEvent,			//													- iOS 14.3
		HKCategoryTypeIdentifierAppleWalkingSteadinessEvent,	// HKCategoryValueAppleWalkingSteadinessEvent 		- iOS 15

		// Symptoms
		HKCategoryTypeIdentifierAbdominalCramps,		// HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierAcne,                   // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierAppetiteChanges,        // HKCategoryValueAppetiteChanges				- iOS 13.6
		HKCategoryTypeIdentifierBladderIncontinence,    // HKCategoryValueSeverity						- iOS 14.0
		HKCategoryTypeIdentifierBloating,               // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierBreastPain,             // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierChestTightnessOrPain,   // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierChills,                 // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierConstipation,           // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierCoughing,               // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierDiarrhea,               // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierDizziness,              // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierDrySkin,                // HKCategoryValueSeverity						- iOS 14.0
		HKCategoryTypeIdentifierFainting,               // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierFatigue,                // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierFever,                  // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierGeneralizedBodyAche,    // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierHairLoss,               // HKCategoryValueSeverity						- iOS 14.0
		HKCategoryTypeIdentifierHeadache,               // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierHeartburn,              // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierHotFlashes,             // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierLossOfSmell,            // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierLossOfTaste,            // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierLowerBackPain,          // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierMemoryLapse,            // HKCategoryValueSeverity						- iOS 14.0
		HKCategoryTypeIdentifierMoodChanges,            // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierNausea,                 // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierNightSweats,            // HKCategoryValueSeverity						- iOS 14.0
		HKCategoryTypeIdentifierPelvicPain,             // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierRapidPoundingOrFlutteringHeartbeat, // HKCategoryValueSeverity			- iOS 13.6
		HKCategoryTypeIdentifierRunnyNose,              // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierShortnessOfBreath,      // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierSinusCongestion,        // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierSkippedHeartbeat,       // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierSleepChanges,           // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierSoreThroat,             // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierVaginalDryness,         // HKCategoryValueSeverity						- iOS 14.0
		HKCategoryTypeIdentifierVomiting,               // HKCategoryValueSeverity						- iOS 13.6
		HKCategoryTypeIdentifierWheezing,               // HKCategoryValueSeverity						- iOS 13.6


		/*--------------------------------------*/
		/*   HKCharacteristicType Identifiers   */
		/*--------------------------------------*/

		HKCharacteristicTypeIdentifierBiologicalSex,		// HKCharacteristicBiologicalSex
		HKCharacteristicTypeIdentifierBloodType,			// HKCharacteristicBloodType
		HKCharacteristicTypeIdentifierDateOfBirth,			// NSDate
		HKCharacteristicTypeIdentifierFitzpatrickSkinType,	// FitzpatrickSkinType					- iOS 9.0
		HKCharacteristicTypeIdentifierWheelchairUse,        // WheelchairUseObject                  - iOS 10.0
		HKCharacteristicTypeIdentifierActivityMoveMode,		// HKActivityMoveModeObject				- iOS 14

		/*-----------------------------------*/
		/*   HKCorrelationType Identifiers   */
		/*-----------------------------------*/

		HKCorrelationTypeIdentifierBloodPressure,
		HKCorrelationTypeIdentifierFood,

		/*--------------------------------*/
		/*   HKDocumentType Identifiers   */
		/*--------------------------------*/

		HKDocumentTypeIdentifierCDA,                        //  - iOS 10.0
	
		/*------------------------------*/
		/*   HKWorkoutType Identifier   */
		/*------------------------------*/

		HKWorkoutTypeIdentifier,

		/*--------------------------------*/
		/*   HKSeriesSample Identifiers   */
		/*--------------------------------*/

		HKWorkoutRouteTypeIdentifier,			// - iOS 11
		HKDataTypeIdentifierHeartbeatSeries,	// - iOS 13

		/*-----------------------------------*/
		/* HKVisionPrescription Identifier   */
		/*-----------------------------------*/

		HKVisionPrescriptionTypeIdentifier,		// - iOS 16

		NUM_TYPES
	};

	/*! @enum WorkoutActivityType
		@ingroup Enumerations
		@brief Identifiers for the types of workouts that HealthKit supports.
	 */
	public enum WorkoutActivityType {
		AmericanFootball = 1,
		Archery,
		AustralianFootball,
		Badminton,
		Baseball,
		Basketball,
		Bowling,
		Boxing, // Kickboxing, Boxing, etc.
		Climbing,
		Cricket,
		CrossTraining, // Any mix of cardio and/or strength and/or flexibility
		Curling,
		Cycling,
		Dance,
		DanceInspiredTraining, // Pilates, Barre, Feldenkrais, etc.
		Elliptical,
		EquestrianSports, // Polo, Horse Racing, Horse Riding, etc.
		Fencing,
		Fishing,
		FunctionalStrengthTraining, // Primarily free weights and/or body weight and/or accessories
		Golf,
		Gymnastics,
		Handball,
		Hiking,
		Hockey, // Ice Hockey, Field Hockey, etc.
		Hunting,
		Lacrosse,
		MartialArts,
		MindAndBody, // Tai chi, meditation, etc.
		MixedMetabolicCardioTraining, // Any mix of cardio-focused exercises
		PaddleSports, // Canoeing, Kayaking, Outrigger, Stand Up Paddle Board, etc.
		Play, // Dodge Ball, Hopscotch, Tetherball, Jungle Gym, etc.
		PreparationAndRecovery, // Foam rolling, stretching, etc.
		Racquetball,
		Rowing,
		Rugby,
		Running,
		Sailing,
		SkatingSports, // Ice Skating, Speed Skating, Inline Skating, Skateboarding, etc.
		SnowSports, // Skiing, Snowboarding, Cross-Country Skiing, etc.
		Soccer,
		Softball,
		Squash,
		StairClimbing,
		SurfingSports, // Traditional Surfing, Kite Surfing, Wind Surfing, etc.
		Swimming,
		TableTennis,
		Tennis,
		TrackAndField, // Shot Put, Javelin, Pole Vaulting, etc.
		TraditionalStrengthTraining, // Primarily machines and/or free weights
		Volleyball,
		Walking,
		WaterFitness,
		WaterPolo,
		WaterSports, // Water Skiing, Wake Boarding, etc.
		Wrestling,
		Yoga,

		// iOS 10 / watchOS 3
		Barre,    // HKWorkoutActivityTypeDanceInspiredTraining
		CoreTraining,
		CrossCountrySkiing,
		DownhillSkiing,
		Flexibility,
		HighIntensityIntervalTraining,
		JumpRope,
		Kickboxing,
		Pilates,    // HKWorkoutActivityTypeDanceInspiredTraining
		Snowboarding,
		Stairs,
		StepTraining,
		WheelchairWalkPace,
		WheelchairRunPace,

		// iOS 11 / watchOS 4
		TaiChi,
		MixedCardio,    // HKWorkoutActivityTypeMixedMetabolicCardioTraining
		HandCycling,

		// iOS 13 / watchOS 6
		DiscSports,
		FitnessGaming,

		Other = 3000,
	}


	/*! @enum SleepAnalysis
		@ingroup Enumerations
		@brief Identifiers for sleep analysis data. Possible values are "InBed" (0), "Asleep" (1) and "Awake" (2).
	 */
	public enum SleepAnalysis {
	   InBed = 0,
	   Asleep,
	   Awake,
	   AsleepCore,
	   AsleepDeep,
	   AsleepREM
	}

	/*! @enum AppleStandHour
		@ingroup Enumerations
		@brief Identifiers for whether the user has stood for at least one minute during the sample.
	 */
	public enum AppleStandHour {
	   Stood = 0,
	   Idle
	}

	/*! @enum CervicalMucusQuality
		@ingroup Enumerations
		@brief Identifiers for representing the quality of the user’s cervical mucus.
	 */
	public enum CervicalMucusQuality {
	   Dry = 1,
	   Sticky,
	   Creamy,
	   Watery,
	   EggWhite
	}

	/*! @enum OvulationTestResult
		@ingroup Enumerations
		@brief Identifiers for recording the result of an ovulation home test.
	 */
	public enum OvulationTestResult {
		Negative = 1,
		//Positive, -- DEPRECATED
		LuteinizingHormoneSurge,
		Indeterminate,
		EstrogenSurge
	}

	/*! @enum PregnancyTestResult
		@ingroup Enumerations
		@brief Identifiers for recording the result of a pregnancy test.
	 */
	public enum PregnancyTestResult {
		Negative = 1,
		Positive, 
		Indeterminate
	}

	/*! @enum ProgresteroneTestResult
		@ingroup Enumerations
		@brief Identifiers for recording the result of a home ovulation confirmation test that use surges in hormone levels to
               confirm if ovulation has occurred.
	 */
	public enum ProgresteroneTestResult {
		Negative = 1,
		Positive, 
		Indeterminate
	}

	/*! @enum MenstrualFlow
		@ingroup Enumerations
		@brief Identifiers for representing menstrual cycles.
	 */
	public enum MenstrualFlow {
	   Unspecified = 0,
	   Light,
	   Medium,
	   Heavy
	}

	/*! @enum Contraceptive
		@ingroup Enumerations
		@brief Identifiers for representing types of contraceptives.
	 */
	public enum Contraceptive {
	   Unspecified = 1,
	   Implant,
	   Injection,
	   IntrauterineDevice,
	   IntravaginalRing,
	   Oral,
	   Patch
	}

	/*! @enum Severity
		@ingroup Enumerations
		@brief Identifiers for representing the severity of a symptom.
	 */
	public enum Severity {
	   Unspecified = 0,
	   NotPresent,
	   Mild,
	   Moderate,
	   Severe
	}

	/*! @enum AppetiteChanges
		@ingroup Enumerations
		@brief Identifiers for representing the direction of appetite changes.
	 */
	public enum AppetiteChanges {
	   Unspecified = 0,
	   NoChange,
	   Decreased,
	   Increased
	}

	// --------------------------------------------

	/*! @enum BiologicalSex
		@ingroup Enumerations
		@brief possible values for Biological Sex
	 */
	public enum BiologicalSex {
		NotSet = 0,
		Female,
		Male,
		Other
	}

	/*! @enum BloodType
		@ingroup Enumerations
		@brief possible values for BloodType
	 */
	public enum BloodType {
		NotSet = 0,
		APositive,
		ANegative,
		BPositive,
		BNegative,
		ABPositive,
		ABNegative,
		OPositive,
		ONegative
	}

	/*! @enum FitzpatrickSkinType
		@ingroup Enumerations
		@brief possible values for Fitzpatrick Skin Type
	 */
	public enum FitzpatrickSkinType {
		FitzpatrickSkinTypeNotSet = 0,
		FitzpatrickSkinTypeI,
		FitzpatrickSkinTypeII,
		FitzpatrickSkinTypeIII,
		FitzpatrickSkinTypeIV,
		FitzpatrickSkinTypeV,
		FitzpatrickSkinTypeVI
	}

	/*! @enum WheelchairUse
		@ingroup Enumerations
		@brief possible values for Wheelchair Use
	 */
	public enum WheelchairUse {
		WheelchairUseNotSet = 0,
		WheelchairUseNo,
		WheelchairUseYes
	}

		// --------------------------------------------

	/*! @enum     StatisticsOptions
		@ingroup  Enumerations
		@brief    Options for specifying the statistic to calculate
	 */
	public enum StatisticsOptions
	{
		None = 0,
		SeparateBySource,
		DiscreteAverage,
		DiscreteMin,
		DiscreteMax,
		CumulativeSum,
		DiscreteMostRecent

	}

	/*! @enum     HKClinicalType
		@ingroup  Enumerations
		@brief    the clinical types supported by HealthKit
	 */
	public enum HKClinicalType
	{
		HKClinicalTypeIdentifierAllergyRecord,
		HKClinicalTypeIdentifierConditionRecord, 
		HKClinicalTypeIdentifierImmunizationRecord,
		HKClinicalTypeIdentifierLabResultRecord,
		HKClinicalTypeIdentifierMedicationRecord,
		HKClinicalTypeIdentifierProcedureRecord,
		HKClinicalTypeIdentifierVitalSignRecord,

		NUM_TYPES
	}

	public enum HKUpdateFrequency
	{
		HKUpdateFrequencyImmediate = 1,
    	HKUpdateFrequencyHourly,
    	HKUpdateFrequencyDaily,
    	HKUpdateFrequencyWeekly
	}

	// --------------------------------------------


		/*!	@brief Storage class for HealthKit data types. Used to create the editor UI & authorization.
			*/
	[System.Serializable]
	public class HKNameValuePair : System.Object {
		public string name;   /*!< human-readable name of the HKDataType */
		public bool read;     /*!< read permission? */
		public bool write;    /*!< write permission? */
		public bool writable; /*!< is writing allowed? */

		/*!	@brief default constructor
		 */
		public HKNameValuePair(string n, bool w) {
			this.name = n;
			this.read = false;
			this.write = false;
			this.writable = w;
		}
	}

	// --------------------------------------------


	/*!	@brief HealthKit data types to authorize. Used to create the editor UI.
	 */
	[ExecuteInEditMode]
	public class HealthKitDataTypes : MonoBehaviour
	{
		/*! @brief serializable representation of the data types to read/write.  */
		public string saveData = null;

		/*! @brief Text to present to the user when iOS requests access to read health data.  */
		public string healthShareUsageDescription = "We require access to health data for testing.";
	
		/*! @brief Text to present to the user when iOS requests access to write health data. */
		public string healthUpdateUsageDescription = "We update health data for testing.";

		/*! @brief Text to present to the user when iOS requests access to write clinical health data. */
		public string clinicalUsageDescription = "We only request clinical data that we need.";

		/*! @brief dictionary of identifier/read+write values */
		public Dictionary<string, HKNameValuePair> data; 

		void Awake() {
			// this.data.Load();
		}

		void OnEnable() {
			Load();
		}

		void OnDisable() {
			// Save();
		}

		private void InitializeEntry<T>(T type, string typeName, bool writable = true) where T: Enum {
			string key = GetIdentifier(type);
			if (!data.ContainsKey(key)) {
				data[key] = new HKNameValuePair(typeName, writable);
			}
		}

		/*! @brief Convenience method to get a HKDataType as a string value
		 */
		public static string GetIdentifier<T>(T type) where T: Enum {
			return Enum.GetName(typeof(T), type);
		}

		private void Initialize() {
			Debug.Log("[initializing]");
			// Body Measurements
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBodyMassIndex, "Body Mass Index");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBodyFatPercentage, "Body Fat Percentage");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierHeight, "Height");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBodyMass, "Body Mass");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierLeanBodyMass, "Lean Body Mass");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWaistCircumference, "Waist Circumference");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierAppleSleepingWristTemperature, "Sleeping Wrist Temperature");

			// Fitness
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierStepCount, "Step Count");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning, "Walking/Running Distance");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDistanceCycling, "Cycling Distance");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDistanceWheelchair, "Wheelchair Distance");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBasalEnergyBurned, "Basal Energy Burned");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierActiveEnergyBurned, "Active Energy Burned");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierFlightsClimbed, "Flights Climbed");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierNikeFuel, "Nike Fuel", false);
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierAppleExerciseTime, "Apple Exercise Time", false);
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierPushCount, "Wheelchair Push Count");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDistanceSwimming, "Swimming Distance");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierSwimmingStrokeCount, "Swimming Stroke Count");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierVO2Max, "Max VO2");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDistanceDownhillSnowSports, "Downhill Snow Sports Distance");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierAppleStandTime, "Stand Time");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingSpeed, "Walking Speed");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingDoubleSupportPercentage, "Walking Double Support %");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingAsymmetryPercentage, "Walking Asymmetry %");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingStepLength, "Walking Step Length");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierSixMinuteWalkTestDistance, "Six Minute Walk Test Distance");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierStairAscentSpeed, "Stair Ascent Speed");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierStairDescentSpeed, "Stair Descent Speed");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierAppleMoveTime, "Move Time");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierAppleWalkingSteadiness, "Walking Steadiness");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierRunningStrideLength, "Running Stride Length");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierRunningVerticalOscillation, "Running Vertical Oscillation");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierRunningGroundContactTime, "Running Ground Contact Time");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierRunningPower, "Running Power");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierRunningSpeed, "Running Speed");
		
			// Vitals
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierHeartRate, "Heart Rate");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBodyTemperature, "Body Temperature");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBasalBodyTemperature, "Basal Body Temperature");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBloodPressureSystolic, "Systolic Blood Pressure");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBloodPressureDiastolic, "Diastolic Blood Pressure");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierRespiratoryRate, "Respiratory Rate");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierRestingHeartRate, "Resting Heart Rate");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingHeartRateAverage, "Average Walking Heart Rate", false);
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierHeartRateVariabilitySDNN, "SDNN Heart Beat-Beat Intervals");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierHeartRateRecoveryOneMinute, "Heart Rate Recovery - One Minute");

			// Results
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierOxygenSaturation, "Oxygen Saturation");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierPeripheralPerfusionIndex, "Peripheral Perfusion Index");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBloodGlucose, "Blood Glucose");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierNumberOfTimesFallen, "Number of Times Fallen");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierElectrodermalActivity, "Electrodermal Activity");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierInhalerUsage, "Inhaler Usage");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierInsulinDelivery, "Insulin Delivery");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierBloodAlcoholContent, "Blood Alcohol Content");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierForcedVitalCapacity, "Forced Vital Capacity");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierForcedExpiratoryVolume1, "Forced Expiratory Volume");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierPeakExpiratoryFlowRate, "Peak Expiratory Flow Rate");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierEnvironmentalAudioExposure, "Environmental Audio Exposure");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierHeadphoneAudioExposure, "Headphone Audio Exposure");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierNumberOfAlcoholicBeverages, "Alcoholic Beverages");

			// Nutrition
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryFatTotal, "Dietary Fat Total");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryFatPolyunsaturated, "Dietary Fat (polyunsaturated)");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryFatMonounsaturated, "Dietary Fat (monounsaturated)");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryFatSaturated, "Dietary Fat (saturated)");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryCholesterol, "Dietary Cholesterol");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietarySodium, "Dietary Sodium");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryCarbohydrates, "Dietary Carbohydrates");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryFiber, "Dietary Fiber");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietarySugar, "Dietary Sugar");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryEnergyConsumed, "Dietary Energy Consumed");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryProtein, "Dietary Protein");

			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryVitaminA, "Vitamin A");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryVitaminB6, "Vitamin B6");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryVitaminB12, "Vitamin B12");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryVitaminC, "Vitamin C");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryVitaminD, "Vitamin D");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryVitaminE, "Vitamin E");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryVitaminK, "Vitamin K");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryCalcium, "Dietary Calcium");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryIron, "Dietary Iron");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryThiamin, "Dietary Thiamin");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryRiboflavin, "Dietary Riboflavin");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryNiacin, "Dietary Niacin");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryFolate, "Dietary Folate");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryBiotin, "Dietary Biotin");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryPantothenicAcid, "Dietary Pantothenic Acid");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryPhosphorus, "Dietary Phosphorus");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryIodine, "Dietary Iodine");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryMagnesium, "Dietary Magnesium");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryZinc, "Dietary Zinc");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietarySelenium, "Dietary Selenium");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryCopper, "Dietary Copper");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryManganese, "Dietary Manganese");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryChromium, "Dietary Chromium");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryMolybdenum, "Dietary Molybdenum");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryChloride, "Dietary Chloride");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryPotassium, "Dietary Potassium");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryCaffeine, "Caffeine");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierDietaryWater, "Dietary Water");

			InitializeEntry(HKDataType.HKQuantityTypeIdentifierUVExposure, "UV Exposure");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierAtrialFibrillationBurden, "Atrial Fibrillation Burden");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierUnderwaterDepth, "Underwater Depth");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWaterTemperature, "Water Temperature");
			
			// iOS 14
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingSpeed, "Walking Speed");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingDoubleSupportPercentage, "Walking Double Support Percentage");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingAsymmetryPercentage, "Walking Asymmetry Percentage");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierWalkingStepLength, "Walking Step Length");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierSixMinuteWalkTestDistance, "Six Minute Walk Test Distance");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierStairAscentSpeed, "Stair Ascent Speed");
			InitializeEntry(HKDataType.HKQuantityTypeIdentifierStairDescentSpeed, "Stair Descent Speed");

			/*--------------------------------*/
			/*   HKCategoryType Identifiers   */
			/*--------------------------------*/

			InitializeEntry(HKDataType.HKCategoryTypeIdentifierSleepAnalysis, "Sleep Analysis");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierAppleStandHour, "Hours Standing", false);
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierCervicalMucusQuality, "Cervical Mucus Quality");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierOvulationTestResult, "Ovulation Test Result");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierMenstrualFlow, "Menstrual Flow");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierIntermenstrualBleeding, "Intermenstrual Bleeding");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierPersistentIntermenstrualBleeding, "Persistent Intermenstrual Bleeding");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierProlongedMenstrualPeriods, "Prolonged Menstrual Periods");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierIrregularMenstrualCycles, "Irregular Menstrual Cycles");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierInfrequentMenstrualCycles, "Infrequent Menstrual Cycles");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierSexualActivity, "Sexual Activity");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierMindfulSession, "Mindful Session");

			InitializeEntry(HKDataType.HKCategoryTypeIdentifierHighHeartRateEvent, "High Heart Rate Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierLowHeartRateEvent, "Low Heart Rate Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierIrregularHeartRhythmEvent, "Irregular Heart Rhythm Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierAudioExposureEvent, "Audio Exposure Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierToothbrushingEvent, "Toothbrushing Event");

			// iOS 13.6
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierAbdominalCramps, "Abdominal Cramps");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierAcne, "Acne");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierAppetiteChanges, "Appetite Changes");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierBloating, "Bloating");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierBreastPain, "Breast Pain");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierChestTightnessOrPain, "Chest Tightness or Pain");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierChills, "Chills");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierConstipation, "Constipation");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierCoughing, "Coughing");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierDiarrhea, "Diarrhea");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierDizziness, "Dizziness");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierFainting, "Fainting");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierFatigue, "Fatigue");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierFever, "Fever");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierGeneralizedBodyAche, "Generalized Body Ache");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierHeadache, "Headache");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierHeartburn, "Heartburn");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierHotFlashes, "Hot Flashes");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierLossOfSmell, "Loss of Smell");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierLossOfTaste, "Loss of Taste");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierLowerBackPain, "Back Pain");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierMoodChanges, "Mood Changes");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierNausea, "Nausea");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierPelvicPain, "Pelvic Pain");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierRapidPoundingOrFlutteringHeartbeat, "Rapid Pounding or Fluttering Heartbeat");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierRunnyNose, "Runny Nose");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierShortnessOfBreath, "Shortness of Breath");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierSinusCongestion, "Sinus Congestion");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierSkippedHeartbeat, "Skipped Heartbeat");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierSleepChanges, "Sleep Changes");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierSoreThroat, "Sore Throat");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierVomiting, "Vomiting");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierWheezing, "Wheezing");

			// iOS 14
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierEnvironmentalAudioExposureEvent, "Environmental Audio Exposure Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierHandwashingEvent, "Handwashing Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierLowCardioFitnessEvent, "Low Cardio Fitness Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierAppleWalkingSteadinessEvent, "Walking Steadiness Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierBladderIncontinence, "Bladder Incontinence");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierDrySkin, "Dry Skin");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierHairLoss, "Hair Loss");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierMemoryLapse, "Memory Lapse");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierNightSweats, "Night Sweats");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierVaginalDryness, "Vaginal Dryness");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierHeadphoneAudioExposureEvent, "Headphone Audio Exposure Event");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierPregnancy, "Pregnancy");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierLactation, "Lactation");
			InitializeEntry(HKDataType.HKCategoryTypeIdentifierContraceptive, "Contraceptive");

			/*--------------------------------------*/
			/*   HKCharacteristicType Identifiers   */
			/*--------------------------------------*/

			InitializeEntry(HKDataType.HKCharacteristicTypeIdentifierBiologicalSex, "Biological Sex", false);
			InitializeEntry(HKDataType.HKCharacteristicTypeIdentifierBloodType, "Blood Type", false);
			InitializeEntry(HKDataType.HKCharacteristicTypeIdentifierDateOfBirth, "Date of Birth", false);
			InitializeEntry(HKDataType.HKCharacteristicTypeIdentifierFitzpatrickSkinType, "Fitzpatrick Skin Type", false);
			InitializeEntry(HKDataType.HKCharacteristicTypeIdentifierWheelchairUse, "Wheelchair use", false);
			InitializeEntry(HKDataType.HKCharacteristicTypeIdentifierActivityMoveMode, "Activity Move Mode", false);

			// iOS 14
			InitializeEntry(HKDataType.HKCharacteristicTypeIdentifierActivityMoveMode, "Activity Move Mode");

			/*-----------------------------------*/
			/*   HKCorrelationType Identifiers   */
			/*-----------------------------------*/

			InitializeEntry(HKDataType.HKCorrelationTypeIdentifierBloodPressure, "Blood Pressure");
			InitializeEntry(HKDataType.HKCorrelationTypeIdentifierFood, "Food");

			/*--------------------------------*/
			/*   HKDocumentType Identifiers   */
			/*--------------------------------*/

			InitializeEntry(HKDataType.HKDocumentTypeIdentifierCDA, "CDA Document");

			/*------------------------------*/
			/*   HKWorkoutType Identifier   */
			/*------------------------------*/

			InitializeEntry(HKDataType.HKWorkoutTypeIdentifier, "Workout Type");

			/*-------------------------------*/
			/*   HKClinicalType Identifier   */
			/*-------------------------------*/

			InitializeEntry(HKClinicalType.HKClinicalTypeIdentifierAllergyRecord, "Allergy Record", false);
			InitializeEntry(HKClinicalType.HKClinicalTypeIdentifierConditionRecord, "Condition Record", false);
			InitializeEntry(HKClinicalType.HKClinicalTypeIdentifierImmunizationRecord, "Immunization Record", false);
			InitializeEntry(HKClinicalType.HKClinicalTypeIdentifierLabResultRecord, "Lab Result Record", false);
			InitializeEntry(HKClinicalType.HKClinicalTypeIdentifierMedicationRecord, "Medication Record", false);
			InitializeEntry(HKClinicalType.HKClinicalTypeIdentifierProcedureRecord, "Procedure Record", false);
			InitializeEntry(HKClinicalType.HKClinicalTypeIdentifierVitalSignRecord, "Vital Sign Record", false);
		}

		// -----------------------------

		/*! @brief Save the authorization preferences.
		 */
		public string Save() {
			string newSaveData = this.saveData;

			#if UNITY_EDITOR
			// Debug.Log("[EDITOR] save");
			if (this.data != null) {
				// Debug.Log("-- have data to save");
				using (MemoryStream stream = new MemoryStream()) {
					BinaryFormatter bin = new BinaryFormatter();
					// Debug.LogFormat("bin.Serialize({0}, {1})", stream, this.data);
					bin.Serialize(stream, this.data);
					string text = Convert.ToBase64String(stream.ToArray());
					newSaveData = text;
				}
			} else {
				Debug.Log("--- NO data to save");
			}
			#endif

			return newSaveData;
		}

		/*! @brief Load the authorization preferences from a supplied file.
			@param saveAsset the save data asset
		 */
		public void Load(TextAsset saveAsset) {
			byte[] bytes = Convert.FromBase64String(saveAsset.text);
			using (MemoryStream stream = new MemoryStream(bytes)) {
				BinaryFormatter bin = new BinaryFormatter();
				this.data = (Dictionary<string, HKNameValuePair>)bin.Deserialize(stream);
			}
		}

		/*! @brief Load the authorization preferences.
		 */
		public void Load() {
			if (this.saveData != null && this.saveData.Length > 0) {
				byte[] bytes = Convert.FromBase64String(this.saveData);
				using (MemoryStream stream = new MemoryStream(bytes)) {
					BinaryFormatter bin = new BinaryFormatter();
					this.data = (Dictionary<string, HKNameValuePair>)bin.Deserialize(stream);
				}
			} else {
				// something went wrong
				this.data = new Dictionary<string, HKNameValuePair>();
			}

			Initialize();
		}

		// -----------------------------

		/*! @brief Create a comma-separated list of HealthKit datatypes to authorize */
		public string Transmit() {
			List<string> readList = new List<string>();
			foreach (KeyValuePair<string, HKNameValuePair> pair in this.data) {
				if (pair.Value.read) {
					var key = pair.Key;
					if (key == "HKCategoryTypeIdentifierEnvironmentalAudioExposureEvent") key = "HKCategoryTypeIdentifierAudioExposureEvent"; // overrides deprecated name
					readList.Add(key);
				}
			}

			List<string> writeList = new List<string>();
			foreach (KeyValuePair<string, HKNameValuePair> pair in this.data) {
				if (pair.Value.write) {
					var key = pair.Key;
					if (key == "HKCategoryTypeIdentifierEnvironmentalAudioExposureEvent") key = "HKCategoryTypeIdentifierAudioExposureEvent"; // overrides deprecated name
					writeList.Add(key);
				}
			}

			return String.Join(",", readList.ToArray()) + "|" + String.Join(",", writeList.ToArray());
		}

		/*! @brief Returns true if there are data types we want to read, and need to request permission to read health data. 
		           Always required.
		 */
		public bool AskForSharePermission() {
			return true;
		}

		/*! @brief Returns true if there are data types we want to write, and need to request permission to write health data. */
		public bool AskForUpdatePermission() {
			foreach (KeyValuePair<string, HKNameValuePair> pair in this.data) {
				if (pair.Value.write) return true;
			}
			return false;
		}

		/*! @brief Returns true if there are clinical data types we want to read, and need to request permission to read health data. */
		public bool AskForClinicalPermission() {
			Array values = Enum.GetValues(typeof(HKClinicalType));
			for (int i = 0; i < (int)HKClinicalType.NUM_TYPES; i++) {
				HKClinicalType t = (HKClinicalType)i;
				if (this.data[t.ToString()].read) return true;
			}
			return false;
		}
	}

}