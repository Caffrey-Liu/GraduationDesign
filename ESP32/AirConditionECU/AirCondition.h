#ifndef AIR_CONDITION_H
#define AIR_CONDITION_H 
#include <Arduino.h>
class PANELMSG01{
  public:
    //REC：0, FRE:1，当空调处于自动模式时，如果外气温度高于6℃时自动变为REC，低于5℃时自动变为FRE。手动模式时由用户操作决定。
    char ILETSET;
    //Face: 000, B/L: 001, Foot: 010, F/D: 011, DEF: 100
    char OLETSET;
    //LO:17.5℃/18.0℃…32℃/HI32.5℃,0.5℃间隔
    char TSETDR;
    //LO:17.5℃/18.0℃…32℃/HI32.5℃,0.5℃间隔
    char TSETPA;
    //8个风速挡位 OFF-L0-M1-M2-M3-M4-M5-Hi
    char BLWSET;
    //OFF:0, ON 1, 后视镜及后窗加热除霜，ON后15min自动OFF
    char RRDEF;
    //OFF: 0，ON:1 前挡风玻璃除雾
    char FRDEF;
    //OFF: 0，ON:1 空调压缩机ON/OFF
    char ACON;
    //0：MANUAL,  1 AUTO 空调自动/手动模式。
    //MANUAL模式：用户设定了：吸入口/吹出口/风机风速/
    //ACON AUTO：用户按了自动按钮，ACECU将根据环境参数、设定温度，自动设定吹出口/吸入口/风机风速
    char ACMODE;

    void getValue(String value);    
};

class AirCondition{
    public:
      //ACMSG01 0x101
      //车室外温度传感器	Tam	250ms	8次平均值	-30.8℃~50.8℃	0.32℃	
      char TAM;
      //车室内温度传感器	Tr	250ms	8次平均值	-6.5℃ ~57.25℃	0.25℃	(-6.5℃超过8分钟报传感器开路故障,57.25℃超过8分钟报传感器短路故障)
      char TR;
      //蒸发器后温度	Te	250ms	4次平均值	-30.0℃ ~ 60.0℃	0.1℃	(-29.7℃超过8分钟报传感器开路故障,59.6℃超过8分钟报传感器短路故障)
      char TE[2];
      //车室内湿度传感器	Rh	250ms	16次平均值	10%~90%	0.16%	
      char RH[2];
      //日照强度传感器	Ts	1000ms	8次平均值	0 ~4.448kW/m2	17.5W/ m2	0 ~4.448kW/m，初始值：0x0，单位：17.5W/ m2
      char TS;

      //ACMSG02 0x102
      //Blower	0 ~ 7	1	8个风速挡位 OFF-L0-M1-M2-M3-M4-M5-Hi
      char BLMLVL;
      //AirmixDr	-14% ~113.5%	0.5%	 
      char AIRMIXDR;
      //AirmixPa	-14% ~113.5%	0.5%	 
      char AIRMIXPA;
      //Inlet	-14% ~113.5%	0.5%	0%: REC(内循环)，100%: FRS(外循环)
      char INLET;
      //Outlet	-14% ~113.5%	0.5%	Face: 0.0 %, B/L: 33.5 %, Foot: 53.5 %,, F/D: 73.5 %, DEF: 100.0 %
      char OUTLET;
      //INLETDIR REC: 0， FRS:1
      char INLETDIR;
      //OUTLETDIR Face: 000, B/L: 001, Foot: 010, F/D: 011, DEF: 100
      char OUTLETDIR;
      //RRDEFSTATUS OFF:0 ON:1, 后车窗及后视镜除雾状态
      char RRDEFSTATUS;
       
      AirCondition();
      void Init();
      void sendACMSG01();
      void sendACMSG02();
      void changeForPANELMSG01(PANELMSG01 panelmsg01);
};
#endif
