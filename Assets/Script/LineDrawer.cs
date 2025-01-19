using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject LinePrefabs;
    public LayerMask CantDrawOverLayer;
    public carController car; // Reference to the carController

    int CantDrawOverLayerIndex;
    public float LinePointsMinDistance;
    public float LineWidth;
    public Gradient LineColor;
    public AudioSource drawAudio;
    Line currentLine;

    private Vector2 initialMousePos;
    private bool isDragging = false;
    private float dragThreshold = 0.1f; // Sensitivity for detecting drag
    private bool hasDrawn = false; // Track if any drawing has happened

    private void Start()
    {
        CantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            hasDrawn = false; // Reset hasDrawn at the start of each click
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(initialMousePos, currentMousePos) > dragThreshold)
            {
                if (!isDragging)
                {
                    isDragging = true;
                    BeginDraw();
                }
                Draw();
                hasDrawn = true; // Mark that drawing has occurred
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();

            if (car != null && hasDrawn) // Only start the car if drawing has occurred
            {
                car.StartCarMovement();
                Timer.instance.StartTimer();


            }
        }
    }

    private void BeginDraw()
    {
        if (currentLine != null)
            return;

        currentLine = Instantiate(LinePrefabs, this.transform).GetComponent<Line>();

        currentLine.SetLineColor(LineColor);
        currentLine.SetLineWidth(LineWidth);
        currentLine.UsePhysics(false);
        drawAudio.Play();
        GameManager.instance.isGameStarted = true;
    }

    private void Draw()
    {
        if (currentLine == null) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(mousePos, LineWidth / 3f, Vector2.zero, 1f, CantDrawOverLayer);

        if (hit)
        {
            EndDraw();
        }
        else
        {
            currentLine.AddPoint(mousePos);
        }
    }

    private void EndDraw()
    {
        if (drawAudio.isPlaying)
            drawAudio.Stop();

        if (currentLine != null)
        {
            if (currentLine.pointsCount < 1)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.gameObject.layer = CantDrawOverLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
}
