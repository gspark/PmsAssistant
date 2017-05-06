using FluorineFx;
using FluorineFx.IO;
using FluorineFx.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PmsAssistant
{
    public class Ihotel
    {
        private const string BaseAddress = "http://119.29.215.133:8090";
        private const string LogIn = "http://119.29.215.133:8090/ipmsthef/loginCenter";
        private const string LogOut = "http://119.29.215.133:8090/ipmsthef/messagebroker/amf";

        private const string Cookie = "http://119.29.215.133:8090/ipmsthef/messagebroker/amf";

        private readonly HttpClient _httpClient;

        private const string AMFHexString = "00 03 00 00 00 01 00 04 6e 75 6c 6c 00 04 2f 33" +
                                                "35 31 00 00 10 c8 0a 00 00 00 01 11 0a 81 13 4f" +
                                                "66 6c 65 78 2e 6d 65 73 73 61 67 69 6e 67 2e 6d" +
                                                "65 73 73 61 67 65 73 2e 52 65 6d 6f 74 69 6e 67" +
                                                "4d 65 73 73 61 67 65 0d 73 6f 75 72 63 65 13 6f" +
                                                "70 65 72 61 74 69 6f 6e 13 74 69 6d 65 73 74 61" +
                                                "6d 70 11 63 6c 69 65 6e 74 49 64 17 64 65 73 74" +
                                                "69 6e 61 74 69 6f 6e 13 6d 65 73 73 61 67 65 49" +
                                                "64 15 74 69 6d 65 54 6f 4c 69 76 65 09 62 6f 64" +
                                                "79 0f 68 65 61 64 65 72 73 01 06 17 73 61 76 65" +
                                                "52 65 73 65 72 76 65 04 00 01 06 29 66 4d 61 73" +
                                                "74 65 72 46 61 63 61 64 65 53 65 72 76 69 63 65" +
                                                "06 49 37 43 33 35 38 31 44 30 2d 46 31 42 36 2d" +
                                                "31 30 31 33 2d 30 35 41 30 2d 44 32 45 35 38 39" +
                                                "36 38 30 33 33 46 04 00 09 09 01 04 05 04 0f 0a" +
                                                "84 23 3b 63 6f 6d 2e 67 72 65 65 6e 63 6c 6f 75" +
                                                "64 2e 64 74 6f 2e 46 4d 61 73 74 65 72 44 74 6f" +
                                                "15 73 61 6c 65 6d 61 6e 44 65 73 1f 6d 61 73 74" +
                                                "65 72 47 72 6f 75 70 4c 69 73 74 15 63 6f 6d 70" +
                                                "61 6e 79 44 65 73 15 63 68 61 6e 6e 65 6c 44 65" +
                                                "73 1d 6d 61 73 74 65 72 4c 69 76 65 4c 69 73 74" +
                                                "0d 73 72 63 44 65 73 15 63 6d 73 43 6f 64 65 44" +
                                                "65 73 15 72 73 76 54 79 70 65 44 65 73 11 76 61" +
                                                "6c 75 65 4d 61 70 11 63 61 72 64 4e 61 6d 65 13" +
                                                "6d 61 73 74 65 72 44 65 73 13 6d 61 73 74 65 72" +
                                                "53 75 62 19 72 61 74 65 63 6f 64 65 46 6c 61 67" +
                                                "15 6c 69 76 65 44 61 79 4e 75 6d 11 6c 69 76 65" +
                                                "48 6f 75 72 15 64 69 73 63 6f 75 6e 74 4e 6f 21" +
                                                "73 65 6c 65 63 74 65 64 53 74 65 70 4e 61 6d 65" +
                                                "0d 6d 73 54 79 70 65 15 6d 61 73 74 65 72 42 61" +
                                                "73 65 0f 66 72 6f 6d 43 72 73 19 6d 61 73 74 65" +
                                                "72 53 74 61 6c 6f 67 17 6d 61 73 74 65 72 47 75" +
                                                "65 73 74 15 72 73 76 53 72 63 4c 69 73 74 21 73" +
                                                "65 72 69 61 6c 56 65 72 73 69 6f 6e 55 49 44 1d" +
                                                "6d 61 73 74 65 72 4c 69 6e 6b 4c 69 73 74 0b 76" +
                                                "61 6c 75 65 17 73 74 61 79 52 73 76 52 61 74 65" +
                                                "13 72 6d 74 79 70 65 44 65 73 13 69 64 74 79 70" +
                                                "65 44 65 73 0d 73 65 78 44 65 73 0d 69 73 54 72" +
                                                "61 79 1f 72 61 74 65 63 6f 64 65 43 61 74 65 44" +
                                                "65 73 17 72 61 74 65 63 6f 64 65 44 65 73 13 6d" +
                                                "61 72 6b 65 74 44 65 73 06 01 0a 07 43 66 6c 65" +
                                                "78 2e 6d 65 73 73 61 67 69 6e 67 2e 69 6f 2e 41" +
                                                "72 72 61 79 43 6f 6c 6c 65 63 74 69 6f 6e 09 01" +
                                                "01 01 06 0d e5 85 b6 e4 bb 96 0a 09 09 01 01 06" +
                                                "19 e4 b8 8a e9 97 a8 e6 95 a3 e5 ae a2 06 01 06" +
                                                "01 0a 0b 01 01 01 0a 82 43 3f 63 6f 6d 2e 67 72" +
                                                "65 65 6e 63 6c 6f 75 64 2e 65 6e 74 69 74 79 2e" +
                                                "4d 61 73 74 65 72 44 65 73 11 63 61 72 64 54 79" +
                                                "70 65 13 63 61 72 64 4c 65 76 65 6c 2a 17 73 61" +
                                                "6c 65 73 4d 61 6e 44 65 73 0f 63 6d 73 43 6f 64" +
                                                "65 17 63 61 72 64 54 79 70 65 44 65 73 11 73 61" +
                                                "6c 65 73 4d 61 6e 0f 72 73 76 54 79 70 65 0d 72" +
                                                "6d 74 79 70 65 19 63 61 72 64 4c 65 76 65 6c 44" +
                                                "65 73 13 63 6f 6d 70 61 6e 79 49 64 52 20 15 63" +
                                                "72 65 61 74 65 55 73 65 72 1d 63 72 65 61 74 65" +
                                                "44 61 74 65 74 69 6d 65 15 6d 6f 64 69 66 79 55" +
                                                "73 65 72 1d 6d 6f 64 69 66 79 44 61 74 65 74 69" +
                                                "6d 65 0f 68 6f 74 65 6c 49 64 05 69 64 19 68 6f" +
                                                "74 65 6c 47 72 6f 75 70 49 64 01 01 01 01 01 01" +
                                                "01 01 01 01 05 7f ff ff ff e0 00 00 00 01 01 06" +
                                                "01 01 06 01 01 05 7f f8 00 00 00 00 00 00 05 7f" +
                                                "f8 00 00 00 00 00 00 05 7f f8 00 00 00 00 00 00" +
                                                "0a 83 73 3f 63 6f 6d 2e 67 72 65 65 6e 63 6c 6f" +
                                                "75 64 2e 65 6e 74 69 74 79 2e 4d 61 73 74 65 72" +
                                                "53 75 62 1d 73 72 63 4d 65 6d 62 65 72 4c 65 76" +
                                                "65 6c 1b 73 72 63 4d 65 6d 62 65 72 44 65 73 63" +
                                                "15 6f 74 61 43 68 61 6e 6e 65 6c 17 73 72 63 43" +
                                                "68 61 6e 6e 65 6c 32 19 73 72 63 48 6f 74 65 6c" +
                                                "44 65 73 63 0f 75 6e 69 6f 6e 69 64 17 73 72 63" +
                                                "43 68 61 6e 6e 65 6c 34 17 73 72 63 43 68 61 6e" +
                                                "6e 65 6c 31 11 6f 74 61 52 73 76 4e 6f 1b 61 67" +
                                                "65 6e 63 79 4f 72 64 65 72 4e 6f 19 73 72 63 48" +
                                                "6f 74 65 6c 43 6f 64 65 0f 67 63 52 73 76 4e 6f" +
                                                "13 6f 74 61 52 65 6d 61 72 6b 17 73 72 63 4d 65" +
                                                "6d 62 65 72 4e 6f 0b 61 70 70 69 64 15 6f 74 68" +
                                                "65 72 52 73 76 4e 6f 17 70 72 6f 64 75 63 74 43" +
                                                "6f 64 65 23 73 72 63 48 6f 74 65 6c 47 72 6f 75" +
                                                "70 44 65 73 63 11 77 65 62 43 6c 61 73 73 23 73" +
                                                "72 63 48 6f 74 65 6c 47 72 6f 75 70 43 6f 64 65" +
                                                "0f 70 61 79 62 61 63 6b 17 73 72 63 43 68 61 6e" +
                                                "6e 65 6c 33 0f 77 65 62 46 72 6f 6d 0d 6f 70 65" +
                                                "6e 69 64 7c 7e 81 00 81 02 81 04 81 06 81 08 06" +
                                                "01 06 01 06 01 06 01 06 01 06 01 06 01 06 01 06" +
                                                "01 06 01 06 01 06 01 06 01 06 01 06 01 06 01 06" +
                                                "01 06 01 06 01 06 01 06 01 06 01 06 01 06 01 06" +
                                                "01 01 06 01 01 05 7f f8 00 00 00 00 00 00 05 7f" +
                                                "f8 00 00 00 00 00 00 05 7f f8 00 00 00 00 00 00" +
                                                "01 04 01 05 7f ff ff ff e0 00 00 00 06 01 01 01" +
                                                "0a 8d 33 41 63 6f 6d 2e 67 72 65 65 6e 63 6c 6f" +
                                                "75 64 2e 65 6e 74 69 74 79 2e 4d 61 73 74 65 72" +
                                                "42 61 73 65 74 11 70 61 63 6b 61 67 65 73 11 73" +
                                                "70 65 63 69 61 6c 73 13 69 73 46 69 78 52 61 74" +
                                                "65 13 69 73 46 69 78 52 6d 6e 6f 11 63 68 69 6c" +
                                                "64 72 65 6e 0d 69 73 53 75 72 65 11 67 72 70 41" +
                                                "63 63 6e 74 11 69 73 57 61 6c 6b 69 6e 0d 73 63" +
                                                "46 6c 61 67 11 69 73 53 65 63 72 65 74 0f 63 72" +
                                                "69 62 4e 75 6d 19 69 73 53 65 63 72 65 74 52 61" +
                                                "74 65 11 63 72 65 64 69 74 4e 6f 13 63 72 65 64" +
                                                "69 74 4d 61 6e 1b 63 72 65 64 69 74 43 6f 6d 70" +
                                                "61 6e 79 17 63 72 65 64 69 74 4d 6f 6e 65 79 0f" +
                                                "72 6d 6f 63 63 49 64 13 70 6b 67 4c 69 6e 6b 49" +
                                                "64 0b 72 73 76 4e 6f 13 77 68 65 72 65 46 72 6f" +
                                                "6d 0f 77 68 65 72 65 54 6f 0b 63 72 73 4e 6f 0f" +
                                                "70 75 72 70 6f 73 65 07 64 65 70 0d 69 73 53 65" +
                                                "6e 64 07 73 72 63 07 61 72 72 11 62 75 69 6c 64" +
                                                "69 6e 67 19 73 61 6c 65 73 43 68 61 6e 6e 65 6c" +
                                                "76 0d 72 73 76 4d 61 6e 15 63 75 74 6f 66 66 44" +
                                                "61 79 73 17 72 61 74 65 43 68 61 6e 67 65 64 15" +
                                                "63 75 74 6f 66 66 44 61 74 65 7a 0f 6f 6c 64 52" +
                                                "61 74 65 15 72 73 76 43 6f 6d 70 61 6e 79 0b 63" +
                                                "6f 4d 73 67 13 70 72 6f 6d 6f 74 69 6f 6e 11 6d" +
                                                "61 73 74 65 72 49 64 17 65 78 74 72 61 42 65 64" +
                                                "4e 75 6d 13 65 78 74 72 61 46 6c 61 67 0d 6d 61" +
                                                "72 6b 65 74 0d 6c 69 6e 6b 49 64 0f 70 61 79 43" +
                                                "6f 64 65 11 73 6f 75 72 63 65 49 64 0f 61 67 65" +
                                                "6e 74 49 64 0d 6d 6f 62 69 6c 65 13 61 6d 65 6e" +
                                                "69 74 69 65 73 17 69 73 50 65 72 6d 61 6e 65 6e" +
                                                "74 17 6c 61 73 74 4e 75 6d 4c 69 6e 6b 11 72 61" +
                                                "63 6b 52 61 74 65 0f 62 69 7a 44 61 74 65 0f 6c" +
                                                "61 73 74 4e 75 6d 09 74 61 67 30 11 6e 65 67 6f" +
                                                "52 61 74 65 17 70 6f 73 74 69 6e 67 46 6c 61 67" +
                                                "13 64 73 63 41 6d 6f 75 6e 74 11 73 61 6c 65 73" +
                                                "6d 61 6e 15 64 73 63 50 65 72 63 65 6e 74 0f 63" +
                                                "6d 73 63 6f 64 65 13 67 72 6f 75 70 43 6f 64 65" +
                                                "19 65 78 74 72 61 42 65 64 52 61 74 65 11 63 72" +
                                                "69 62 52 61 74 65 11 6c 69 6d 69 74 41 6d 74 0d" +
                                                "72 65 6d 61 72 6b 21 72 61 74 65 63 6f 64 65 43" +
                                                "61 74 65 67 6f 72 79 0f 63 68 61 6e 6e 65 6c 11" +
                                                "72 61 74 65 63 6f 64 65 07 73 74 61 09 72 6d 6e" +
                                                "6f 09 61 72 6e 6f 11 72 73 76 53 72 63 49 64 19" +
                                                "67 72 6f 75 70 4d 61 6e 61 67 65 72 11 72 73 76" +
                                                "43 6c 61 73 73 0f 62 6c 6f 63 6b 49 64 0f 69 73" +
                                                "52 65 73 72 76 0b 61 64 75 6c 74 0d 72 65 73 53" +
                                                "74 61 11 75 70 52 6d 74 79 70 65 0d 72 65 73 44" +
                                                "65 70 0d 63 68 61 72 67 65 11 75 70 52 65 61 73" +
                                                "6f 6e 07 70 61 79 11 75 70 52 65 6d 61 72 6b 15" +
                                                "6d 6b 74 61 63 74 43 6f 64 65 13 64 73 63 52 65" +
                                                "61 73 6f 6e 11 72 65 61 6c 52 61 74 65 0d 65 78" +
                                                "70 53 74 61 0b 72 73 76 49 64 0b 72 6d 6e 75 6d" +
                                                "1d 6d 61 73 74 65 72 52 73 76 53 72 63 49 64 11" +
                                                "6d 65 6d 62 65 72 4e 6f 15 69 73 52 6d 70 6f 73" +
                                                "74 65 64 0b 74 6d 53 74 61 15 6d 65 6d 62 65 72" +
                                                "54 79 70 65 13 72 6d 70 6f 73 74 53 74 61 0d 63" +
                                                "72 65 64 69 74 17 69 6e 6e 65 72 43 61 72 64 49" +
                                                "64 7c 7e 81 00 81 02 81 04 81 06 81 08 06 07 30" +
                                                "30 31 06 01 06 01 06 03 46 06 83 02 04 00 06 83" +
                                                "02 04 00 06 83 02 06 01 06 83 02 04 00 06 83 02" +
                                                "06 01 06 01 06 01 05 7f ff ff ff e0 00 00 00 05" +
                                                "7f ff ff ff e0 00 00 00 05 7f ff ff ff e0 00 00" +
                                                "00 06 01 06 01 06 01 06 01 06 01 08 01 42 75 bd" +
                                                "73 18 b0 00 00 06 83 02 06 07 57 49 4b 08 01 42" +
                                                "75 bd 2e 6e 90 00 00 06 01 06 01 06 05 53 53 06" +
                                                "13 e5 bc a0 e4 b8 80 e4 b8 80 05 7f ff ff ff e0" +
                                                "00 00 00 02 08 01 42 75 bd 35 4c 60 00 00 04 00" +
                                                "05 7f ff ff ff e0 00 00 00 06 01 06 01 06 01 04" +
                                                "00 04 00 06 3d 30 30 30 30 30 30 30 30 30 30 30" +
                                                "30 30 30 30 30 30 30 30 30 30 30 30 30 30 30 30" +
                                                "30 30 30 06 07 57 41 4b 05 7f ff ff ff e0 00 00" +
                                                "00 06 01 04 00 04 00 06 01 06 01 06 83 02 04 00" +
                                                "04 00 01 04 00 06 01 04 00 06 03 30 04 00 06 01" +
                                                "04 00 06 01 06 01 04 00 04 00 04 00 06 01 06 03" +
                                                "52 06 07 4f 54 48 06 07 52 41 43 06 83 10 06 01" +
                                                "06 01 04 00 06 01 06 83 02 04 00 06 03 54 04 01" +
                                                "06 01 06 01 01 04 00 06 01 04 00 06 01 06 01 06" +
                                                "01 04 00 06 01 04 00 04 01 04 00 06 01 06 83 02" +
                                                "06 01 06 01 06 83 02 04 00 05 7f f8 00 00 00 00" +
                                                "00 00 06 01 01 06 01 01 05 7f f8 00 00 00 00 00" +
                                                "00 05 7f f8 00 00 00 00 00 00 05 7f f8 00 00 00" +
                                                "00 00 00 01 01 0a 86 43 43 63 6f 6d 2e 67 72 65" +
                                                "65 6e 63 6c 6f 75 64 2e 65 6e 74 69 74 79 2e 4d" +
                                                "61 73 74 65 72 47 75 65 73 74 82 3c 0b 6e 61 6d" +
                                                "65 33 0f 74 69 6d 65 73 49 6e 0f 76 69 73 61 45" +
                                                "6e 64 09 69 64 4e 6f 07 66 61 78 0b 74 69 74 6c" +
                                                "65 19 65 6e 74 65 72 44 61 74 65 45 6e 64 0b 65" +
                                                "6d 61 69 6c 13 76 69 73 61 47 72 61 6e 74 0d 73" +
                                                "74 72 65 65 74 11 69 6e 74 65 72 65 73 74 13 70" +
                                                "72 6f 66 69 6c 65 49 64 09 63 69 74 79 11 76 69" +
                                                "73 61 54 79 70 65 17 70 72 6f 66 69 6c 65 54 79" +
                                                "70 65 0b 6e 61 6d 65 32 09 6e 61 6d 65 0b 62 69" +
                                                "72 74 68 0f 63 6f 75 6e 74 72 79 07 73 65 78 0d" +
                                                "69 64 43 6f 64 65 0d 6e 61 74 69 6f 6e 17 6e 61" +
                                                "6d 65 43 6f 6d 62 69 6e 65 11 64 69 76 69 73 69" +
                                                "6f 6e 13 76 69 73 61 42 65 67 69 6e 0f 7a 69 70" +
                                                "43 6f 64 65 15 73 61 6c 75 74 61 74 69 6f 6e 13" +
                                                "70 68 6f 74 6f 53 69 67 6e 0d 76 69 73 61 4e 6f" +
                                                "82 18 09 72 61 63 65 11 72 65 6c 69 67 69 6f 6e" +
                                                "15 6f 63 63 75 70 61 74 69 6f 6e 07 76 69 70 0b" +
                                                "70 68 6f 6e 65 11 70 68 6f 74 6f 50 69 63 13 65" +
                                                "6e 74 65 72 50 6f 72 74 0b 69 64 45 6e 64 0d 63" +
                                                "61 72 65 65 72 0b 73 74 61 74 65 11 6c 61 73 74" +
                                                "4e 61 6d 65 13 65 6e 74 65 72 44 61 74 65 13 66" +
                                                "69 72 73 74 4e 61 6d 65 11 6c 61 6e 67 75 61 67" +
                                                "65 7c 7e 81 00 81 02 81 04 81 06 81 08 06 01 06" +
                                                "01 05 7f ff ff ff e0 00 00 00 01 06 01 06 01 06" +
                                                "01 01 06 01 06 01 06 01 06 01 04 00 06 01 06 01" +
                                                "06 0b 47 55 45 53 54 06 17 5a 68 61 6e 67 20 59" +
                                                "69 20 59 69 06 83 08 01 06 05 43 4e 06 03 31 06" +
                                                "05 30 31 06 83 74 06 01 06 01 01 06 01 06 01 05" +
                                                "7f ff ff ff e0 00 00 00 06 01 06 01 06 01 06 01" +
                                                "06 01 06 83 0e 06 01 05 7f ff ff ff e0 00 00 00" +
                                                "06 01 01 06 01 06 01 06 01 01 06 01 06 01 06 01" +
                                                "01 06 01 01 05 7f f8 00 00 00 00 00 00 05 7f f8" +
                                                "00 00 00 00 00 00 05 7f f8 00 00 00 00 00 00 0a" +
                                                "09 09 03 01 0a 84 63 39 63 6f 6d 2e 67 72 65 65" +
                                                "6e 63 6c 6f 75 64 2e 65 6e 74 69 74 79 2e 52 73" +
                                                "76 53 72 63 82 20 82 42 13 6c 69 73 74 4f 72 64" +
                                                "65 72 82 28 0b 61 63 63 6e 74 81 46 81 40 0f 6f" +
                                                "63 63 46 6c 61 67 82 46 0f 61 72 72 44 61 74 65" +
                                                "0f 64 65 70 44 61 74 65 82 0e 15 72 73 76 41 72" +
                                                "72 44 61 74 65 15 72 73 76 44 65 70 44 61 74 65" +
                                                "11 72 73 76 4f 63 63 49 64 15 69 73 53 75 72 65" +
                                                "52 61 74 65 82 50 82 54 81 70 82 1a 76 81 7c 82" +
                                                "66 82 68 82 6e 82 70 82 00 82 08 82 3c 81 3e 82" +
                                                "3e 7c 7e 81 00 81 02 81 04 81 06 81 08 04 82 0c" +
                                                "06 83 14 04 00 04 82 0c 04 00 04 00 01 06 01 06" +
                                                "01 08 01 42 75 bd 2e 6e 90 00 00 08 01 42 75 bd" +
                                                "73 18 b0 00 00 06 83 0c 08 18 08 16 05 7f ff ff" +
                                                "ff e0 00 00 00 06 83 02 05 7f ff ff ff e0 00 00" +
                                                "00 04 01 01 01 06 83 06 02 06 01 04 82 0c 04 01" +
                                                "04 00 04 82 0c 04 00 01 06 01 06 83 10 06 01 01" +
                                                "06 01 01 05 7f f8 00 00 00 00 00 00 05 7f f8 00" +
                                                "00 00 00 00 00 05 7f f8 00 00 00 00 00 00 05 42" +
                                                "75 bd 2e 4c 54 30 00 0a 09 09 01 01 01 0a 09 09" +
                                                "01 01 06 83 06 01 01 02 06 13 e9 97 a8 e5 b8 82" +
                                                "e7 b1 bb 01 06 64 06 83 02 0a 0d 15 44 53 45 6e" +
                                                "64 70 6f 69 6e 74 01 09 44 53 49 64 06 07 6e 69" +
                                                "6c 19 75 73 65 72 44 74 6f 42 79 74 65 73 0c 89" +
                                                "63 78 da 85 94 cd 4e db 40 14 85 5f 05 cd da b1" +
                                                "66 c6 19 c7 be 59 41 08 10 15 08 25 a1 12 aa 2a" +
                                                "34 f6 0c 64 5a ff c9 76 5a 28 ca b2 14 2a 75 d3" +
                                                "1f 75 59 55 ea 1b 74 17 78 9d 34 ea 8a 57 e8 d8" +
                                                "21 80 a1 14 cb 92 e5 f1 d1 b9 67 be 7b c7 c7 3c" +
                                                "49 e0 d8 8f 85 04 44 90 21 64 e6 a7 2a c9 01 fd" +
                                                "79 f7 69 32 fe 36 fd 75 3e 3d ff 7e b3 de 8e 00" +
                                                "a9 b5 38 97 01 32 94 00 62 04 2a cb bb a9 90 29" +
                                                "e0 91 e1 a9 b7 cb 3c d7 4e 14 93 46 0d b3 1a b6" +
                                                "16 30 86 f2 46 86 cf b3 81 d2 42 44 91 31 28 2c" +
                                                "e6 75 5b 4f 77 7b 4f 6a 2b 95 ea ef 3f 4e c6 27" +
                                                "bf 2f be 4c 7f 8e 27 e3 cf 97 17 a7 93 af 27 93" +
                                                "0f 3f a6 67 a7 97 17 67 d5 3c 8f 69 cb 5a ab 69" +
                                                "3c 4c 3a 02 58 19 9b 55 73 df 28 2a 91 56 1f 4a" +
                                                "f4 70 fd 92 ca 1d f7 20 3e 50 51 5f 85 72 31 49" +
                                                "7a 32 7d 5d 22 98 03 aa 2f 90 06 58 0c 58 03 dd" +
                                                "08 5b 81 92 51 fe 5f d5 51 a2 53 ae 77 57 3b 9b" +
                                                "7b fd dd ad f6 de 5a b7 df 5e 47 c6 30 d3 ee c7" +
                                                "3e f7 07 52 6f 16 5f 33 a7 c6 6c 5f 1b bb ba 0f" +
                                                "a9 d4 4d 2a 1a 95 eb 5a 65 15 bb 86 dd 1a 6d 2c" +
                                                "10 0a 75 0b 08 9e 8b 76 0a 3b b4 b8 bc d1 d9 2c" +
                                                "f6 9c e4 ad d2 a5 b7 b3 d5 de 7e d6 e9 75 b7 67" +
                                                "ab 05 57 ec ba 86 0c b9 0a 00 21 43 1e e6 29 5f" +
                                                "09 f8 01 20 fc 8f eb 5e 4f ca d7 4e d9 18 cd 8f" +
                                                "ba 16 36 54 b6 32 8c fc 5e 22 7d c5 b5 67 5f 93" +
                                                "cd d6 78 a0 a9 e8 41 79 19 7b 80 96 8a 9c b7 48" +
                                                "6b 32 fe ab 5e ce f3 61 06 68 f3 0a d4 9d 79 d4" +
                                                "20 6d 60 0c 88 9e f5 30 f6 54 20 8b b8 61 2c d4" +
                                                "fe 51 85 c8 7d ee 33 d1 8c 48 81 31 e2 e1 15 d0" +
                                                "84 67 d9 9b 38 15 80 1c 22 7c d7 13 1e a3 02 d7" +
                                                "85 4f 31 b6 6c e1 09 c7 22 96 14 98 31 ad 1d c4" +
                                                "51 59 33 e3 af 67 2d ca e4 61 71 f4 46 86 b6 28" +
                                                "e3 ab 38 ba 3e 91 b4 68 45 1c 26 c3 5c a6 9d 68" +
                                                "3f 06 f4 9c b8 d4 24 b6 63 5a c4 24 ac d1 24 b6" +
                                                "6b 52 56 37 29 b6 4c 9b 34 e7 5f 99 7e 34 75 7d" +
                                                "02 18 5c 61 39 60 73 cf 81 ba ed 80 e0 de 3e 38" +
                                                "d4 73 c1 23 44 36 09 71 4c 42 a8 c9 1a a6 5b 7f" +
                                                "71 7b e2 ab 53 fe 48 cf 1c 6a 57 7e 06 46 c8 7d" +
                                                "40 6e 8b b5 9c b6 eb b0 16 5d 42 a3 d1 5f 00 75" +
                                                "7e 28 21 44 53 52 65 71 75 65 73 74 54 69 6d 65" +
                                                "6f 75 74 04 84 58 3f 66 6c 65 78 2e 6d 65 73 73" +
                                                "61 67 69 6e 67 2e 72 65 71 75 65 73 74 2e 6c 61" +
                                                "6e 67 75 61 67 65 06 0b 7a 68 5f 43 4e 01";

        private const string AMF_INI = "00 03 00 00 00 01 00 04 6e 75 6c 6c 00 02 2f 31" +
                                       "00 00 00 e0 0a 00 00 00 01 11 0a 81 13 4d 66 6c" +
                                       "65 78 2e 6d 65 73 73 61 67 69 6e 67 2e 6d 65 73" +
                                       "73 61 67 65 73 2e 43 6f 6d 6d 61 6e 64 4d 65 73" +
                                       "73 61 67 65 13 6f 70 65 72 61 74 69 6f 6e 1b 63" +
                                       "6f 72 72 65 6c 61 74 69 6f 6e 49 64 13 74 69 6d" +
                                       "65 73 74 61 6d 70 11 63 6c 69 65 6e 74 49 64 17" +
                                       "64 65 73 74 69 6e 61 74 69 6f 6e 13 6d 65 73 73" +
                                       "61 67 65 49 64 15 74 69 6d 65 54 6f 4c 69 76 65" +
                                       "09 62 6f 64 79 0f 68 65 61 64 65 72 73 04 05 06" +
                                       "01 04 00 01 06 01 06 49 34 32 30 39 39 32 43 41" +
                                       "2d 35 41 36 45 2d 37 34 36 43 2d 38 35 38 44 2d" +
                                       "44 32 44 30 45 37 46 34 39 39 30 43 04 00 0a 0b" +
                                       "01 01 0a 05 25 44 53 4d 65 73 73 61 67 69 6e 67" +
                                       "56 65 72 73 69 6f 6e 04 01 09 44 53 49 64 06 07" +
                                       "6e 69 6c 01";

        private string logCode;

        private string jsessionid;

        public Ihotel()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(BaseAddress) };
        }

        public async Task<bool> Login()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"password", "1234"},
                {"userName", "MY"},
                {"typeCode", "CQYSK-F"},
                {"type", "LOGIN_TYPE_HOTEL"},
                {"flex.messaging.request.language", "zh_CN"}
            });

            //await异步等待回应
            var response = await _httpClient.PostAsync(LogIn, content);

            //await异步
            var ret = await response.Content.ReadAsStringAsync();
            if (ret.Contains("faultCode"))
            {
                return false;
            }
            logCode = ret;

            await InitAMF();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dep">2017-5-5 6:00:00</param>
        /// <param name="arr">到达时间 2017-5-4 10:00:00</param>
        /// <param name="cutoffDate">截止时间 2017-5-4 12:00:00</param>
        /// <returns></returns>
        public async Task<bool> SaveReserve(string name, DateTime dep, DateTime arr, DateTime cutoffDate)
        {
            var ad = new AMFDeserializer(new MemoryStream(StrToToHexByte(AMFHexString)));
            var message = ad.ReadAMFMessage();

            foreach (var body in message.Bodies)
            {
                object[] content = body.Content as object[];
                RemotingMessage rm = content[0] as RemotingMessage;
                object[] bodys = rm.body as object[];
                ASObject ab = bodys[2] as ASObject;

                ASObject masterBase = ab["masterBase"] as ASObject;
                masterBase["dep"] = dep;
                masterBase["arr"] = arr;
                masterBase["rsvMan"] = name;
                masterBase["cutoffDate"] = cutoffDate;

                ASObject masterGuest = ab["masterGuest"] as ASObject;
                masterGuest["name"] = name;
                masterGuest["name2"] = "Ma Yi Yi";
                masterGuest["sex"] = "1";
            }

            var m = new MemoryStream();
            AMFSerializer amfSerializer = new AMFSerializer(m);
            amfSerializer.WriteMessage(message);
            amfSerializer.Flush();

            //await异步等待回应
            var response = await _httpClient.PostAsync(Cookie + ";" + this.jsessionid, new ByteArrayContent(m.ToArray()));

            //await异步
            var ret = await response.Content.ReadAsByteArrayAsync();
            ad = new AMFDeserializer(new MemoryStream(ret));
            message = ad.ReadAMFMessage();
            return true;
        }

        public async Task<bool> Logout()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"password", "1234"},
                {"userName", "MY"},
                {"typeCode", "CQYSK-F"},
                {"flex.messaging.request.language", "zh_CN"}
            });

            //await异步等待回应
            var response = await _httpClient.PostAsync(LogOut, content);

            //await异步
            var ret = await response.Content.ReadAsStringAsync();
            if (ret.Contains("faultCode"))
            {
                return false;
            }
            logCode = ret;
            return true;
        }

        /// <summary>  
        /// 16进制字符串转字节数组  
        /// </summary>  
        /// <param name="hexString"></param>  
        /// <returns></returns>  
        public byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;

        }

        private async Task<bool> InitAMF()
        {
            var ad = new AMFDeserializer(new MemoryStream(StrToToHexByte(AMF_INI)));
            var message = ad.ReadAMFMessage();

            foreach (var body in message.Bodies)
            {
                object[] content = body.Content as object[];
                CommandMessage cm = content[0] as CommandMessage;
                cm.messageId = Guid.NewGuid().ToString("D");
            }

            var m = new MemoryStream();
            AMFSerializer amfSerializer = new AMFSerializer(m);
            amfSerializer.WriteMessage(message);
            amfSerializer.Flush();

            //await异步等待回应
            var response = await _httpClient.PostAsync(Cookie, new ByteArrayContent(m.ToArray()));
            //await异步
            var ret = await response.Content.ReadAsByteArrayAsync();
            ad = new AMFDeserializer(new MemoryStream(ret));
            message = ad.ReadAMFMessage();
            if (response.StatusCode != HttpStatusCode.OK) return false;
            var c = response.Headers.GetValues("Set-Cookie");
            foreach (var header in c)
            {
                if (header.Contains("JSESSIONID"))
                {
                    string[] ss = header.Split(';');
                    foreach (var s in ss)
                    {
                        if (s.Contains("JSESSIONID"))
                        {
                            this.jsessionid = s;
                            return true;
                        }
                    }
                }
            }
            return true;
        }
    }
}
