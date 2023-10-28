//
//  BEHealthKit+dummyData.m
//  Unity-iPhone
//
//  Created by greay on 5/16/15.
//
//

#import "BEHealthKit+dummyData.h"
#import <HealthKit/HealthKit.h>

/*! @brief Like NSRange, but with floats.
 */
typedef struct {
	CGFloat location;	/*!< the start of the range. */
	CGFloat length;	/*!< the length of the range. */
} FRange;

/*! @brief Make an FRange. */
NS_INLINE FRange FRangeMake(CGFloat loc, CGFloat len) {
	FRange r;
	r.location = loc;
	r.length = len;
	return r;
}

/*! @brief Convenience method for a 1/100 chance. */
BOOL chance(CGFloat n) {
	 return ((arc4random_uniform(100.0)) < n);
}

#define RAND_PRECISION 1024
CGFloat R(FRange range) {
	CGFloat r = arc4random_uniform(RAND_PRECISION) / (float)RAND_PRECISION;
	CGFloat v = range.location + range.length * r;
	return v;
}

@implementation BEHealthKit (dummyData)

- (void)generateDummyData
{
	/*--------------------------------*/
	/*   HKQuantityType Identifiers   */
	/*--------------------------------*/
	
	// Body Measurements
	[self dummyQuantityType:HKQuantityTypeIdentifierBodyMassIndex unit:[HKUnit countUnit] rangeForData:FRangeMake(18, 35) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierBodyFatPercentage unit:[HKUnit percentUnit] rangeForData:FRangeMake(0, 0.35) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierHeight unit:[HKUnit footUnit] rangeForData:FRangeMake(4, 7) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierBodyMass unit:[HKUnit poundUnit] rangeForData:FRangeMake(91, 300) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierLeanBodyMass unit:[HKUnit poundUnit] rangeForData:FRangeMake(80, 200) doTimes:10];
	
	// Fitness
	[self dummyQuantityType:HKQuantityTypeIdentifierStepCount unit:[HKUnit countUnit] rangeForData:FRangeMake(0, 2000) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDistanceWalkingRunning unit:[HKUnit mileUnit] rangeForData:FRangeMake(0, 10) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDistanceCycling unit:[HKUnit mileUnit] rangeForData:FRangeMake(0, 20) doTimes:10];
	if (&HKQuantityTypeIdentifierDistanceWheelchair != NULL) [self dummyQuantityType:HKQuantityTypeIdentifierDistanceWheelchair unit:[HKUnit mileUnit] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierBasalEnergyBurned unit:[HKUnit kilocalorieUnit] rangeForData:FRangeMake(1500, 2100) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierActiveEnergyBurned unit:[HKUnit kilocalorieUnit] rangeForData:FRangeMake(120, 2400) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierFlightsClimbed unit:[HKUnit countUnit] rangeForData:FRangeMake(0, 10) doTimes:10];
	// HKQuantityTypeIdentifierNikeFuel
	// HKQuantityTypeIdentifierAppleExerciseTime
	if (&HKQuantityTypeIdentifierPushCount != NULL) [self dummyQuantityType:HKQuantityTypeIdentifierPushCount unit:[HKUnit countUnit] rangeForData:FRangeMake(0, 10) doTimes:10];
	if (&HKQuantityTypeIdentifierDistanceSwimming != NULL) [self dummyQuantityType:HKQuantityTypeIdentifierDistanceSwimming unit:[HKUnit mileUnit] rangeForData:FRangeMake(0, 20) doTimes:10];
	if (&HKQuantityTypeIdentifierSwimmingStrokeCount != NULL) [self dummyQuantityType:HKQuantityTypeIdentifierSwimmingStrokeCount unit:[HKUnit countUnit] rangeForData:FRangeMake(0, 10) doTimes:10];
	
	// Vitals
	[self dummyQuantityType:HKQuantityTypeIdentifierHeartRate unit:[[HKUnit countUnit] unitDividedByUnit:[HKUnit minuteUnit]] rangeForData:FRangeMake(50, 110) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierBodyTemperature unit:[HKUnit degreeFahrenheitUnit] rangeForData:FRangeMake(97.5, 3.5) doTimes:10];
	if (&HKQuantityTypeIdentifierBasalBodyTemperature != NULL) [self dummyQuantityType:HKQuantityTypeIdentifierBasalBodyTemperature unit:[HKUnit degreeFahrenheitUnit] rangeForData:FRangeMake(97.5, 3.5) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierBloodPressureSystolic unit:[HKUnit millimeterOfMercuryUnit] rangeForData:FRangeMake(90, 190) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierBloodPressureDiastolic unit:[HKUnit millimeterOfMercuryUnit] rangeForData:FRangeMake(60, 120) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierRespiratoryRate unit:[[HKUnit countUnit] unitDividedByUnit:[HKUnit minuteUnit]] rangeForData:FRangeMake(8, 30) doTimes:10];
	
	// Results
	[self dummyQuantityType:HKQuantityTypeIdentifierOxygenSaturation unit:[HKUnit percentUnit] rangeForData:FRangeMake(0, 1) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierPeripheralPerfusionIndex unit:[HKUnit percentUnit] rangeForData:FRangeMake(0, 1) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierBloodGlucose unit:[[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] unitDividedByUnit:[HKUnit literUnitWithMetricPrefix:HKMetricPrefixDeci]]  rangeForData:FRangeMake(70, 210) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierNumberOfTimesFallen unit:[HKUnit countUnit] rangeForData:FRangeMake(0, 4) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierElectrodermalActivity unit:[HKUnit siemenUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 30) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierInhalerUsage unit:[HKUnit countUnit] rangeForData:FRangeMake(0, 40) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierBloodAlcoholContent unit:[HKUnit percentUnit] rangeForData:FRangeMake(0, 0.25) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierForcedVitalCapacity unit:[HKUnit literUnit] rangeForData:FRangeMake(2, 6) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierForcedExpiratoryVolume1 unit:[HKUnit literUnit] rangeForData:FRangeMake(1.5, 5.5) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierPeakExpiratoryFlowRate unit:[[HKUnit literUnit] unitDividedByUnit:[HKUnit minuteUnit]] rangeForData:FRangeMake(3, 7) doTimes:10];
	
	// Nutrition
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryFatTotal unit:[HKUnit gramUnit] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryFatPolyunsaturated unit:[HKUnit gramUnit] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryFatMonounsaturated unit:[HKUnit gramUnit] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryFatSaturated unit:[HKUnit gramUnit] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryCholesterol unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 300) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietarySodium unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 1500) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryCarbohydrates unit:[HKUnit gramUnit] rangeForData:FRangeMake(0, 130) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryFiber unit:[HKUnit gramUnit] rangeForData:FRangeMake(0, 38) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietarySugar unit:[HKUnit gramUnit] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryEnergyConsumed unit:[HKUnit kilocalorieUnit] rangeForData:FRangeMake(0, 1000) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryProtein unit:[HKUnit gramUnit] rangeForData:FRangeMake(0, 56) doTimes:10];
	
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryVitaminA unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 900) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryVitaminB6 unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryVitaminB12 unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryVitaminC unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryVitaminD unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryVitaminE unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryVitaminK unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryCalcium unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryIron unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryThiamin unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryRiboflavin unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryNiacin unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryFolate unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryBiotin unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryPantothenicAcid unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryPhosphorus unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryIodine unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryMagnesium unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryZinc unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietarySelenium unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryCopper unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryManganese unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryChromium unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryMolybdenum unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMicro] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryChloride unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryPotassium unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 20) doTimes:10];
	[self dummyQuantityType:HKQuantityTypeIdentifierDietaryCaffeine unit:[HKUnit gramUnitWithMetricPrefix:HKMetricPrefixMilli] rangeForData:FRangeMake(0, 256) doTimes:10];
	if (&HKQuantityTypeIdentifierDietaryWater != NULL) [self dummyQuantityType:HKQuantityTypeIdentifierDietaryWater unit:[HKUnit literUnit] rangeForData:FRangeMake(0.25, 2) doTimes:10];
	
	if (&HKQuantityTypeIdentifierUVExposure != NULL) [self dummyQuantityType:HKQuantityTypeIdentifierUVExposure unit:[HKUnit countUnit] rangeForData:FRangeMake(0, 5) doTimes:10];
	
	/*--------------------------------*/
	/*   HKCategoryType Identifiers   */
	/*--------------------------------*/
	
	[self dummyCategoryType:HKCategoryTypeIdentifierSleepAnalysis rangeForData:FRangeMake(0, 1) doTimes:10];
