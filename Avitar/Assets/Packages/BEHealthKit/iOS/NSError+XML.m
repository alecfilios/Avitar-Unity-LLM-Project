//
//  NSError+XML.m
//  Unity-iPhone
//
//  Created by greay on 6/3/15.
//
//

#import "NSError+XML.h"
#import "XMLDictionary/XMLDictionary.h"

@implementation NSError (XML)

- (NSDictionary *)dictionary
{
	NSMutableDictionary *dict = [NSMutableDictionary dictionary];
	dict[XMLDictionaryNodeNameKey] = @"error";

	dict[@"code"] = @(self.code);
	dict[@"domain"] = self.domain;
	dict[@"userInfo"] = self.userInfo;
	
	return dict;
}

- (NSString *)XMLString
{
	return [[self dictionary] XMLString];
}


@end
