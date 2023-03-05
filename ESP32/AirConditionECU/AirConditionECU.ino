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
  // try to parse packet
  int packetSize = CAN.parsePacket();

  if (packetSize) {
    // received a packet
    Serial.print("Received ");

    if (CAN.packetRtr()) {
      // Remote transmission request, packet contains no data
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

      //控制空调开关指令
      if (CAN.packetId() == 0x100){

      }
      //控制空调状态指令
      else if (CAN.packetId() == 0x101){

      }
      //控制空调数值指令
      else if (CAN.packetId() == 0x102){

      }
      value = "";
      while (CAN.available()) {
        value = value + (char)CAN.read();
      }
      Serial.println(value);
    }

    Serial.println();
  }
}

void sendPacket(){
  // send packet: id is 11 bits, packet can contain up to 8 bytes of data
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
