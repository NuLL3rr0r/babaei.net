#  (The MIT License)
#
#  Copyright (c) 2013 Mohammad S. Babaei
#
#  Permission is hereby granted, free of charge, to any person obtaining a copy
#  of this software and associated documentation files (the "Software"), to deal
#  in the Software without restriction, including without limitation the rights
#  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
#  copies of the Software, and to permit persons to whom the Software is
#  furnished to do so, subject to the following conditions:
#
#  The above copyright notice and this permission notice shall be included in
#  all copies or substantial portions of the Software.
#
#  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
#  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
#  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
#  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
#  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
#  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#  THE SOFTWARE.



TEMPLATE = app
CONFIG += console
CONFIG -= app_bundle
CONFIG -= qt

QMAKE_CXXFLAGS_DEBUG += -MTd
QMAKE_CXXFLAGS_RELEASE += -MT

# Used to reduce the size of the header files and speed up compilation.
# Excludes things like cryptography, DDE, RPC, the Windows Shell and Winsock.
DEFINES += WIN32_LEAN_AND_MEAN

INCLUDEPATH += $$PWD/./win_deps/include
DEPENDPATH += $$PWD/./win_deps/include
LIBS += -L$$PWD/./win_deps/lib

win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/./win_deps/lib -lcryptlib_d
else:win32:CONFIG(release, debug|release): LIBS += -L$$PWD/./win_deps/lib -lcryptlib

SOURCES += main.cpp\
        crypto.cpp

HEADERS  += crypto.hpp

