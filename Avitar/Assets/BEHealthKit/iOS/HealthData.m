//
//  HealthData.m
//  Unity-iPhone
//
//  Created by greay on 3/28/15.
//
//

#import "HealthData.h"
#import <HealthKit/HealthKit.h>
#import "BEHealthKit.h"
#import "XMLDictionary/XMLDictionary.h"
#import "NSDate+bridge.h"
#import "NSError+XML.h"

// ---------------------
// MARK: serialization
// ---------------------

@implementation HKObjectType (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"identifier"] = self.identifier;
	return dict;
}

@end


@implementation HKSampleType (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	// nothing?
	return dict;
}

@end


@implementation HKQuantityType (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	NSString *aggregationStyle;
	switch (self.aggregationStyle) {
		case HKQuantityAggregationStyleCumulative:
			aggregationStyle = @"cumulative";
//		case HKQuantityAggregationStyleDiscrete:
		case HKQuantityAggregationStyleDiscreteArithmetic:
			aggregationStyle = @"discreteArithmetic";
		case HKQuantityAggregationStyleDiscreteTemporallyWeighted:
			aggregationStyle = @"discreteTemporallyWeighted";
		case HKQuantityAggregationStyleDiscreteEquivalentContinuousLevel:
			aggregationStyle = @"discreteEquivalentContinuousLevel";
	}
	dict[@"aggregationStyle"] = aggregationStyle;
	return dict;
}

@end


@implementation HKCorrelationType (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	// nothing?
	return dict;
}

@end

@implementation HKQuantity (serialization)

- (id)be_serializableWithUnit:(HKUnit *)unit {
//	NSArray *units = @[[HKUnit countUnit], [HKUnit percentUnit], [HKUnit gramUnit], [HKUnit kilocalorieUnit],
//					   [[HKUnit countUnit] unitDividedByUnit:[HKUnit minuteUnit]],
//					   [HKUnit degreeCelsiusUnit], [HKUnit millimeterOfMercuryUnit],
//					   [[HKUnit gramUnit] unitDividedByUnit:[HKUnit literUnit]],
//					   [HKUnit siemenUnit],
//					   [[HKUnit literUnit] unitDividedByUnit:[HKUnit hourUnit]]
//					   ];
	if ([self isCompatibleWithUnit:unit]) {
		NSMutableDictionary *dict = [NSMutableDictionary dictionary];
		dict[@"unit"] = [unit unitString];
		dict[@"value"] = @([self doubleValueForUnit:unit]);
		
		return dict;
	}
	NSLog(@"error; don't know which unit to use!");
	return nil;
}

@end

@implementation HKSourceRevision (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"source"] = self.source.name;
	dict[@"bundleID"] = self.source.bundleIdentifier;
	dict[@"version"] = self.version;
	if (@available(iOS 11.0, *)) {
//		dict[@"osVersion"] = self.operatingSystemVersion;
		dict[@"productType"] = self.productType;
	}
	return dict;
}

@end

@implementation HKObject (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"source"] = [self.sourceRevision be_serializable];
	if (self.metadata) {
		NSMutableDictionary *metadata = [@{} mutableCopy];
		for (NSString *key in self.metadata) {
			NSString *safeKey = [[key XMLEncodedString] stringByReplacingOccurrencesOfString:@" " withString:@"_"];
			if ([self.metadata[key] isKindOfClass:[NSString class]] || [self.metadata[key] isKindOfClass:[NSNumber class]]) {
				metadata[safeKey] = self.metadata[key];
			}
			else if ([self.metadata[key] isKindOfClass:[NSDate class]]) {
				metadata[safeKey] = [(NSDate *)self.metadata[key] bridgeToken];
			}
			else if ([self.metadata[key] isKindOfClass:[HKQuantity class]]) {
				HKQuantity *quantity = self.metadata[key];
				if (@available(iOS 13.0, *)) {
					if ([key isEqualToString:HKMetadataKeyAverageMETs]) {
						metadata[safeKey] = [quantity be_serializableWithUnit:[HKUnit unitFromString:@"kcal/(kg*hr)"]]; // metabolic equivalent of task
					}
					// else...?
				}
			}
			else {
				NSLog(@"error:don't know how to handle metadata[%@] = %@ (%@)", key, self.metadata[key], [self.metadata[key] class]);
			}
		}
		dict[@"metadata"] = metadata;
		
	}
	return dict;
}

