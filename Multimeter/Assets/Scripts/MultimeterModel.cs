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
    public float Input_U { get; private set; } //����������
    public float Input_I { get; private set; } //���� ����
    public float Input_P { get; private set; } //��������
    public float Input_R { get; private set; } //�������������
    public float Output { get; private set; } //����� �� �������

    // ����� ��� ��������� �������� ����������
    public void SetU(float voltage)
    {
        Input_U = voltage;
        OnMeasurementChanged();
    }

    // ����� ��� ��������� �������� ���� ����
    public void SetI(float current)
    {
        Input_I = current;
        OnMeasurementChanged();
    }

    // ����� ��� ��������� �������� �������������
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

    // ����� ��� ������������ ������ ���������
    public void ChangeMode(int mode)
    {
        CurrentMode = (MeasurementMode)mode;
        OnMeasurementChanged();
    }

    public void CalculateOutput() 
    {
        //���������� ����������� �����������
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
    // ������� ��� ����������� ����������� � ��������� ������
    public event System.Action MeasurementChanged;

    // ����� ��� ������ ������� ��� ��������� ���������
    private void OnMeasurementChanged()
    {
        CalculateOutput();
        MeasurementChanged?.Invoke();
    }
}
