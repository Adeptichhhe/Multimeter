using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    public MultimeterModel Model;
    public MultimeterView View;
    
    public float R;//������������� ��
    public float P;//�������� ����

    private void Start()
    {
        Model = new MultimeterModel();
        // �������� �� ������� ��������� ���������
        Model.MeasurementChanged += UpdateView;
        // ��������� ��������� ��������
        Model.SetP(P);
        Model.SetR(R);
    }

    private void UpdateView()
    {
        // ��������� ������������� ��� ��������� ������
        View.UpdateDisplay(Model.CurrentMode, Model.Input_U, Model.Input_I, Model.Input_R, Model.Output);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // ���������, �������� �� ��� � ������
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            if (View.ModeTransform == hit.transform.parent)
            {
                // ������ ���� �� ���� ��� ��������� ����
                View.UpdateColor(1);
                float scrollInput = Input.GetAxis("Mouse ScrollWheel");

                // ���� �������� ���� ����������
                if (scrollInput != 0f)
                {
                    // ���������� ����������� ���������
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