@end



@implementation HKSample (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	dict[@"startDate"] = [self.startDate bridgeToken];
	dict[@"endDate"] = [self.endDate bridgeToken];
	dict[@"sampleType"] = [self.sampleType be_serializable];
	return dict;
}

@end


// < iOS 13
@implementation HKQuantitySample (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	dict[@"quantityType"] = [self.quantityType be_serializable];
	
	HKUnit *unit = [[BEHealthKit sharedHealthKit] defaultUnitForSampleType:self.quantityType];
	dict[@"quantity"] = [self.quantity be_serializableWithUnit:unit];
	
	return dict;
}

@end

@implementation NSDateInterval (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"startDate"] = [self.startDate bridgeToken];
	dict[@"endDate"] = [self.endDate bridgeToken];
	dict[@"duration"] = @(self.duration);
	return dict;
}

@end

// >= iOS 13
@implementation HKDiscreteQuantitySample (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	HKUnit *unit = [[BEHealthKit sharedHealthKit] defaultUnitForSampleType:self.quantityType];

	dict[@"minimumQuantity"] = @([self.minimumQuantity doubleValueForUnit:unit]);
	dict[@"maximumQuantity"] = @([self.maximumQuantity doubleValueForUnit:unit]);
	dict[@"averageQuantity"] = @([self.averageQuantity doubleValueForUnit:unit]);
	dict[@"mostRecentQuantity"] = @([self.mostRecentQuantity doubleValueForUnit:unit]);
	dict[@"mostRecentQuantityDateInterval"] = [self.mostRecentQuantityDateInterval be_serializable];
	return dict;
}

@end

// >= iOS 13
@implementation HKCumulativeQuantitySample (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	HKUnit *unit = [[BEHealthKit sharedHealthKit] defaultUnitForSampleType:self.quantityType];

	dict[@"sumQuantity"] = @([self.sumQuantity doubleValueForUnit:unit]);
	
	return dict;
}

@end


@implementation HKCorrelation (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
#warning this doesnt seem to do much of anything!
	dict[@"correlationType"] = [self.correlationType be_serializable];
	
	NSMutableArray *objects = [NSMutableArray array];
	for (HKSample *object in self.objects) {
		#warning what class are these? 
		[objects addObject:[object be_serializable]];
	}
	dict[@"objects"] = objects;
	
	return dict;
}

@end


@implementation HKWorkoutEvent (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"date"] = [self.date bridgeToken];
	dict[@"eventType"] = @(self.type);
	
	return dict;
}

@end


@implementation HKWorkout (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	dict[@"duration"] = @(self.duration);
	dict[@"totalDistance"] = [self.totalDistance be_serializableWithUnit:[HKUnit mileUnit]];
	dict[@"energyBurned"] = [self.totalEnergyBurned be_serializableWithUnit:[HKUnit kilocalorieUnit]];
	dict[@"activityType"] = @(self.workoutActivityType);
	
	NSMutableArray *events = [NSMutableArray array];
	for (HKWorkoutEvent *event in self.workoutEvents) {
		[events addObject:[event be_serializable]];
	}
	dict[@"events"] = events;
	
	return dict;
}

@end


@implementation HKCategoryType (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	return dict;
}

@end


@implementation HKCategorySample (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	dict[@"categoryType"] = [self.categoryType be_serializable];
	dict[@"value"] = @(self.value);
	return dict;
}

@end


@implementation CMPedometerData (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"startDate"] = [self.startDate bridgeToken];
	dict[@"endDate"] = [self.endDate bridgeToken];
	dict[@"numberOfSteps"] = self.numberOfSteps;
	return dict;
}

@end

@implementation HKStatistics (serialization)

