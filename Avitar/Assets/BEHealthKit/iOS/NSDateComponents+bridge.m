//
//  NSDateComponents+bridge.m
//  Unity-iPhone
//
//  Created by greay on 7/25/19.
//

#import "NSDateComponents+bridge.h"
#import "XMLDictionary/XMLDictionary.h"

@implementation NSDateComponents (bridge)

+ (instancetype)dateComponentsFromBridgeString:(char *)xml
{
	if (!xml) return nil;
	
	NSString *str = [NSString stringWithCString:xml encoding:NSUTF8StringEncoding];
	NSDictionary *dict = [NSDictionary dictionaryWithXMLString:str];
	NSDateComponents *components = [[NSDateComponents alloc] init];
	
	components.second = [dict[@"s"] integerValue];
	components.minute = [dict[@"m"] integerValue];
	components.hour = [dict[@"H"] integerValue];
	components.day = [dict[@"d"] integerValue];
	
	return components;
}

@end
