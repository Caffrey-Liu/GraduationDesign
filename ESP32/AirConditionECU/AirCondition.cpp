#include <CAN.h>
#include "AirCondition.h"
AirCondition::AirCondition(){
  Init();
}

void AirCondition::Init(){
  TAM = 191;
  TR = 140;
  TE[0] = 1;
  TE[1] = 164 + rand() % 50;
  RH[0] = char(1);
  RH[1] = char(69 + rand() % 20);
  TS = 63 + rand() % 20;

  BLMLVL = 0;
  AIRMIXDR = 68;
  AIRMIXPA = 68;
  INLET = 28;
  OUTLET = 28;
  INLETDIR = 0;
  OUTLETDIR = 0;
  RRDEFSTATUS = 0;
}

void AirCondition::changeForPANELMSG01(PANELMSG01 panelmsg01){
  BLMLVL = panelmsg01.BLWSET;
  INLETDIR = panelmsg01.ILETSET;
  Serial.println((int)INLETDIR);
  OUTLETDIR = panelmsg01.OLETSET;
  RRDEFSTATUS = panelmsg01.RRDEF;
  //FRDEF
  //ACON
  //ACMODE
  //TSETDR
  //TSETPA
}

void AirCondition::sendACMSG01(){

  TAM = 191 + rand() % 10;
  TR = 140 + rand() % 10;
  TE[0] = 1;
  TE[1] = 164 + rand() % 30;
  RH[0] = char(1);
  RH[1] = char(69 + rand() % 10);
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
  CAN.write(INLET);
  CAN.write(OUTLET);
  temp = 0b01110000 & (OUTLETDIR <<4);
  temp = temp + INLETDIR;
  CAN.write(temp);
  temp = 0b00000001 & RRDEFSTATUS;
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