- (id)be_serializableWithUnit:(HKUnit *)unit {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"startDate"] = [self.startDate bridgeToken];
	dict[@"endDate"] = [self.endDate bridgeToken];
	dict[@"aggregationStyle"] = (self.quantityType == HKQuantityAggregationStyleCumulative) ? @"cumulative" : @"discrete";
	
	dict[@"sumQuantity"] = @([self.sumQuantity doubleValueForUnit:unit]);
	dict[@"minimumQuantity"] = @([self.minimumQuantity doubleValueForUnit:unit]);
	dict[@"maximumQuantity"] = @([self.maximumQuantity doubleValueForUnit:unit]);
	dict[@"averageQuantity"] = @([self.averageQuantity doubleValueForUnit:unit]);
	if (@available(iOS 12.0, *)) {
		dict[@"mostRecentQuantity"] = @([self.mostRecentQuantity doubleValueForUnit:unit]);
	}
	
	return dict;
}

@end

@implementation HKDocumentSample (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	dict[@"documentType"] = [self.documentType be_serializable];
	return dict;
}

@end

@implementation HKCDADocument (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"author"] = self.authorName;
	dict[@"custodian"] = self.custodianName;
	dict[@"patient"] = self.patientName;
	dict[@"title"] = self.title;
	dict[@"data"] = [NSDictionary dictionaryWithXMLData:self.documentData];
	return dict;
}

@end

@implementation HKCDADocumentSample (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	dict[@"document"] = [self.document be_serializable];
	return dict;
}


@end


// ---------------------
// MARK: XML
// ---------------------


@implementation HealthData

+ (NSString *)XMLFromSamples:(NSArray *)samples datatype:(HKObjectType *)datatype error:(NSError *)error
{
	if ([datatype isKindOfClass:[HKQuantityType class]]) {
		return [HealthData XMLFromQuantitySamples:samples datatype:datatype.identifier error:error];
	}
	if ([datatype isKindOfClass:[HKWorkoutType class]]) {
		HKWorkoutActivityType workoutType = 0;
		for (HKWorkout *workout in samples) {
			workoutType = workout.workoutActivityType;
		}
		return [HealthData XMLFromWorkoutSamples:samples workoutType:(int)workoutType error:error];
	}
	NSLog(@"error; flesh this out more");
	return nil;
}

+ (NSString *)XMLFromQuantitySamples:(NSArray *)quantitySamples datatype:(NSString *)datatype error:(NSError *)error
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"quantity";
	dict[@"datatype"] = datatype;
	
	NSMutableArray *samples = [NSMutableArray array];
	for (HKQuantitySample *sample in quantitySamples) {
		[samples addObject:[sample be_serializable]];
	}
	dict[@"quantitySample"] = samples;
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
//	return [self plistXML:dict];
}

+ (NSString *)XMLFromCombinedTotal:(double)total datatype:(NSString *)datatype error:(NSError *)error
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"combined";
	dict[@"datatype"] = datatype;
	dict[@"total"] = @(total);
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	return [dict XMLString];
}

+ (NSString *)XMLFromStatistics:(HKStatistics *)statistics datatype:(HKQuantityType *)sampleType error:(NSError *)error
{
	HKUnit *unit = [[BEHealthKit sharedHealthKit] defaultUnitForSampleType:sampleType];
	
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"datatype"] = sampleType.identifier;
	dict[@"unit"] = [unit unitString];
	dict[XMLDictionaryNodeNameKey] = @"statistics";
	dict[@"statistics"] = [statistics be_serializableWithUnit:unit];
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
}

+ (NSString *)XMLFromStatisticsCollection:(HKStatisticsCollection *)collection datatype:(HKQuantityType *)sampleType anchorDate:(NSDate *)anchorDate interval:(NSDateComponents *)interval error:(NSError *)error
{
	HKUnit *unit = [[BEHealthKit sharedHealthKit] defaultUnitForSampleType:sampleType];
	
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"statisticsCollection";
	dict[@"datatype"] = sampleType.identifier;
	dict[@"unit"] = [unit unitString];
	NSMutableArray *statistics = [NSMutableArray array];
//	for (HKStatistics *stats in collection.statistics) {
//		[statistics addObject:[stats be_serializableWithUnit:unit]];
//	}
	[collection enumerateStatisticsFromDate:anchorDate toDate:[NSDate date] withBlock:^(HKStatistics *stats, BOOL *stop) {
		[statistics addObject:[stats be_serializableWithUnit:unit]];
	}];
	dict[@"statistics"] = statistics;
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
}


