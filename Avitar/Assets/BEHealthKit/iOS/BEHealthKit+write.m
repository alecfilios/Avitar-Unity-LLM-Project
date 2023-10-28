//
//  BEHealthKit+write.m
//  Unity-iPhone
//
//  Created by greay on 10/10/19.
//

#import "BEHealthKit+write.h"

#import "XMLDictionary/XMLDictionary.h"

#import "NSDate+bridge.h"

typedef void (^CompletionBlock)(BOOL success, NSError *error);
void saveSampleWithCompletion(NSString *datatype, HKObject *sample, CompletionBlock completion);

@implementation BEHealthKit (write)

@end

void saveSample(NSString *datatype, HKObject *sample);

void _WriteQuantity(char *identifier, char *unitString, double doubleValue, char *startDateString, char *endDateString)
{
	NSString *identifierString = [NSString stringWithCString:identifier encoding:NSUTF8StringEncoding];
	HKQuantityType *quantityType = [HKQuantityType quantityTypeForIdentifier:identifierString];
	HKUnit *unit = [HKUnit unitFromString:[NSString stringWithCString:unitString encoding:NSUTF8StringEncoding]];
	HKQuantity *quantity = [HKQuantity quantityWithUnit:unit doubleValue:doubleValue];
	NSDate *startDate = [NSDate dateFromBridgeString:startDateString];
	NSDate *endDate = [NSDate dateFromBridgeString:endDateString];
	
	HKQuantitySample *sample = [HKQuantitySample quantitySampleWithType:quantityType quantity:quantity startDate:startDate endDate:endDate];
	saveSample(identifierString, sample);
}

void _WriteCategory(char *identifier, int intValue, char *startDateString, char *endDateString)
{
	NSString *identifierString = [NSString stringWithCString:identifier encoding:NSUTF8StringEncoding];
	HKCategoryType *categoryType = [HKCategoryType categoryTypeForIdentifier:identifierString];
	NSDate *startDate = [NSDate dateFromBridgeString:startDateString];
	NSDate *endDate = [NSDate dateFromBridgeString:endDateString];
	NSLog(@"[writing category data from %@ to %@]", startDate, endDate);
	
	HKCategorySample *sample = [HKCategorySample categorySampleWithType:categoryType value:intValue startDate:startDate endDate:endDate];
	
	saveSample(identifierString, sample);
}



void _WriteWorkoutSimple(int activityID, char *startDateString, char *endDateString)
{
	HKWorkoutActivityType activityType = (HKWorkoutActivityType)activityID;
	NSDate *startDate = [NSDate dateFromBridgeString:startDateString];
	NSDate *endDate = [NSDate dateFromBridgeString:endDateString];
	
	HKWorkout *sample = [HKWorkout workoutWithActivityType:activityType startDate:startDate endDate:endDate];
	
	saveSample(HKWorkoutTypeIdentifier, sample);
}

