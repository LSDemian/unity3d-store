#!/usr/bin/python

import sys
import shutil

from mod_pbxproj import XcodeProject
projectPath = sys.argv[1]

project = XcodeProject.Load(projectPath + '/Unity-iPhone.xcodeproj/project.pbxproj')

project.add_file('System/Library/Frameworks/Security.framework', tree='SDKROOT')
project.add_file('usr/lib/libsqlite3.0.dylib', tree='SDKROOT')
project.add_file('System/Library/Frameworks/StoreKit.framework', tree='SDKROOT')
project.add_other_ldflags('-ObjC')

project.saveFormat3_2()

try:
    # This will create a new file or **overwrite an existing file**.
    f = open("/Users/test/test.txt", "w")
    try:
        f.write(projectPath + '/Unity-iPhone.xcodeproj/project.pbxproj') # Write a string to a file
    finally:
        f.close()
except IOError:
    pass
