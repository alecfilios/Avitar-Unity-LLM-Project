//
//  NSError+XML.h
//  Unity-iPhone
//
//  Created by greay on 6/3/15.
//
//

#import <Foundation/Foundation.h>

/*! @brief Helper category to generate XML from an NSError, for sending to Unity.
 */
@interface NSError (XML)

/*! @brief 			dictionary representation for XML generation.
 */
- (NSDictionary *)dictionary;

/*! @brief 			generate XML from an error.
 */
- (NSString *)XMLString;

@end
 
