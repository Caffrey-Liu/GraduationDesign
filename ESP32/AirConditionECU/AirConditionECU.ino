#include <CAN.h>
#include "AirCondition.h"

String value = "";
AirCondition airCondition;

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
      //控制空调开关指令
      if (CAN.packetId() == 0x100){
        airCondition.AirConditionPower();
        Serial.println("change AirConditionPower");
      }
      //控制空调状态指令
      else if (CAN.packetId() == 0x101){
        airCondition.AirConditionType(value);
        Serial.println("change AirConditionType");
      }
      //控制空调数值指令
      else if (CAN.packetId() == 0x102){
        airCondition.AirConditionRegulate(value);
        Serial.println("change AirConditionRegulate");
      }
    }
  }
}

void sendPacket(){
  Serial.print("Sending packet ... ");

  CAN.beginPacket(0x200);
  CAN.write(airCondition.power);
  CAN.endPacket();

  CAN.beginPacket(0x201);
  CAN.write(airCondition.heatType);
  CAN.write(airCondition.circulateType);
  CAN.write(airCondition.backGlassHeat);
  CAN.write(airCondition.frontGlassHeat);
  CAN.write(airCondition.windDirection);
  CAN.endPacket();

  CAN.beginPacket(0x202);
  CAN.write(airCondition.leftTemperature);
  CAN.write(airCondition.rightTemperature);
  CAN.write(airCondition.windSpeed);
  CAN.endPacket();

  Serial.println("done");

  delay(1000);
}