+ (NSString *)XMLFromCategorySamples:(NSArray *)categorySamples datatype:(NSString *)datatype error:(NSError *)error
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"category";
	dict[@"datatype"] = datatype;
	
	NSMutableArray *samples = [NSMutableArray array];
	for (HKCategorySample *sample in categorySamples) {
		[samples addObject:[sample be_serializable]];
	}
	dict[@"categorySample"] = samples;
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
}

+ (NSString *)XMLFromCorrelationSamples:(NSArray *)correlationSamples datatype:(NSString *)datatype error:(NSError *)error
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"correlation";
	dict[@"datatype"] = datatype;
	
	NSMutableArray *samples = [NSMutableArray array];
	for (HKCorrelation *sample in correlationSamples) {
		[samples addObject:[sample be_serializable]];
	}
	dict[@"correlationSample"] = samples;
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
}

+ (NSString *)XMLFromWorkoutSamples:(NSArray *)workoutSamples workoutType:(int)workoutType error:(NSError *)error
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"workout";
	dict[@"datatype"] = HKWorkoutTypeIdentifier;
	dict[@"workoutType"] = @(workoutType);
	
	NSMutableArray *samples = [NSMutableArray array];
	for (HKWorkout *sample in workoutSamples) {
		[samples addObject:[sample be_serializable]];
	}
	dict[@"workoutSample"] = samples;
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
}


+ (NSString *)XMLFromCharacteristic:(id)characteristic datatype:(NSString *)datatype error:(NSError *)error
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"characteristic";
	dict[@"datatype"] = datatype;
	
	if ([characteristic isKindOfClass:[HKBiologicalSexObject class]]) {
		dict[@"sex"] = @([(HKBiologicalSexObject *)characteristic biologicalSex]);
	} else if ([characteristic isKindOfClass:[HKBloodTypeObject class]]) {
		dict[@"bloodType"] = @([(HKBloodTypeObject *)characteristic bloodType]);
	} else if ([characteristic isKindOfClass:[HKFitzpatrickSkinTypeObject class]]) {
		dict[@"skinType"] = @([(HKFitzpatrickSkinTypeObject *)characteristic skinType]);
	} else if ([characteristic isKindOfClass:[HKWheelchairUseObject class]]) {
		dict[@"wheelchairUse"] = @([(HKWheelchairUseObject *)characteristic wheelchairUse]);
	} else if ([characteristic isKindOfClass:[NSDate class]]) {
		dict[@"DOB"] = [(NSDate *)characteristic bridgeToken];
	} else {
		NSLog(@"Error; unrecognized characteristic:%@", [characteristic class]);
		return nil;
	}
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
}

+ (NSString *)XMLFromHealthDocuments:(id)documents error:(NSError *)error
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"documents";
	dict[@"datatype"] = HKDocumentTypeIdentifierCDA;
	NSMutableArray *samples = [NSMutableArray array];
	for (HKCDADocumentSample *sample in documents) {
		[samples addObject:[sample be_serializable]];
	}
	dict[@"documents"] = samples;
	
	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
}

+ (NSString *)XMLFromPedometerData:(CMPedometerData *)data error:(NSError *)error
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"pedometerData"] = [data be_serializable];
	dict[XMLDictionaryNodeNameKey] = @"pedometer";

	if (error) {
		dict[@"error"] = [error dictionary];
	}
	
	return [dict XMLString];
}


+ (NSString *)plistXML:(NSDictionary *)plist
{
	NSError *error;
	NSData *data = [NSPropertyListSerialization dataWithPropertyList:plist format:NSPropertyListXMLFormat_v1_0 options:0 error: &error];
	if (data == nil) {
		NSLog (@"error serializing to xml: %@", error);
		return nil;
	}
	return [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
}

@end

