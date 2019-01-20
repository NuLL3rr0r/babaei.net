///  (The MIT License)
///
///  Copyright (c) 2013 Mamadou Babaei
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



#include <iostream>
#include <string>
#include "crypto.hpp"

using std::cin;
using std::cout;
using std::endl;
using std::string;
using namespace BabaeiNet;

const string pangram = "The quick brown fox jumps over the lazy dog.";

int main()
{
    string err;

    cout << endl;

    string encrypted;
    if (Crypto::Encrypt(pangram, encrypted, err)) {
        cout << "Original :  " << pangram
             << endl
             << "AES      :  " << encrypted
             << endl;
    } else {
        cout << "Encryption error :  " << err
             << endl;
    }

    cout << endl;

    string decrypted;
    if (Crypto::Decrypt(encrypted, decrypted, err)) {
        cout << "AES      :  " << encrypted
             << endl
             << "Original :  " << decrypted
             << endl;
    } else {
        cout << "Decryption error :  " << err
             << endl;
    }

    cout << endl;

    string sha1;
    if (Crypto::GenerateHash(pangram, sha1, err)) {
        cout << "Original :  " << pangram
             << endl
             << "SHA-1    :  " << sha1
             << endl;
    } else {
        cout << "Hash generation error :  " << err
             << endl;
    }


    cout << endl << "Press enter to exit, please." << endl;
    cin.get();

    return 0;
}

