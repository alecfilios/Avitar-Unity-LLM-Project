//
//  BEPedometer.h
//  Unity-iPhone
//
//  Created by greay on 3/7/16.
//
//

#import <Foundation/Foundation.h>

/*! @brief Handles Pedometer requests from Unity.
 */
@interface BEPedometer : NSObject

/*! @brief              reads pedometer data
	@details            Query for pedometer data between the start & end dates
	@param startDate    the starting limit for the query.
	@param endDate      the end date.
 */
- (void)queryPedometerDataFromDate:(NSDate *)startDate toDate:(NSDate *)endDate;

/*! @brief              start polling pedometer data
	@details			Start polling pedometer data at a given date (or now, if no date is supplied)
	@param startDate	the time to start reading (or nil for now)
 */
- (void)startPedometerUpdatesFromDate:(NSDate *)startDate;

/*! @brief              stop polling the pedometer
 */
- (void)stopPedometerUpdates;

@end
