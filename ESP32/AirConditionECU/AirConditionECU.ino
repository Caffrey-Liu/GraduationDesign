#include <CAN.h>
#include "AirCondition.h"

String value = "";
AirCondition airCondition;
PANELMSG01 panelmsg01;

void setup() {
  Serial.begin(9600);
  while (!Serial);

  Serial.println("CAN Sender");

  // start the CAN bus at 500 kbps
  if (!CAN.begin(500E3)) {
    Serial.println("Starting CAN failed!");
    while (1);
  }
}

void loop() {
  receivePacket();
  sendPacket();
}

void receivePacket(){

  int packetSize = CAN.parsePacket();

  if (packetSize) {

    Serial.print("Received ");

    if (CAN.packetRtr()) {
      Serial.print("RTR ");
    }

    Serial.print("packet with id 0x");
    Serial.print(CAN.packetId(), HEX);

    if (CAN.packetRtr()) {
      Serial.print(" and requested length ");
      Serial.println(CAN.packetDlc());
    } else {
      Serial.print(" and length ");
      Serial.println(packetSize);
      
      value = "";
      while (CAN.available()) {
        value = value + (char)CAN.read();
      }
      Serial.println(value);
      //面板指令
      if (CAN.packetId() == 0x222){
        panelmsg01.getValue(value);
        airCondition.changeForPANELMSG01(panelmsg01);
        Serial.println("According to panelmsg change AirCondition");
      }
    }
  }
}

void sendPacket(){
  //Serial.print("Sending packet ... ");

  airCondition.sendACMSG01();
  airCondition.sendACMSG02();

  //Serial.println("done");

  delay(250);
}
