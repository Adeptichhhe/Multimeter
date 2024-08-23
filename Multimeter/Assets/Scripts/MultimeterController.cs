using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    public MultimeterModel Model;
    public MultimeterView View;
    
    public float R;//сопротивление Ом
    public float P;//мощность Ватт

    private void Start()
    {
        Model = new MultimeterModel();
        // Подписка на событие изменения измерений
        Model.MeasurementChanged += UpdateView;
        // Установка начальных значений
        Model.SetP(P);
        Model.SetR(R);
    }

    private void UpdateView()
    {
        // Обновляем представление при изменении данных
        View.UpdateDisplay(Model.CurrentMode, Model.Input_U, Model.Input_I, Model.Input_R, Model.Output);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Проверяем, попадает ли луч в объект
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            if (View.ModeTransform == hit.transform.parent)
            {
                // Меняем цвет на цвет при наведении мыши
                View.UpdateColor(1);
                float scrollInput = Input.GetAxis("Mouse ScrollWheel");

                // Если колесико мыши прокручено
                if (scrollInput != 0f)
                {
                    // Определяем направление прокрутки
                    int direction = scrollInput > 0 ? 1 : -1;
                    View.ChangePosition(direction);
                    Model.ChangeMode(View.currentPositionIndex);
                }
            }
            else 
            {
                View.UpdateColor(0);
            }
        }
    }
}

