//
//  NSDate+bridge.h
//  Unity-iPhone
//
//  Created by greay on 3/28/15.
//
//

#import <Foundation/Foundation.h>

/*! @brief Category for bridging dates between C# and Objective-C
 */
@interface NSDate (conversion)


+ (instancetype)dateFromBridgeString:(char *)stamp;	/*!< @brief create an NSDate from a timestamp */
+ (instancetype)dateFromToken:(NSNumber *)n; /*!< @brief create an NSDate from a token (UNIX timestamp) */
- (NSString *)bridgeToken; /*!< @brief create a timestamp */

@end
