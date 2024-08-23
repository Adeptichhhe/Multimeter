using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class MultimeterView : MonoBehaviour
{
    public TextMeshPro OutputDisplay;
    public TextMeshProUGUI VoltageText;
    public TextMeshProUGUI VoltageVarText;
    public TextMeshProUGUI CurrentText;
    public TextMeshProUGUI ResistanceText;

    public Transform ModeTransform;
    public Color[] colors; // Цвета контроллера режимами
    private Renderer ModeRenderer;
    private float[] positions = { 0f, 45f, 135f, 225, 315f }; // Углы для каждой позиции
    public int currentPositionIndex = 0; // Начальная позиция

    private void Start()
    {
        ModeRenderer = ModeTransform.Find("Cylinder").GetComponent<Renderer>();
        UpdateColor(0);
    }
    // Метод для обновления отображения
    private void CleanTexts()
    {
        VoltageText.text = $"V";
        VoltageVarText.text = $"~";
        CurrentText.text = $"A";
        ResistanceText.text = $"Ω";
    }
    static string FormatToTwoDecimalPlaces(float value)
    {
        return value.ToString("F2");
    }
    public void UpdateDisplay(MultimeterModel.MeasurementMode mode, float voltage, float current, float resistance, float output)
    {
        CleanTexts();
        switch (mode)
        {
            case MultimeterModel.MeasurementMode.Resistance:
                ResistanceText.text = $"Ω {resistance}";
                break;
            case MultimeterModel.MeasurementMode.Current:
                CurrentText.text = $"A {current}";
                break;
            case MultimeterModel.MeasurementMode.Voltage:
                VoltageText.text = $"V {voltage}";
                break;
            case MultimeterModel.MeasurementMode.Voltage_Var:
                VoltageVarText.text = $"~ 0,01";
                break;
        }
        OutputDisplay.text = FormatToTwoDecimalPlaces(output);
    }
    public void UpdateColor(int index)
    {
        ModeRenderer.material.color = colors[index];
    }
    public void ChangePosition(int direction)
    {
        // Обновляем индекс текущей позиции
        currentPositionIndex = (currentPositionIndex + direction + positions.Length) % positions.Length;

        // Поворачиваем ModeTransform в новую позицию
        float targetAngle = positions[currentPositionIndex];
        ModeTransform.localRotation = Quaternion.Euler(0, 0, targetAngle);
    }
}

