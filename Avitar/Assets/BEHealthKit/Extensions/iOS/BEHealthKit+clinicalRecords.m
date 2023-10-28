//
//  BEHealthKit+clinicalRecords.m
//  Unity-iPhone
//
//  Created by greay on 3/9/20.
//

#import "BEHealthKit+clinicalRecords.h"
#import "HealthData+clinicalRecords.h"

#import "NSDate+bridge.h"
#import "NSDateComponents+bridge.h"


@implementation BEHealthKit (clinicalRecords)

- (void)readHealthRecords:(HKSampleType *)sampleType resultsHandler:(void (^)(id result, NSError *error))resultsHandler
{
	HKSampleQuery *sampleQuery = [[HKSampleQuery alloc] initWithSampleType:sampleType
																 predicate:nil
																	 limit:HKObjectQueryNoLimit
														   sortDescriptors:nil
															resultsHandler:^(HKSampleQuery *query, NSArray *results, NSError *error) {
																resultsHandler(results, error);
															}];
	
	[self.healthStore executeQuery:sampleQuery];
}

@end

void _ReadHealthRecord(char *identifier) {
	if (@available(iOS 12.0, *)) {
		NSString *identifierString = [NSString stringWithCString:identifier encoding:NSUTF8StringEncoding];
		HKSampleType *sampleType = [HKObjectType clinicalTypeForIdentifier:identifierString];
		if (!sampleType) {
			NSLog(@"Error; unknown clinical-type identifier '%@'", identifierString);
			NSError *err = [NSError errorWithDomain:@"beliefengine" code:BEHK_ERROR_UNKNOWN_DATATYPE userInfo:@{NSLocalizedDescriptionKey:[NSString stringWithFormat:@"Unknown quantity-type identifier %@.", identifierString]}];
			[[BEHealthKit sharedHealthKit] errorOccurred:err];
			return;
		}

		BEHealthKit *kit = [BEHealthKit sharedHealthKit];
		[kit readHealthRecords:sampleType resultsHandler:^(id result, NSError *error) {
			if (!result) {
				[kit errorOccurred:error];
				return;
			}
			/*
			 guard let actualSamples = samples else {
				// Handle the error here.
				print("*** An error occurred: \(error?.localizedDescription ?? "nil") ***")
				return
			 }
			 
			 let allergySamples = actualSamples as? [HKClinicalRecord]
			 // Do something with the allergy samples here...
			 */
			
			NSString *xml = [HealthData XMLFromClinicalRecords:result datatype:sampleType];
			UnitySendMessage([[BEHealthKit sharedHealthKit].controllerName cStringUsingEncoding:NSUTF8StringEncoding], "ParseHealthXML", [xml cStringUsingEncoding:NSUTF8StringEncoding]);
		}];
	} else {
		NSLog(@"Error; clinical records are not available < iOS 12.0");
		NSError *err = [NSError errorWithDomain:@"beliefengine" code:BEHK_ERROR_FEATURE_UNAVAILABLE userInfo:@{NSLocalizedDescriptionKey:@"Error; clinical records are not available < iOS 12.0"}];
		[[BEHealthKit sharedHealthKit] errorOccurred:err];
	}
}