//	[self dummyCategoryType:HKCategoryTypeIdentifierAppleStandHour rangeForData:FRangeMake(0, 1) doTimes:10];
	if (&HKCategoryTypeIdentifierCervicalMucusQuality != NULL) [self dummyCategoryType:HKCategoryTypeIdentifierCervicalMucusQuality rangeForData:FRangeMake(1, 5) doTimes:10];
	if (&HKCategoryTypeIdentifierOvulationTestResult != NULL) [self dummyCategoryType:HKCategoryTypeIdentifierOvulationTestResult rangeForData:FRangeMake(1, 3) doTimes:10];
	if (&HKCategoryTypeIdentifierMenstrualFlow != NULL) [self dummyCategoryType:HKCategoryTypeIdentifierMenstrualFlow rangeForData:FRangeMake(1, 4) doTimes:10];
	if (&HKCategoryTypeIdentifierIntermenstrualBleeding != NULL) [self dummyCategoryType:HKCategoryTypeIdentifierIntermenstrualBleeding rangeForData:FRangeMake(0, 0) doTimes:10];
	if (&HKCategoryTypeIdentifierSexualActivity != NULL) [self dummyCategoryType:HKCategoryTypeIdentifierSexualActivity rangeForData:FRangeMake(0, 0) doTimes:10];
	if (&HKCategoryTypeIdentifierMindfulSession != NULL) [self dummyCategoryType:HKCategoryTypeIdentifierMindfulSession rangeForData:FRangeMake(0, 0) doTimes:10];
	
	
	/*-----------------------------------*/
	/*   HKCorrelationType Identifiers   */
	/*-----------------------------------*/
	
