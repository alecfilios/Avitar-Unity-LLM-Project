//
//  HealthData+clinicalRecords.m
//  Unity-iPhone
//
//  Created by greay on 3/9/20.
//

#import "HealthData+clinicalRecords.h"
#import "NSDate+bridge.h"
#import "BEHealthKit.h"
#import "../../iOS/XMLDictionary/XMLDictionary.h"

@implementation HKFHIRResource (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[@"identifier"] = self.identifier;
	dict[@"resourceType"] = self.resourceType;
	dict[@"sourceURL"] = [self.sourceURL absoluteString];
	
	// JSONSerialization.jsonObject(with: fhirRecord.data, options: [])
	NSError *error;
	dict[@"data"] = [NSJSONSerialization JSONObjectWithData:self.data options:nil error:&error];
	if (error) {
		NSLog(@"error deserializing JSON: %@", error);
	}
	return dict;
}

@end

@implementation HKClinicalRecord (serialization)

- (id)be_serializable {
	NSMutableDictionary *dict = [super be_serializable];
	dict[@"clinicalType"] = [self.clinicalType be_serializable];
	dict[@"displayName"] = self.displayName;
	dict[@"FHIRResource"] = [self.FHIRResource be_serializable];
	return dict;
}

@end


// ---------------------
// MARK: XML
// ---------------------


@implementation HealthData (clinicalRecords)

+ (NSString *)XMLFromClinicalRecords:(id)records datatype:(HKSampleType *)datatype
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"records";
	dict[@"datatype"] = datatype;

	if (@available(iOS 12.0, *)) {
		NSMutableArray *samples = [NSMutableArray array];
		for (HKClinicalRecord *sample in records) {
			[samples addObject:[sample be_serializable]];
		}
		dict[@"records"] = samples;
	}

	return [dict XMLString];
}

@end
