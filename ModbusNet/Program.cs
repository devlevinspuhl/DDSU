// use default COM port settings
using FluentModbus;
using ModBusUtil.extension;
using System.IO.Ports;


// use custom COM port settings:
var client = new ModbusRtuClient()
{
    BaudRate = 9600,
    Parity = Parity.None,
    StopBits = StopBits.Two
    
};

client.Connect("COM10");

// interpret data as float
var floatData = client.ReadInputRegisters<ushort>(12, 8192, 2);
var firstValue = floatData[0];
var secondValue = floatData[1];
var value = MbUtil.UshortToFloat(firstValue,secondValue);


 floatData = client.ReadHoldingRegisters<ushort>(12, 8194, 2);