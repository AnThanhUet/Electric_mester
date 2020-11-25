//-------1. Ma thiet bi-------//
byte MTB_Gateway[5]   = {0x45, 0x4D, 0x47, 0x4C, 0x00};     // Ma thiet bi Gateway Electric Meter
byte MTB_Node[5]      = {0x45, 0x4D, 0x4E, 0x33, 0x4C};     // Ma thiet bi Node Electric Meter

//-------2. Loai ban tin-------//
byte LBT_request      = 0xA1;                               // Ban tin yeu cau lay du lieu
byte LBT_response     = 0xA2;                               // Ban tin tra ve du lieu

//-------3. Kieu du lieu-------//
byte KDL_Bool          = 0x01;                               // kieu boolean
byte KDL_Byte          = 0x02;                               // kieu byte
byte KDL_Char          = 0x03;                               // kieu Char
byte KDL_U_Char        = 0x04;                               // kieu unsigned char
byte KDL_Word          = 0x05;                               // kieu word
byte KDL_U_Int         = 0x06;                               // kieu unsigned int
byte KDL_Int           = 0x07;                               // kieu int
byte KDL_U_Long        = 0x08;                               // kieu unsigned long
byte KDL_Long          = 0x09;                               // kieu long
byte KDL_Float         = 0x0A;                               // kieu float

//-------4. Ten truong du lieu-------//
byte TT_Vol[2]        = {0xE1, 0x00};                       // dien ap dong dien - V
byte TT_Amp[2]        = {0xE2, 0x00};                       // cuong do dong dien - A
byte TT_Freq[2]       = {0xE3, 0x00};                       // tan so dong dien - Hz
byte TT_Eng[2]        = {0xE4, 0x00};                       // nang luong tieu thu - KWh
byte TT_Pow[2]        = {0xE5, 0x00};                       // Cong suat tuc thoi - W
byte TT_Pf[2]         = {0xE6, 0x00};                       // He so song suat (Cos Phi) - rad
