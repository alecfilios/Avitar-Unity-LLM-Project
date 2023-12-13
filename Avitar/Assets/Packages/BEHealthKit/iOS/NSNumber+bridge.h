//
//  NSNumber+bridge.h
//  Unity-iPhone
//
//  Created by greay on 12/10/20.
//
//

#import <Foundation/Foundation.h>

/*! @brief Category for bridging numbers between C# and Objective-C
 */
@interface NSNumber (conversion)

/*! @brief 			formatter for timestamps used internally.
 */
+ (NSNumberFormatter *)bridgeFormatter;

/*! @brief 			default localized number formatter.
 */
+ (NSNumberFormatter *)localizedFormatter;

@end