#warning "expand" correlation types
	 // e.g. HKCorrelationTypeIdentifierBloodPressure is actually HKQuantityTypeIdentifierBloodPressureSystolic + HKQuantityTypeIdentifierBloodPressureDiastolic
	[self dummyCorrelationType:HKCorrelationTypeIdentifierBloodPressure doTimes:10];
	[self dummyCorrelationType:HKCorrelationTypeIdentifierFood doTimes:10];
	
	/*------------------------------*/
	/*   HKWorkoutType Identifier   */
	/*------------------------------*/
	
	[self dummyWorkoutType:HKWorkoutTypeIdentifier unit:[HKUnit countUnit] rangeForData:FRangeMake(0, 0) doTimes:10];

}


// ---------------------------------------------
#pragma mark -
// ---------------------------------------------


- (NSDate *)dummyDate
{
	FRange range = FRangeMake(0, 60 * 60 * 24);
	NSTimeInterval interval = R(range);
	return [NSDate dateWithTimeIntervalSinceNow:-interval];
}

- (NSDate *)dummyDateBeforeDate:(NSDate *)date
{
	FRange range = FRangeMake(0, 60 * 60 * 24);
	NSTimeInterval interval = R(range);
	return [NSDate dateWithTimeInterval:-interval sinceDate:date];
}

- (NSDate *)dummyDateAfterDate:(NSDate *)date
{
	FRange range = FRangeMake(0, 60 * 60 * 24);
	NSTimeInterval interval = R(range);
	return [NSDate dateWithTimeInterval:interval sinceDate:date];
}

// ---------------------------------------------
#pragma mark -
// ---------------------------------------------


- (void)dummyQuantityType:(NSString *)identifier unit:(HKUnit *)unit rangeForData:(FRange)range  doTimes:(NSInteger)n
{
	for (int i = 0; i < n; i++) {
		[self dummyQuantityType:identifier unit:unit rangeForData:range];
	}
}


- (void)dummyCategoryType:(NSString *)identifier rangeForData:(FRange)range  doTimes:(NSInteger)n
{
	for (int i = 0; i < n; i++) {
		[self dummyCategoryType:identifier rangeForData:range];
	}
}

- (void)dummyCorrelationType:(NSString *)identifier doTimes:(NSInteger)n
{
	for (int i = 0; i < n; i++) {
		[self dummyCorrelationType:identifier];
	}
}

