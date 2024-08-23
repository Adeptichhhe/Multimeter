using System;
using UnityEngine;

public class MultimeterModel
{
    public enum MeasurementMode
    {
        Off,
        Resistance,
        Current,
        Voltage_Var,
        Voltage
    }

    public MeasurementMode CurrentMode { get; private set; } = MeasurementMode.Off;
    public float Input_U { get; private set; } //Напряжение
    public float Input_I { get; private set; } //Сила тока
    public float Input_P { get; private set; } //Мощность
    public float Input_R { get; private set; } //Сопротивление
    public float Output { get; private set; } //Вывод на дисплей

    // Метод для установки значения напряжения
    public void SetU(float voltage)
    {
        Input_U = voltage;
        OnMeasurementChanged();
    }

    // Метод для установки значения силы тока
    public void SetI(float current)
    {
        Input_I = current;
        OnMeasurementChanged();
    }

    // Метод для установки значения сопротивления
    public void SetR(float resistance)
    {
        Input_R = resistance;
        OnMeasurementChanged();
    }
    public void SetP(float current) 
    {
        Input_P = current;
        OnMeasurementChanged();
    }

    // Метод для переключения режима измерений
    public void ChangeMode(int mode)
    {
        CurrentMode = (MeasurementMode)mode;
        OnMeasurementChanged();
    }

    public void CalculateOutput() 
    {
        //вычисление недостающих показателей
        Input_I = CalculateCurrentByPowerAndResistance(Input_P, Input_R);
        Input_U = CalculateVoltageByResistanceAndPower(Input_R, Input_P);
        switch (CurrentMode)
        {
            case MeasurementMode.Off:
                Output = 0f;
                break;
            case MeasurementMode.Resistance:
                Output = Input_R;
                break;
            case MeasurementMode.Current:
                Output = Input_I;
                break;
            case MeasurementMode.Voltage:
                Output = Input_U;
                break;
            case MeasurementMode.Voltage_Var:
                Output = 0.01f;
                break;
            default:
                Output = 0f;
                break;
        }
    }
    static float CalculateCurrentByPowerAndResistance(float power, float resistance)
    {
        if (resistance == 0)
        {
            return 0f;
        }
        return Mathf.Sqrt(power / resistance);
    }

    static float CalculateVoltageByResistanceAndPower(float resistance, float power)
    {
        if (resistance <= 0)
        {
            return 0;
        }
        if (power < 0)
        {
            return 0;
        }
        return Mathf.Sqrt(power * resistance);
    }
    // Событие для уведомления контроллера о изменении данных
    public event System.Action MeasurementChanged;

    // Метод для вызова события при изменении измерений
    private void OnMeasurementChanged()
    {
        CalculateOutput();
        MeasurementChanged?.Invoke();
    }
}
