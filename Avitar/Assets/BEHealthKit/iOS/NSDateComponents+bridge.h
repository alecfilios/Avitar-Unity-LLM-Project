//
//  NSDateComponents+bridge.h
//  Unity-iPhone
//
//  Created by greay on 7/25/19.
//

#import <Foundation/Foundation.h>

/*! @brief Category for bridging date components between C# and Objective-C
 */
@interface NSDateComponents (conversion)

+ (instancetype)dateComponentsFromBridgeString:(char *)xml;	/*!< @brief create an NSDateComponents from a xml string */

@end