void _WriteWorkout(int activityID, char *startDateString, char *endDateString, double energyBurned, char *energyUnits, double distance, char *distanceUnits)
{
	/*
	 // Provide summary information when creating the workout.
	 HKWorkout *run = [HKWorkout workoutWithActivityType:HKWorkoutActivityTypeRunning
	 startDate:start
	 endDate:end
	 duration:0
	 totalEnergyBurned:energyBurned
	 totalDistance:distance
	 metadata:nil];
	 */
	
	HKWorkoutActivityType activityType = (HKWorkoutActivityType)activityID;
	
	HKQuantity *cal = nil;
	if (energyUnits != nil) {
		HKUnit *unit = [HKUnit unitFromString:[NSString stringWithCString:energyUnits encoding:NSUTF8StringEncoding]];
		cal = [HKQuantity quantityWithUnit:unit doubleValue:energyBurned];
	}
	HKQuantity *d =	nil;
	if (distanceUnits != nil) {
		HKUnit *unit = [HKUnit unitFromString:[NSString stringWithCString:distanceUnits encoding:NSUTF8StringEncoding]];
		d = [HKQuantity quantityWithUnit:unit doubleValue:distance];
	}
	
	NSDate *startDate = [NSDate dateFromBridgeString:startDateString];
	NSDate *endDate = [NSDate dateFromBridgeString:endDateString];
	
	HKWorkout *sample = [HKWorkout workoutWithActivityType:activityType startDate:startDate endDate:endDate duration:0 totalEnergyBurned:cal totalDistance:d metadata:nil];

	saveSampleWithCompletion(HKWorkoutTypeIdentifier, sample, ^(BOOL success, NSError *error) {
		HKHealthStore *store = [BEHealthKit sharedHealthKit].healthStore;
		/*
		 If the total energy burned or total distance are nonzero values, create a set of corresponding samples that add up to the calculated totals. Associate these samples with the workout by calling the health store’s addSamples:toWorkout:completion: method.
		 */
		
		if (cal) {
			HKQuantityType *qtype = [HKQuantityType quantityTypeForIdentifier: HKQuantityTypeIdentifierActiveEnergyBurned];
			if ([store authorizationStatusForType:qtype] == HKAuthorizationStatusSharingAuthorized) {
				HKQuantitySample *calSample = [HKQuantitySample quantitySampleWithType:qtype quantity:cal startDate:startDate endDate:endDate];
				[store addSamples:[NSArray arrayWithObject:calSample] toWorkout:sample completion:^(BOOL success, NSError * _Nullable error) {
					if (!success) NSLog(@"error: %@", error);
				}];
			} else {
				NSLog(@"Warning: attempting to save energy burned with exercise, but lack authorization.");
			}
		}
		if (d) {
			HKQuantityType *qtype = nil;
			switch (activityType) {
				case HKWorkoutActivityTypeCycling:
					qtype = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierDistanceCycling];
					break;
				case HKWorkoutActivityTypeSwimming:
				case HKWorkoutActivityTypeWaterFitness:
				case HKWorkoutActivityTypeWaterPolo:
				case HKWorkoutActivityTypeWaterSports:
					qtype = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierDistanceSwimming];
					break;
				case HKWorkoutActivityTypeDownhillSkiing:
				case HKWorkoutActivityTypeSnowSports:
				case HKWorkoutActivityTypeSnowboarding:
					if (@available(iOS 11.2, *)) {
						qtype = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierDistanceDownhillSnowSports];
					} else {
						// not the best fallback, but what else do I use?
						qtype = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierDistanceWalkingRunning];
					}
					break;
				default:
					qtype = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierDistanceWalkingRunning];
					break;
			}
			
			if ([store authorizationStatusForType:qtype] == HKAuthorizationStatusSharingAuthorized) {
				HKQuantitySample *dSample = [HKQuantitySample quantitySampleWithType:qtype quantity:d startDate:startDate endDate:endDate];
				[store addSamples:[NSArray arrayWithObject:dSample] toWorkout:sample completion:^(BOOL success, NSError * _Nullable error) {
					if (!success) NSLog(@"error: %@", error);
				}];
			} else {
				NSLog(@"Warning: attempting to save distance with exercise, but lack authorization.");
			}
		}
	});
}


void saveSampleWithCompletion(NSString *datatype, HKObject *sample, CompletionBlock completion) {
	BEHealthKit *kit = [BEHealthKit sharedHealthKit];
	[kit.healthStore saveObject:sample withCompletion:^(BOOL success, NSError *error) {
		if (!success) {
			NSLog(@"error: %@", error);
		} else {
			if (completion != NULL) completion(success, error);
		}
		
		NSMutableDictionary *dict = [@{@"success":@(success), XMLDictionaryNodeNameKey:@"write"} mutableCopy];
		dict[@"datatype"] = datatype;
		if ([sample isKindOfClass:[HKWorkout class]]) {
			int workoutType = [(HKWorkout *)sample workoutActivityType];
			dict[@"workoutType"] = @(workoutType);
		}
		if (error) {
			dict[@"error"] = error;
		}
		NSString *xml = [dict XMLString];
		UnitySendMessage([[BEHealthKit sharedHealthKit].controllerName cStringUsingEncoding:NSUTF8StringEncoding], "ParseHealthXML", [xml cStringUsingEncoding:NSUTF8StringEncoding]);
		
	}];
}

void saveSample(NSString *datatype, HKObject *sample) {
	saveSampleWithCompletion(datatype, sample, NULL);
}
