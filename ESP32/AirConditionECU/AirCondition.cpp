#include "AirCondition.h"
AirCondition::AirCondition(){
  Init();
}

void AirCondition::Init(){
  power = 0;

  heatType = 0;
  circulateType = 0;
  backGlassHeat = 0;
  frontGlassHeat = 0;
  windDirection = 1;

  leftTemperature = 42;
  rightTemperature = 42;
  windSpeed = 50;
}

void AirCondition::AirConditionPower(){
  //关闭，所有值置为初始值
  if (power == 1){
    Init();
  }
  else power = 1;
}

void AirCondition::AirConditionType(String value){
  heatType = value[0];
  circulateType = value[1];
  backGlassHeat = value[2];
  frontGlassHeat = value[3];
  windDirection = value[4];
}

void AirCondition::AirConditionRegulate(String value){
  leftTemperature = value[0];
  rightTemperature = value[1];
  windSpeed = value[2];
}