#  (The MIT License)
#
#  Copyright (c) 2013 Mamadou Babaei
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



CMAKE_MINIMUM_REQUIRED ( VERSION 2.8 FATAL_ERROR )
PROJECT ( CRYPTOTEST )

SET ( dep_DIR ${CRYPTOTEST_SOURCE_DIR} )
SET ( CMAKE_INSTALL_PREFIX "${CRYPTOTEST_SOURCE_DIR}/release/" )


SET ( CXX_FLAGS_WARN_LEVEL "-Wall -Wextra -Wno-unused-parameter" )
SET ( CXX_FLAGS_ISO_CPP "-pedantic" )
SET ( CXX_FLAGS_USE_PIPE "-pipe" )
SET ( CXX_FLAGS_OPT "-O2 -march=native -mtune=native" )
SET ( CXX_FLAGS_OMIT_FP "-fomit-frame-pointer" )
SET ( CXX_FLAGS_SIZE_OPT "-ffunction-sections -fdata-sections" )
SET ( CXX_FLAGS_THREADS "-pthread" )
SET ( LD_FLAGS_SIZE_OPT "-Wl,--gc-sections" )

SET ( CMAKE_CXX_FLAGS "${CXX_FLAGS_WARN_LEVEL} ${CXX_FLAGS_ISO_CPP} ${CXX_FLAGS_USE_PIPE} ${CXX_FLAGS_OPT} ${CXX_FLAGS_OMIT_FP} ${CXX_FLAGS_SIZE_OPT} ${CXX_FLAGS_THREADS}" )
SET ( CMAKE_EXE_LINKER_FLAGS "${LD_FLAGS_SIZE_OPT}" )


FIND_PACKAGE ( dep )

IF ( DEP_FOUND )
    INCLUDE_DIRECTORIES ( ${CRYPTOPP_INCLUDE_DIR} )
    LINK_DIRECTORIES ( ${CRYPTOPP_LIBRARY} )

    INCLUDE ( _src.cmake )
    
    SET ( CRYPTOTEST_BIN "CryptoTest" )

    ADD_EXECUTABLE ( ${CRYPTOTEST_BIN} ${CRYPTOTEST_SOURCE_FILES} )

    TARGET_LINK_LIBRARIES (
        ${CRYPTOTEST_BIN}
        ${CRYPTOPP_LIBRARY}
    )

    ADD_CUSTOM_COMMAND ( TARGET ${CRYPTOTEST_BIN}
        POST_BUILD
        COMMAND strip ${CRYPTOTEST_BIN}
        COMMAND strip -R.comment ${CRYPTOTEST_BIN}
        WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}
    )

    INSTALL ( FILES
        "${CMAKE_CURRENT_BINARY_DIR}/${CRYPTOTEST_BIN}"
        DESTINATION "${CMAKE_INSTALL_PREFIX}"
        PERMISSIONS
        OWNER_READ OWNER_WRITE OWNER_EXECUTE
        GROUP_READ GROUP_EXECUTE
        WORLD_READ WORLD_EXECUTE
    )
    
ENDIF ( DEP_FOUND )


