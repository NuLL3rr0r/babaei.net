///  (The MIT License)
///
///  Copyright (c) 2013 M.S. Babaei
///
///  Permission is hereby granted, free of charge, to any person obtaining a copy
///  of this software and associated documentation files (the "Software"), to deal
///  in the Software without restriction, including without limitation the rights
///  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
///  copies of the Software, and to permit persons to whom the Software is
///  furnished to do so, subject to the following conditions:
///
///  The above copyright notice and this permission notice shall be included in
///  all copies or substantial portions of the Software.
///
///  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
///  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
///  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
///  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
///  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
///  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
///  THE SOFTWARE.



#include <stdexcept>

#if defined(_WIN32) || defined(_WIN64)
#include <windows.h>
#endif /* defined(_WIN32) || defined(_WIN64) */

#include <cryptopp/aes.h>
#include <cryptopp/ccm.h>
#include <cryptopp/cryptlib.h>
#include <cryptopp/filters.h>
#include <cryptopp/hex.h>
#include <cryptopp/sha.h>
#include "crypto.hpp"


using namespace std;
using namespace CryptoPP;
using namespace BabaeiNet;


const string Crypto::UNKNOWN_ERROR = "Unknown error!";

/*
* Use this nice HEX/ASCII converter and your editor's replace dialog,
* to create your own Key and IV.
* http://www.dolcevie.com/js/converter.html
*/

unsigned char Crypto::m_key[] = {
    0x60, 0x4e, 0x75, 0x4c, 0x4c, 0x33, 0x72, 0x72, 0x30, 0x72, 0x2f, 0x2e, 0x3f, 0x4b, 0x33, 0x59
}; // `NuLL3rr0r/.?K3Y

unsigned char Crypto::m_iv[] = {
    0x4d, 0x53, 0x2d, 0x42, 0x41, 0x42, 0x41, 0x45, 0x49, 0x2d, 0x69, 0x76, 0x2f, 0x2e, 0x3f, 0x71
}; // MS-BABAEI-iv/.?q


bool Crypto::Encrypt(const string &plainText, string &out_encodedText,
                     string &out_error)
{
    try {
        string cipher;

        CBC_Mode<AES>::Encryption enc;
        enc.SetKeyWithIV(m_key, sizeof(m_key), m_iv, sizeof(m_iv));

        cipher.clear();
        StringSource(plainText, true,
                     new StreamTransformationFilter(enc, new StringSink(cipher)));

        out_encodedText.clear();
        StringSource(cipher, true, new HexEncoder(new StringSink(out_encodedText)));

        return true;
    }

    catch (CryptoPP::Exception &ex) {
        out_error.assign(ex.what());
    }

    catch (std::exception &ex) {
        out_error.assign(ex.what());
    }

    catch (...) {
        out_error.assign(UNKNOWN_ERROR);
    }

    return false;
}


bool Crypto::Decrypt(const string &cipherText, string &out_recoveredText,
                     string &out_error)
{
    try {
        string cipher;

        CBC_Mode<AES>::Decryption dec;
        dec.SetKeyWithIV(m_key, sizeof(m_key), m_iv, sizeof(m_iv));

        cipher.clear();
        StringSource(cipherText, true, new HexDecoder(new StringSink(cipher)));

        out_recoveredText.clear();
        StringSource s(cipher, true,
                       new StreamTransformationFilter(dec, new StringSink(out_recoveredText)));

        return true;
    }

    catch (CryptoPP::Exception &ex) {
        out_error.assign(ex.what());
    }

    catch (std::exception &ex) {
        out_error.assign(ex.what());
    }

    catch (...) {
        out_error.assign(UNKNOWN_ERROR);
    }

    return false;
}


bool Crypto::GenerateHash(const string &text, string &out_digest, string &out_error)
{
    try {
        SHA1 hash;

        out_digest.clear();
        StringSource(text, true,
                     new HashFilter(hash, new HexEncoder(new StringSink(out_digest))));

        return true;
    }

    catch (CryptoPP::Exception &ex) {
        out_error.assign(ex.what());
    }

    catch (std::exception &ex) {
        out_error.assign(ex.what());
    }

    catch (...) {
        out_error.assign(UNKNOWN_ERROR);
    }

    return false;
}


