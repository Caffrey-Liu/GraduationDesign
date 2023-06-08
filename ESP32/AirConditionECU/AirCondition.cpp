#include <CAN.h>
#include "AirCondition.h"
AirCondition::AirCondition(){
  Init();
}

void AirCondition::Init(){
  TAM = 191;
  TR = 140;
  TE[1] = 1;
  TE[0] = 164;
  RH[1] = char(1);
  RH[0] = char(69 + rand() % 20);
  TS = 63 + rand() % 20;

  BLMLVL = 0;
  AIRMIXDR = 68;
  AIRMIXPA = 68;
  INLET = 28;
  OUTLET = 28;
  INLETDIR = 0;
  OUTLETDIR = 0;
  RRDEFSTATUS = 0;

  ENGID = 170;
  ENGSPF[0] = 0;
  ENGSPF[1] = 0;
  TW = 32;
  
  TAMRaw[0] = 0;
  TAMRaw[1] = 0;
  TRRaw[0] = 0;
  TRRaw[1] = 0;
  TERaw[0] =0;
  TERaw[1] =0;
  RHRaw[0] = 0;
  RHRaw[1] = 0;
  TSin = 0;

  RCOMP[0] = 0;
  RCOMP[1] = 0;
  COMPStatus = 0;
}

void AirCondition::changeForPANELMSG01(PANELMSG01 panelmsg01){
  BLMLVL = panelmsg01.BLWSET;
  INLETDIR = panelmsg01.ILETSET;
  Serial.println((int)INLETDIR);
  OUTLETDIR = panelmsg01.OLETSET;
  RRDEFSTATUS = panelmsg01.RRDEF;
  //FRDEF
  COMPStatus = panelmsg01.ACON + 1;
  //ACMODE
  //TSETDR
  //TSETPA
}

void AirCondition::sendACMSG01(){

  TAM = 191 + rand() % 10;
  TR = 140 + rand() % 10;
  TE[1] = 1;
  TE[0] = 164 + rand() % 10;
  RH[1] = char(1);
  RH[0] = char(69 + rand() % 10);
  TS = 63 + rand() % 20;

  CAN.beginPacket(0x101);
  CAN.write(TAM);
  CAN.write(TR);
  CAN.write(TE[0]);
  CAN.write(TE[1]);
  CAN.write(RH[0]);
  CAN.write(RH[1]);
  CAN.write(TS);
  CAN.endPacket();
}
void AirCondition::sendACMSG02(){
  char temp;  
  CAN.beginPacket(0x102);
  temp = 0b00001111 & BLMLVL;
  CAN.write(temp);
  CAN.write(AIRMIXDR);
  CAN.write(AIRMIXPA);

  if (INLETDIR == 0) INLET = 28;
  else INLET = 228;
  if (OUTLETDIR == 0) OUTLET = 28;
  else if (OUTLETDIR == 1) OUTLET = 95;
  else if (OUTLETDIR == 2) OUTLET = 135;
  else if (OUTLETDIR == 3) OUTLET = 175;
  else OUTLET = 228;

  CAN.write(INLET);
  CAN.write(OUTLET);
  temp = 0b01110000 & (OUTLETDIR <<4);
  temp = temp + INLETDIR;
  CAN.write(temp);
  temp = 0b00000001 & RRDEFSTATUS;
  CAN.write(temp);
  CAN.endPacket();
}

void AirCondition::sendENGMSG01(){
  CAN.beginPacket(0x060);
  CAN.write(ENGID);
  if (COMPStatus == 0 || COMPStatus == 1){
      ENGSPF[0] = 0;
      ENGSPF[1] = 0;
  }
  if (COMPStatus == 2){
      ENGSPF[0] = 117 + rand() % 10;
      ENGSPF[1] = 48 + rand() % 200;
  }
  CAN.write(ENGSPF[0]);
  CAN.write(ENGSPF[1]);
  TW = 180 + rand() % 20;
  CAN.write(TW);
  CAN.endPacket();
}

void AirCondition::sendFRNMSG01(){
  char temp;
  CAN.beginPacket(0x300);
  TAMRaw[0] = 32 + rand() % 4;
  CAN.write(TAMRaw[0]);

  TAMRaw[1] = 0 + rand() % 256;
  TRRaw[0] = 8 + rand () % 2;
  temp = (0b11000000 & TAMRaw[1]) + (0b00111111 & TRRaw[0]);
  CAN.write(temp);

  TRRaw[1] = 0 + rand() % 256;
  TERaw[0] = 2;
  temp = (0b11110000 & TRRaw[1]) + (0b00001111 & TERaw[0]);
  CAN.write(temp);

  TERaw[1] =0 + rand() % 50;
  RHRaw[0] = 0;
  temp = (0b11111100 & TERaw[1]) + (0b00000011 & RHRaw[0]);
  CAN.write(temp);

  RHRaw[1] = 128 + rand() % 50;
  CAN.write(RHRaw[1]);

  temp = 0;
  CAN.write(temp);

  TSin = 128 + rand() %128;
  CAN.write(TSin);
  CAN.endPacket();
}

void AirCondition::sendACCMPMSG01(){
  CAN.beginPacket(0x333);
  if (COMPStatus == 2){
      RCOMP[0] = 39 + rand() % 3;
      RCOMP[1] = 16 + rand() % 50;
  }
  if (COMPStatus == 0 || COMPStatus == 1){
      RCOMP[0] = 0;
      RCOMP[1] = 0;
  }
  char temp;
  CAN.write(RCOMP[0]);
  CAN.write(RCOMP[1]);
  temp = 0b00000011 & COMPStatus;
  CAN.write(temp);
  CAN.endPacket();
}

void PANELMSG01::getValue(String value){
  ILETSET = value[0] & 0b00000001;
  OLETSET = (value[0] >> 4) & 0b00000111;
  TSETDR = value[1];
  TSETPA = value[2];
  BLWSET = value[3] & 0b00001111;
  RRDEF = (value[3] >> 4) & 0b00000001;
  FRDEF = (value[3] >> 5) & 0b00000001;
  ACON = (value[3] >> 6) & 0b00000001;
  ACMODE = (value[3] >> 7) & 0b00000001;
}