- (void)dummyWorkoutType:(NSString *)identifier unit:(HKUnit *)unit rangeForData:(FRange)range doTimes:(NSInteger)n
{
	for (int i = 0; i < n; i++) {
		[self dummyWorkoutType:identifier unit:unit rangeForData:range];
	}
}


// ---------------------------------------------
#pragma mark -
// ---------------------------------------------


- (void)dummyQuantityType:(NSString *)identifier unit:(HKUnit *)unit rangeForData:(FRange)range
{
	HKQuantityType *type = [HKQuantityType quantityTypeForIdentifier:identifier];
	if ([self.healthStore authorizationStatusForType:type] == HKAuthorizationStatusSharingAuthorized) {
		double v = R(range);
		HKQuantity *quantity = [HKQuantity quantityWithUnit:unit doubleValue:v];
		NSDate *date = [self dummyDate];
		HKQuantitySample *sample = [HKQuantitySample quantitySampleWithType:type quantity:quantity startDate:date endDate:date];
		[self.healthStore saveObject:sample withCompletion:^(BOOL success, NSError *error) {
			if (!success) {
				NSLog(@"error saving dummy data for %@:%@", type, error);
				[self errorOccurred:error];
			} else {
				NSLog(@"[saved dummy data for %@]", type);
			}
		}];
	}
}

					 
- (void)dummyCategoryType:(NSString *)identifier rangeForData:(FRange)range
{
	HKCategoryType *type = [HKCategoryType categoryTypeForIdentifier:identifier];
	if ([self.healthStore authorizationStatusForType:type] == HKAuthorizationStatusSharingAuthorized) {
		NSInteger value = range.location + (CGFloat)arc4random_uniform(range.length);
		NSDate *endDate = [self dummyDate];
		NSDate *startDate = [self dummyDateBeforeDate:endDate];
		
		NSDictionary *meta = nil;
		if (&HKCategoryTypeIdentifierMenstrualFlow != NULL) {
			if ([type isEqual:[HKCategoryType categoryTypeForIdentifier:HKCategoryTypeIdentifierMenstrualFlow]]) {
				/*
				 - When using single samples to cover the whole period:
				   pass the start of the menstrual period to the startDate parameter. Pass the end of the period to the endDate parameter, and set the HKMetadataKeyMenstrualCycleStart value to YES.
				 - When using multiple samples to record a single period:
				   the startDate and endDate parameters should mark the beginning and ending of each individual sample. Set the HKMetadataKeyMenstrualCycleStart value for the first sample in the period to YES. Use NO for any additional samples.
				   Different samples can use different menstrualFlow values to record the changes in flow over time.
				 */
				meta = @{ HKMetadataKeyMenstrualCycleStart: @(YES) };
			}
		}
		HKObject *sample = [HKCategorySample categorySampleWithType:type value:value startDate:startDate endDate:endDate metadata:meta];
		[self.healthStore saveObject:sample withCompletion:^(BOOL success, NSError *error) {
			if (!success) {
				NSLog(@"error saving dummy data for %@:%@", type, error);
				[self errorOccurred:error];
			} else {
				NSLog(@"[saved dummy data for %@]", type);
			}
		}];
	}
}

