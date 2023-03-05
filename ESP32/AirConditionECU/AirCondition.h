#ifndef AIR_CONDITION_H
#define AIR_CONDITION_H 
#include <Arduino.h>

class AirCondition{
    public:
       //开关 0为关闭 1为开启 默认为0
       char power;

       //加热模式 0为制冷 1为制热 默认为0
       char heatType;
       //内外循环模式 0为外循环 1为内循环 默认为0
       char circulateType;
       //后视镜加热 0为关闭 1为开启 默认为0
       char backGlassHeat;
       //前挡风玻璃加热 0为关闭 1为开启 默认为0
       char frontGlassHeat;
       //空调风向 0为吹挡风玻璃 1为吹脸 2为吹脚 默认为1
       char windDirection;

       //左出风口温度 默认为21度 调节范围16-30度 单位为0.5 
       char leftTemperature;
       //右出风口温度 默认为21度 调节范围16-30度 单位为0.5 
       char rightTemperature;
       //风速 默认为50 调节范围10-100 单位为10
       char windSpeed;  
       
       AirCondition();
};

#endif
