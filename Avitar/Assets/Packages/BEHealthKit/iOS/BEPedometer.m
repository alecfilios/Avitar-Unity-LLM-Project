//
//  BEPedometer.m
//  Unity-iPhone
//
//  Created by greay on 3/7/16.
//
//

#import "BEPedometer.h"
#import "BEHealthKit.h"
#import "HealthData.h"
#import <CoreMotion/CoreMotion.h>

// @cond INTERNAL
@interface BEPedometer ()

@property (nonatomic, strong) CMPedometer *pedometer;

@end
// @endcond

// ----------------------------

@implementation BEPedometer

- (instancetype)init {
	if (self = [super init]) {
		self.pedometer = [[CMPedometer alloc] init];
	}
	return self;
}

- (void)queryPedometerDataFromDate:(NSDate *)start toDate:(NSDate *)end
{
	[self.pedometer queryPedometerDataFromDate:start toDate:end withHandler:^(CMPedometerData *pedometerData, NSError *error) {
		NSString *xml = xml = [HealthData XMLFromPedometerData:pedometerData error:error];
		UnitySendMessage([[BEHealthKit sharedHealthKit].controllerName cStringUsingEncoding:NSUTF8StringEncoding], "ParseHealthXML", [xml cStringUsingEncoding:NSUTF8StringEncoding]);
	}];
}

- (void)startPedometerUpdatesFromDate:(NSDate *)start
{
	[self.pedometer startPedometerUpdatesFromDate:start withHandler:^(CMPedometerData *pedometerData, NSError *error) {
		NSString *xml = xml = [HealthData XMLFromPedometerData:pedometerData error:error];
		NSLog(@"update: (%@ -> %@) : %@", pedometerData.startDate, pedometerData.endDate, pedometerData.numberOfSteps);
		UnitySendMessage([[BEHealthKit sharedHealthKit].controllerName cStringUsingEncoding:NSUTF8StringEncoding], "ParseHealthXML", [xml cStringUsingEncoding:NSUTF8StringEncoding]);
	}];
}

- (void)stopPedometerUpdates
{
	[self.pedometer stopPedometerUpdates];
}

@end