- (void)dummyCorrelationType:(NSString *)identifier
{
	HKObject *object = nil;
	HKCorrelationType *type = nil;
	
	if ([identifier isEqualToString:HKCorrelationTypeIdentifierBloodPressure]) {
		type = [HKCorrelationType correlationTypeForIdentifier:identifier];
		HKQuantityType *diastolicType = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierBloodPressureDiastolic];
		HKQuantityType *systolicType = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierBloodPressureSystolic];
		if ([self.healthStore authorizationStatusForType:type] == HKAuthorizationStatusSharingAuthorized &&
			[self.healthStore authorizationStatusForType:diastolicType] == HKAuthorizationStatusSharingAuthorized &&
			[self.healthStore authorizationStatusForType:systolicType] == HKAuthorizationStatusSharingAuthorized) 
		{
			HKUnit *unit = [HKUnit millimeterOfMercuryUnit];
			
			NSDate *endDate = [self dummyDate];
			NSDate *startDate = [self dummyDateBeforeDate:endDate];
			
			HKQuantitySample *dPressure = [HKQuantitySample quantitySampleWithType:diastolicType quantity:[HKQuantity quantityWithUnit:unit doubleValue:R(FRangeMake(60, 120))] startDate:startDate endDate:endDate];
			HKQuantitySample *sPressure = [HKQuantitySample quantitySampleWithType:systolicType quantity:[HKQuantity quantityWithUnit:unit doubleValue:R(FRangeMake(90, 190))] startDate:startDate endDate:endDate];
			
			HKCorrelation *correlation = [HKCorrelation correlationWithType:type startDate:startDate endDate:endDate objects:[NSSet setWithObjects:dPressure, sPressure, nil]];
			object = correlation;
		}
	} else if ([identifier isEqualToString:HKCorrelationTypeIdentifierFood]) {
		type = [HKCorrelationType correlationTypeForIdentifier:identifier];
		HKQuantityType *energyConsumedType = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierDietaryEnergyConsumed];
		if ([self.healthStore authorizationStatusForType:type] == HKAuthorizationStatusSharingAuthorized &&
			[self.healthStore authorizationStatusForType:energyConsumedType] == HKAuthorizationStatusSharingAuthorized)
		{
			NSDate *now = [NSDate date];
			
			HKQuantity *energyQuantityConsumed = [HKQuantity quantityWithUnit:[HKUnit jouleUnit] doubleValue:R(FRangeMake(0, 1000))];
			
			HKQuantitySample *energyConsumedSample = [HKQuantitySample quantitySampleWithType:energyConsumedType quantity:energyQuantityConsumed startDate:now endDate:now];
			NSSet *energyConsumedSamples = [NSSet setWithObject:energyConsumedSample];
			
			NSDictionary *foodCorrelationMetadata = @{HKMetadataKeyFoodType:@"a food"};
			
			HKCorrelation *foodCorrelation = [HKCorrelation correlationWithType:type startDate:now endDate:now objects:energyConsumedSamples metadata:foodCorrelationMetadata];

			object = foodCorrelation;
		}
	}
		
	if (object) {
		[self.healthStore saveObject:object withCompletion:^(BOOL success, NSError *error) {
			if (!success) {
				NSLog(@"error saving dummy data for %@: %@", type, error);
				[self errorOccurred:error];
			} else {
				NSLog(@"[saved dummy data for %@]", type);
			}
		}];
	}
}

- (void)dummyWorkoutType:(NSString *)identifier unit:(HKUnit *)unit rangeForData:(FRange)range
{
	HKWorkoutType *type = [HKWorkoutType workoutType];
	if ([self.healthStore authorizationStatusForType:type] == HKAuthorizationStatusSharingAuthorized) {

		NSDate *endDate = [self dummyDate];
		NSDate *startDate = [self dummyDateBeforeDate:endDate];

		HKQuantity *distance = [HKQuantity quantityWithUnit:[HKUnit mileUnit] doubleValue:5];
		HKQuantity *calories = [HKQuantity quantityWithUnit:[HKUnit kilocalorieUnit] doubleValue:2];
		
		HKWorkout *workout = [HKWorkout workoutWithActivityType:HKWorkoutActivityTypeRunning startDate:startDate endDate:endDate duration:fabs([endDate timeIntervalSinceDate:startDate]) totalEnergyBurned:calories totalDistance:distance metadata:nil];
		[self.healthStore saveObject:workout withCompletion:^(BOOL success, NSError *error) {
			if (!success) {
				NSLog(@"error saving dummy data for %@:%@", type, error);
				[self errorOccurred:error];
			}
		}];

	}
}

@end


void _GenerateDummyData(char *dataTypesString)
{
	BEHealthKit *kit = [BEHealthKit sharedHealthKit];
	NSArray *types = parseTransmission(dataTypesString);
	NSArray *combined = [[[NSSet setWithArray:types[0]] setByAddingObjectsFromSet:[NSSet setWithArray:types[1]]] allObjects];
	[kit authorizeHealthKitToRead:types[0] write:combined completion:^(bool success, NSError *error) {
		if (!success) {
			NSLog(@"Error authorizing healthkit:%@", error);
			[kit errorOccurred:error];
			return;
		} else {
			[kit generateDummyData];
		}
	}];
}
