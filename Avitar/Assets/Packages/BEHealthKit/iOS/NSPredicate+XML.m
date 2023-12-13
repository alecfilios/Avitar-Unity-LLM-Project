//
//  NSPredicate+XML.m
//  UnityFramework
//
//  Created by greay on 5/9/23.
//

#import "NSPredicate+XML.h"
#import "XMLDictionary.h"

#import <HealthKit/HealthKit.h>

@implementation NSPredicate (XML)

+ (instancetype)predicateWithBridgeFormat:(NSString *)string
{
	if ([string isEqualToString:@"metadata.HKMetadataKeyWasUserEntered != YES"]) {
		return [NSPredicate predicateWithFormat:@"metadata.%K != YES", HKMetadataKeyWasUserEntered];
	} else {
		return [NSPredicate predicateWithFormat:string];
	}
}

+ (instancetype)predicateFromXMLString:(NSString *)xmlString
{
	NSDictionary *xml = [NSDictionary dictionaryWithXMLString:xmlString];
	NSString *root = xml[@"__name"];
	if ([root isEqualToString:@"predicate"]) {
		return [NSPredicate predicateWithBridgeFormat:xml[@"format"]];
	} else if ([root isEqualToString:@"compoundPredicate"]) {
		NSMutableArray *subPredicates = [NSMutableArray array];
		for (NSString *subString in xml[@"subPredicates"]) {
			[subPredicates addObject:[NSPredicate predicateWithBridgeFormat:subString]];
		}
		if ([xml[@"type"] isEqualToString:@"AndPredicate"]) {
			return [NSCompoundPredicate orPredicateWithSubpredicates:subPredicates];
		} else if ([xml[@"type"] isEqualToString:@"OrPredicate"]) {
			return [NSCompoundPredicate orPredicateWithSubpredicates:subPredicates];
		} else if ([xml[@"type"] isEqualToString:@"NotPredicate"]) {
			return [NSCompoundPredicate orPredicateWithSubpredicates:subPredicates];
		}
	}

	return nil;
}

@end
